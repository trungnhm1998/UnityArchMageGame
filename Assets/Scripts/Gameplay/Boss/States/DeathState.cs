using System.Collections;
using UnityEngine;

namespace ArchMageTest.Gameplay.Boss.States
{
    internal class DeathState : IState
    {
        private static readonly int IsDead = Animator.StringToHash("IsDead");
        private BossBehaviour _bossBehaviour;

        public void OnEnter(BossBehaviour bossBehaviour)
        {
            _bossBehaviour = bossBehaviour;
            var animator = bossBehaviour.GetComponent<Animator>();
            animator.SetBool(IsDead, true);

            bossBehaviour.StartCoroutine(CoDestroy());
        }

        private IEnumerator CoDestroy()
        {
            yield return new WaitForSeconds(2f);
            _bossBehaviour.Destroy();
        }

        public void OnExit(BossBehaviour bossBehaviour) { }

        public void Update() { }
    }
}