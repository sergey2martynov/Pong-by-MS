using UnityEngine;

public class OpponentController : MonoBehaviour
{
    [SerializeField] private ProjectStarter _projectStarter;

    [SerializeField] private SwitchingControlOfTheRightRacket _switchingControl;

    [SerializeField] private Transform _ballPosition;

    [SerializeField] private float _speed = 30f;

    [SerializeField] private Players _player;

    private GameStateController _gameStateController;

    private Vector2 _vectorAimedAtTheBall;

    private Vector2 _startingPositionOfTheRightPlayer;

    private Vector2 _maximumUpPositionRacket;

    private Vector2 _maximumDownPositionRacket;

    private void Start()
    {
        _gameStateController = _projectStarter.GetGameController();

        _gameStateController.GameStateChanged += RreturnTheRacketToTheCenter;

        _startingPositionOfTheRightPlayer = new Vector2(PlayingFieldParameters.RightRacketXPosition, 0);

        _maximumUpPositionRacket =
            new Vector2(PlayingFieldParameters.RightRacketXPosition, PlayingFieldParameters.UpperBound);

        _maximumDownPositionRacket =
            new Vector2(PlayingFieldParameters.RightRacketXPosition, PlayingFieldParameters.BottomBound);
    }

    private void OnDestroy()
    {
        _gameStateController.GameStateChanged -= RreturnTheRacketToTheCenter;
    }

    private void FixedUpdate()
    {
        if (_switchingControl.NumberOfPlayers == NumberOfPlayers.OnePlayer)
        {
            if (_gameStateController.GameState == GameState.Game ||
                _gameStateController.GameState == GameState.BallOutOfGame)
            {
                if (transform.position.y <= PlayingFieldParameters.UpperBound &&
                    transform.position.y >= PlayingFieldParameters.BottomBound)
                {
                    _vectorAimedAtTheBall = new Vector2(50.7f, _ballPosition.position.y);
                    transform.position =
                        Vector3.MoveTowards(transform.position, _vectorAimedAtTheBall, Time.deltaTime * _speed);
                }
            }

            if (transform.position.y > PlayingFieldParameters.UpperBound)
            {
                transform.position = _maximumUpPositionRacket;
            }

            if (transform.position.y < PlayingFieldParameters.BottomBound)
            {
                transform.position = _maximumDownPositionRacket;
            }
        }
    }

    public Players GetPlayer()
    {
        return _player;
    }

    private void RreturnTheRacketToTheCenter()
    {
        if (_gameStateController.GameState == GameState.GameOver)
        {
            transform.position = _startingPositionOfTheRightPlayer;
        }
    }
}