using UnityEngine;

namespace ArchMageTest.Gameplay.Input
{
    public class InputBus : ScriptableObject
    {
        private GameInput _gameInput;
        public GameInput GameInput => _gameInput;

        private void OnEnable()
        {
            _gameInput ??= new GameInput();
        }
    }
}