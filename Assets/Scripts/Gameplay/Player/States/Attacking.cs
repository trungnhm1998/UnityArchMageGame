using ArchMageTest.Gameplay.Events;
using UnityEngine;

namespace ArchMageTest.Gameplay.Player.States
{
    /// <summary>
    /// wait until animation event is triggered to active hit box
    /// </summary>
    public class Attacking : StateBase
    {
        private PlayerBehaviour _playerBehaviour;
        private readonly LookAtBus _lookAtBus;
        private VoidEventChannelSO _enterIdleEvent;
        private VoidEventChannelSO _enterWalkingEvent;

        public Attacking(LookAtBus lookAtBus, VoidEventChannelSO enterIdleEvent, VoidEventChannelSO enterWalkingEvent)
        {
            _enterWalkingEvent = enterWalkingEvent;
            _enterIdleEvent = enterIdleEvent;
            _lookAtBus = lookAtBus;
        }

        public override void Enter(PlayerBehaviour playerBehaviour)
        {
            _playerBehaviour = playerBehaviour;
            playerBehaviour.Animator.SetBool(PlayerBehaviour.IsAttacking, true);
            Debug.Log("Attacking");
            Transform transform;
            (transform = playerBehaviour.transform).rotation =
                Quaternion.LookRotation(_lookAtBus.LookAt - playerBehaviour.transform.position, Vector3.up);
            playerBehaviour.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

            _enterIdleEvent.EventRaised += ToIdleState;
            _enterWalkingEvent.EventRaised += ToWalkingState;
        }

        /// <summary>
        /// Exit only when animation ended event is triggered
        /// </summary>
        /// <param name="playerBehaviour"></param>
        public override void Exit(PlayerBehaviour playerBehaviour)
        {
            base.Exit(playerBehaviour);
            playerBehaviour.Animator.SetBool(PlayerBehaviour.IsAttacking, false);

            _enterIdleEvent.EventRaised -= ToIdleState;
            _enterWalkingEvent.EventRaised -= ToWalkingState;
        }

        private void ToIdleState()
        {
            _playerBehaviour.ChangeState(_playerBehaviour.IdleState);
        }

        private void ToWalkingState()
        {
            _playerBehaviour.ChangeState(_playerBehaviour.WalkState);
        }
    }
}