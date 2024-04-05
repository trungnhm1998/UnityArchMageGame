using GameplayAbilitySystem.AbilitySystem.Components;
using UnityEngine;

namespace GameplayAbilitySystem.AbilitySystem.ScriptableObjects
{
    public abstract class AbilityScriptableObject : ScriptableObject
    {
        public virtual GameplayAbilitySpec GetAbilitySpec(AbilitySystemBehaviour owner)
        {
            var ability = CreateAbility();
            ability.InitAbility(owner, this);
            return ability;
        }

        protected abstract GameplayAbilitySpec CreateAbility();
    }

    /// <summary>
    /// Override this to create new ability SO with a new abstract ability
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AbilityScriptableObject<T> : AbilityScriptableObject where T : GameplayAbilitySpec, new()
    {
        protected override GameplayAbilitySpec CreateAbility() => new T();
    }
}