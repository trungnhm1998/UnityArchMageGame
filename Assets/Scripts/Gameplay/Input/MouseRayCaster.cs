using ArchMageTest.Gameplay.Character;
using UnityEngine;

namespace ArchMageTest.Gameplay.Input
{
    public class MouseRayCaster : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private LookAtBus _lookAtBus;

        private RaycastHit _hit;

        private void Update()
        {
            Ray ray = _mainCamera.ScreenPointToRay(UnityEngine.Input.mousePosition);
            if (!Physics.Raycast(ray, out _hit)) return;
            Debug.DrawLine(ray.origin, _hit.point, Color.red);
            _lookAtBus.LookAt = _hit.point;
        }

        private void OnDrawGizmos()
        {
            if (_hit.collider == null) return;
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(_hit.point, 0.1f);
        }
    }
}