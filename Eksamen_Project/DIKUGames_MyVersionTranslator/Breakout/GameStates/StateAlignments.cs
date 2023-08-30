namespace Breakout.GameStates;

/// <summary>
/// A default container for the dictionary of corresponding GameStateTypes (keys) and GameStates (the actual state)
/// for the use by StateMachine. 
/// </summary>
public static class StateAlignments {
    public static Dictionary<GameStateType, IBreakoutGameState> Transitions { get; } 
        = new()
        {
            {GameStateType.MainMenu, new MainMenu()},
            {GameStateType.GameRunning, new GameRunning()},
            {GameStateType.GamePaused, new GamePaused()},
            {GameStateType.GameOver, new GameOver()}
        };

}