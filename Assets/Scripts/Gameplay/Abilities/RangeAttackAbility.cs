using System.Collections;
using System.Collections.Generic;
using ArchMageTest.Gameplay.Events;
using GameplayAbilitySystem.AbilitySystem;
using GameplayAbilitySystem.AbilitySystem.Components;
using GameplayAbilitySystem.AbilitySystem.ScriptableObjects;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ArchMageTest.Gameplay.Abilities
{
    public class RangeAttackAbility : ArchMageAbilityBase
    {
        [SerializeField] private Projectile _projectilePrefab;
        public Projectile ProjectilePrefab => _projectilePrefab;
        [SerializeField] private string _nameOfPointToSpawnProjectile;
        public string NameOfPointToSpawnProjectile => _nameOfPointToSpawnProjectile;
        [field: SerializeField] public float ProjectileSpeed { get; private set; }
        [field: SerializeField] public VoidEventChannelSO _spawnProjectileEvent { get; private set; }

        protected override GameplayAbilitySpec CreateAbility()
        {
            var spec = new RangeAttackAbilitySpec(this);
            spec.SetEventChannel(_spawnProjectileEvent);
            spec.SetProjectileSpeed(ProjectileSpeed);
            return spec;
        }
    }

    public class RangeAttackAbilitySpec : GameplayAbilitySpec
    {
        private Animator _animator;
        private static readonly int RangeAttack = Animator.StringToHash("RangeAttack");
        private VoidEventChannelSO _spawnProjectileEvent;
        private List<Vector3> _directions;
        private bool _casting;
        private float _projectileSpeed;
        private RangeAttackAbility _def;
        private IAttacker _attacker;


        public RangeAttackAbilitySpec(RangeAttackAbility def)
        {
            _def = def;
        }

        public void SetProjectileSpeed(float projectileSpeed) => _projectileSpeed = projectileSpeed;

        public override void InitAbility(AbilitySystemBehaviour owner, AbilityScriptableObject abilitySO)
        {
            base.InitAbility(owner, abilitySO);
            _animator = Owner.GetComponent<Animator>();
            _attacker = Owner.GetComponent<IAttacker>();
        }

        protected override IEnumerator OnAbilityActive()
        {
            _attacker.Attacked += AnimationEnded;
            _casting = true;
            GameObject gameObject;
            var pointToSpawnProjectile =
                (gameObject = Owner.gameObject).transform.Find(_def.NameOfPointToSpawnProjectile);
            var forward = gameObject.transform.forward;
            var backward = -forward;
            var right = gameObject.transform.right;
            var left = -right;
            _directions = new List<Vector3> { forward, backward, right, left };

            _spawnProjectileEvent.EventRaised += SpawnProjectile;
            _animator.SetTrigger(RangeAttack);
            Debug.Log("Range attack");
            // spawn projectile, if any hit target dealt damage once?
            yield return new WaitUntil(() => _casting == false);
            EndAbility();
        }

        protected override void OnAbilityEnded()
        {
            _attacker.Attacked += AnimationEnded;
            _spawnProjectileEvent.EventRaised -= SpawnProjectile;
            base.OnAbilityEnded();
        }

        private void SpawnProjectile()
        {
            _spawnProjectileEvent.EventRaised -= SpawnProjectile;
            foreach (var direction in _directions)
            {
                var projectile = Object.Instantiate(_def.ProjectilePrefab, Owner.transform.position,
                    Quaternion.identity);
                projectile.Launch(direction, _projectileSpeed);
                projectile.RegisterHitEvent(DamageTarget);
            }
        }

        private void DamageTarget(AbilitySystemBehaviour target)
        {
            Debug.Log("Applying " +
                      $"{_def.AbilityParameters.Damage}dmg * {_def.AbilityParameters.Level}lvl " +
                      $"= {_def.AbilityParameters.Damage * _def.AbilityParameters.Level} to target");

            var targetAttributeSystem = target.AttributeSystem;
            targetAttributeSystem.TryGetAttributeValue(AttributeSets.Health, out var healthValue);
            healthValue.BaseValue -= _def.AbilityParameters.Damage * _def.AbilityParameters.Level;
            targetAttributeSystem.SetAttributeValue(AttributeSets.Health, healthValue);
        }

        private void AnimationEnded()
        {
            _casting = false;
        }

        public void SetEventChannel(VoidEventChannelSO spawnProjectileEvent) =>
            _spawnProjectileEvent = spawnProjectileEvent;
    }
}