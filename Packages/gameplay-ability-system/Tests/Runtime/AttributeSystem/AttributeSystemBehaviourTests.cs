using GameplayAbilitySystem.AttributeSystem;
using GameplayAbilitySystem.AttributeSystem.Components;
using GameplayAbilitySystem.AttributeSystem.ScriptableObjects;
using NUnit.Framework;
using UnityEngine;

namespace Tests.Runtime.AttributeSystem
{
    [TestFixture]
    public class AttributeSystemBehaviourTests
    {
        private AttributeSystemBehaviour _attributeSystemBehaviour;
        private AttributeScriptableObject _mockAttributeScriptableObject;
        private Modifier _mockModifier;

        [SetUp]
        public void SetUp()
        {
            _attributeSystemBehaviour = new GameObject().AddComponent<AttributeSystemBehaviour>();
            _mockAttributeScriptableObject = ScriptableObject.CreateInstance<AttributeScriptableObject>();
            _mockModifier = new Modifier();
        }

        [Test]
        public void AddAttribute_AddsNewAttributeToSystem()
        {
            _attributeSystemBehaviour.AddAttribute(_mockAttributeScriptableObject);

            Assert.IsTrue(_attributeSystemBehaviour.HasAttribute(_mockAttributeScriptableObject, out _));
        }

        [Test]
        public void AddAttribute_IgnoresDuplicateAttributes()
        {
            _attributeSystemBehaviour.AddAttribute(_mockAttributeScriptableObject);
            _attributeSystemBehaviour.AddAttribute(_mockAttributeScriptableObject);

            Assert.AreEqual(1, _attributeSystemBehaviour.Attributes.Count);
        }

        [Test]
        public void TryAddModifierToAttribute_ModifiesAttributeInSystem()
        {
            _attributeSystemBehaviour.AddAttribute(_mockAttributeScriptableObject);

            var result =
                _attributeSystemBehaviour.TryAddModifierToAttribute(_mockModifier,
                    _mockAttributeScriptableObject);

            Assert.IsTrue(result);
        }

        [Test]
        public void TryAddModifierToAttribute_ReturnsFalseWhenAttributeNotInSystem()
        {
            var result =
                _attributeSystemBehaviour.TryAddModifierToAttribute(_mockModifier,
                    _mockAttributeScriptableObject);

            Assert.IsFalse(result);
        }

        [Test]
        public void HasAttribute_ReturnsTrueWhenAttributeInSystem()
        {
            _attributeSystemBehaviour.AddAttribute(_mockAttributeScriptableObject);

            var result = _attributeSystemBehaviour.HasAttribute(_mockAttributeScriptableObject, out _);

            Assert.IsTrue(result);
        }

        [Test]
        public void HasAttribute_ReturnsFalseWhenAttributeNotInSystem()
        {
            var result = _attributeSystemBehaviour.HasAttribute(_mockAttributeScriptableObject, out _);

            Assert.IsFalse(result);
        }

        [Test]
        public void ResetAllAttributes_ResetsAllAttributeValues()
        {
            _attributeSystemBehaviour.AddAttribute(_mockAttributeScriptableObject);
            _attributeSystemBehaviour.TryAddModifierToAttribute(_mockModifier,
                _mockAttributeScriptableObject);

            _attributeSystemBehaviour.ResetAllAttributes();

            Assert.AreEqual(0, _attributeSystemBehaviour.AttributeValues[0].CurrentValue);
        }

        [Test]
        public void ResetAttributeModifiers_ResetsAllAttributeModifiers()
        {
            // _attributeSystemBehaviour.AddAttribute(_mockAttributeScriptableObject);
            // _attributeSystemBehaviour.TryAddModifierToAttribute(_mockModifier,
            //     _mockAttributeScriptableObject);
            //
            // _attributeSystemBehaviour.ResetAttributeModifiers();

            // Assert.AreEqual(0, _attributeSystemBehaviour.AttributeValues[0].ExternalModifier.Value);
            // Assert.AreEqual(0, _attributeSystemBehaviour.AttributeValues[0].CoreModifier.Value);
        }
    }
}