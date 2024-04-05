using GameplayAbilitySystem.AttributeSystem.ScriptableObjects;
using UnityEngine;

namespace GameplayAbilitySystem.AttributeSystem.Components
{
    public class InlineStatsInitializer : MonoBehaviour, IStatInitializer
    {
        [SerializeField] private AttributeSystemBehaviour _attributeSystem;
        [SerializeField] private AttributeWithValue[] _database;
        [SerializeField] private bool _initOnStart = false;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_attributeSystem != null) return;
            _attributeSystem = GetComponent<AttributeSystemBehaviour>();
        }
#endif

        private void Start()
        {
            if (_initOnStart) InitStats();
        }

        public void InitStats()
        {
            for (int i = 0; i < _database.Length; i++)
            {
                var initValue = _database[i];
                _attributeSystem.AddAttribute(initValue.Attribute);
                _attributeSystem.SetAttributeBaseValue(initValue.Attribute, initValue.Value);
                
            }

            _attributeSystem.UpdateAttributeValues();
            
            InitAllAttributes();
        }

        private void InitAllAttributes()
        {
            for (int i = 0; i < _attributeSystem.AttributeValues.Count; i++)
            {
                var attributeValue = _attributeSystem.AttributeValues[i];
                _attributeSystem.AttributeValues[i] =
                    attributeValue.Attribute.CalculateInitialValue(attributeValue,
                        _attributeSystem.AttributeValues);
            }
        }
    }
}