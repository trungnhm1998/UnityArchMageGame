using System;
using UnityEngine;

namespace AttributeSystem
{
    [Serializable]
    public struct AttributeValue
    {
        [field: SerializeField] public AttributeScriptableObject AttributeDef { get; set; }
        [field: SerializeField] public double CurrentValue { get; set; }

        public AttributeValue(AttributeScriptableObject attributeDef)
        {
            AttributeDef = attributeDef;
            CurrentValue = 0f;
        }

        public AttributeValue Clone()
        {
            return new AttributeValue
            {
                CurrentValue = CurrentValue,
                AttributeDef = AttributeDef
            };
        }
    }
}