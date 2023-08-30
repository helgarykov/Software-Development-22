using System.Collections.Generic;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using DIKUArcade;
using Breakout.LevelManagement;
using DIKUArcade.Math;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Physics;
using DIKUArcade.Timers;
using System.IO;
using Breakout.GameStates;

namespace Breakout {
    /// <summary>
    /// The game class serves akin to program.cs. It is a wrapper class, that instantiates different things.
    /// It enables the eventBus, and sets the keyHandlers.
    /// </summary>
    public class Game : DIKUGame, IGameEventProcessor {
        
        private StateMachine stateMachine;
        
        private GameEventBus gamebus;
        private PowerUpDistributor pop;
        public static float topY = 0.95f;
        public static float bottomY = 0f;
        public static float leftX = 0f;
        public static float rightX = 1f;
        public static string font = "OCR A Extended";
        public static Image ExtraLifeImage = new(Path.Combine("Assets", "Images", "LifePickUp.png"));
        public static Image HardBallImage = new(Path.Combine("Assets", "Images", "FireBallPickUp.png"));
        public static Image MoreTimeImage = new(Path.Combine("Assets", "Images", "BombPickUp.png"));
        public static Image RocketImage = new(Path.Combine("Assets", "Images", "RocketPickUp.png"));
        public static Image ExtraSpeedImage = new(Path.Combine("Assets", "Images", "HalfSpeedPowerUp.png"));

        

        /// <summary>
        /// A game constructor that instantiates a new game object with windowArgs.
        /// </summary>
        public Game(WindowArgs windowArgs) : base(windowArgs) {
            
            gamebus = BreakoutBus.GetBus();
            window.SetKeyEventHandler(KeyHandler);
            gamebus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent, GameEventType.PlayerEvent, GameEventType.WindowEvent,
                                                                    GameEventType.ControlEvent, GameEventType.GameStateEvent, GameEventType.StatusEvent });

            stateMachine = new StateMachine(StateAlignments.Transitions);
            pop = new PowerUpDistributor();
        }

        /// <summary>
        /// Method that makes sure that the game state
        /// which is active, is rendered to the screen.
        /// </summary>
        public override void Render() {
            stateMachine.RenderState();
        }
        /// <summary>
        /// Method that makes it possible to close the game window
        /// by pressing Esc.
        /// </summary>
        private void KeyHandler(KeyboardAction action, KeyboardKey key) {
            stateMachine.PassKeyEvent(action, key);
        }
        
        /// <summary>
        /// This function is called continously to update game logic. 
        /// Calls lower level functions through the statemachine
        /// </summary>
        public override void Update() {
            window.PollEvents();
            stateMachine.UpdateState();
            gamebus.ProcessEventsSequentially();
        }

        public void ProcessEvent(GameEvent gameEvent) {
        }
    }
}

