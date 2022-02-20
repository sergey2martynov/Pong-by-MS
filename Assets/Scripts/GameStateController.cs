using System;

public class GameStateController
{
    public event Action GameStateChanged;

    private GameStates _gameStates = GameStates.ChoiceOfNumberOfPlayers;

    public GameStates StagePrevious { get; private set; }

    public GameStates GameState
    {
        get => _gameStates;
        set
        {
            StagePrevious = _gameStates;

            _gameStates = value;

            GameStateChanged?.Invoke();
        }
    }
}