using System;
using UnityEngine;

public class BallCollisionWithRacketDetector : MonoBehaviour
{
    public event Action<Collision2D> BallCollidedWithRacket;

    private void OnCollisionEnter2D(Collision2D other)
    {
        BallCollidedWithRacket?.Invoke(other);
    }
}