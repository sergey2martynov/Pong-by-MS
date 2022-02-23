using UnityEngine;

public class RestartController : MonoBehaviour
{
    [SerializeField] private RestartButtonController _restartButtonController;

    [SerializeField] private GameOverMenuController _gameOverMenuController;

    [SerializeField] private StartMenuDisabler _startMenuDisabler;

    [SerializeField] private ProjectStarter _projectStarter;

    private GameStateController _gameStateController;

    private void Start()
    {
        _gameStateController = _projectStarter.GetGameController();

        _restartButtonController.Clicked += RestartTheGame;
    }

    private void OnDestroy()
    {
        _restartButtonController.Clicked -= RestartTheGame;
    }

    private void RestartTheGame()
    {
        _gameOverMenuController.DisableGameOverMenu(false);

        _startMenuDisabler.DisableStartMenu(true);

        _gameStateController.GameState = GameState.ChoiceOfNumberOfPlayers;
    }
}