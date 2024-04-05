using System;
using GameplayAbilitySystem.AbilitySystem.Components;

namespace ArchMageTest.Gameplay.Abilities
{
    public interface IAttacker
    {
        public event Action Attacked;
        public event Action<AbilitySystemBehaviour> TargetHit;

        public void OnTargetHit(AbilitySystemBehaviour target);
    }
}