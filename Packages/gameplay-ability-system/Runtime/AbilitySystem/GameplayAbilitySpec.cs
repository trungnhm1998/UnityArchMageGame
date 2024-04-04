using System.Collections;
using GameplayAbilitySystem.AbilitySystem.Components;
using GameplayAbilitySystem.AbilitySystem.ScriptableObjects;
using UnityEngine;

namespace GameplayAbilitySystem.AbilitySystem
{
    public abstract class GameplayAbilitySpec
    {
        private bool _isActive;
        public bool IsActive => _isActive;

        private AbilityScriptableObject _abilitySO;

        public AbilityScriptableObject AbilitySO
        {
            get => _abilitySO;
            set => _abilitySO = value;
        }

        private AbilitySystemBehaviour _owner;

        public AbilitySystemBehaviour Owner
        {
            get => _owner;
            set => _owner = value;
        }

        /// <summary>
        /// Initiation method of ability
        /// </summary>
        /// <param name="owner">Owner of this ability</param>
        /// <param name="abilitySO">Ability's data SO</param>
        public virtual void InitAbility(AbilitySystemBehaviour owner, AbilityScriptableObject abilitySO)
        {
            _owner = owner;
            _abilitySO = abilitySO;
        }

        public virtual bool TryActiveAbility()
        {
            if (AbilitySO == null)
            {
                Debug.LogWarning("Try to active a Ability with null data");
                return false;
            }

            if (_owner == null)
            {
                Debug.LogWarning($"Try to active a Ability [{AbilitySO.name}] with invalid owner");
                return false;
            }

            _owner.TryActiveAbility(this);
            return true;
        }

        /// <summary>
        /// Not the same as granted a ability, ability might be granted but not activate yet
        /// </summary>
        public void ActivateAbility()
        {
            if (!CanActiveAbility()) return;
            _owner.StartCoroutine(InternalActiveAbility());
        }

        private IEnumerator InternalActiveAbility()
        {
            _isActive = true;
            yield return OnAbilityActive();
        }

        /// <summary>
        /// Not the same as ability being removed, ability ended but still in the system
        ///
        /// This should always be called
        /// </summary>
        public void EndAbility()
        {
            if (!_isActive || _owner == null) return;

            _isActive = false;
            _owner.StopCoroutine(OnAbilityActive());
            OnAbilityEnded();
        }

        protected virtual void OnAbilityEnded() { }

        /// <summary>
        /// Need the owner to active so we could use the coroutine
        /// </summary>
        /// <returns></returns>
        public virtual bool CanActiveAbility()
        {
            return !_isActive && _owner != null && _owner.isActiveAndEnabled && DoesAbilitySatisfyTagRequirements();
        }

        /// <summary>
        /// This ability can only active if the Owner system has all the required tags
        /// and none of the Ignore tags
        /// Source and Target are not implemented yet
        /// </summary>
        protected virtual bool DoesAbilitySatisfyTagRequirements() => true;

        public virtual void OnAbilityRemoved(GameplayAbilitySpec gameplayAbilitySpec)
        {
            EndAbility();
        }

        public virtual void OnAbilityGranted(GameplayAbilitySpec gameplayAbilitySpec) { }

        /// <summary>
        /// Will be called by <see cref="ActivateAbility"/> when the ability is active, implement this for custom logic
        /// 
        /// Using IEnumerator so the ability can produce step by step like having delay time, etc...
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerator OnAbilityActive();
    }
}