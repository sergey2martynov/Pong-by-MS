using UnityEngine;
using UnityEngine.UI;

public class GameOverMenuController : MonoBehaviour
{
    [SerializeField] private SwitchingControlOfTheRightRacket _switchingControlOfTheRightRacket;

    [SerializeField] private ScoreController _scoreController;

    [SerializeField] private ProjectStarter _projectStarter;

    [SerializeField] private GameObject _blackPanel;

    [SerializeField] private GameObject _whitePanel;

    [SerializeField] private GameObject _buttonRestart;

    [SerializeField] private Text _finalScoreText;

    [SerializeField] private Text _winnerText;

    private GameStateController _gameStateController;

    private void Start()
    {
        _gameStateController = _projectStarter.GetGameController();

        _gameStateController.GameStateChanged += DetermineTheResult;
    }

    private void OnDestroy()
    {
        _gameStateController.GameStateChanged -= DetermineTheResult;
    }

    private void DetermineTheResult()
    {
        if (_gameStateController.GameState == GameState.GameOver)
        {
            if (_switchingControlOfTheRightRacket.NumberOfPlayers == NumberOfPlayers.OnePlayer &&
                _scoreController.Winner == Players.LeftPlayer)
            {
                _winnerText.text = "You Win!";
            }
            else if (_switchingControlOfTheRightRacket.NumberOfPlayers == NumberOfPlayers.OnePlayer &&
                     _scoreController.Winner == Players.RightPlayer)
            {
                _winnerText.text = "You Lose!";
            }
            else if (_switchingControlOfTheRightRacket.NumberOfPlayers == NumberOfPlayers.TwoPlayers &&
                     _scoreController.Winner == Players.LeftPlayer)
            {
                _winnerText.text = "Left Player Win!";
            }
            else if (_switchingControlOfTheRightRacket.NumberOfPlayers == NumberOfPlayers.TwoPlayers &&
                     _scoreController.Winner == Players.RightPlayer)
            {
                _winnerText.text = "Right Player Win!";
            }

            _finalScoreText.text = _scoreController.LeftPlayerScore + " : " +
                                   _scoreController.RightPlayerScore;

            DisableGameOverMenu(true);
        }
    }

    public void DisableGameOverMenu(bool isActive)
    {
        _blackPanel.SetActive(isActive);
        _whitePanel.SetActive(isActive);
        _winnerText.gameObject.SetActive(isActive);
        _finalScoreText.gameObject.SetActive(isActive);
        _buttonRestart.SetActive(isActive);
    }
}