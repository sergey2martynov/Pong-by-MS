using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private Text _scoreLeftPlayerText;

    [SerializeField] private Text _scoreRightPlayerText;

    [SerializeField] private ProjectStarter _projectStarter;

    [SerializeField] private List<GoalDetector> _goalDetectors;

    [SerializeField] private int _winningScore;

    private GameStateController _gameStateController;

    public int ScoreLeftPlayer { get; private set; }

    public int ScoreRightPlayer { get; private set; }

    public string Winner { get; private set; }


    private void Start()
    {
        foreach (var wall in _goalDetectors)
        {
            wall.BallFlewOverTheLine += ScoreAPoint;
        }

        _gameStateController = _projectStarter.GetGameController();

        _gameStateController.GameStateChanged += ResetTheScore;
    }

    private void ScoreAPoint(GameObject line)
    {
        if (line.gameObject.name == "LeftWall")
        {
            ScoreRightPlayer++;
            _scoreRightPlayerText.text = ScoreRightPlayer.ToString();
            CheckForVictory(ScoreRightPlayer, "RightPlayer");
        }
        else if (line.gameObject.name == "RightWall")
        {
            ScoreLeftPlayer++;
            _scoreLeftPlayerText.text = ScoreLeftPlayer.ToString();
            CheckForVictory(ScoreLeftPlayer, "LeftPlayer");
        }
    }

    private void CheckForVictory(int score, string player)
    {
        if (score == _winningScore)
        {
            Winner = player;
            _gameStateController.GameState = GameStates.GameOver;
        }
    }

    private void ResetTheScore()
    {
        if (_gameStateController.GameState == GameStates.ChoiceOfNumberOfPlayers)
        {
            ScoreLeftPlayer = 0;
            ScoreRightPlayer = 0;
            _scoreLeftPlayerText.text = ScoreLeftPlayer.ToString();
            _scoreRightPlayerText.text = ScoreRightPlayer.ToString();
        }
    }

    private void OnDestroy()
    {
        foreach (var wall in _goalDetectors)
        {
            wall.BallFlewOverTheLine -= ScoreAPoint;
        }

        _gameStateController.GameStateChanged -= ResetTheScore;
    }
}