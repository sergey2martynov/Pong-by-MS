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

        _gameStateController.GameStateChanged += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        _gameStateController.GameStateChanged -= OnGameStateChanged;
    }

    private void FixedUpdate()
    {
        if (_gameStateController.GameState == GameState.Pause)
        {
            if (_gameStateController.PreviousStage == GameState.Start)
            {
                _startTime += Time.deltaTime;
            }

            if (_gameStateController.PreviousStage == GameState.BallOutOfGame)
            {
                _balReturnTime += Time.deltaTime;
            }
        }
        else
        {
            if (_gameStateController.GameState == GameState.Start && (Time.time - _startTime >= StationaryBallTime))
            {
                PushTheBall();
                _gameStateController.GameState = GameState.Game;
            }

            if (transform.position.x < _leftBorder || transform.position.x > _rightBorder)
            {
                transform.position = new Vector2(0, 0);
                _rigidbody2D.velocity = Vector2.zero;
                _balReturnTime = Time.time;

                if (_gameStateController.GameState != GameState.GameOver)
                {
                    _gameStateController.GameState = GameState.BallOutOfGame;
                }
            }

            if (Time.time - _balReturnTime >= StationaryBallTime &&
                _gameStateController.GameState == GameState.BallOutOfGame)
            {
                _balReturnTime = 0;
                PushTheBall();
                _gameStateController.GameState = GameState.Game;
            }
        }
    }

    private void OnGameStateChanged()
    {
        MarkTheTime();

        ReturnTheBallToTheCenter();

        FixPositionAndSpeed();

        StartMovingAgain();
    }

    private void MarkTheTime()
    {
        _startTime = Time.time;
    }

    private void PushTheBall()
    {
        _rigidbody2D.velocity = Vector2.right * _ballSpeed;
    }

    private void ReturnTheBallToTheCenter()
    {
        if (_gameStateController.GameState == GameState.ChoiceOfNumberOfPlayers)
        {
            transform.position = new Vector2(0, 0);
            _rigidbody2D.velocity = Vector2.zero;
        }
    }

    private void FixPositionAndSpeed()
    {
        if (_gameStateController.GameState == GameState.Pause && _gameStateController.PreviousStage == GameState.Game)
        {
            _fixedVelocity = _rigidbody2D.velocity;
            _rigidbody2D.velocity = Vector2.zero;
        }
    }

    private void StartMovingAgain()
    {
        if (_gameStateController.GameState == GameState.Game && _gameStateController.PreviousStage == GameState.Pause)
        {
            _rigidbody2D.velocity = _fixedVelocity;
        }
    }
}