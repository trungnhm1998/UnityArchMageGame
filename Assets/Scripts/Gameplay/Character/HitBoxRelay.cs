using GameplayAbilitySystem.AbilitySystem.Components;
using UnityEngine;
using UnityEngine.Events;

namespace ArchMageTest.Gameplay.Character
{
    public class HitBoxRelay : MonoBehaviour
    {
        [SerializeField] private UnityEvent<AbilitySystemBehaviour> _onHit;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(gameObject.tag)) return; // ignore if hit box hit player due to bones
            if (!other.TryGetComponent(out AbilitySystemBehaviour target)) return;
            _onHit.Invoke(target);
        }
    }
}