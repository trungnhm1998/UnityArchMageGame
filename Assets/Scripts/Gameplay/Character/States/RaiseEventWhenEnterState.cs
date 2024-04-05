using ArchMageTest.Gameplay.Events;
using UnityEngine;

namespace ArchMageTest.Gameplay.Character.States
{
    public class RaiseEventWhenEnterState : StateMachineBehaviour
    {
        [SerializeField] private VoidEventChannelSO _eventToRaise;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            _eventToRaise.RaiseEvent();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex) { }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex) { }

        public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex) { }

        public override void OnStateIK(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex) { }
    }
}