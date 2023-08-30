namespace Breakout.GameStates;
using DIKUArcade.Events;
using DIKUArcade.Input;

/// <summary>
/// A class that is used for switching seamlessly between game states: MainMenu,
/// GameRunning, GamePaused and GameOver.
/// Runs independently of GameEventBus.
/// </summary>
public class StateMachine
{
    private IBreakoutGameState activeState;
    private Dictionary<GameStateType, IBreakoutGameState> transitions;

    public IBreakoutGameState ActiveState {
        get{return activeState;}
    }
        
    
    public StateMachine(Dictionary<GameStateType, IBreakoutGameState> transitions)
    {
        this.transitions = transitions;
        activeState = transitions[GameStateType.MainMenu]; // sets the active state type to a current state
    }

    /// <summary>
    /// Processes the GameStateType through the dictionary.
    /// </summary>
    /// <param name="key"> The key to run in dictionary to get the value, IBreakoutState.</param>
    /// <returns> The corresponding game state</returns>
    /// <exception cref="Exception"></exception>
    private IBreakoutGameState ProcessTransition(GameStateType key) {
        try
        {
            activeState.SelectedAction = SelectedAction.None;
            return transitions[key];
        } catch (ArgumentException) {
            throw new Exception("Illegal state transition.");
        }
    }
    
    /// <summary>
    /// Changes the current game state into the next game state.
    /// </summary>
    /// <param name="state"> the game state changed into </param>
    public void ChangeStates(GameStateType state)
    {
        var nextState = ProcessTransition(state);
        nextState.OnEnterState(activeState);
        activeState = nextState;
    }
    
    /// <summary>
    /// Checks whether the current state should chance, and conditionally
    /// starts a transition.
    /// </summary>
    private void CheckStateChange()
    {
        var stateCheck = activeState.GetNextState();

        if (activeState != transitions[stateCheck])
        {
            ChangeStates(stateCheck);
        }
    }

    /// <summary>
    ///Checks for gameState updates and updates the gameState itself.
    /// </summary>
    public void UpdateState()
    {  
        CheckStateChange();
        activeState.UpdateState();
    }

    public void RenderState()
    {
        activeState.RenderState();
    }
    
    /// <summary>
    /// Passes the KeyEvent to the activeState.
    /// </summary>
    public void PassKeyEvent(KeyboardAction action, KeyboardKey key) {
        activeState.HandleKeyEvent(action,key);
    }
}

    




    