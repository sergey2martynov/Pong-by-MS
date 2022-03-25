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
        if (_gameStateController.GameState == GameState.Game || _gameStateController.GameState == GameState.Start ||
            _gameStateController.GameState == GameState.BallOutOfGame)
        {
            _gameStateController.GameState = GameState.Pause;
            _pauseScreenDisabler.SetPauseScreenActive(true);
            return;
        }

        if (_gameStateController.GameState == GameState.Pause && _gameStateController.PreviousStage == GameState.Game)
        {
            _gameStateController.GameState = GameState.Game;
            _pauseScreenDisabler.SetPauseScreenActive(false);
            return;
        }

        if (_gameStateController.GameState == GameState.Pause &&
            _gameStateController.PreviousStage == GameState.Start)
        {
            _gameStateController.GameState = GameState.Start;
            _pauseScreenDisabler.SetPauseScreenActive(false);
            return;
        }

        if (_gameStateController.GameState == GameState.Pause &&
            _gameStateController.PreviousStage == GameState.BallOutOfGame)
        {
            _gameStateController.GameState = GameState.BallOutOfGame;
            _pauseScreenDisabler.SetPauseScreenActive(false);
        }
    }
}