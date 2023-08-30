using System.Collections.Generic;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using DIKUArcade;
using Galaga.GalagaStates;


namespace Galaga {
    ///<Summary> The game class serves akin to Program.cs. It is kind of a wrapper class, that instantiates some different things.
    /// Most notably, it enables the eventBus, and sets the keyHandlers. It also handles all window commands. </summary>
    public class Game : DIKUGame, IGameEventProcessor {
        
        private GameEventBus eventBus;
        private StateMachine statemachine {get; set;}
        
        // A game constructor that instantiates a new game object with windowArgs.
        public Game(WindowArgs windowArgs) : base(windowArgs) {
            eventBus = GalagaBus.GetBus();
            // This initialization tells the created GameBus which gameventtypes that it should encompass. 
            eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent, GameEventType.PlayerEvent, GameEventType.WindowEvent,
                                                                    GameEventType.ControlEvent, GameEventType.GameStateEvent });
            statemachine = new StateMachine();
            eventBus.Subscribe(GameEventType.WindowEvent, this);
            // Each of the gameStates contains a KeyHandler function, thus, they all need to be assigned as keyHandlers
            // in the window, to process inputs.
            window.SetKeyEventHandler(GamePaused.GetInstance().HandleKeyEvent);
            window.SetKeyEventHandler(MainMenu.GetInstance().HandleKeyEvent);
            window.SetKeyEventHandler(GameRunning.GetInstance().HandleKeyEvent);
            window.SetKeyEventHandler(GameOver.GetInstance().HandleKeyEvent);
        }
        ///<Summary> This function reacts to a certain command sent through the Eventbus. When this command is sent, the window
        /// is closed, terminating the program. </summary>
        ///<param name = "gameEvent"> The function input is any GameEvent, however a later if statement specifies the type WindowEvent </param>
        ///<returns> void <returns>
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.WindowEvent) {
                switch (gameEvent.Message) {
                        case "CLOSE_WINDOW":
                            if (statemachine.ActiveState == MainMenu.GetInstance()) {
                                window.CloseWindow();
                            }
                            break;
                        default:
                            break;
                        }
                }
        }
        ///<Summary> This function is continously called through DIKUArcade functionalities. The result is a Render function,
        /// that can continously paste graphics to the screen. At this state, the function simply calls lower level Render functions, 
        /// based on which state is active. This is achieved through the class Statemachine.cs </summary>
        ///<returns> void <returns>
        public override void Render() {
            statemachine.ActiveState.RenderState();
        }
        ///<Summary> Similar to Render, this function is also called continously by DIKUArcade functions. However, this function
        /// is used to continously update game logic, wherever it is necessary. Like Render, this function also calls lower level
        /// functions through the statemachine.  </summary>
        ///<returns> void <returns>
        public override void Update() {
            window.PollEvents();
            eventBus.ProcessEventsSequentially();
            statemachine.ActiveState.UpdateState();
        }
    }
}

