using ArchMageTest.Gameplay.Abilities;
using GameplayAbilitySystem.AbilitySystem;
using GameplayAbilitySystem.AbilitySystem.Components;
using UnityEngine;

namespace ArchMageTest.Gameplay.Boss.States
{
    public class RangeAttackState : StateBaseSO
    {
        [SerializeField] private RangeAttackAbility _rangeAttackAbility;
        [SerializeField] private MoveTowardPlayerState _moveTowardPlayerState;


        private AbilitySystemBehaviour _abilitySystem;
        private GameplayAbilitySpec _spec;

        public override void OnEnter(BossBehaviour bossBehaviour)
        {
            base.OnEnter(bossBehaviour);
            _abilitySystem = bossBehaviour.GetComponent<AbilitySystemBehaviour>();
            _spec = _abilitySystem.GiveAbility(_rangeAttackAbility);

            _spec.TryActiveAbility();
            _spec.AbilityEndedEvent += BackToMoveState;
        }
        
        public override void OnExit(BossBehaviour bossBehaviour)
        {
            base.OnExit(bossBehaviour);
            _spec.AbilityEndedEvent -= BackToMoveState;
        }

        private void BackToMoveState()
        {
            _spec.AbilityEndedEvent -= BackToMoveState;
            StateMachine.ChangeState(_moveTowardPlayerState);
        }
    }
}