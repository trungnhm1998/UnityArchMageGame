using ArchMageTest.Gameplay.Character;
using UnityEngine;

namespace ArchMageTest.Gameplay.Events
{
    [CreateAssetMenu(menuName = "Create DamageableEvent", fileName = "DamageableEvent", order = 0)]
    public class DamageableEvent : GenericEventChannelSO<IDamageable> { }
}