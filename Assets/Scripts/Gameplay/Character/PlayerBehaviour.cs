using System;
using ArchMageTest.Gameplay.Abilities;
using ArchMageTest.Gameplay.Character.States;
using ArchMageTest.Gameplay.Events;
using GameplayAbilitySystem.AbilitySystem.Components;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ArchMageTest.Gameplay.Character
{
    public class PlayerBehaviour : MonoBehaviour, GameInput.IDefaultActions, IAttacker
    {
        public static readonly int IsWalking = Animator.StringToHash("IsWalking");
        public static readonly int IsAttacking = Animator.StringToHash("IsAttacking");

        [SerializeField] private AbilitySystemBehaviour _abilitySystem;
        [SerializeField] private Animator _animator;
        public Animator Animator => _animator;
        [SerializeField] private LookAtBus _lookAtBus;
        [SerializeField] private AnimationBasedAbility _attackAbility;
        [SerializeField] private VoidEventChannelSO _attackAnimationEndEvent;


        public Vector2 InputVector { get; set; }

        public IState IdleState { get; } = new Idle();
        public IState WalkState { get; } = new Walk();
        public IState AttackingState { get; private set; }

        private IState _currentState;

        private void OnValidate()
        {
            if (_abilitySystem == null) _abilitySystem = GetComponent<AbilitySystemBehaviour>();
        }

        private string _currentStateName;
        public void ChangeState(IState state)
        {
            _currentStateName = state.GetType().Name;
            _currentState?.Exit(this);
            _currentState = state;
            _currentState.Enter(this);
        }

        private GameInput _gameInput;

        private void Awake()
        {
            _gameInput = new GameInput();
            _gameInput.Default.Enable();
            _gameInput.Default.SetCallbacks(this);
            AttackingState = new Attacking(
                _lookAtBus,
                _abilitySystem.GiveAbility<AnimationBaseAbilitySpec>(_attackAbility),
                _attackAnimationEndEvent);
        }

        private void Start()
        {
            ChangeState(IdleState);
        }

        private void Update()
        {
            _currentState.Update();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            InputVector = context.ReadValue<Vector2>();
            _animator.SetBool(IsWalking, InputVector != Vector2.zero);
            _currentState.Move(context);
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            _currentState.Attack(context);
        }

        public event Action<AbilitySystemBehaviour> TargetHit;

        public void OnTargetHit(AbilitySystemBehaviour target)
        {
            Debug.Log($"Player Attacking {target.gameObject.name}");
            TargetHit?.Invoke(target);
        }

        private void OnGUI()
        {
            GUI.Label(new Rect(10, 10, 200, 20), $"Current State: {_currentStateName}");
        }
    }
}