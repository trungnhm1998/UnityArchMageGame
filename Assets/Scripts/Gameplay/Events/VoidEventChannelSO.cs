using UnityEngine;
using UnityEngine.Events;

namespace ArchMageTest.Gameplay.Events
{
    [CreateAssetMenu(menuName = "Events/Void Event")]
    public class VoidEventChannelSO : ScriptableObject
    {
        public UnityAction EventRaised;

        public void RaiseEvent()
        {
            OnRaiseEvent();
        }

        private void OnRaiseEvent()
        {
            if (EventRaised == null)
            {
                Debug.LogWarning($"Event was raised on {name} but no one was listening.");
                return;
            }

            EventRaised.Invoke();
        }
    }
}