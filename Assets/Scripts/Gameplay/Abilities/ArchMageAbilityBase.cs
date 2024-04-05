using System;
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

    public abstract class ArchMageAbilityBase : AbilityScriptableObject
    {
        [field: SerializeField] public AbilityParameters AbilityParameters { get; private set; }
    }
}