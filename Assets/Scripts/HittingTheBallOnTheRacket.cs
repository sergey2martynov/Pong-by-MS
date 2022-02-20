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

    private void BounceBall(Collision2D racket)
    {
        if (racket.gameObject.name == "LeftPlayer")
        {
            CalculateDirection(racket, 1);
        }

        if (racket.gameObject.name == "RightPlayer")
        {
            CalculateDirection(racket, -1);
        }
    }

    private void CalculateDirection(Collision2D racket, int vectorX)
    {
        var calculatedY = HitFactor(transform.position, racket.transform.position, racket.collider.bounds.size.y);
        var calculateDirection = new Vector2(vectorX, calculatedY).normalized;
        _rigidbody2D.velocity = calculateDirection * _ballSpeed;
    }

    private float HitFactor(Vector2 ballPosition, Vector2 playerPosition, float playerHeight)
    {
        return (ballPosition.y - playerPosition.y) / playerHeight;
    }

    private void OnDestroy()
    {
        _ballCollision.BallCollidedWithRacket -= BounceBall;
    }
}