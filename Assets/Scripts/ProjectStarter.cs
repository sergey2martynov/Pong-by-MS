using UnityEngine;

public class ProjectStarter : MonoBehaviour
{
    private GameStateController _gameStateController;

    private void Awake()
    {
        _gameStateController = new GameStateController();
    }

    public GameStateController GetGameController()
    {
        return _gameStateController;
    }
}