using UnityEngine.InputSystem;

namespace ArchMageTest.Gameplay.Character.States
{
    public interface IState
    {
        public void Exit(PlayerBehaviour playerBehaviour);
        public void Enter(PlayerBehaviour playerBehaviour);
        public void Update();
        public void Attack(InputAction.CallbackContext context);
        public void Move(InputAction.CallbackContext context);
    }
}