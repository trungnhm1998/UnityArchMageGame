using AttributeSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ArchMageTest.Gameplay.Player
{
    public class PlayerBehaviour : MonoBehaviour
    {
        [SerializeField] private AttributeSystemBehaviour _attributeSystemBehaviour;
        [SerializeField] private AttributeScriptableObject _speedAttribute;
        [SerializeField] private float _speed = 5f;
        [SerializeField] private LookAtBus _lookAtBus;

        private Transform _transform;
        private Vector2 _inputVector;


        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }

        public void Move(InputAction.CallbackContext context)
        {
            _inputVector = context.ReadValue<Vector2>();
        }

        public void Attack(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            Debug.Log("Attacking");
            _transform.rotation = Quaternion.LookRotation(_lookAtBus.LookAt - _transform.position, Vector3.up);
        }

        private void Update()
        {
            if (_inputVector == Vector2.zero) return;
            _inputVector.Normalize(); // prevent diagonal movement from being faster
            _transform.position += new Vector3(_inputVector.x, 0, _inputVector.y) * _speed * Time.deltaTime;
            var targetRotation = Quaternion.LookRotation(new Vector3(_inputVector.x, 0, _inputVector.y), Vector3.up);
            _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, 0.15f);
        }
    }
}