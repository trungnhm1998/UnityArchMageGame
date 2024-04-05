using ArchMageTest.Gameplay.Abilities;
using GameplayAbilitySystem.AbilitySystem;
using GameplayAbilitySystem.AbilitySystem.Components;
using UnityEngine;

namespace ArchMageTest.Gameplay.Boss.States
{
    public class MeleeAttackState : StateBaseSO
    {
        [SerializeField] private MoveTowardPlayerState _moveTowardPlayerState;
        [SerializeField] private AnimationBasedAbility _meleeAttackAbility;
        private AbilitySystemBehaviour _abilitySystem;
        private GameplayAbilitySpec _spec;

        public override void OnEnter(BossBehaviour bossBehaviour)
        {
            base.OnEnter(bossBehaviour);
            _abilitySystem = bossBehaviour.GetComponent<AbilitySystemBehaviour>();
            _spec = _abilitySystem.GiveAbility<GameplayAbilitySpec>(_meleeAttackAbility);

            _spec.TryActiveAbility();
            _spec.AbilityEndedEvent += BackToMoveState;
        }

        private void BackToMoveState()
        {
            _spec.AbilityEndedEvent -= BackToMoveState;
            StateMachine.ChangeState(_moveTowardPlayerState);
        }

        public override void OnExit(BossBehaviour bossBehaviour)
        {
            base.OnExit(bossBehaviour);
            _spec.AbilityEndedEvent -= BackToMoveState;
        }
    }
}