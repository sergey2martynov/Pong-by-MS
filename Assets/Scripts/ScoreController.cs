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

    public int LeftPlayerScore { get; private set; }

    public int RightPlayerScore { get; private set; }

    public Players Winner { get; private set; }

    private void Start()
    {
        foreach (var wall in _goalDetectors)
        {
            wall.GoalDetected += ScoreAPoint;
        }

        _gameStateController = _projectStarter.GetGameController();

        _gameStateController.GameStateChanged += ResetTheScore;
    }

    private void ScoreAPoint(GameObject line)
    {
        if (line.GetComponent<GoalDetector>().GetScorer() == Players.RightPlayer)
        {
            RightPlayerScore++;
            _scoreRightPlayerText.text = RightPlayerScore.ToString();
            CheckForVictory(RightPlayerScore, Players.RightPlayer);
        }
        else if (line.GetComponent<GoalDetector>().GetScorer() == Players.LeftPlayer)
        {
            LeftPlayerScore++;
            _scoreLeftPlayerText.text = LeftPlayerScore.ToString();
            CheckForVictory(LeftPlayerScore, Players.LeftPlayer);
        }
    }

    private void CheckForVictory(int score, Players player)
    {
        if (score == _winningScore)
        {
            Winner = player;
            _gameStateController.GameState = GameState.GameOver;
        }
    }

    private void ResetTheScore()
    {
        if (_gameStateController.GameState == GameState.ChoiceOfNumberOfPlayers)
        {
            LeftPlayerScore = 0;
            RightPlayerScore = 0;
            _scoreLeftPlayerText.text = LeftPlayerScore.ToString();
            _scoreRightPlayerText.text = RightPlayerScore.ToString();
        }
    }

    private void OnDestroy()
    {
        foreach (var wall in _goalDetectors)
        {
            wall.GoalDetected -= ScoreAPoint;
        }

        _gameStateController.GameStateChanged -= ResetTheScore;
    }
}