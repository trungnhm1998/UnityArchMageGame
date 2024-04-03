using UnityEngine.InputSystem;

namespace ArchMageTest.Gameplay.Player.States
{
    public abstract class StateBase : IState
    {
        private PlayerBehaviour _playerBehaviour;
        protected PlayerBehaviour Player => _playerBehaviour;

        public virtual void Enter(PlayerBehaviour playerBehaviour)
        {
            _playerBehaviour = playerBehaviour;
        }

        public virtual void Exit(PlayerBehaviour playerBehaviour) { }

        public virtual void Update() { }

        public virtual void Attack(InputAction.CallbackContext context) { }

        public virtual void Move(InputAction.CallbackContext context) { }
    }
}