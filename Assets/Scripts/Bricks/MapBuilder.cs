﻿using BrickHill;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class MapBuilder : MonoBehaviour
{
    public MapManager mapManager;

    public int BricksPerMesh = 100; // how many bricks per chunk/mesh - lower = faster generation per mesh, but more gameobjects; higher = slower generation per mesh, but less gameobjects; basically generation speed vs post generation performance

    // shapes(TM)
    public Mesh[] SlopeMesh;
    public Mesh[] WedgeMesh;
    public Mesh[] ArchMesh;
    public Mesh DomeMesh;
    public Mesh[] CylinderMesh;
    public Mesh RoundSlopeMesh;
    public Mesh[] BarsMesh;
    public Mesh FlagMesh;
    public Mesh[] PoleMesh;
    public Mesh VentMesh;

    public Material SmoothMat;
    public Material StudMat;
    public Material InletMat;
    public Material SpawnpointMat;

    // these are automatically generated, only the opaque ones need to be set
    public Material SmoothMatAlpha;
    public Material StudMatAlpha;
    public Material InletMatAlpha;
    public Material SpawnpointMatAlpha;

    public float StudTile = 1f; // these are modified by resourcepacks, they adjust how often the textures repeat
    public float InletTile = 1f;

    public Shader AlphaShader; // for generating the alpha materials

    public float NoCollisionThreshold = 0.05f;

    // mesh stuff
    private List<Vector3> verts = new List<Vector3>();
    private List<Color> vertexColors = new List<Color>();
    private int vertexCount = 0; // i could probably just do verts.Count... but this might be more performant? dunno

    private List<int> smoothTris = new List<int>(); // the reason there are separate tri and uv lists for the different material types is because every unique material requires a separate submesh
    private List<int> studTris = new List<int>();
    private List<int> inletTris = new List<int>();
    private List<int> spawnpointTris = new List<int>();

    private List<Vector2> smoothUVs = new List<Vector2>();
    private List<Vector2> studUVs = new List<Vector2>();
    private List<Vector2> inletUVs = new List<Vector2>();
    private List<Vector2> spawnpointUVs = new List<Vector2>();

    private List<Chunk> Chunks = new List<Chunk>();

    private void Awake () {
        // create alpha materials
        SmoothMatAlpha = new Material(SmoothMat);
        SmoothMatAlpha.shader = AlphaShader;

        StudMatAlpha = new Material(StudMat);
        StudMatAlpha.shader = AlphaShader;

        InletMatAlpha = new Material(InletMat);
        InletMatAlpha.shader = AlphaShader;

        SpawnpointMatAlpha = new Material(SpawnpointMat);
        SpawnpointMatAlpha.shader = AlphaShader;
    }

    public void RebuildEntireMap () {
        if (!mapManager.mapIsLoaded) return;

        // clear all existing chunks
        for (int i = 0; i < Chunks.Count; i++) {
            Chunks[i].Destroy();
        }
        Chunks.Clear();

        AddBricks(mapManager.LoadedMap.Bricks);
    }

    public void AddBricks (List<Map.Brick> bricks) {
        List<Map.Brick> opaque = new List<Map.Brick>();
        List<Map.Brick> alpha = new List<Map.Brick>();
        List<Map.Brick> collision = new List<Map.Brick>();

        // sort bricks
        for (int i = 0; i < bricks.Count; i++) {
            if (bricks[i].BrickColor.a == 1) {
                opaque.Add(bricks[i]);
            } else {
                alpha.Add(bricks[i]);
            }

            if (bricks[i].Collision & bricks[i].Scale.x > NoCollisionThreshold && bricks[i].Scale.y > NoCollisionThreshold && bricks[i].Scale.z > NoCollisionThreshold) {
                collision.Add(bricks[i]);
            }
        }

        while (opaque.Count > 0) {
            Chunk c = GetUnfinishedChunk(0);
            int added = c.AddBricks(opaque, BricksPerMesh);
            MeshChunk(c);
            if (added != opaque.Count) {
                opaque = opaque.GetRange(added, opaque.Count - added);
            } else {
                break;
            }
        }

        while (alpha.Count > 0) {
            Chunk c = GetUnfinishedChunk(1);
            int added = c.AddBricks(alpha, BricksPerMesh);
            MeshChunk(c);
            if (added != alpha.Count) {
                alpha = alpha.GetRange(added, alpha.Count - added);
            } else {
                break;
            }
        }

        while (collision.Count > 0) {
            Chunk c = GetUnfinishedChunk(2);
            int added = c.AddBricks(collision, BricksPerMesh);
            MeshChunk(c);
            if (added != collision.Count) {
                collision = collision.GetRange(added, collision.Count - added);
            } else {
                break;
            }
        }
    }

    public Chunk GetChunk (int chunkID) {
        if (chunkID < 0 || chunkID >= Chunks.Count) return null;
        return Chunks[chunkID];
    }

    public void UpdateChunk (int chunkID) {
        if (chunkID < 0 || chunkID >= Chunks.Count) return;
        MeshChunk(Chunks[chunkID]);
    }

    private void MeshChunk (Chunk chunk) {
        ClearMeshData();
        for (int i = 0; i < chunk.BrickCount; i++) {
            MeshBrick(mapManager.LoadedMap.GetBrick(chunk.Bricks[i]));
        }
        ApplyMeshData(chunk.ID);
        SetMeshGO(chunk.ID, chunk.ChunkType);
        ClearMeshData();
    }

    private void ChangeCurrentChunk (Chunk chunk) {
        if (chunk.Mesh == null) {
            chunk.Mesh = new Mesh();
            chunk.Mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
            chunk.Mesh.subMeshCount = 4;
        }
        
        ClearMeshData();

        chunk.Mesh.GetVertices(verts);
        chunk.Mesh.GetColors(vertexColors);

        smoothTris = new List<int>(chunk.Mesh.GetTriangles(0));
        studTris = new List<int>(chunk.Mesh.GetTriangles(1));
        inletTris = new List<int>(chunk.Mesh.GetTriangles(2));
        spawnpointTris = new List<int>(chunk.Mesh.GetTriangles(3));

        chunk.Mesh.GetUVs(0, smoothUVs);
        chunk.Mesh.GetUVs(1, studUVs);
        chunk.Mesh.GetUVs(2, inletUVs);
        chunk.Mesh.GetUVs(3, spawnpointUVs);
    }


    // generates mesh data for desired brick
    public void MeshBrick (Map.Brick brick) {
        // convert transform from bh to unity
        Vector3 adjustedPosition = new Vector3(brick.Position.x * -1, brick.Position.z, brick.Position.y);
        Vector3 adjustedScale = new Vector3(brick.Scale.x, brick.Scale.z, brick.Scale.y);
        if (brick.ScuffedScale) {
            adjustedScale = new Vector3(adjustedScale.z, adjustedScale.y, adjustedScale.x);
            float adjustment = (adjustedScale.x - adjustedScale.z) / 2;
            adjustedPosition.x += adjustment;
            adjustedPosition.z += adjustment;
        }

        // generate geometry for this brick
        BuildShape(brick.Shape, adjustedPosition, adjustedScale, brick.RotationX, brick.RotationY, -brick.Rotation, brick.BrickColor);
    }

    public void ApplyMeshData (int index = -1) {
        if (Chunks.Count <= index) return; // cannot apply data if there are no meshes
        if (index == -1) index = Chunks.Count - 1; // if index is -1, the desired mesh index is the last mesh in the list
        
        Mesh m = Chunks[index].Mesh;
        if (m == null) {
            m = new Mesh();
            m.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
            m.subMeshCount = 4;
            Chunks[index].Mesh = m;
        }

        m.Clear();
        m.subMeshCount = 4;

        m.SetVertices(verts);
        // submesh tris
        m.SetTriangles(smoothTris, 0);
        m.SetTriangles(studTris, 1);
        m.SetTriangles(inletTris, 2);
        m.SetTriangles(spawnpointTris, 3);
        // submesh uvs
        m.SetUVs(0, smoothUVs);
        m.SetUVs(1, studUVs);
        m.SetUVs(2, inletUVs);
        m.SetUVs(3, spawnpointUVs);

        m.SetColors(vertexColors);

        m.RecalculateNormals();
    }

    public void ClearMeshData () {
        verts.Clear();
        vertexColors.Clear();
        vertexCount = 0;
        smoothTris.Clear();
        studTris.Clear();
        inletTris.Clear();
        spawnpointTris.Clear();
        smoothUVs.Clear();
        studUVs.Clear();
        inletUVs.Clear();
        spawnpointUVs.Clear();
    }

    public void SetMeshGO (int meshIndex, int chunkType) {
        if (Chunks.Count <= meshIndex) return; // cannot create mesh GO if there is no mesh

        GameObject meshGO;
        if (Chunks[meshIndex].GameObject != null) {
            // GO already exists, needs to be updated
            meshGO = Chunks[meshIndex].GameObject;
        } else {
            // GO does not yet exist, needs to be created
            meshGO = new GameObject();
            meshGO.name = chunkType == 0 ? "Opaque Chunk" : chunkType == 1 ? "Alpha Chunk" : "Collision Chunk";

            // add components
            if (chunkType < 2) {
                // Opaque/Alpha
                meshGO.AddComponent<MeshFilter>();
                meshGO.AddComponent<MeshRenderer>();
            } else {
                // Collision
                meshGO.AddComponent<MeshCollider>();
            }

            Chunks[meshIndex].GameObject = meshGO; // assign gameobject to chunk
        }

        if (chunkType < 2) {
            // opaque/alpha
            meshGO.GetComponent<MeshFilter>().sharedMesh = Chunks[meshIndex].Mesh;
            //Material[] mats = new Material[] { new Material(SmoothMat), new Material(StudMat), new Material(InletMat), new Material(SpawnpointMat) };
            MeshRenderer renderer = meshGO.GetComponent<MeshRenderer>();
            for (int i = 0; i < renderer.materials.Length; i++) Destroy(renderer.materials[i]);
            Material[] mats = new Material[] { new Material(SmoothMat), new Material(StudMat), new Material(InletMat), new Material(SpawnpointMat) };
            if (chunkType == 1) for (int i = 0; i < mats.Length; i++) mats[i].shader = AlphaShader; // transparent geometry uses a different shader
            renderer.materials = mats;
        } else {
            // collision
            meshGO.GetComponent<MeshCollider>().sharedMesh = Chunks[meshIndex].Mesh;
        }
    }

    public Chunk GetUnfinishedChunk (byte chunkType) {
        for (int i = 0; i < Chunks.Count; i++) {
            if (Chunks[i].BrickCount < BricksPerMesh && Chunks[i].ChunkType == chunkType) {
                return Chunks[i];
            }
        }
        Chunk returnChunk = new Chunk();
        returnChunk.ChunkType = chunkType;
        returnChunk.ID = Chunks.Count;
        Chunks.Add(returnChunk);
        return returnChunk;
    }

    private void BuildShape (Map.Brick.ShapeType shape, Vector3 position, Vector3 scale, int rotationX, int rotationY, int rotationZ, Color vertexColor) {
        switch (shape) {
            case Map.Brick.ShapeType.slope: CreateSlope(position, scale, rotationZ, vertexColor, rotationX, rotationY); break;
            case Map.Brick.ShapeType.wedge: CreateWedge(position, scale, rotationZ, vertexColor, rotationX, rotationY); break;
            case Map.Brick.ShapeType.spawnpoint: CreateCube(position, scale, rotationZ, vertexColor, true, rotationX, rotationY); break;
            case Map.Brick.ShapeType.arch: CreateArch(position, scale, rotationZ, vertexColor, rotationX, rotationY); break;
            case Map.Brick.ShapeType.dome: CreateDome(position, scale, rotationZ, vertexColor, rotationX, rotationY); break;
            case Map.Brick.ShapeType.cylinder: CreateCylinder(position, scale, rotationZ, vertexColor, rotationX, rotationY); break;
            case Map.Brick.ShapeType.round_slope: CreateRoundSlope(position, scale, rotationZ, vertexColor, rotationX, rotationY); break;
            case Map.Brick.ShapeType.flag: CreateFlag(position, scale, rotationZ, vertexColor, rotationX, rotationY); break;
            case Map.Brick.ShapeType.pole: CreatePole(position, scale, rotationZ, vertexColor, rotationX, rotationY); break;
            case Map.Brick.ShapeType.bars: CreateBars(position, scale, rotationZ, vertexColor, rotationX, rotationY); break;
            case Map.Brick.ShapeType.vent: CreateVent(position, scale, rotationZ, vertexColor, rotationX, rotationY); break;
            default: CreateCube(position, scale, rotationZ, vertexColor, false, rotationX, rotationY); break;
        }
    }

    // SHAPES

    private void CreateCube (Vector3 origin, Vector3 scale, int rotation, Color vertexColor, bool spawnpoint = false, int rotationX = 0, int rotationY = 0) {
        origin.x -= scale.x; // ye
        Vector3 rot = new Vector3(rotationX, rotation, rotationY);
        Vector3 center = origin + scale / 2f;

        verts.AddRange(new Vector3[] {
            // bottom face
            Helper.RotatePointAroundPivot(center, origin, rot), Helper.RotatePointAroundPivot(center, origin + new Vector3(scale.x, 0, 0), rot), Helper.RotatePointAroundPivot(center, origin + new Vector3(scale.x, 0, scale.z), rot), Helper.RotatePointAroundPivot(center, origin + new Vector3(0, 0, scale.z), rot),
            // top face
            Helper.RotatePointAroundPivot(center, origin + new Vector3(0, scale.y, 0), rot), Helper.RotatePointAroundPivot(center, origin + new Vector3(scale.x, scale.y, 0), rot), Helper.RotatePointAroundPivot(center, origin + new Vector3(scale.x, scale.y, scale.z), rot), Helper.RotatePointAroundPivot(center, origin + new Vector3(0, scale.y, scale.z), rot),
            // front face
            Helper.RotatePointAroundPivot(center, origin, rot), Helper.RotatePointAroundPivot(center, origin + new Vector3(scale.x, 0, 0), rot), Helper.RotatePointAroundPivot(center, origin + new Vector3(scale.x, scale.y, 0), rot), Helper.RotatePointAroundPivot(center, origin + new Vector3(0, scale.y, 0), rot),
            // bacl fac
            Helper.RotatePointAroundPivot(center, origin + new Vector3(0,0,scale.z), rot), Helper.RotatePointAroundPivot(center, origin + new Vector3(scale.x, 0, scale.z), rot), Helper.RotatePointAroundPivot(center, origin + new Vector3(scale.x, scale.y, scale.z), rot), Helper.RotatePointAroundPivot(center, origin + new Vector3(0, scale.y, scale.z), rot),
            // left fac
            Helper.RotatePointAroundPivot(center, origin + new Vector3(0,0,scale.z), rot), Helper.RotatePointAroundPivot(center, origin, rot), Helper.RotatePointAroundPivot(center, origin + new Vector3(0, scale.y, 0), rot), Helper.RotatePointAroundPivot(center, origin + new Vector3(0, scale.y, scale.z), rot),
            // right fa
            Helper.RotatePointAroundPivot(center, origin + new Vector3(scale.x,0,scale.z), rot), Helper.RotatePointAroundPivot(center, origin + new Vector3(scale.x, 0, 0), rot), Helper.RotatePointAroundPivot(center, origin + new Vector3(scale.x, scale.y, 0), rot), Helper.RotatePointAroundPivot(center, origin + new Vector3(scale.x, scale.y, scale.z), rot)
        });

        smoothTris.AddRange(new int[] {
            // front face i guess
            vertexCount + 10, vertexCount + 9, vertexCount + 8, vertexCount + 8, vertexCount + 11, vertexCount + 10,
            // back
            vertexCount + 12, vertexCount + 13, vertexCount + 14, vertexCount + 14, vertexCount + 15, vertexCount + 12,
            // left
            vertexCount + 18, vertexCount + 17, vertexCount + 16, vertexCount + 16, vertexCount + 19, vertexCount + 18,
            // right
            vertexCount + 20, vertexCount + 21, vertexCount + 22, vertexCount + 22, vertexCount + 23, vertexCount + 20
        });

        if (spawnpoint) {
            spawnpointTris.AddRange(new int[] {
                // top face
                vertexCount + 6, vertexCount + 5, vertexCount + 4, vertexCount + 4, vertexCount + 7, vertexCount + 6,
            });
        } else {
            studTris.AddRange(new int[] {
                // top face
                vertexCount + 6, vertexCount + 5, vertexCount + 4, vertexCount + 4, vertexCount + 7, vertexCount + 6,
            });
        }

        inletTris.AddRange(new int[] {
            // bottom face
            vertexCount + 0, vertexCount + 1, vertexCount + 2, vertexCount + 2, vertexCount + 3, vertexCount + 0,
        });

        Vector2[] uvs = new Vector2[] {
            new Vector2(0f, 0f), new Vector2(scale.x / InletTile, 0f), new Vector2(scale.x / InletTile, scale.z / InletTile), new Vector2(0f, scale.z / InletTile),
            new Vector2(0f, 0f), new Vector2(spawnpoint ? 1.0f : scale.x / StudTile, 0f), new Vector2(spawnpoint ? 1.0f : scale.x / StudTile, spawnpoint ? 1.0f : scale.z / StudTile), new Vector2(0f, spawnpoint ? 1.0f : scale.z / StudTile),
            new Vector2(0f, 0f), new Vector2(1f, 0f), new Vector2(1f, 1f), new Vector2(0f, 1f),
            new Vector2(0f, 0f), new Vector2(1f, 0f), new Vector2(1f, 1f), new Vector2(0f, 1f),
            new Vector2(0f, 0f), new Vector2(1f, 0f), new Vector2(1f, 1f), new Vector2(0f, 1f),
            new Vector2(0f, 0f), new Vector2(1f, 0f), new Vector2(1f, 1f), new Vector2(0f, 1f)
        };

        smoothUVs.AddRange(uvs);
        studUVs.AddRange(uvs);
        inletUVs.AddRange(uvs);
        spawnpointUVs.AddRange(uvs);

        vertexColors.AddRange(new Color[] {
            vertexColor, vertexColor, vertexColor, vertexColor,
            vertexColor, vertexColor, vertexColor, vertexColor,
            vertexColor, vertexColor, vertexColor, vertexColor,
            vertexColor, vertexColor, vertexColor, vertexColor,
            vertexColor, vertexColor, vertexColor, vertexColor,
            vertexColor, vertexColor, vertexColor, vertexColor

        });

        vertexCount += 24;
    }

    private void CreateSlope (Vector3 origin, Vector3 scale, int rotation, Color vertexColor, int rotationX = 0, int rotationY = 0) {
        AddMesh(SlopeMesh[0], origin, origin, new Vector3(scale.x, 0.3f, scale.z), scale, rotation, vertexColor, rotationX, rotationY); // base
        AddMesh(SlopeMesh[1], origin + new Vector3((scale.x-1) / 2, 0.3f, 0), origin, scale - new Vector3(1, 0.3f, 0), scale, rotation, vertexColor, rotationX, rotationY); // main
        AddMesh(SlopeMesh[2], origin - new Vector3(scale.x-1, 0, 0), origin, new Vector3(1.0f, scale.y, scale.z), scale, rotation, vertexColor, rotationX, rotationY); // back
    }

    private void CreateWedge (Vector3 origin, Vector3 scale, int rotation, Color vertexColor, int rotationX = 0, int rotationY = 0) {
        AddMesh(WedgeMesh[0], origin + new Vector3((scale.x-1)/2-1, scale.y/2, 0), origin, scale - new Vector3(1,0,0), scale, rotation, vertexColor, rotationX, rotationY); // main
        AddMesh(WedgeMesh[1], origin + new Vector3(0, scale.y/2, 0), origin, new Vector3(1.0f, scale.y, scale.z), scale, rotation, vertexColor, rotationX, rotationY); // side
    }

    private void CreateArch (Vector3 origin, Vector3 scale, int rotation, Color vertexColor, int rotationX = 0, int rotationY = 0) {
        AddMesh(ArchMesh[0], origin + new Vector3(0,0,1), origin, scale - new Vector3(0,0.3f,2), scale, rotation, vertexColor, rotationX, rotationY);
        AddMesh(ArchMesh[1], origin + new Vector3(0, scale.y, 0), origin, new Vector3(scale.x, 0.3f, scale.z), scale, rotation, vertexColor, rotationX, rotationY);
        AddMesh(ArchMesh[2], origin, origin, new Vector3(scale.x, scale.y, 1), scale, rotation, vertexColor, rotationX, rotationY);
        AddMesh(ArchMesh[3], origin + new Vector3(0,0,scale.z-1), origin, new Vector3(scale.x, scale.y, 1), scale, rotation, vertexColor, rotationX, rotationY);
    }

    private void CreateDome (Vector3 origin, Vector3 scale, int rotation, Color vertexColor, int rotationX = 0, int rotationY = 0) {
        AddMesh(DomeMesh, origin + new Vector3(0, scale.y/2, 0), origin, scale, scale, rotation, vertexColor, rotationX, rotationY);
    }

    private void CreateCylinder (Vector3 origin, Vector3 scale, int rotation, Color vertexColor, int rotationX = 0, int rotationY = 0) {
        AddMesh(CylinderMesh[0], origin + new Vector3(0, 0.3f, 0), origin, scale - new Vector3(0, 0.3f, 0), scale, rotation, vertexColor, rotationX, rotationY);
        AddMesh(CylinderMesh[1], origin - new Vector3(0.1f,0,-0.1f), origin, new Vector3(scale.x - 0.2f, 0.3f, scale.z - 0.2f), scale, rotation, vertexColor, rotationX, rotationY);
    }

    private void CreateRoundSlope (Vector3 origin, Vector3 scale, int rotation, Color vertexColor, int rotationX = 0, int rotationY = 0) {
        AddMesh(RoundSlopeMesh, origin + new Vector3(0, scale.y/2, 0), origin, scale, scale, rotation, vertexColor, rotationX, rotationY);
    }

    private void CreateFlag (Vector3 origin, Vector3 scale, int rotation, Color vertexColor, int rotationX = 0, int rotationY = 0) {
        AddMesh(FlagMesh, origin + new Vector3(0, scale.y/2, 0), origin, scale, scale, rotation, vertexColor, rotationX, rotationY);
    }

    private void CreatePole (Vector3 origin, Vector3 scale, int rotation, Color vertexColor, int rotationX = 0, int rotationY = 0) {
        AddMesh(PoleMesh[0], origin, origin, Vector3.one, scale, rotation, vertexColor, rotationX, rotationY);
        AddMesh(PoleMesh[1], origin + new Vector3(0, scale.y/2, 0), origin, new Vector3(1,scale.y-0.6f,1), scale, rotation, vertexColor, rotationX, rotationY);
        AddMesh(PoleMesh[2], origin + new Vector3(0, scale.y - 0.3f, 0), origin, Vector3.one, scale, rotation, vertexColor, rotationX, rotationY);
    }

    private void CreateBars (Vector3 origin, Vector3 scale, int rotation, Color vertexColor, int rotationX = 0, int rotationY = 0) {
        bool zAxis = scale.z > scale.x;
        int axisLen = zAxis ? (int)scale.z : (int)scale.x;
        for (int i = 0; i < axisLen; i++) {
            Vector3 offset = new Vector3(!zAxis ? i : 0, 0, zAxis ? i : 0);
            AddMesh(BarsMesh[0], origin + offset + new Vector3(0,scale.y / 2 + 0.15f,0), origin, new Vector3(1,scale.y-0.9f,1), scale, rotation, vertexColor, rotationX, rotationY);
            AddMesh(BarsMesh[1], origin + offset, origin, Vector3.one, scale, rotation, vertexColor, rotationX, rotationY);
            AddMesh(BarsMesh[2], origin + offset + new Vector3(0,scale.y,0), origin, Vector3.one, scale, rotation, vertexColor, rotationX, rotationY);
        }
    }

    private void CreateVent (Vector3 origin, Vector3 scale, int rotation, Color vertexColor, int rotationX = 0, int rotationY = 0) {
        for (int x = 0; x < (int)scale.x; x++) {
            AddMesh(VentMesh, origin + new Vector3(x - scale.x + 1, 0, 0), origin, new Vector3(1, scale.y, scale.z), scale, rotation, vertexColor, rotationX, rotationY);
        }
    }

    private void AddMesh (Mesh source, Vector3 origin, Vector3 corner, Vector3 meshScale, Vector3 brickScale, int rotation, Color vertexColor, int rotationX = 0, int rotationY = 0) {
        origin.x -= meshScale.x / 2; // ye
        origin.z += meshScale.z / 2;
        Vector3 rot = new Vector3(rotationX, rotation, rotationY);
        Vector3 center = corner + new Vector3(-brickScale.x / 2,brickScale.y / 2,brickScale.z / 2);

        // add vertices
        Vector3[] _verts = source.vertices;
        for (int i = 0; i < _verts.Length; i++) {
            _verts[i].Scale(meshScale); // scale mesh prior to offset
            _verts[i] += origin; // offset mesh according to specified position
            _verts[i] = Helper.RotatePointAroundPivot(center, _verts[i], rot); // rotate as needed
        }
        verts.AddRange(_verts);

        // add tris
        int[] tris = source.triangles;
        for (int i = 0; i < tris.Length; i++) tris[i] += vertexCount; // make sure tris correspond to the correct vertices
        smoothTris.AddRange(tris); // custom meshes are smooth for now

        // add uvs
        smoothUVs.AddRange(source.uv);
        studUVs.AddRange(source.uv);
        inletUVs.AddRange(source.uv);
        spawnpointUVs.AddRange(source.uv);

        // add vertex colors
        Color[] vColors = new Color[source.vertices.Length];
        for (int i = 0; i < vColors.Length; i++) vColors[i] = vertexColor;
        vertexColors.AddRange(vColors);

        // fin
        vertexCount += source.vertices.Length;
    }

}

public class Chunk {
    public int ID;
    public List<int> Bricks = new List<int>(); // IDs of the bricks that this chunk contains
    public byte ChunkType; // Opaque, Alpha, Collision
    public bool SingleBrick; // if this chunk only contains a single brick

    public Mesh Mesh; // Mesh of this chunk
    public GameObject GameObject; // GameObject that houses this chunk's mesh

    public int BrickCount { get { return Bricks.Count;} } // shortcut

    public int AddBricks (List<Map.Brick> bricksToAdd, int limit) {
        int amountToAdd = limit - Bricks.Count;
        if (bricksToAdd.Count > amountToAdd) {
            // wont be able to add everything
            for (int i = 0; i < amountToAdd; i++) {
                Bricks.Add(bricksToAdd[i].ID);

                if (ChunkType == 2) {
                    bricksToAdd[i].CollisionMeshID = ID;
                } else {
                    bricksToAdd[i].VisibleMeshID = ID;
                }
            }
            return amountToAdd;
        } else {
            // can add everything
            for (int i = 0; i < bricksToAdd.Count; i++) {
                Bricks.Add(bricksToAdd[i].ID);

                if (ChunkType == 2) {
                    bricksToAdd[i].CollisionMeshID = ID;
                } else {
                    bricksToAdd[i].VisibleMeshID = ID;
                }
            }
            return bricksToAdd.Count;
        }
    }

    public void RemoveBricks (List<Map.Brick> bricksToRemove) {
        for (int i = 0; i < bricksToRemove.Count; i++) {
            Bricks.Remove(bricksToRemove[i].ID);
        }
    }

    public void RemoveBrick (Map.Brick brickToRemove) {
        Bricks.Remove(brickToRemove.ID);
    }

    public void Destroy () {
        MonoBehaviour.Destroy(GameObject);
        MonoBehaviour.Destroy(Mesh);
        Bricks.Clear();
    }
}
