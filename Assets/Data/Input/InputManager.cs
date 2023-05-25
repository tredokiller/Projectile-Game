using UnityEngine;

namespace Data.Input
{
    public class InputManager : MonoBehaviour
    {
        private static InputManager _instance;
        public static InputManager Instance => _instance;

        private GameInput _gameInput;
        private GameInput.PlayerActions _playerActions;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject); // Destroy duplicate instances
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject); // Preserve the instance across scene changes

            _gameInput = new GameInput();
            _playerActions = _gameInput.Player;
        }

        private void OnEnable()
        {
            _gameInput.Enable();
        }

        private void OnDisable()
        {
            _gameInput.Disable();
        }

        public GameInput.PlayerActions GetPlayerActions()
        {
            return _playerActions;
        }
    }
}