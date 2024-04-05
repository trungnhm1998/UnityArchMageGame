using GameplayAbilitySystem.AbilitySystem.Components;
using UnityEngine;
using UnityEngine.Events;

namespace ArchMageTest.Gameplay.Character
{
    public class HitBoxRelay : MonoBehaviour
    {
        [SerializeField] private UnityEvent<AbilitySystemBehaviour> _onHit;
        [SerializeField] private UnityEvent<AbilitySystemBehaviour> _onStay;
        

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(gameObject.tag)) return; // ignore if hit box hit player due to bones
            if (!other.TryGetComponent(out AbilitySystemBehaviour target)) return;
            _onHit.Invoke(target);
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag(gameObject.tag)) return; // ignore if hit box hit player due to bones
            if (!other.TryGetComponent(out AbilitySystemBehaviour target)) return;
            _onStay.Invoke(target);
        }
    }
}