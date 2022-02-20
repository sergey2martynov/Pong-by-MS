using System;
using UnityEngine;

public class RacketController : MonoBehaviour
{
    [SerializeField] private ProjectStarter _projectStarter;

    [SerializeField] private Rigidbody2D _rigidbody2D;

    [SerializeField] private string _axis;

    [SerializeField] private float _speed = 30f;

    private GameStateController _gameStateController;

    private Vector2 _startingPositionOfTheLeftPlayer;

    private Vector2 _startingPositionOfTheRightPlayer;

    private void Start()
    {
        _startingPositionOfTheLeftPlayer = new Vector2(-50.6f, 0);

        _startingPositionOfTheRightPlayer = new Vector2(50.7f, 0);

        _gameStateController = _projectStarter.GetGameController();

        _gameStateController.GameStateChanged += RreturnTheRacketsToTheCenter;
    }

    private void FixedUpdate()
    {
        if (_gameStateController.GameState == GameStates.Game || _gameStateController.GameState == GameStates.Start ||
            _gameStateController.GameState == GameStates.BallOutOfGame)
        {
            var input = Input.GetAxisRaw(_axis);
            _rigidbody2D.velocity = new Vector2(0, input) * _speed;
        }
        else
        {
            _rigidbody2D.velocity = Vector2.zero;
        }
    }

    private void RreturnTheRacketsToTheCenter()
    {
        if (_gameStateController.GameState == GameStates.GameOver)
        {
            if (_rigidbody2D.gameObject.name == "LeftPlayer")
            {
                transform.position = _startingPositionOfTheLeftPlayer;
            }
            else if (_rigidbody2D.gameObject.name == "RightPlayer")
            {
                transform.position = _startingPositionOfTheRightPlayer;
            }
        }
    }

    private void OnDestroy()
    {
        _gameStateController.GameStateChanged -= RreturnTheRacketsToTheCenter;
    }
}