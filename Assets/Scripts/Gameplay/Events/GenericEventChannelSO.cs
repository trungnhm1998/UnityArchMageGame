using UnityEngine;
using UnityEngine.Events;

namespace ArchMageTest.Gameplay.Events
{
    public abstract class GenericEventChannelSO<T> : ScriptableObject
    {
        public event UnityAction<T> EventRaised;

        public virtual void RaiseEvent(T obj) => OnRaiseEvent(obj);

        protected virtual void OnRaiseEvent(T obj)
        {
            if (EventRaised == null)
            {
                Debug.LogWarning($"Event was raised on {name} but no one was listening.");
                return;
            }

            EventRaised.Invoke(obj);
        }
    }
}