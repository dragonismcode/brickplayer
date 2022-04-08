// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Input/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""a0babf7b-3d1d-4f27-975a-48a3609c5d16"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""72d0733e-33e8-44cb-9b6e-6e6ddef34ae1"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""2bd122cf-eccf-468f-9acd-05d7f4f12b8a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""InvertVector2(invertX=false)"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""Value"",
                    ""id"": ""76fe25d8-3643-4014-a6d7-2f57e10600c4"",
                    ""expectedControlType"": ""Analog"",
                    ""processors"": ""Normalize(min=-1,max=1)"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Enable Mouse"",
                    ""type"": ""Button"",
                    ""id"": ""a19b60a5-3ec4-42b0-b9d7-e2a56931a799"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""cbd18836-5114-4755-a17b-c72f56293b30"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Click"",
                    ""type"": ""Button"",
                    ""id"": ""b25fac07-9359-40d1-889b-f52117311819"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Toggle Menu"",
                    ""type"": ""Button"",
                    ""id"": ""4fdaeb79-f0be-402c-b3d1-1e3628193378"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Toggle Chat"",
                    ""type"": ""Button"",
                    ""id"": ""3be3020a-9100-446a-83c6-7085ffa3df97"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Chatbox"",
                    ""type"": ""Button"",
                    ""id"": ""27010a41-d682-47a5-8d77-25ab1ad8841c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""85f23c3b-d159-4ed1-80f9-4d95bc05c735"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Game"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""55381769-de19-4c87-b682-975b645a573d"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""d72a5ea1-a758-4a99-abcd-a984e84c91d5"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Game"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""12f2eb7a-4671-499b-a26f-df04c757068a"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Game"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""567b6298-cbee-4714-8abb-935175b76f9e"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Game"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""8dfe10f8-4f15-41eb-a34f-ef6ed72f506f"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Game"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""3c104508-d4e6-431f-b747-6d1a3fde36d0"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""d360dd1b-25c8-400c-8628-2c4697313d1a"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Game"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""aa48976c-139d-4571-bf2b-b6fa110fc0b5"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Game"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""04c7802c-5783-455a-b0ca-a4766d275e11"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Game"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""d97eefe9-43eb-49a8-825a-f8f44fe8aa5d"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Game"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""75c9b8bc-c95e-4f03-abbf-d416fa2d2f7b"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Game"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a1c1661d-1586-4e4e-9e44-abe94719da9d"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Game"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c9c75749-da97-4a94-9621-136c75ca56d4"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Game"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9243fd4a-01d3-4c91-bf51-ee727ee60546"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Game"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5f6887a1-8e22-4465-aab5-c8fd819d4afa"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Game"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d28878a9-2634-4063-bef9-bf3e99e073a0"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Game"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cf97d54d-1533-4ad6-85b9-5cb0a902126e"",
                    ""path"": ""<Gamepad>/dpad/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fa8f0df7-e83a-4ef3-976e-d66c85b4748b"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5b93b5a0-2202-4490-ae29-495d6f0fa648"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Enable Mouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""920757fe-1644-49d8-9d8c-83fe5638885f"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Enable Mouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c84733cd-344e-44bd-b61f-a239f6e1e31d"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Enable Mouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e926130b-6955-45fa-9a6d-f9b23758b1ec"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Enable Mouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5a86980c-3e9b-4f0c-9529-e71a470bc0e5"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Enable Mouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""05665545-a91c-4973-a095-9c8ceff8a528"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Toggle Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f69f5612-9576-4b25-9d16-76108f815183"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Toggle Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fce79e9e-f2e4-455a-8654-af5282f4785e"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Toggle Chat"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""520d9f0e-cc03-4be8-833e-b335abef7360"",
                    ""path"": ""<Keyboard>/backquote"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Toggle Chat"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fa59ef2c-d616-46ca-a265-f76ab6c76ed1"",
                    ""path"": ""<Keyboard>/t"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Chatbox"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Game"",
            ""bindingGroup"": ""Game"",
            ""devices"": []
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Look = m_Player.FindAction("Look", throwIfNotFound: true);
        m_Player_Zoom = m_Player.FindAction("Zoom", throwIfNotFound: true);
        m_Player_EnableMouse = m_Player.FindAction("Enable Mouse", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Click = m_Player.FindAction("Click", throwIfNotFound: true);
        m_Player_ToggleMenu = m_Player.FindAction("Toggle Menu", throwIfNotFound: true);
        m_Player_ToggleChat = m_Player.FindAction("Toggle Chat", throwIfNotFound: true);
        m_Player_Chatbox = m_Player.FindAction("Chatbox", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_Look;
    private readonly InputAction m_Player_Zoom;
    private readonly InputAction m_Player_EnableMouse;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Click;
    private readonly InputAction m_Player_ToggleMenu;
    private readonly InputAction m_Player_ToggleChat;
    private readonly InputAction m_Player_Chatbox;
    public struct PlayerActions
    {
        private @Controls m_Wrapper;
        public PlayerActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Look => m_Wrapper.m_Player_Look;
        public InputAction @Zoom => m_Wrapper.m_Player_Zoom;
        public InputAction @EnableMouse => m_Wrapper.m_Player_EnableMouse;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Click => m_Wrapper.m_Player_Click;
        public InputAction @ToggleMenu => m_Wrapper.m_Player_ToggleMenu;
        public InputAction @ToggleChat => m_Wrapper.m_Player_ToggleChat;
        public InputAction @Chatbox => m_Wrapper.m_Player_Chatbox;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Look.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @Zoom.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnZoom;
                @Zoom.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnZoom;
                @Zoom.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnZoom;
                @EnableMouse.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEnableMouse;
                @EnableMouse.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEnableMouse;
                @EnableMouse.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEnableMouse;
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Click.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnClick;
                @Click.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnClick;
                @Click.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnClick;
                @ToggleMenu.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleMenu;
                @ToggleMenu.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleMenu;
                @ToggleMenu.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleMenu;
                @ToggleChat.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleChat;
                @ToggleChat.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleChat;
                @ToggleChat.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleChat;
                @Chatbox.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnChatbox;
                @Chatbox.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnChatbox;
                @Chatbox.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnChatbox;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @Zoom.started += instance.OnZoom;
                @Zoom.performed += instance.OnZoom;
                @Zoom.canceled += instance.OnZoom;
                @EnableMouse.started += instance.OnEnableMouse;
                @EnableMouse.performed += instance.OnEnableMouse;
                @EnableMouse.canceled += instance.OnEnableMouse;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Click.started += instance.OnClick;
                @Click.performed += instance.OnClick;
                @Click.canceled += instance.OnClick;
                @ToggleMenu.started += instance.OnToggleMenu;
                @ToggleMenu.performed += instance.OnToggleMenu;
                @ToggleMenu.canceled += instance.OnToggleMenu;
                @ToggleChat.started += instance.OnToggleChat;
                @ToggleChat.performed += instance.OnToggleChat;
                @ToggleChat.canceled += instance.OnToggleChat;
                @Chatbox.started += instance.OnChatbox;
                @Chatbox.performed += instance.OnChatbox;
                @Chatbox.canceled += instance.OnChatbox;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_GameSchemeIndex = -1;
    public InputControlScheme GameScheme
    {
        get
        {
            if (m_GameSchemeIndex == -1) m_GameSchemeIndex = asset.FindControlSchemeIndex("Game");
            return asset.controlSchemes[m_GameSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
        void OnEnableMouse(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnClick(InputAction.CallbackContext context);
        void OnToggleMenu(InputAction.CallbackContext context);
        void OnToggleChat(InputAction.CallbackContext context);
        void OnChatbox(InputAction.CallbackContext context);
    }
}
