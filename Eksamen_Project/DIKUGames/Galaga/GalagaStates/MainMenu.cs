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
    /// A class to manage the game state "Main Menu",
    /// that consists of two buttons: New Game & Quit.
    /// And a background image.
    /// </summary>
    public class MainMenu : IGameState, IGameEventProcessor {
        private static MainMenu instance = null;
        private Entity backGroundImage;
        private Text[] menuButtons = {new Text(("New Game"), new Vec2F(0.1f, 0.3f), new Vec2F(0.5f, 0.5f)) , 
                                    new Text(("Quit"), new Vec2F(0.1f, 0.1f), new Vec2F(0.5f, 0.5f))};
        private int activemenuButton;
        private int maxMenuButtons;
        private GameEventBus eventBus;
        public bool firstAccess {set; get;}
        public bool active {get; set;}
        
        ///<Summary> This function is similar to a contructor in its functionality, however, implementing
        /// it like this, allows for the function to be called again later. This is useful, as a way to
        /// "reset" the state, without deleting and creating a new instance. It instantizes the background,
        /// and it colors the text, as the default text color is black, which is the same as the background image.
        /// As the MainMenu is the first state to be entered, it also enables the "active" field in its contructor.
        /// This is used to determine which state should currently be allowed to handle key inputs. </summary>
        ///<returns> void <returns>
        public void InitializeGameState () {
            backGroundImage = new Entity(new DynamicShape(0f, 0f, 1f, 1f), new Image(Path.Combine("Assets", "Images", "TitleImage.png")));
            eventBus = GalagaBus.GetBus();
            activemenuButton = 0;
            maxMenuButtons = 2;
            menuButtons[1].SetColor(new Vec3F(1f, 1f, 1f));
            menuButtons[0].SetColor(new Vec3F(1f, 1f, 1f));
            firstAccess = true;
            active = true;
        }
        
        ///<Summary> Similar to the functionality seen in the galagabus, this function checks to see
        /// if a MainMenu instance already exists. If it does, it returns the instance, if it doesnt, it 
        /// creates an instance, and returns that instance. The difference between the syntax of Galagabus
        /// is, that this allows for multiple functions to be called as a new instance is created. This is 
        /// useful, as the InitilizeGameState function should be called immediately as a new instance is created,
        /// as it is structurally the same a constructor. </summary>
        ///<returns> The currently MainMenu instance <returns>
        public static MainMenu GetInstance() {
            if (MainMenu.instance == null) {
                MainMenu.instance = new MainMenu();
                MainMenu.instance.InitializeGameState();
            }
            return MainMenu.instance;
        }
        
        ///<Summary> This function allows for the gameState to be returned to its original state. It is not
        /// particularly useful in the mainMenu, as the mainMenu does not really contain any dynamic values
        /// or entities. </summary>
        ///<returns> void <returns>
        public void ResetState() {
            MainMenu.instance.InitializeGameState();
        }
        
        ///<Summary> As this state has no dynamic values, updateState is not used. However, it is part of the
        /// interface, and thus has to be here. </summary>
        ///<returns> void <returns>
        public void UpdateState() {
            return;
        }
        
        ///<Summary> This function is continously called through Game.cs, only if MainMenu is the current active 
        /// state. It renders the two menu Buttons, as well as the background Image behind it. The first if statement
        /// changes the color of the two buttons, so that the currently selected one is white, and the not selected
        /// one is light-gray.  </summary>
        ///<returns> void <returns>
        public void RenderState() {
            if (activemenuButton == 1) {
                menuButtons[0].SetColor(new Vec3F(0.5f, 0.5f, 0.5f));
                menuButtons[1].SetColor(new Vec3F(1f, 1f, 1f));
            } else {
                menuButtons[1].SetColor(new Vec3F(0.5f, 0.5f, 0.5f));
                menuButtons[0].SetColor(new Vec3F(1f, 1f, 1f));
            }
            backGroundImage.RenderEntity();
            for (var i = 0; i < maxMenuButtons; i++) {
                menuButtons[i].RenderText();
            }
        }
        ///<Summary> This function is what is assigned as keyHandler through DIKUArcade functionalities.
        /// the inputted KeyboardAction typically represents either KeyPress, or KeyRelease. For this function,
        /// only keyPress is needed, so it is ensured that the entered action is a KeyPress, with an if statement.
        /// If the if-statement is successful, it calls its own function KeyPress, and sends along the pressed Key. </summary>
        ///<param name = "action, key"> action is a KeyboardAction, and key is a KeyboardKey. </param>
        ///<returns> void <returns>
        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            if (action == KeyboardAction.KeyPress) {
                KeyPress(key);
            }
        }
        
        ///<Summary> This function is called by HandleKeyEvent whenever a key is pressed on the keyboard. Immediately,
        /// it checks to see if this state is the current active one. If its not, the function ends. If it continues
        /// the function enables functionality to the keys Up, Down, Enter and Escape. If the up button is pressed,
        /// the activemenuButton is changed, so that the selected button moves up 1 button. The same functionality
        /// is applied to the down button. They both use if statements to ensure that the activeButton stays within
        /// the allowed buttons. 
        /// If Enter is pressed, it checks which button is currently the active button. If it
        /// is the New Game button, it sends a message through the Eventbus, that is recieved by the Statemachine.
        /// This changes the current state to GameRunning, which starts the game. If it is pressed while Quit is selected
        /// a message is sent that closes the window via. the Game class. 
        /// This is also where the firstAccess field is utilized. If it is the first time that the state is changed to
        /// GameRunning, it is not reset. However, if it is the second case, then the GameRunning state is reset.
        /// A shortcut is also implemented, that allows for the Escape key to directly close the window. </summary>
        ///<param name = "key"> key is a KeyboardKey </param>
        ///<returns> void <returns>
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
                            GalagaBus.GetBus().RegisterEvent(new GameEvent{ 
                                EventType = GameEventType.GameStateEvent, 
                                Message = "CHANGE_STATE", StringArg1 = "GAME_RUNNING"});
                            if (firstAccess == false) {
                                GameRunning.GetInstance().ResetState();
                            active = false;
                            }
                            break;
                        case 1:
                            GalagaBus.GetBus().RegisterEvent(new GameEvent{ 
                                EventType = GameEventType.WindowEvent, 
                                Message = "CLOSE_WINDOW"});
                            break;
                        default:
                            break;
                        }
                        break;
                case KeyboardKey.Escape:
                    GalagaBus.GetBus().RegisterEvent(new GameEvent{ 
                        EventType = GameEventType.WindowEvent, 
                        Message = "CLOSE_WINDOW"});
                    break;
                }
            }    
        }
        ///<Summary> As the function does not need to react to any GameEvents, this function can be left
        /// empty.  </summary>
        ///<returns> void <returns>
        public void ProcessEvent(GameEvent gameEvent) {
            return;
        }
    }
}