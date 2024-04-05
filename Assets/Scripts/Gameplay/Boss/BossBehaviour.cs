using System;
using ArchMageTest.Gameplay.Abilities;
using ArchMageTest.Gameplay.Boss.States;
using GameplayAbilitySystem.AbilitySystem.Components;
using UnityEngine;

namespace ArchMageTest.Gameplay.Boss
{
    public class BossBehaviour : MonoBehaviour, IAttacker
    {
        [SerializeField] private SpawnState _spawnState;

        private IState _currentState;

        private void Awake()
        {
            ChangeState(_spawnState);
        }

#if UNITY_EDITOR
        private string _currentStateName;
#endif

        public void ChangeState(IState newState)
        {
#if UNITY_EDITOR
            _currentStateName = newState.GetType().Name;
#endif
            if (_currentState != null)
                _currentState.OnExit(this);
            _currentState = newState;
            _currentState.OnEnter(this);
        }

        public void Update()
        {
            _currentState?.Update();
        }

        private void OnGUI()
        {
#if UNITY_EDITOR
            GUI.Label(new Rect(10, 30, 300, 20), $"Boss State: {_currentStateName}");
#endif
        }

        public event Action Attacked;
        public event Action<AbilitySystemBehaviour> TargetHit;

        public void OnTargetHit(AbilitySystemBehaviour target)
        {
            Debug.Log("Boss Attacking");
            TargetHit?.Invoke(target);
        }

        public void OnAttacked() => Attacked?.Invoke();
    }
}