using UnityEngine;

public class OpponentController : MonoBehaviour
{
    [SerializeField] private ProjectStarter _projectStarter;

    [SerializeField] private Transform _ballPosition;

    [SerializeField] private float _speed = 30f;

    private GameStateController _gameStateController;

    private Vector2 _vectorAimedAtTheBall;

    private Vector2 _startingPositionOfTheRightPlayer;

    private const float UpperBound = 29.43501f;

    private const float BottomBound = -29.585f;

    private void Start()
    {
        _gameStateController = _projectStarter.GetGameController();

        _startingPositionOfTheRightPlayer = new Vector2(50.7f, 0);

        _gameStateController.GameStateChanged += RreturnTheRacketToTheCenter;
    }

    private void FixedUpdate()
    {
        if (_gameStateController.GameState == GameStates.Game ||
            _gameStateController.GameState == GameStates.BallOutOfGame)
        {
            if (transform.position.y <= UpperBound && transform.position.y >= BottomBound)
            {
                _vectorAimedAtTheBall = new Vector2(50.7f, _ballPosition.position.y);
                transform.position =
                    Vector3.MoveTowards(transform.position, _vectorAimedAtTheBall, Time.deltaTime * _speed);
            }
        }

        if (transform.position.y > UpperBound)
        {
            transform.position = new Vector2(50.70003f, UpperBound);
        }

        if (transform.position.y < BottomBound)
        {
            transform.position = new Vector2(50.70003f, BottomBound);
        }
    }

    private void RreturnTheRacketToTheCenter()
    {
        if (_gameStateController.GameState == GameStates.GameOver)
        {
            transform.position = _startingPositionOfTheRightPlayer;
        }
    }
}