//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.2.0
//     from Assets/Controllers/PlayerControls.inputactions
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

public partial class @PlayerControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""GamePlay"",
            ""id"": ""8ad498a7-d0fb-4476-8757-dc6a73670fee"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""61b6e1cb-427d-4b63-9440-cf5d4ca8fd26"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""07dd5915-e6c4-40d1-8592-fc99e761fa30"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""9d015634-f2f8-40ff-b166-f07f00592f39"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""bbb5713d-c5e2-4416-aa13-4cc137a8f101"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""ZQSD"",
                    ""id"": ""c70162d4-8781-414e-b94d-ef70888f48dd"",
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
                    ""id"": ""c8c96b68-d8b4-4f17-b733-c90b0f1f35ed"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""5280111c-d7c8-4616-af01-32ca01e37541"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""31e9debe-0044-4ee5-a182-7d609b1a47be"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""407e6aa2-33f2-412c-a344-38d525507e0c"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""9ae17581-f819-45a3-a0f3-98eb4ea9b630"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8fa2d8cf-8b43-41e7-810b-eb1e807fb0ca"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Battle"",
            ""id"": ""58d0a40c-33c8-44b9-8852-9e0dc1bbf0c6"",
            ""actions"": [
                {
                    ""name"": ""MeleeAttack"",
                    ""type"": ""Button"",
                    ""id"": ""d250b4e9-6165-4791-8dfa-53000d9f776b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RangedAttack"",
                    ""type"": ""Button"",
                    ""id"": ""1b3d254c-1c74-4d69-9c8d-9a8353d916b5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""AttackDirection"",
                    ""type"": ""Value"",
                    ""id"": ""9290cc7e-35fe-4764-b4fe-978625013957"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""73895618-2c4e-413a-9976-10d50573480d"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MeleeAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6ec45e7f-5aa9-4100-97f0-9b001dfac0e3"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RangedAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8205b484-ef8d-4546-aedd-18e2b9fa82c7"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AttackDirection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Dialogue"",
            ""id"": ""fb50b1aa-595a-4c9a-a9ef-bd91b13f7c0a"",
            ""actions"": [
                {
                    ""name"": ""DialogueInteraction"",
                    ""type"": ""Button"",
                    ""id"": ""142cde3b-c299-4d94-8b12-36dbcb280368"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""de04974b-bc87-4fbf-a63e-51708a9bdb24"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DialogueInteraction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""CheatCode"",
            ""id"": ""8004fd96-6426-448c-bafe-dfa96e4d1414"",
            ""actions"": [
                {
                    ""name"": ""Kill boss"",
                    ""type"": ""Button"",
                    ""id"": ""e03a5fb3-7799-42f8-871a-9bd179d53aa4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""WinterShape"",
                    ""type"": ""Button"",
                    ""id"": ""f063b53e-4dce-48d1-a76b-ca4456b06a28"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""FallShape"",
                    ""type"": ""Button"",
                    ""id"": ""b28e67e2-21fd-46aa-995c-a595fbca1bf3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""43bc1c90-919b-4a68-bb70-b127fcbdf497"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Kill boss"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d24840da-618c-4afe-868b-7a7116eccbed"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WinterShape"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""77707173-2ba9-4b72-b014-12fbfc0c6f1b"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FallShape"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // GamePlay
        m_GamePlay = asset.FindActionMap("GamePlay", throwIfNotFound: true);
        m_GamePlay_Move = m_GamePlay.FindAction("Move", throwIfNotFound: true);
        m_GamePlay_Interact = m_GamePlay.FindAction("Interact", throwIfNotFound: true);
        m_GamePlay_Jump = m_GamePlay.FindAction("Jump", throwIfNotFound: true);
        // Battle
        m_Battle = asset.FindActionMap("Battle", throwIfNotFound: true);
        m_Battle_MeleeAttack = m_Battle.FindAction("MeleeAttack", throwIfNotFound: true);
        m_Battle_RangedAttack = m_Battle.FindAction("RangedAttack", throwIfNotFound: true);
        m_Battle_AttackDirection = m_Battle.FindAction("AttackDirection", throwIfNotFound: true);
        // Dialogue
        m_Dialogue = asset.FindActionMap("Dialogue", throwIfNotFound: true);
        m_Dialogue_DialogueInteraction = m_Dialogue.FindAction("DialogueInteraction", throwIfNotFound: true);
        // CheatCode
        m_CheatCode = asset.FindActionMap("CheatCode", throwIfNotFound: true);
        m_CheatCode_Killboss = m_CheatCode.FindAction("Kill boss", throwIfNotFound: true);
        m_CheatCode_WinterShape = m_CheatCode.FindAction("WinterShape", throwIfNotFound: true);
        m_CheatCode_FallShape = m_CheatCode.FindAction("FallShape", throwIfNotFound: true);
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

    // GamePlay
    private readonly InputActionMap m_GamePlay;
    private IGamePlayActions m_GamePlayActionsCallbackInterface;
    private readonly InputAction m_GamePlay_Move;
    private readonly InputAction m_GamePlay_Interact;
    private readonly InputAction m_GamePlay_Jump;
    public struct GamePlayActions
    {
        private @PlayerControls m_Wrapper;
        public GamePlayActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_GamePlay_Move;
        public InputAction @Interact => m_Wrapper.m_GamePlay_Interact;
        public InputAction @Jump => m_Wrapper.m_GamePlay_Jump;
        public InputActionMap Get() { return m_Wrapper.m_GamePlay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GamePlayActions set) { return set.Get(); }
        public void SetCallbacks(IGamePlayActions instance)
        {
            if (m_Wrapper.m_GamePlayActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMove;
                @Interact.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnInteract;
                @Jump.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnJump;
            }
            m_Wrapper.m_GamePlayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
            }
        }
    }
    public GamePlayActions @GamePlay => new GamePlayActions(this);

    // Battle
    private readonly InputActionMap m_Battle;
    private IBattleActions m_BattleActionsCallbackInterface;
    private readonly InputAction m_Battle_MeleeAttack;
    private readonly InputAction m_Battle_RangedAttack;
    private readonly InputAction m_Battle_AttackDirection;
    public struct BattleActions
    {
        private @PlayerControls m_Wrapper;
        public BattleActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MeleeAttack => m_Wrapper.m_Battle_MeleeAttack;
        public InputAction @RangedAttack => m_Wrapper.m_Battle_RangedAttack;
        public InputAction @AttackDirection => m_Wrapper.m_Battle_AttackDirection;
        public InputActionMap Get() { return m_Wrapper.m_Battle; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BattleActions set) { return set.Get(); }
        public void SetCallbacks(IBattleActions instance)
        {
            if (m_Wrapper.m_BattleActionsCallbackInterface != null)
            {
                @MeleeAttack.started -= m_Wrapper.m_BattleActionsCallbackInterface.OnMeleeAttack;
                @MeleeAttack.performed -= m_Wrapper.m_BattleActionsCallbackInterface.OnMeleeAttack;
                @MeleeAttack.canceled -= m_Wrapper.m_BattleActionsCallbackInterface.OnMeleeAttack;
                @RangedAttack.started -= m_Wrapper.m_BattleActionsCallbackInterface.OnRangedAttack;
                @RangedAttack.performed -= m_Wrapper.m_BattleActionsCallbackInterface.OnRangedAttack;
                @RangedAttack.canceled -= m_Wrapper.m_BattleActionsCallbackInterface.OnRangedAttack;
                @AttackDirection.started -= m_Wrapper.m_BattleActionsCallbackInterface.OnAttackDirection;
                @AttackDirection.performed -= m_Wrapper.m_BattleActionsCallbackInterface.OnAttackDirection;
                @AttackDirection.canceled -= m_Wrapper.m_BattleActionsCallbackInterface.OnAttackDirection;
            }
            m_Wrapper.m_BattleActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MeleeAttack.started += instance.OnMeleeAttack;
                @MeleeAttack.performed += instance.OnMeleeAttack;
                @MeleeAttack.canceled += instance.OnMeleeAttack;
                @RangedAttack.started += instance.OnRangedAttack;
                @RangedAttack.performed += instance.OnRangedAttack;
                @RangedAttack.canceled += instance.OnRangedAttack;
                @AttackDirection.started += instance.OnAttackDirection;
                @AttackDirection.performed += instance.OnAttackDirection;
                @AttackDirection.canceled += instance.OnAttackDirection;
            }
        }
    }
    public BattleActions @Battle => new BattleActions(this);

    // Dialogue
    private readonly InputActionMap m_Dialogue;
    private IDialogueActions m_DialogueActionsCallbackInterface;
    private readonly InputAction m_Dialogue_DialogueInteraction;
    public struct DialogueActions
    {
        private @PlayerControls m_Wrapper;
        public DialogueActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @DialogueInteraction => m_Wrapper.m_Dialogue_DialogueInteraction;
        public InputActionMap Get() { return m_Wrapper.m_Dialogue; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DialogueActions set) { return set.Get(); }
        public void SetCallbacks(IDialogueActions instance)
        {
            if (m_Wrapper.m_DialogueActionsCallbackInterface != null)
            {
                @DialogueInteraction.started -= m_Wrapper.m_DialogueActionsCallbackInterface.OnDialogueInteraction;
                @DialogueInteraction.performed -= m_Wrapper.m_DialogueActionsCallbackInterface.OnDialogueInteraction;
                @DialogueInteraction.canceled -= m_Wrapper.m_DialogueActionsCallbackInterface.OnDialogueInteraction;
            }
            m_Wrapper.m_DialogueActionsCallbackInterface = instance;
            if (instance != null)
            {
                @DialogueInteraction.started += instance.OnDialogueInteraction;
                @DialogueInteraction.performed += instance.OnDialogueInteraction;
                @DialogueInteraction.canceled += instance.OnDialogueInteraction;
            }
        }
    }
    public DialogueActions @Dialogue => new DialogueActions(this);

    // CheatCode
    private readonly InputActionMap m_CheatCode;
    private ICheatCodeActions m_CheatCodeActionsCallbackInterface;
    private readonly InputAction m_CheatCode_Killboss;
    private readonly InputAction m_CheatCode_WinterShape;
    private readonly InputAction m_CheatCode_FallShape;
    public struct CheatCodeActions
    {
        private @PlayerControls m_Wrapper;
        public CheatCodeActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Killboss => m_Wrapper.m_CheatCode_Killboss;
        public InputAction @WinterShape => m_Wrapper.m_CheatCode_WinterShape;
        public InputAction @FallShape => m_Wrapper.m_CheatCode_FallShape;
        public InputActionMap Get() { return m_Wrapper.m_CheatCode; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CheatCodeActions set) { return set.Get(); }
        public void SetCallbacks(ICheatCodeActions instance)
        {
            if (m_Wrapper.m_CheatCodeActionsCallbackInterface != null)
            {
                @Killboss.started -= m_Wrapper.m_CheatCodeActionsCallbackInterface.OnKillboss;
                @Killboss.performed -= m_Wrapper.m_CheatCodeActionsCallbackInterface.OnKillboss;
                @Killboss.canceled -= m_Wrapper.m_CheatCodeActionsCallbackInterface.OnKillboss;
                @WinterShape.started -= m_Wrapper.m_CheatCodeActionsCallbackInterface.OnWinterShape;
                @WinterShape.performed -= m_Wrapper.m_CheatCodeActionsCallbackInterface.OnWinterShape;
                @WinterShape.canceled -= m_Wrapper.m_CheatCodeActionsCallbackInterface.OnWinterShape;
                @FallShape.started -= m_Wrapper.m_CheatCodeActionsCallbackInterface.OnFallShape;
                @FallShape.performed -= m_Wrapper.m_CheatCodeActionsCallbackInterface.OnFallShape;
                @FallShape.canceled -= m_Wrapper.m_CheatCodeActionsCallbackInterface.OnFallShape;
            }
            m_Wrapper.m_CheatCodeActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Killboss.started += instance.OnKillboss;
                @Killboss.performed += instance.OnKillboss;
                @Killboss.canceled += instance.OnKillboss;
                @WinterShape.started += instance.OnWinterShape;
                @WinterShape.performed += instance.OnWinterShape;
                @WinterShape.canceled += instance.OnWinterShape;
                @FallShape.started += instance.OnFallShape;
                @FallShape.performed += instance.OnFallShape;
                @FallShape.canceled += instance.OnFallShape;
            }
        }
    }
    public CheatCodeActions @CheatCode => new CheatCodeActions(this);
    public interface IGamePlayActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
    }
    public interface IBattleActions
    {
        void OnMeleeAttack(InputAction.CallbackContext context);
        void OnRangedAttack(InputAction.CallbackContext context);
        void OnAttackDirection(InputAction.CallbackContext context);
    }
    public interface IDialogueActions
    {
        void OnDialogueInteraction(InputAction.CallbackContext context);
    }
    public interface ICheatCodeActions
    {
        void OnKillboss(InputAction.CallbackContext context);
        void OnWinterShape(InputAction.CallbackContext context);
        void OnFallShape(InputAction.CallbackContext context);
    }
}
