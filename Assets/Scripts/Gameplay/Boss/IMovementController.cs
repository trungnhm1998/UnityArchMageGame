using UnityEngine;

namespace ArchMageTest.Gameplay.Boss
{
    public interface IMovementController
    {
        public void MoveTo(Vector3 moveTo);
        public void StopMovement();
    }
}