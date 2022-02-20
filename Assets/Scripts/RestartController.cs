using UnityEngine;

public class RestartController : MonoBehaviour
{
    [SerializeField] private ButtonsController _buttonsController;

    [SerializeField] private GameOverMenuController _gameOverMenuController;

    [SerializeField] private StartMenuDisabler _startMenuDisabler;

    [SerializeField] private ProjectStarter _projectStarter;

    private GameStateController _gameStateController;

    private void Start()
    {
        _gameStateController = _projectStarter.GetGameController();

        _buttonsController.ButtonClicked += RestarTheGame;
    }

    private void RestarTheGame(string nameButton)
    {
        if (nameButton == "ButtonRestart")
        {
            _gameOverMenuController.DisableGameOverMenu(false);

            _startMenuDisabler.DisableStartMenu(true);

            _gameStateController.GameState = GameStates.ChoiceOfNumberOfPlayers;
        }
    }

    private void OnDestroy()
    {
        _buttonsController.ButtonClicked -= RestarTheGame;
    }
}