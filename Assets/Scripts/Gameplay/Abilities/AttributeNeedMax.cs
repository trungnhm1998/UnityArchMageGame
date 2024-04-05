using System.Collections.Generic;
using GameplayAbilitySystem.AttributeSystem;
using GameplayAbilitySystem.AttributeSystem.ScriptableObjects;
using UnityEngine;

namespace ArchMageTest.Gameplay.Abilities
{
    [CreateAssetMenu(menuName = "Create AttributeNeedMax", fileName = "AttributeNeedMax", order = 0)]
    public class AttributeNeedMax : AttributeScriptableObject
    {
        [SerializeField] private AttributeScriptableObject _maxAttribute;

        public override AttributeValue CalculateInitialValue(AttributeValue attributeValue, List<AttributeValue> otherAttributeValues)
        {
            var maxAttributeValue =
                otherAttributeValues.Find(x =>
                    x.Attribute ==
                    _maxAttribute); // TODO: Use attribute system as a param instead of Find like this

            attributeValue.BaseValue = maxAttributeValue.BaseValue;
            attributeValue.CurrentValue = maxAttributeValue.CurrentValue;

            return attributeValue;
        }
    }
}