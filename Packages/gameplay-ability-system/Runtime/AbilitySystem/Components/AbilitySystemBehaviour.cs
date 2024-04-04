using System;
using System.Collections.Generic;
using GameplayAbilitySystem.AbilitySystem.ScriptableObjects;
using GameplayAbilitySystem.AttributeSystem.Components;
using UnityEngine;
using UnityEngine.Assertions;

namespace GameplayAbilitySystem.AbilitySystem.Components
{
    [RequireComponent(typeof(AttributeSystemBehaviour))]
    public partial class AbilitySystemBehaviour : MonoBehaviour
    {
        public delegate void AbilityGranted(GameplayAbilitySpec grantedAbility);

        public event AbilityGranted AbilityGrantedEvent;
        [SerializeField] private AttributeSystemBehaviour _attributeSystem;

        public AttributeSystemBehaviour AttributeSystem
        {
            get => _attributeSystem;
            set => _attributeSystem = value;
        }

        private List<GameplayAbilitySpec> _grantedAbilities = new();
        public IReadOnlyList<GameplayAbilitySpec> GrantedAbilities => _grantedAbilities;

#if UNITY_EDITOR
        private void OnValidate()
        {
            ValidateComponents();
        }
#endif

        private void ValidateComponents()
        {
            if (!_attributeSystem) _attributeSystem = GetComponent<AttributeSystemBehaviour>();
        }

        private void Awake()
        {
            ValidateComponents();
            Assert.IsNotNull(_attributeSystem, $"Attribute System is required!");
        }

        private void OnDestroy()
        {
            RemoveAllAbilities();
        }

        /// <summary>
        /// Add/Give/Grant ability to the system. Only ability that in the system can be active
        /// There's only 1 ability per system (no duplicate ability)
        /// </summary>
        /// <param name="abilityDef"></param>
        /// <returns>A <see cref="GameplayAbilitySpec"/> to handle (humble object) their ability logic</returns>
        public GameplayAbilitySpec GiveAbility(AbilityScriptableObject abilityDef)
        {
            if (abilityDef == null)
                throw new NullReferenceException("AbilitySystemBehaviour::GiveAbility::AbilityDef is null");

            for (var index = 0; index < _grantedAbilities.Count; index++)
            {
                var ability = _grantedAbilities[index];
                if (ability.AbilitySO == abilityDef)
                    return ability;
            }

            var grantedAbility = abilityDef.GetAbilitySpec(this);

            _grantedAbilities.Add(grantedAbility);
            OnGrantedAbility(grantedAbility);

            return grantedAbility;
        }

        public T GiveAbility<T>(AbilityScriptableObject abilityDef) where T : GameplayAbilitySpec
            => (T)GiveAbility(abilityDef); // can I somehow make this generic?

        private void OnGrantedAbility(GameplayAbilitySpec gameplayAbilitySpecSpec)
        {
            if (!gameplayAbilitySpecSpec.AbilitySO) return;
            Debug.Log(
                $"AbilitySystemBehaviour::OnGrantedAbility {gameplayAbilitySpecSpec.AbilitySO.name} to {gameObject.name}");
            gameplayAbilitySpecSpec.OnAbilityGranted(gameplayAbilitySpecSpec);
            AbilityGrantedEvent?.Invoke(gameplayAbilitySpecSpec);
        }

        public void TryActiveAbility(GameplayAbilitySpec inGameplayAbilitySpecSpec)
        {
            if (inGameplayAbilitySpecSpec.AbilitySO == null) return;
            foreach (var abilitySpec in _grantedAbilities)
            {
                if (abilitySpec != inGameplayAbilitySpecSpec) continue;
                inGameplayAbilitySpecSpec.ActivateAbility();
            }
        }

        public bool RemoveAbility(GameplayAbilitySpec gameplayAbilitySpec)
        {
            List<GameplayAbilitySpec> grantedAbilitiesList = _grantedAbilities;
            for (int i = grantedAbilitiesList.Count - 1; i >= 0; i--)
            {
                var grantedSpec = grantedAbilitiesList[i];
                if (grantedSpec == gameplayAbilitySpec)
                {
                    OnRemoveAbility(gameplayAbilitySpec);
                    grantedAbilitiesList.RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        public bool RemoveAbility(AbilityScriptableObject abilityScriptableObject)
        {
            for (int i = _grantedAbilities.Count - 1; i >= 0; i--)
            {
                var grantedSpec = _grantedAbilities[i];
                if (grantedSpec.AbilitySO != abilityScriptableObject) continue;
                OnRemoveAbility(grantedSpec);
                _grantedAbilities.RemoveAt(i);
                return true;
            }

            return false;
        }

        private void OnRemoveAbility(GameplayAbilitySpec gameplayAbilitySpecSpec)
        {
            if (!gameplayAbilitySpecSpec.AbilitySO) return;

            gameplayAbilitySpecSpec.OnAbilityRemoved(gameplayAbilitySpecSpec);
        }

        public void RemoveAllAbilities()
        {
            for (int i = _grantedAbilities.Count - 1; i >= 0; i--)
            {
                var abilitySpec = _grantedAbilities[i];
                _grantedAbilities.RemoveAt(i);
                OnRemoveAbility(abilitySpec);
            }

            _grantedAbilities = new List<GameplayAbilitySpec>();
        }
    }
}