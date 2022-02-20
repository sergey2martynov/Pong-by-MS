using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private ProjectStarter _projectStarter;

    [SerializeField] private Rigidbody2D _rigidbody2D;

    [SerializeField] private float _ballSpeed = 80f;

    [SerializeField] private float _leftBorder = -85f;

    [SerializeField] private float _rightBorder = 85f;

    private GameStateController _gameStateController;

    private float _balReturnTime;

    private float _startTime;

    private const float StationaryBallTime = 2f;
    
    private Vector2 _fixedVelocity;

    private void Start()
    {
        _gameStateController = _projectStarter.GetGameController();

        _gameStateController.GameStateChanged += MarkTheTime;

        _gameStateController.GameStateChanged += RreturnTheBallToTheCenter;

        _gameStateController.GameStateChanged += FixPositionAndSpeed;

        _gameStateController.GameStateChanged += StartMovingAgain;
    }

    private void FixedUpdate()
    {
        if (_gameStateController.GameState == GameStates.Pause)
        {
            if (_gameStateController.StagePrevious == GameStates.Start)
            {
                _startTime += Time.deltaTime;
            }

            if (_gameStateController.StagePrevious == GameStates.BallOutOfGame)
            {
                _balReturnTime += Time.deltaTime;
            }
        }
        else
        {
            if (_gameStateController.GameState == GameStates.Start && (Time.time - _startTime >= StationaryBallTime))
            {
                PushTheBall();
                _gameStateController.GameState = GameStates.Game;
            }

            if (transform.position.x < _leftBorder || transform.position.x > _rightBorder)
            {
                transform.position = new Vector2(0, 0);
                _rigidbody2D.velocity = Vector2.zero;
                _balReturnTime = Time.time;

                if (_gameStateController.GameState != GameStates.GameOver)
                {
                    _gameStateController.GameState = GameStates.BallOutOfGame;
                }
            }

            if (Time.time - _balReturnTime >= StationaryBallTime &&
                _gameStateController.GameState == GameStates.BallOutOfGame)
            {
                _balReturnTime = 0;
                PushTheBall();
                _gameStateController.GameState = GameStates.Game;
            }
        }
    }

    private void MarkTheTime()
    {
        _startTime = Time.time;
    }

    private void PushTheBall()
    {
        _rigidbody2D.velocity = Vector2.right * _ballSpeed;
    }

    private void RreturnTheBallToTheCenter()
    {
        if (_gameStateController.GameState == GameStates.ChoiceOfNumberOfPlayers)
        {
            transform.position = new Vector2(0, 0);
            _rigidbody2D.velocity = Vector2.zero;
        }
    }

    private void FixPositionAndSpeed()
    {
        if (_gameStateController.GameState == GameStates.Pause && _gameStateController.StagePrevious == GameStates.Game)
        {
            _fixedVelocity = _rigidbody2D.velocity;
            _rigidbody2D.velocity = Vector2.zero;
        }
    }

    private void StartMovingAgain()
    {
        if (_gameStateController.GameState == GameStates.Game && _gameStateController.StagePrevious == GameStates.Pause)
        {
            _rigidbody2D.velocity = _fixedVelocity;
        }
    }

    private void OnDestroy()
    {
        _gameStateController.GameStateChanged -= MarkTheTime;
        _gameStateController.GameStateChanged -= RreturnTheBallToTheCenter;
        _gameStateController.GameStateChanged -= FixPositionAndSpeed;
        _gameStateController.GameStateChanged -= StartMovingAgain;
    }
}