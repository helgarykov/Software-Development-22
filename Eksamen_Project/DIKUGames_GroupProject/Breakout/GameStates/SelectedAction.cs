namespace Breakout.GameStates;

/// <summary>
/// An enum for selected action types
/// (transitions between states: "If SelectedAction is press/change to
/// MainMenu - return an object of MainMenu)) that correspond to specific gameState types.
/// </summary>
public enum SelectedAction
{
    None,
    MainMenu,
    PauseGame,
    StartGame,
    ExitGame,
    CloseGame
}
