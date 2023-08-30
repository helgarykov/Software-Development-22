using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Input;
using DIKUArcade.Events;
using System.IO;
using System;

namespace Galaga.GalagaStates {
    /// <summary>
    /// A class for a GamePaused game state.
    /// </summary>
    public class GamePaused : IGameState, IGameEventProcessor {
        private static GamePaused instance = null;
        private GameEventBus eventBus;
        private Entity pausedPlate;
        private Text[] menuButtons = {new Text(("Continue"), new Vec2F(0.35f, 0.25f), new Vec2F(0.3f, 0.3f)) , 
                                        new Text(("Main Menu"), new Vec2F(0.35f, 0.15f), new Vec2F(0.3f, 0.3f)) , 
                                            new Text(("PAUSED"), new Vec2F(0.32f, 0.2f), new Vec2F(0.5f, 0.5f))};
        private int activemenuButton;
        private int maxMenuButtons;
        public bool active {get; set;}
        
        /// <summary>
        /// A method to represent the GamePaused state and its graphical output:
        /// the background image and the menu buttons "Continue", "Main Menu", "Paused",
        /// that are colored light green.
        /// </summary>
        public void InitializeGameState () {
            eventBus = GalagaBus.GetBus();
            eventBus.Subscribe(GameEventType.GameStateEvent, this);
            pausedPlate = new Entity(new DynamicShape(0.25f, 0.25f, 0.5f, 0.5f), new Image(Path.Combine("Assets", "Images", "Paused.png")));
            activemenuButton = 0;
            maxMenuButtons = 2;
            menuButtons[1].SetColor(new Vec3F(1f, 1f, 1f));
            menuButtons[0].SetColor(new Vec3F(1f, 1f, 1f));
            menuButtons[2].SetColor(new Vec3F(1f, 1f, 1f));
        }
        /// <summary>
        /// A method to create an instance of a GamePaused state.
        /// </summary>
        /// <returns> A instance of GamePaused state.</returns>
        public static GamePaused GetInstance() {
            if (GamePaused.instance == null) {
                GamePaused.instance = new GamePaused();
                GamePaused.instance.InitializeGameState();
            }
            return GamePaused.instance;
        }
        /// <summary>
        /// A method that invokes GamePaused state.
        /// This is useful when e.g. pausing and then resuming the game,
        /// without changing the values of the private variables.
        /// </summary>
        public void ResetState() {
            GamePaused.instance.InitializeGameState();
        }
        /// <summary>
        /// The method is part of the IGameState interface it implements.
        /// Yet, it's irrelevant for the GamePaused state.
        /// </summary>
        public void UpdateState() {
            return;
        }
        /// <summary>
        /// This function is continously called through Game.cs, only if MainMenu is the current active 
        /// state. It renders the GameRunning state and two menu Buttons.
        /// The first if statement changes the color of the two buttons,
        /// so that the currently selected MainMenu is white, and the not selected one, Continue" is light-gray.
        /// </summary>
        public void RenderState() {
            GameRunning.GetInstance().RenderState();
            if (activemenuButton == 1) {
                menuButtons[0].SetColor(new Vec3F(0.5f, 0.5f, 0.5f));
                menuButtons[1].SetColor(new Vec3F(1f, 1f, 1f));
            } else {
                menuButtons[1].SetColor(new Vec3F(0.5f, 0.5f, 0.5f));
                menuButtons[0].SetColor(new Vec3F(1f, 1f, 1f));
            }
            // If GamePaused is the current state, change the background image to "GamePaused"
            // and show the thee buttons. 
            pausedPlate.RenderEntity();
            for (var i = 0; i < maxMenuButtons; i++) {
                menuButtons[i].RenderText();
            menuButtons[2].RenderText();
            }
        }
        
        /// <summary>
        /// This function is what is assigned as keyHandler through DIKUArcade functionalities.
        /// the inputted KeyboardAction typically represents either KeyPress, or KeyRelease. For this function,
        /// only keyPress is needed, so it is ensured that the entered action is a KeyPress, with an if statement.
        /// If the if-statement is successful, it calls its own function KeyPress, and sends along the pressed Key.
        /// </summary>
        /// <param name="action">A KeyboardAction enum.</param>
        /// <param name="key"> Enumeration representing the keyboard key.</param>
        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            if (action == KeyboardAction.KeyPress) {
                KeyPress(key);
            }
        }
        
        //TODO: Silas, her må du også meget gerne tilføje XML.
        public void KeyPress(KeyboardKey key) {
            if (active == true) {
                switch(key) {
                    case KeyboardKey.Up:
                            if (activemenuButton != 0) {
                                activemenuButton --;
                            }
                            break;
                    case KeyboardKey.Down:
                            if (activemenuButton != maxMenuButtons) {
                                activemenuButton ++;
                            }
                            break;
                    case KeyboardKey.Enter:
                            switch (activemenuButton) {
                                case 0:
                                    GalagaBus.GetBus().RegisterEvent(new GameEvent{ EventType = GameEventType.GameStateEvent, 
                                                                        Message = "CHANGE_STATE", StringArg1 = "GAME_RUNNING"});
                                    active = false;
                                    break;
                                case 1:
                                    GalagaBus.GetBus().RegisterEvent(new GameEvent{ EventType = GameEventType.GameStateEvent, 
                                                                        Message = "CHANGE_STATE", StringArg1 = "MAIN_MENU"});
                                    active = false;
                                    break;
                                default:
                                    break;
                            }
                            break;
                }
            }
        }
        /// <summary>
        /// As the function does not need to react to any GameEvents, this function can be left
        /// empty.
        /// </summary>
        public void ProcessEvent(GameEvent gameEvent) {
            return ;
        }  
    }
}