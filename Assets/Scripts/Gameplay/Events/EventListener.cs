using UnityEngine;
using UnityEngine.Events;

namespace ArchMageTest.Gameplay.Events
{
    public class EventListener : MonoBehaviour
    {
        [SerializeField] private VoidEventChannelSO _eventChannel;

        public UnityEvent OnEventRaised;

        private void OnEnable() => _eventChannel.EventRaised += OnResponse;

        private void OnDisable() => _eventChannel.EventRaised -= OnResponse;

        private void OnResponse() => OnEventRaised?.Invoke();
    }
}