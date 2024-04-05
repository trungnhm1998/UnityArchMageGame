using UnityEngine;

namespace ArchMageTest.Gameplay.Character
{
    public class CharacterTransformBus : ScriptableObject
    {
        private GameObject _gameObject;
        public GameObject GameObject => _gameObject; // using get set here doesn't clear the cache between session
        public void SetGameObject(GameObject gameObject) => _gameObject = gameObject;

        private void Reset()
        {
            _gameObject = null;
        }
    }
}