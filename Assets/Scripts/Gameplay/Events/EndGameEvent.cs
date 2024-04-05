using ArchMageTest.Gameplay.Abilities;
using GameplayAbilitySystem.AttributeSystem;
using GameplayAbilitySystem.AttributeSystem.Components;
using GameplayAbilitySystem.AttributeSystem.ScriptableObjects;
using UnityEngine;

namespace ArchMageTest.Gameplay.Events
{
    [CreateAssetMenu(menuName = "Create EndGameEvent", fileName = "EndGameEvent", order = 0)]
    public class EndGameEvent : AttributesEventBase
    {
        [SerializeField] private bool _isGameWon;

        public override void PreAttributeChange(AttributeSystemBehaviour attributeSystem,
            ref AttributeValue newAttributeValue) { }

        public override void PostAttributeChange(AttributeSystemBehaviour attributeSystem,
            ref AttributeValue oldAttributeValue,
            ref AttributeValue newAttributeValue)
        {
            if (newAttributeValue.Attribute != AttributeSets.Health) return;
            if (newAttributeValue.CurrentValue > 0) return;

            if (_isGameWon)
            {
                Debug.Log("Game Won");
            }
            else
            {
                Debug.Log("Game Over");
            }
        }
    }
}