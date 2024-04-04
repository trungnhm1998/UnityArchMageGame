using ArchMageTest.Gameplay.Abilities;
using ArchMageTest.Gameplay.Events;
using UnityEngine;

namespace ArchMageTest.Gameplay.Player.States
{
    /// <summary>
    /// wait until animation event is triggered to active hit box
    /// </summary>
    public class Attacking : StateBase
    {
        private readonly LookAtBus _lookAtBus;
        private AnimationBaseAbilitySpec _attackAbility;
        private VoidEventChannelSO _attackAnimationEndEvent;

        public Attacking(LookAtBus lookAtBus, AnimationBaseAbilitySpec attackAbility,
            VoidEventChannelSO attackAnimationEndEvent)
        {
            _attackAnimationEndEvent = attackAnimationEndEvent;
            _attackAbility = attackAbility;
            _lookAtBus = lookAtBus;
        }

        public override void Enter(PlayerBehaviour playerBehaviour)
        {
            base.Enter(playerBehaviour);
            LookAtClickPosition(playerBehaviour);
            _attackAbility.TryActiveAbility();

            _attackAnimationEndEvent.EventRaised += ChangeState;
        }

        private void LookAtClickPosition(PlayerBehaviour playerBehaviour)
        {
            Transform transform;
            (transform = playerBehaviour.transform).LookAt(_lookAtBus.LookAt);
            playerBehaviour.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }

        /// <summary>
        /// Exit only when animation ended event is triggered
        /// </summary>
        /// <param name="playerBehaviour"></param>
        public override void Exit(PlayerBehaviour playerBehaviour)
        {
            base.Exit(playerBehaviour);
            _attackAnimationEndEvent.EventRaised -= ChangeState;
            PlayerComp.Animator.SetBool(PlayerBehaviour.IsAttacking, false);
        }

        private void ChangeState()
        {
            if (PlayerComp.Animator.GetBool(PlayerBehaviour.IsWalking))
            {
                PlayerComp.ChangeState(PlayerComp.WalkState);
                return;
            }

            PlayerComp.ChangeState(PlayerComp.IdleState);
        }
    }
}