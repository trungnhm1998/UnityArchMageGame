using UnityEngine.InputSystem;

namespace ArchMageTest.Gameplay.Character.States
{
    public abstract class StateBase : IState
    {
        private PlayerBehaviour _playerCompBehaviour;
        protected PlayerBehaviour PlayerComp => _playerCompBehaviour;

        public virtual void Enter(PlayerBehaviour playerBehaviour)
        {
            _playerCompBehaviour = playerBehaviour;
        }

        public virtual void Exit(PlayerBehaviour playerBehaviour) { }

        public virtual void Update() { }

        public virtual void Attack(InputAction.CallbackContext context) { }

        public virtual void Move(InputAction.CallbackContext context) { }
    }
}