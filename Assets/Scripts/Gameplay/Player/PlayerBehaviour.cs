using ArchMageTest.Gameplay.Events;
using ArchMageTest.Gameplay.Player.States;
using AttributeSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ArchMageTest.Gameplay.Player
{
    public class PlayerBehaviour : MonoBehaviour, GameInput.IDefaultActions
    {
        public static readonly int IsWalking = Animator.StringToHash("IsWalking");
        public static readonly int IsAttacking = Animator.StringToHash("IsAttacking");

        [SerializeField] private AttributeSystemBehaviour _attributeSystemBehaviour;
        [SerializeField] private AttributeScriptableObject _speedAttribute;
        [SerializeField] private Animator _animator;
        public Animator Animator => _animator;
        [SerializeField] private LookAtBus _lookAtBus;
        [SerializeField] private VoidEventChannelSO _enterIdleEvent;
        [SerializeField] private VoidEventChannelSO _enterIWalkingEvent;

        public Vector2 InputVector { get; set; }

        public IState IdleState { get; } = new Idle();
        public IState WalkState { get; } = new Walk();
        public IState AttackingState { get; private set; }

        private IState _currentState;

        public void ChangeState(IState state)
        {
            Debug.Log($"Change state to {state}");
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
            AttackingState = new Attacking(_lookAtBus, _enterIdleEvent, _enterIWalkingEvent);
        }

        private void Start()
        {
            ChangeState(IdleState);
        }

        private void Update()
        {
            _currentState.Update();
        }

        private void OnGUI()
        {
            GUI.Label(new Rect(10, 10, 300, 20), $"vector: {InputVector}");
            var time = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            GUI.Label(new Rect(10, 30, 300, 20), $"time: {time}");
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
    }
}