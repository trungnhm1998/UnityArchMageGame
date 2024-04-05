using ArchMageTest.Gameplay.Character;
using UnityEngine;

namespace ArchMageTest.Gameplay.Boss.States
{
    public class MoveTowardPlayerState : StateBaseSO
    {
        [SerializeField] private CharacterTransformBus _playerTransform;
        [SerializeField] private MeleeAttackState _meleeAttackState;
        [SerializeField] private RangeAttackState _rangeAttackState;
        [SerializeField] private float _moveTimeout = 3f;
        [SerializeField] private float _chanceToMeleeAttack = .8f;

        private IMovementController _movementController;

        private bool _inRange;

        public override void OnEnter(BossBehaviour bossBehaviour)
        {
            base.OnEnter(bossBehaviour);
            _movementController = bossBehaviour.GetComponent<IMovementController>();
            _movementController.MoveTo(_playerTransform.GameObject.transform.position);
            _timeLeft = _moveTimeout;
            _inRange = false;
        }

        public override void OnExit(BossBehaviour bossBehaviour)
        {
            base.OnExit(bossBehaviour);
            _movementController.StopMovement();
        }

        public void EnterAttackRange()
        {
            if (Random.value <= _chanceToMeleeAttack)
            {
                StateMachine.ChangeState(_meleeAttackState);
                return;
            }

            StateMachine.ChangeState(_rangeAttackState);
        }

        public void StayInRange()
        {
            _inRange = true;
        }

        private float _timeLeft;

        public override void Update()
        {
            base.Update();
            LookAtPlayer();

            _timeLeft -= Time.deltaTime;
            if (_timeLeft > 0) return;
            if (_inRange)
            {
                EnterAttackRange();
                return;
            }

            StateMachine.ChangeState(_rangeAttackState);
        }

        private void LookAtPlayer()
        {
            var direction = _playerTransform.GameObject.transform.position - StateMachine.transform.position;
            Transform transform;
            (transform = StateMachine.transform).rotation = Quaternion.LookRotation(direction);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
    }
}