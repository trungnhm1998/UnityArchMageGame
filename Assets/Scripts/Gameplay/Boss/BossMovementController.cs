using ArchMageTest.Gameplay.Abilities;
using ArchMageTest.Gameplay.Character;
using GameplayAbilitySystem.AttributeSystem.Components;
using UnityEngine;

namespace ArchMageTest.Gameplay.Boss
{
    // drive using AI or input

    /// <summary>
    /// Move until reach the position and then raise event
    ///
    /// using rx or async can be more readable but I'll use event here
    /// </summary>
    public class BossMovementController : MonoBehaviour, IMovementController
    {
        [SerializeField] private CharacterTransformBus _characterTransformBus;
        [SerializeField] private AttributeSystemBehaviour _attributeSystem;

        private Vector3 _direction;
        private bool _isMoving;

        public void MoveTo(Vector3 moveTo)
        {
            _isMoving = true;
        }

        public void StopMovement() => _isMoving = false;

        private void Update()
        {
            if (!_isMoving) return;

            _attributeSystem.TryGetAttributeValue(AttributeSets.Speed, out var speedValue);
            var speed = speedValue.CurrentValue;
            var direction = _characterTransformBus.GameObject.transform.position - transform.position;
            transform.position += direction.normalized * speed * Time.deltaTime;
        }
    }
}