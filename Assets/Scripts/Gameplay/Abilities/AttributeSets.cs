using GameplayAbilitySystem.AttributeSystem.ScriptableObjects;
using UnityEngine;

namespace ArchMageTest.Gameplay.Abilities
{
    public class AttributeSets : ScriptableObject
    {
        [SerializeField] private AttributeScriptableObject _maxHealth;
        public static AttributeScriptableObject MaxHealth { get; private set; }

        [SerializeField] private AttributeScriptableObject _health;
        public static AttributeScriptableObject Health { get; private set; }

        [SerializeField] private AttributeScriptableObject _speed;
        public static AttributeScriptableObject Speed { get; private set; }

        private void OnEnable()
        {
            Health = _health;
            Speed = _speed;
            MaxHealth = _maxHealth;
        }
    }
}