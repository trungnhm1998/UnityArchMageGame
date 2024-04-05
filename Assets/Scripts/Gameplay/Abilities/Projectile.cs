using System;
using System.Collections;
using GameplayAbilitySystem.AbilitySystem.Components;
using UnityEngine;

namespace ArchMageTest.Gameplay.Abilities
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float _selfDestructTime = 5f;

        private float _projectileSpeed;
        private Action<AbilitySystemBehaviour> _damageTarget;

        public void Launch(Vector3 direction, float projectileSpeed)
        {
            _projectileSpeed = projectileSpeed;
            transform.forward = direction;
            StartCoroutine(CoSelfDestruct());
        }

        private IEnumerator CoSelfDestruct()
        {
            yield return new WaitForSeconds(_selfDestructTime);
            Destroy(gameObject);
        }

        private void Update()
        {
            var transform1 = transform;
            transform1.position += transform1.forward * _projectileSpeed * Time.deltaTime;
        }

        public void RegisterHitEvent(Action<AbilitySystemBehaviour> damageTargetCallback) =>
            _damageTarget = damageTargetCallback;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(gameObject.tag)) return; // cannot damage self
            if (!other.TryGetComponent(out AbilitySystemBehaviour target)) return;
            _damageTarget?.Invoke(target);
            Destroy(gameObject);
        }
    }
}