using System.Collections;
using UnityEngine;

namespace ArchMageTest.Gameplay.Boss.States
{
    public interface IState
    {
        void OnEnter(BossBehaviour bossBehaviour);
        void OnExit(BossBehaviour bossBehaviour);
        void Update();
    }

    public abstract class StateBaseSO : ScriptableObject, IState
    {
        private BossBehaviour _bossBehaviour;
        protected BossBehaviour StateMachine => _bossBehaviour;

        public virtual void OnEnter(BossBehaviour bossBehaviour)
        {
            _bossBehaviour = bossBehaviour;
        }

        public virtual void OnExit(BossBehaviour bossBehaviour) { }

        public virtual void Update() { }

        protected void StartCoroutine(IEnumerator coroutine)
        {
            StateMachine.StartCoroutine(coroutine);
        }

        protected void StopCoroutine(IEnumerator coroutine)
        {
            StateMachine.StopCoroutine(coroutine);
        }
    }
}