using UnityEngine;

namespace AbilitySystem
{
    public abstract class AbilityScriptableObject : ScriptableObject
    {
        public AbilitySpec GetAbilitySpec(AbilitySystemBehaviour owner)
        {
            AbilitySpec abilitySpec = CreateAbilitySpec();
            abilitySpec.Init(owner, this);
            return abilitySpec;
        }
        protected abstract AbilitySpec CreateAbilitySpec();
    }

    public abstract class AbilityScriptableObject<T> : AbilityScriptableObject where T : AbilitySpec, new()
    {
        protected override AbilitySpec CreateAbilitySpec() => new();
    }
}