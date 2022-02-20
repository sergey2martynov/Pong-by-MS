using System;
using UnityEngine;

public class GoalDetector : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;

    public event Action<GameObject> BallFlewOverTheLine;

    private void OnTriggerEnter2D(Collider2D other)
    {
        BallFlewOverTheLine?.Invoke(_gameObject);
    }
}