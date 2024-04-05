using GameplayAbilitySystem.AttributeSystem.ScriptableObjects;
using UnityEngine;

namespace ArchMageTest.Gameplay.Abilities
{
    public class AttributeSets : ScriptableObject
    {
        [SerializeField] private AttributeScriptableObject _health;
        public static AttributeScriptableObject Health { get; set; }
        
        [SerializeField] private AttributeScriptableObject _speed;
        public static AttributeScriptableObject Speed { get; set; }

        private void OnEnable()
        {
            Health = _health;
            Speed = _speed;
        }
    }
}