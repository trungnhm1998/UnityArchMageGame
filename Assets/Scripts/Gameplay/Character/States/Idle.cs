using UnityEngine;
using UnityEngine.InputSystem;

namespace ArchMageTest.Gameplay.Character.States
{
    public class Idle : StateBase
    {
        public override void Enter(PlayerBehaviour playerBehaviour)
        {
            base.Enter(playerBehaviour);
            playerBehaviour.Animator.SetBool(PlayerBehaviour.IsAttacking, false);
        }

        public override void Move(InputAction.CallbackContext context)
        {
            base.Move(context);
            PlayerComp.InputVector = context.ReadValue<Vector2>();
            PlayerComp.ChangeState(PlayerComp.WalkState);
        }

        public override void Attack(InputAction.CallbackContext context)
        {
            base.Attack(context);
            PlayerComp.ChangeState(PlayerComp.AttackingState);
        }
        
        public override void Update()
        {
            base.Update();
            if (PlayerComp.InputVector == Vector2.zero) return;
            PlayerComp.ChangeState(PlayerComp.WalkState);
        }
    }
}