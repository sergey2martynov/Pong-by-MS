using System.Collections.Generic;
using UnityEngine;

public class StartMenuDisabler : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    [SerializeField] private List<ButtonsController> _buttons = new List<ButtonsController>();

    [SerializeField] private ProjectStarter _projectStarter;

    private GameStateController _gameStateController;

    private void Start()
    {
        _gameStateController = _projectStarter.GetGameController();
    }

    public void DisableStartMenu(bool isActive)
    {
        _panel.SetActive(isActive);

        foreach (var button in _buttons)
        {
            button.gameObject.SetActive(isActive);
        }

        _gameStateController.GameState = GameStates.Start;
    }
}