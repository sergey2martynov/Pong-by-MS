using UnityEngine;

public class RacketController : MonoBehaviour
{
    [SerializeField] private ProjectStarter _projectStarter;

    [SerializeField] private SwitchingControlOfTheRightRacket _switchingControl;

    [SerializeField] private Rigidbody2D _rigidbody2D;

    [SerializeField] private string _axis;

    [SerializeField] private float _speed = 30f;

    [SerializeField] private Players _player;

    private GameStateController _gameStateController;

    private Vector2 _startingPositionOfTheLeftPlayer;

    private Vector2 _startingPositionOfTheRightPlayer;

    private void Start()
    {
        _startingPositionOfTheLeftPlayer = new Vector2(PlayingFieldParameters.LeftRacketXPosition, 0);

        _startingPositionOfTheRightPlayer = new Vector2(PlayingFieldParameters.RightRacketXPosition, 0);

        _gameStateController = _projectStarter.GetGameController();

        _gameStateController.GameStateChanged += ReturnTheRacketsToTheCenter;
    }

    private void OnDestroy()
    {
        _gameStateController.GameStateChanged -= ReturnTheRacketsToTheCenter;
    }

    private void FixedUpdate()
    {
        if (_player == Players.LeftPlayer || _player == Players.RightPlayer &&
            _switchingControl.NumberOfPlayers == NumberOfPlayers.TwoPlayers)
        {
            if (_gameStateController.IsAllowedToMove())
            {
                var input = Input.GetAxisRaw(_axis);
                _rigidbody2D.velocity = new Vector2(0, input) * _speed;
            }
            else
            {
                _rigidbody2D.velocity = Vector2.zero;
            }
        }
    }

    private void ReturnTheRacketsToTheCenter()
    {
        if (_gameStateController.GameState == GameState.GameOver)
        {
            if (_player == Players.LeftPlayer)
            {
                transform.position = _startingPositionOfTheLeftPlayer;
            }
            else if (_player == Players.RightPlayer)
            {
                transform.position = _startingPositionOfTheRightPlayer;
            }
        }
    }

    public Players GetPlayer()
    {
        return _player;
    }
}