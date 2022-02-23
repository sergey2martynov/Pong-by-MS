using System;

public class GameStateController
{
    public event Action GameStateChanged;

    private GameState _gameStates = GameState.ChoiceOfNumberOfPlayers;

    public GameState PreviousStage { get; private set; }

    public GameState GameState
    {
        get => _gameStates;
        set
        {
            PreviousStage = _gameStates;

            _gameStates = value;

            GameStateChanged?.Invoke();
        }
    }

    public bool IsAllowedToMove()
    {
        if (GameState == GameState.Game || GameState == GameState.Start ||
            GameState == GameState.BallOutOfGame)
        {
            return true;
        }

        return false;
    }
}