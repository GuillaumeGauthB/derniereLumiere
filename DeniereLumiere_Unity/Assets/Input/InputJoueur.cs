//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Input/InputJoueur.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @InputJoueur : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputJoueur()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputJoueur"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""29192838-daa3-4fb7-9d7f-5f9ee2ca1685"",
            ""actions"": [
                {
                    ""name"": ""Saut"",
                    ""type"": ""Button"",
                    ""id"": ""a93f6f19-5f42-4576-8907-c479334a803f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Mouvement"",
                    ""type"": ""Value"",
                    ""id"": ""6003782c-b8c9-482a-bbfc-bf0cffc0b091"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Courrir"",
                    ""type"": ""Button"",
                    ""id"": ""c4fe28c2-f638-44c3-b011-1b0e34c54549"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold"",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d59b10a9-82d7-491e-bf0c-48200980c495"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Saut"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fe6b1847-4098-4de5-bb3c-d88d0f2d9045"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Saut"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Keyboard and Mouse"",
                    ""id"": ""bd7feb80-526a-41b3-886f-6b5321a6734b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouvement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""left"",
                    ""id"": ""4fc7c06a-7be2-4ae6-a165-4a6d3ce26044"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouvement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""63e04ec2-7453-4778-9e12-d1d27fcbdef5"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouvement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""bf814507-80aa-4c92-8e83-9b0688beda08"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouvement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""a10ea9e5-0394-4bd0-afbe-9ba86b6ca3a6"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouvement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Gamepad"",
                    ""id"": ""d5a967ed-880e-4b8f-b4c4-cdafc089ee4e"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouvement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""left"",
                    ""id"": ""a19bc375-b1b4-4dda-bd50-25ecb37591ed"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouvement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""307fad4b-9677-4b78-b76d-0157b5f4bd55"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouvement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""1f6165cc-4bda-45b1-a431-1244aeb4f53f"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouvement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""ac228669-2d20-4e11-bb8b-994ec76657a9"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouvement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""492246c6-8055-4124-8933-98d729c63b3f"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Courrir"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Saut = m_Player.FindAction("Saut", throwIfNotFound: true);
        m_Player_Mouvement = m_Player.FindAction("Mouvement", throwIfNotFound: true);
        m_Player_Courrir = m_Player.FindAction("Courrir", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Saut;
    private readonly InputAction m_Player_Mouvement;
    private readonly InputAction m_Player_Courrir;
    public struct PlayerActions
    {
        private @InputJoueur m_Wrapper;
        public PlayerActions(@InputJoueur wrapper) { m_Wrapper = wrapper; }
        public InputAction @Saut => m_Wrapper.m_Player_Saut;
        public InputAction @Mouvement => m_Wrapper.m_Player_Mouvement;
        public InputAction @Courrir => m_Wrapper.m_Player_Courrir;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Saut.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSaut;
                @Saut.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSaut;
                @Saut.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSaut;
                @Mouvement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouvement;
                @Mouvement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouvement;
                @Mouvement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouvement;
                @Courrir.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCourrir;
                @Courrir.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCourrir;
                @Courrir.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCourrir;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Saut.started += instance.OnSaut;
                @Saut.performed += instance.OnSaut;
                @Saut.canceled += instance.OnSaut;
                @Mouvement.started += instance.OnMouvement;
                @Mouvement.performed += instance.OnMouvement;
                @Mouvement.canceled += instance.OnMouvement;
                @Courrir.started += instance.OnCourrir;
                @Courrir.performed += instance.OnCourrir;
                @Courrir.canceled += instance.OnCourrir;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnSaut(InputAction.CallbackContext context);
        void OnMouvement(InputAction.CallbackContext context);
        void OnCourrir(InputAction.CallbackContext context);
    }
}
