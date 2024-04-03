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
            Player.ChangeState(Player.AttackingState);
        }

        public override void Update()
        {
            base.Update();

            if (Player.InputVector == Vector2.zero)
            {
                Player.ChangeState(Player.IdleState);
                return;
            }

            Player.transform.position +=
                new Vector3(Player.InputVector.x, 0, Player.InputVector.y) * 5f * Time.deltaTime;
            var targetRotation =
                Quaternion.LookRotation(new Vector3(Player.InputVector.x, 0, Player.InputVector.y), Vector3.up);
            Player.transform.rotation = Quaternion.Slerp(Player.transform.rotation, targetRotation, 0.15f);
        }
    }
}