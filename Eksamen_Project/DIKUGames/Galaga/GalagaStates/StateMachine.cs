using DIKUArcade.Events;
using DIKUArcade.State;


namespace Galaga.GalagaStates {
   
    /// <summary>
    /// A class that is used for switching between game states: MainMenu,
    /// GameRunning, GamePaused and GameOver.
    /// It implements ProcessEvent().
    /// </summary>
    public class StateMachine : IGameEventProcessor {
        public IGameState ActiveState {get; private set;}
        // A constructor that gets or creates a GameEventBus,
        // subscribes it to a StateEvent and InputEvent,
        // and assigns MainMenu state to ActiveState.
        // It also instantizes all of the currently implemented
        // gameStates.
        public StateMachine() {
            GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);
            ActiveState = MainMenu.GetInstance();
            GameRunning.GetInstance();
            GamePaused.GetInstance();
            GameOver.GetInstance();
        }
    
    ///<Summary> This function is capable of converting the input GameStateType to
    /// a given IGameType. This means that the game can properly select the running 
    /// gamestate instance, based on the input. The firstAccess modifier on MainMenu
    /// is necessary for the program to know whether the game has just been launched, 
    /// or a game session has just ended, and a new game is about to be started. </summary>
    ///<param name = "stateType"> stateType is the GameStateType, that the function should switch to </param>
    ///<returns> void <returns>
    public void SwitchState(GameStateType stateType)
    {
        if (stateType == GameStateType.GameRunning)
        {
            ActiveState = GameRunning.GetInstance();
            GameRunning.GetInstance().active = true;
        }
        else if (stateType == GameStateType.MainMenu)
        {
            ActiveState = MainMenu.GetInstance();
            MainMenu.GetInstance().firstAccess = false;
            MainMenu.GetInstance().active = true;
        }
        else if (stateType == GameStateType.GamePaused)
        {
            ActiveState = GamePaused.GetInstance();
            GamePaused.GetInstance().active = true;
        }
        else if (stateType == GameStateType.GameOver)
        {
            ActiveState = GameOver.GetInstance();
            GameOver.GetInstance().active = true;
        }
    }
    
        ///<Summary> This function reacts to Eventbus GameEvents of the GameStateEvent type.
        /// in addition to this, it only reacts to GameStateEvents related to changing states, as 
        /// denoted by the message "CHANGE_STATE". It utilizes the StateTransformer functions to convert
        /// from an attached StringArg to a GameStateType, and then it directly calls SwitchState
        /// with the result. </summary>
        ///<param name = "gameEvent"> gameEvent is a GameEvent </param>
        ///<returns> void <returns>
    public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.GameStateEvent) {
                if (gameEvent.Message == "CHANGE_STATE") {
                    SwitchState(StateTransformer.TransformStringToState(gameEvent.StringArg1));
                }  
            }
        }
    }
}