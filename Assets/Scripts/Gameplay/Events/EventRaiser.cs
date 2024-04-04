using UnityEngine;

namespace ArchMageTest.Gameplay.Events
{
    public class EventRaiser : MonoBehaviour
    {
        public void RaiseEvent<T>(GenericEventChannelSO<T> eventChannel, T obj)
        {
            eventChannel.RaiseEvent(obj);
        }
        
        public void RaiseEvent(VoidEventChannelSO eventChannel)
        {
            eventChannel.RaiseEvent();
        }
    }
}