using System;
using System.Collections;
using System.Linq;
using GameplayAbilitySystem.AbilitySystem;
using GameplayAbilitySystem.AbilitySystem.Components;
using GameplayAbilitySystem.AbilitySystem.ScriptableObjects;
using NUnit.Framework;
using UnityEngine;

namespace Tests.Runtime.AbilitySystem
{
    public class MockAbilityDef : AbilityScriptableObject<MockAbilitySpec> { }

    public class MockAbilitySpec : GameplayAbilitySpec
    {
        protected override IEnumerator OnAbilityActive()
        {
            yield return null;
        }
    }

    [TestFixture]
    public class AbilitySystemBehaviourTests
    {
        private AbilitySystemBehaviour _abilitySystemBehaviour;
        private AbilityScriptableObject _mockAbilityScriptableObject;

        [SetUp]
        public void SetUp()
        {
            _abilitySystemBehaviour = new GameObject().AddComponent<AbilitySystemBehaviour>();
            _mockAbilityScriptableObject = ScriptableObject.CreateInstance<MockAbilityDef>();
        }

        [Test]
        public void GiveAbility_WhenAbilityIsNull_ThrowsNullReferenceException()
        {
            Assert.Throws<NullReferenceException>(() => _abilitySystemBehaviour.GiveAbility(null));
        }

        [Test]
        public void GiveAbility_WhenAbilityIsNotNull_AddsAbilityToGrantedAbilities()
        {
            var grantedAbility = _abilitySystemBehaviour.GiveAbility(_mockAbilityScriptableObject);
            Assert.Contains(grantedAbility, _abilitySystemBehaviour.GrantedAbilities.ToList());
        }
        
        [Test]
        public void GiveAbility_WhenAbilityIsNotNull_ReturnsAbilitySpec()
        {
            var grantedAbility = _abilitySystemBehaviour.GiveAbility(_mockAbilityScriptableObject);
            Assert.IsNotNull(grantedAbility);
            Assert.AreEqual(_mockAbilityScriptableObject, grantedAbility.AbilitySO);
            Assert.IsInstanceOf<MockAbilitySpec>(grantedAbility);
        }
    }
}