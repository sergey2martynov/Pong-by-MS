using System.Collections.Generic;
using UnityEngine;

public class SwitchingControlOfTheRightRacket : MonoBehaviour
{
    [SerializeField] private List<ButtonsController> _buttons = new List<ButtonsController>();

    [SerializeField] private RacketController _racketRightController;

    [SerializeField] private OpponentController _opponentController;

    [SerializeField] private StartMenuDisabler _startMenuDisabler;

    [SerializeField] private ProjectStarter _projectStarter;

    private GameStateController _gameStateController;

    public int NumberOfPlayers { get; private set; }

    private void Start()
    {
        _gameStateController = _projectStarter.GetGameController();

        foreach (var button in _buttons)
        {
            button.ButtonClicked += SetTheNumberOfPlayers;
        }

        _gameStateController.GameStateChanged += TurnOffAllControl;
    }

    private void SetTheNumberOfPlayers(string nameButton)
    {
        if (nameButton == "ButtonOnePlayer")
        {
            NumberOfPlayers = 1;

            TurnOffTheOpponentController(true);
        }
        else if (nameButton == "ButtonTwoPlayers")
        {
            NumberOfPlayers = 2;
            TurnOffTheRacketRightController(true);
        }

        _startMenuDisabler.DisableStartMenu(false);
    }

    private void TurnOffTheOpponentController(bool isActive)
    {
        _opponentController.enabled = isActive;
    }

    private void TurnOffTheRacketRightController(bool isActive)
    {
        _racketRightController.enabled = isActive;
    }

    private void TurnOffAllControl()
    {
        if (_gameStateController.GameState == GameStates.ChoiceOfNumberOfPlayers)
        {
            TurnOffTheRacketRightController(false);
            TurnOffTheOpponentController(false);
        }
    }

    private void OnDestroy()
    {
        foreach (var button in _buttons)
        {
            button.ButtonClicked -= SetTheNumberOfPlayers;

            _gameStateController.GameStateChanged -= TurnOffAllControl;
        }
    }
}