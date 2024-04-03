using UnityEngine;

namespace ArchMageTest.Gameplay.Player
{
    public class LookAtBus : ScriptableObject
    {
        [field: SerializeField] public Vector3 LookAt { get; set; }
    }
}