using UnityEngine;

namespace ArchMageTest.Gameplay.Character
{
    public class LookAtBus : ScriptableObject
    {
        [field: SerializeField] public Vector3 LookAt { get; set; }
    }
}