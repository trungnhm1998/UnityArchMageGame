using UnityEngine;
using UnityEngine.InputSystem;

namespace ArchMageTest.Gameplay.Player.States
{
    public class Walk : StateBase
    {
        public override void Enter(PlayerBehaviour playerBehaviour)
        {
            base.Enter(playerBehaviour);
        }

        public override void Exit(PlayerBehaviour playerBehaviour)
        {
            base.Exit(playerBehaviour);
        }

        public override void Move(InputAction.CallbackContext context)
        {
            base.Move(context);
        }

        public override void Attack(InputAction.CallbackContext context)
        {
            base.Attack(context);
            PlayerComp.ChangeState(PlayerComp.AttackingState);
        }

        public override void Update()
        {
            base.Update();

            if (PlayerComp.InputVector == Vector2.zero)
            {
                PlayerComp.ChangeState(PlayerComp.IdleState);
                return;
            }

            PlayerComp.transform.position +=
                new Vector3(PlayerComp.InputVector.x, 0, PlayerComp.InputVector.y) * 5f * Time.deltaTime;
            var targetRotation =
                Quaternion.LookRotation(new Vector3(PlayerComp.InputVector.x, 0, PlayerComp.InputVector.y), Vector3.up);
            PlayerComp.transform.rotation = Quaternion.Slerp(PlayerComp.transform.rotation, targetRotation, 0.15f);
        }
    }
}