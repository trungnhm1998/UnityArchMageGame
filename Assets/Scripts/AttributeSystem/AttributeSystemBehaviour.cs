using System.Collections.Generic;
using UnityEngine;

namespace AttributeSystem
{
    public class AttributeSystemBehaviour : MonoBehaviour
    {
        [SerializeField] private AttributeScriptableObject[] _attributes;
        [SerializeField] private AttributeValue[] _attributeValues;

        private readonly Dictionary<AttributeScriptableObject, int> _attributeIndexCache = new();
        private bool _cacheDirty;

        private void Awake()
        {

        }

        /// <summary>
        /// Get Attribute indices from the cache if the cache were dirty little ***
        /// we will UnStale the cache and update it
        /// </summary>
        /// <returns></returns>
        public Dictionary<AttributeScriptableObject, int> GetAttributeIndexCache()
        {
            if (!_cacheDirty) return _attributeIndexCache;

            _attributeIndexCache.Clear();
            for (int index = 0; index < _attributeValues.Length; index++)
            {
                _attributeIndexCache.Add(_attributeValues[index].AttributeDef, index);
            }

            _cacheDirty = false;

            return _attributeIndexCache;
        }


        public void AddAttribute(AttributeScriptableObject attributeDef) { }

        public void SetAttributeBaseValue(AttributeScriptableObject attributeDef, double value) { }

        public void UpdateAttributeValues()
        {
            foreach (var attributeValue in _attributeValues) { }
        }
    }
}