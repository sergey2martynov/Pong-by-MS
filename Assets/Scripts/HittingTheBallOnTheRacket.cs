using UnityEngine;

public class HittingTheBallOnTheRacket : MonoBehaviour
{
    [SerializeField] private BallCollisionWithRacketDetector _ballCollision;

    [SerializeField] private Rigidbody2D _rigidbody2D;

    [SerializeField] private float _ballSpeed;

    private void Start()
    {
        _ballCollision.BallCollidedWithRacket += BounceBall;
    }

    private void OnDestroy()
    {
        _ballCollision.BallCollidedWithRacket -= BounceBall;
    }

    private void BounceBall(Collision2D racket)
    {
        if (racket.gameObject.GetComponent<RacketController>() != null)
        {
            CalculateDirection(racket, racket.gameObject.GetComponent<RacketController>().GetPlayer());
            return;
        }
        if (racket.gameObject.GetComponent<OpponentController>() != null)
        {
            CalculateDirection(racket, racket.gameObject.GetComponent<OpponentController>().GetPlayer());
        }
    }

    private void CalculateDirection(Collision2D racket, Players player)
    {
        if (player == Players.LeftPlayer)
        {
            var calculatedY = HitFactor(transform.position, racket.transform.position, racket.collider.bounds.size.y);
            var calculateDirection = new Vector2(1, calculatedY).normalized;
            _rigidbody2D.velocity = calculateDirection * _ballSpeed;
        }
        else if (player == Players.RightPlayer)
        {
            var calculatedY = HitFactor(transform.position, racket.transform.position, racket.collider.bounds.size.y);
            var calculateDirection = new Vector2(-1, calculatedY).normalized;
            _rigidbody2D.velocity = calculateDirection * _ballSpeed;
        }
    }

    private float HitFactor(Vector2 ballPosition, Vector2 playerPosition, float playerHeight)
    {
        return (ballPosition.y - playerPosition.y) / playerHeight;
    }
}