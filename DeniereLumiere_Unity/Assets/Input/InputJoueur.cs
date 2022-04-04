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
                },
                {
                    ""name"": ""Accroupir"",
                    ""type"": ""Button"",
                    ""id"": ""235ef93c-4304-470b-8cf4-7983ce2d7279"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Attaque Physique"",
                    ""type"": ""Button"",
                    ""id"": ""13a79382-c2dc-46d5-b47c-cf73a6f3a1c2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""f632c3a4-0877-4b0b-8925-d2b1836056d1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Enclencher Tir"",
                    ""type"": ""Button"",
                    ""id"": ""a49a9d94-930b-4743-ad87-a82ff828f806"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d59b10a9-82d7-491e-bf0c-48200980c495"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
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
                    ""groups"": ""Gamepad"",
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
                    ""groups"": ""Keyboard"",
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
                    ""groups"": ""Keyboard"",
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
                    ""groups"": ""Keyboard"",
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
                    ""groups"": ""Keyboard"",
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
                    ""groups"": ""Gamepad"",
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
                    ""groups"": ""Gamepad"",
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
                    ""groups"": ""Gamepad"",
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
                    ""groups"": ""Gamepad"",
                    ""action"": ""Mouvement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""c55a3d39-4bec-4f5c-b7eb-09b497d7b201"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Courrir"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1946a964-248d-4de9-bd8c-6036262c1a1c"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Courrir"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c1c12614-8fb1-486c-8fcf-bd489c060e89"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Accroupir"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6f293f86-3cfc-46a8-af50-270aa705c889"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Accroupir"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6587b868-2667-453c-9c52-8f2c585e3a96"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Accroupir"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5a44de47-9219-45da-bdb0-30164dd8d640"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Attaque Physique"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8388a101-c8da-409e-a189-a260dcf1cf67"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Attaque Physique"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ae02edbc-02eb-478b-b1a6-7c509a2ac906"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f5132bb4-3a01-47c8-b795-7b94e25b2997"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b1c28b2a-139e-462b-99c8-7c7bdbdd5704"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Enclencher Tir"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a6a5e249-9f84-4ea2-9c9e-6607eec9ef1c"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Enclencher Tir"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""TirLucioles"",
            ""id"": ""e1382296-a909-4f6c-9f0b-5d3b33868d00"",
            ""actions"": [
                {
                    ""name"": ""Position Souris"",
                    ""type"": ""Value"",
                    ""id"": ""29c1b2fd-480f-4583-b489-5e2d3c71ab8e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Tir"",
                    ""type"": ""Button"",
                    ""id"": ""165ca871-36c6-4e5a-ba29-ade70b3fb8b8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Annuler Tir"",
                    ""type"": ""Button"",
                    ""id"": ""4f5f0462-3fb4-47c2-b02b-ecb4d1166a0d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f1dd64c5-2cdc-42ed-8bf5-16ffe99515b9"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Clavier;Keyboard"",
                    ""action"": ""Position Souris"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""910417dd-bdb8-45b0-be37-23380deb1358"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Manette;Gamepad"",
                    ""action"": ""Position Souris"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""80dde9d8-8b98-454d-b66d-bf611fe4a2a3"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Clavier;Keyboard"",
                    ""action"": ""Tir"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1c9cfe16-e1e6-4d7b-800c-be455923cb74"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Manette;Gamepad"",
                    ""action"": ""Tir"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f944f14e-760d-4604-9bb1-4ef979d323ad"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Clavier;Keyboard"",
                    ""action"": ""Annuler Tir"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d63fea5e-c923-463b-b86d-586e04945706"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Manette;Gamepad"",
                    ""action"": ""Annuler Tir"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Saut = m_Player.FindAction("Saut", throwIfNotFound: true);
        m_Player_Mouvement = m_Player.FindAction("Mouvement", throwIfNotFound: true);
        m_Player_Courrir = m_Player.FindAction("Courrir", throwIfNotFound: true);
        m_Player_Accroupir = m_Player.FindAction("Accroupir", throwIfNotFound: true);
        m_Player_AttaquePhysique = m_Player.FindAction("Attaque Physique", throwIfNotFound: true);
        m_Player_Dash = m_Player.FindAction("Dash", throwIfNotFound: true);
        m_Player_EnclencherTir = m_Player.FindAction("Enclencher Tir", throwIfNotFound: true);
        // TirLucioles
        m_TirLucioles = asset.FindActionMap("TirLucioles", throwIfNotFound: true);
        m_TirLucioles_PositionSouris = m_TirLucioles.FindAction("Position Souris", throwIfNotFound: true);
        m_TirLucioles_Tir = m_TirLucioles.FindAction("Tir", throwIfNotFound: true);
        m_TirLucioles_AnnulerTir = m_TirLucioles.FindAction("Annuler Tir", throwIfNotFound: true);
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
    private readonly InputAction m_Player_Accroupir;
    private readonly InputAction m_Player_AttaquePhysique;
    private readonly InputAction m_Player_Dash;
    private readonly InputAction m_Player_EnclencherTir;
    public struct PlayerActions
    {
        private @InputJoueur m_Wrapper;
        public PlayerActions(@InputJoueur wrapper) { m_Wrapper = wrapper; }
        public InputAction @Saut => m_Wrapper.m_Player_Saut;
        public InputAction @Mouvement => m_Wrapper.m_Player_Mouvement;
        public InputAction @Courrir => m_Wrapper.m_Player_Courrir;
        public InputAction @Accroupir => m_Wrapper.m_Player_Accroupir;
        public InputAction @AttaquePhysique => m_Wrapper.m_Player_AttaquePhysique;
        public InputAction @Dash => m_Wrapper.m_Player_Dash;
        public InputAction @EnclencherTir => m_Wrapper.m_Player_EnclencherTir;
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
                @Accroupir.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAccroupir;
                @Accroupir.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAccroupir;
                @Accroupir.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAccroupir;
                @AttaquePhysique.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttaquePhysique;
                @AttaquePhysique.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttaquePhysique;
                @AttaquePhysique.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttaquePhysique;
                @Dash.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                @EnclencherTir.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEnclencherTir;
                @EnclencherTir.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEnclencherTir;
                @EnclencherTir.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEnclencherTir;
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
                @Accroupir.started += instance.OnAccroupir;
                @Accroupir.performed += instance.OnAccroupir;
                @Accroupir.canceled += instance.OnAccroupir;
                @AttaquePhysique.started += instance.OnAttaquePhysique;
                @AttaquePhysique.performed += instance.OnAttaquePhysique;
                @AttaquePhysique.canceled += instance.OnAttaquePhysique;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @EnclencherTir.started += instance.OnEnclencherTir;
                @EnclencherTir.performed += instance.OnEnclencherTir;
                @EnclencherTir.canceled += instance.OnEnclencherTir;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // TirLucioles
    private readonly InputActionMap m_TirLucioles;
    private ITirLuciolesActions m_TirLuciolesActionsCallbackInterface;
    private readonly InputAction m_TirLucioles_PositionSouris;
    private readonly InputAction m_TirLucioles_Tir;
    private readonly InputAction m_TirLucioles_AnnulerTir;
    public struct TirLuciolesActions
    {
        private @InputJoueur m_Wrapper;
        public TirLuciolesActions(@InputJoueur wrapper) { m_Wrapper = wrapper; }
        public InputAction @PositionSouris => m_Wrapper.m_TirLucioles_PositionSouris;
        public InputAction @Tir => m_Wrapper.m_TirLucioles_Tir;
        public InputAction @AnnulerTir => m_Wrapper.m_TirLucioles_AnnulerTir;
        public InputActionMap Get() { return m_Wrapper.m_TirLucioles; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TirLuciolesActions set) { return set.Get(); }
        public void SetCallbacks(ITirLuciolesActions instance)
        {
            if (m_Wrapper.m_TirLuciolesActionsCallbackInterface != null)
            {
                @PositionSouris.started -= m_Wrapper.m_TirLuciolesActionsCallbackInterface.OnPositionSouris;
                @PositionSouris.performed -= m_Wrapper.m_TirLuciolesActionsCallbackInterface.OnPositionSouris;
                @PositionSouris.canceled -= m_Wrapper.m_TirLuciolesActionsCallbackInterface.OnPositionSouris;
                @Tir.started -= m_Wrapper.m_TirLuciolesActionsCallbackInterface.OnTir;
                @Tir.performed -= m_Wrapper.m_TirLuciolesActionsCallbackInterface.OnTir;
                @Tir.canceled -= m_Wrapper.m_TirLuciolesActionsCallbackInterface.OnTir;
                @AnnulerTir.started -= m_Wrapper.m_TirLuciolesActionsCallbackInterface.OnAnnulerTir;
                @AnnulerTir.performed -= m_Wrapper.m_TirLuciolesActionsCallbackInterface.OnAnnulerTir;
                @AnnulerTir.canceled -= m_Wrapper.m_TirLuciolesActionsCallbackInterface.OnAnnulerTir;
            }
            m_Wrapper.m_TirLuciolesActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PositionSouris.started += instance.OnPositionSouris;
                @PositionSouris.performed += instance.OnPositionSouris;
                @PositionSouris.canceled += instance.OnPositionSouris;
                @Tir.started += instance.OnTir;
                @Tir.performed += instance.OnTir;
                @Tir.canceled += instance.OnTir;
                @AnnulerTir.started += instance.OnAnnulerTir;
                @AnnulerTir.performed += instance.OnAnnulerTir;
                @AnnulerTir.canceled += instance.OnAnnulerTir;
            }
        }
    }
    public TirLuciolesActions @TirLucioles => new TirLuciolesActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnSaut(InputAction.CallbackContext context);
        void OnMouvement(InputAction.CallbackContext context);
        void OnCourrir(InputAction.CallbackContext context);
        void OnAccroupir(InputAction.CallbackContext context);
        void OnAttaquePhysique(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnEnclencherTir(InputAction.CallbackContext context);
    }
    public interface ITirLuciolesActions
    {
        void OnPositionSouris(InputAction.CallbackContext context);
        void OnTir(InputAction.CallbackContext context);
        void OnAnnulerTir(InputAction.CallbackContext context);
    }
}
