using System;
using UnityEngine;

public class GoalDetector : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;

    [SerializeField] private Players _scorer;

    public event Action<GameObject> GoalDetected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        GoalDetected?.Invoke(_gameObject);
    }

    public Players GetScorer()
    {
        return _scorer;
    }
}