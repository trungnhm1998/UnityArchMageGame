using System;
using UnityEngine;

namespace AttributeSystem
{
    [Serializable]
    public struct AttributeWithValue
    {
        public AttributeScriptableObject AttributeDef;
        public double Value;
    }

    [RequireComponent(typeof(AttributeSystemBehaviour))]
    public class StatsInitializer : MonoBehaviour
    {
        [SerializeField] private AttributeSystemBehaviour _attributeSystemBehaviour;
        [SerializeField] private AttributeWithValue[] _stats;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_attributeSystemBehaviour != null) return;
            _attributeSystemBehaviour = GetComponent<AttributeSystemBehaviour>();
        }
#endif

        private void Start()
        {
            foreach (var attributeWithValue in _stats)
            {
                _attributeSystemBehaviour.AddAttribute(attributeWithValue.AttributeDef);
                _attributeSystemBehaviour.SetAttributeBaseValue(attributeWithValue.AttributeDef, attributeWithValue.Value);
            }
            
            _attributeSystemBehaviour.UpdateAttributeValues();
        }
    }
}