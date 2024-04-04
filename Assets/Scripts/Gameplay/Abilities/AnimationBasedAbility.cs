using System;
using System.Collections;
using GameplayAbilitySystem.AbilitySystem;
using GameplayAbilitySystem.AbilitySystem.Components;
using GameplayAbilitySystem.AbilitySystem.ScriptableObjects;
using UnityEngine;

namespace ArchMageTest.Gameplay.Abilities
{
    [Serializable]
    public struct AbilityParameters
    {
        public int Level;
        public float Damage;
    }

    [CreateAssetMenu(menuName = "Create AnimationBasedAbility", fileName = "AnimationBasedAbility", order = 0)]
    public class AnimationBasedAbility : AbilityScriptableObject
    {
        [field: SerializeField] public AbilityParameters AbilityParameters { get; private set; }
        [SerializeField] private ParameterType _parameterType;
        [SerializeField] private string _parameterName;

        [SerializeField] private bool _boolValue;
        public bool BoolValue => _boolValue;
        [SerializeField] private float _floatValue;
        public float FloatValue => _floatValue;
        [SerializeField] private int _intValue;
        public int IntValue => _intValue;

        public enum ParameterType
        {
            Trigger,
            Bool,
            Float,
            Int
        }

        protected override GameplayAbilitySpec CreateAbility() =>
            new AnimationBaseAbilitySpec(_parameterType, _parameterName, this);
    }

    public class AnimationBaseAbilitySpec : GameplayAbilitySpec
    {
        private readonly AnimationBasedAbility.ParameterType _parameterType;
        private readonly int _parameterHash;

        public AnimationBaseAbilitySpec(AnimationBasedAbility.ParameterType parameterType, string parameterName,
            AnimationBasedAbility abilityDef)
        {
            _abilityDef = abilityDef;
            _parameterType = parameterType;
            _parameterHash = Animator.StringToHash(parameterName);
        }

        private IAttacker _attacker;
        private Animator _animator;
        private AnimationBasedAbility _abilityDef;

        public override void InitAbility(AbilitySystemBehaviour owner, AbilityScriptableObject abilitySO)
        {
            base.InitAbility(owner, abilitySO);
            _animator = owner.GetComponent<Animator>();
            _attacker = owner.GetComponent<IAttacker>();
        }

        protected override IEnumerator OnAbilityActive()
        {
            _attacker.TargetHit += TargetHit;
            SetParameter(); // this will play the animation
            // wait for animation to finish
            yield return new WaitForSeconds(1f);
            EndAbility(); // TODO: Could do template pattern here so I won't forget to call EndAbility
        }

        private void TargetHit(AbilitySystemBehaviour target)
        {
            Debug.Log("Applying " +
                      $"{_abilityDef.AbilityParameters.Damage}dmg * {_abilityDef.AbilityParameters.Level}lvl " +
                      $"= {_abilityDef.AbilityParameters.Damage * _abilityDef.AbilityParameters.Level} to target");
        }

        protected override void OnAbilityEnded()
        {
            Debug.Log("Ability ended");
            base.OnAbilityEnded();
            _attacker.TargetHit -= TargetHit;
        }

        private void SetParameter()
        {
            switch (_parameterType)
            {
                case AnimationBasedAbility.ParameterType.Bool:
                    _animator.SetBool(_parameterHash, _abilityDef.BoolValue);
                    break;
                case AnimationBasedAbility.ParameterType.Int:
                    _animator.SetInteger(_parameterHash, _abilityDef.IntValue);
                    break;
                case AnimationBasedAbility.ParameterType.Float:
                    _animator.SetFloat(_parameterHash, _abilityDef.FloatValue);
                    break;
                case AnimationBasedAbility.ParameterType.Trigger:
                    _animator.SetTrigger(_parameterHash);
                    break;
            }
        }
    }
}