using UnityEngine;
using UnityEngine.InputSystem;

namespace ArchMageTest.Gameplay.Player.States
{
    public class Idle : StateBase
    {
        public override void Enter(PlayerBehaviour playerBehaviour)
        {
            base.Enter(playerBehaviour);
        }

        public override void Move(InputAction.CallbackContext context)
        {
            base.Move(context);
            Player.InputVector = context.ReadValue<Vector2>();
            Player.ChangeState(Player.WalkState);
        }

        public override void Attack(InputAction.CallbackContext context)
        {
            base.Attack(context);
            Player.ChangeState(Player.AttackingState);
        }
        
        public override void Update()
        {
            base.Update();
            if (Player.InputVector == Vector2.zero) return;
            Player.ChangeState(Player.WalkState);
        }
    }
}