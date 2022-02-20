using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;

    [SerializeField] private ProjectStarter _projectStarter;

    [SerializeField] private PauseScreenDisabler _pauseScreenDisabler;

    private GameStateController _gameStateController;

    private void Start()
    {
        _gameStateController = _projectStarter.GetGameController();

        _inputManager.EscapePressed += TakeAPause;
    }

    private void TakeAPause()
    {
        if (_gameStateController.GameState == GameStates.Game || _gameStateController.GameState == GameStates.Start ||
            _gameStateController.GameState == GameStates.BallOutOfGame)
        {
            _gameStateController.GameState = GameStates.Pause;
            _pauseScreenDisabler.DisablePauseScreen(true);
            return;
        }

        if (_gameStateController.GameState == GameStates.Pause && _gameStateController.StagePrevious == GameStates.Game)
        {
            _gameStateController.GameState = GameStates.Game;
            _pauseScreenDisabler.DisablePauseScreen(false);
            return;
        }

        if (_gameStateController.GameState == GameStates.Pause &&
            _gameStateController.StagePrevious == GameStates.Start)
        {
            _gameStateController.GameState = GameStates.Start;
            _pauseScreenDisabler.DisablePauseScreen(false);
            return;
        }

        if (_gameStateController.GameState == GameStates.Pause &&
            _gameStateController.StagePrevious == GameStates.BallOutOfGame)
        {
            _gameStateController.GameState = GameStates.BallOutOfGame;
            _pauseScreenDisabler.DisablePauseScreen(false);
        }
    }
}