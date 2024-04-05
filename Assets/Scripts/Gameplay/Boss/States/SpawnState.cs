using System.Collections;
using UnityEngine;

namespace ArchMageTest.Gameplay.Boss.States
{
    public class SpawnState : StateBaseSO
    {
        [SerializeField] private float _spawnTime = 1f;
        [SerializeField] private MoveTowardPlayerState _moveTowardPlayerState;

        public override void OnEnter(BossBehaviour bossBehaviour)
        {
            base.OnEnter(bossBehaviour);

            StartCoroutine(CoTransitionToMove());
        }

        private IEnumerator CoTransitionToMove()
        {
            yield return new WaitForSeconds(_spawnTime); // demo purposes
            StateMachine.ChangeState(_moveTowardPlayerState);
        }
    }
}