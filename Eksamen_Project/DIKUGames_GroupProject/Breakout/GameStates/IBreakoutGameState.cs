using DIKUArcade.State;

namespace Breakout.GameStates;

public interface IBreakoutGameState : IGameState {
   
    /// <summary>
    /// The current state which StateMachine should change into.
    /// </summary>
    GameStateType ActiveState { get; set; }
    
    SelectedAction SelectedAction { get; set; }
    
    /// <summary>
    /// The function that updates the activeState in StateMachine and changes it if
    /// any of the game states wish to switch to another state.
    /// </summary>
    /// <returns> A gameState that StateMachine can use to change  </returns>
    GameStateType GetNextState();
    
    /// <summary>
    /// Called when the state is entered from another state. Gives the state
    /// a chance to do clean up tasks, reset, etc.
    /// </summary>
    void OnEnterState(IGameState previousState);
}