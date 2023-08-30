using System;

namespace Galaga.GalagaStates {
    
    /// <summary>
    /// A helper-class with two subsidiary methods
    /// that are used when converting from GameStateType to string, and the other way around.
    // This is useful, as strings are frequently used to pass messages through the EventBus. 
    /// </summary>
    public class StateTransformer  {
        
        ///<Summary> This function uses a switch statement to convert from a string to any of the 
        /// currently utilized gameStates. If a string thats not currently being used as a gameStateType,
        /// the function throws an exception. </summary>
        ///<param name = "state"> state is a string, representing a gameState </param>
        ///<returns> The corresponding GameStateType, to the entered string <returns>
        public static GameStateType TransformStringToState(string state)
        {
            return state switch
            {
                "MAIN_MENU" => GameStateType.MainMenu,
                "GAME_RUNNING" => GameStateType.GameRunning,
                "GAME_PAUSED" => GameStateType.GamePaused,
                "GAME_OVER" => GameStateType.GameOver,
                _ => throw new ArgumentException(String.Format("{0} is not a valid GameStateType", state))
            };
        }
        
        ///<Summary> This function uses a switch statement to convert any of the currently utilized GameStateTypes
        /// to a standardized string. <Summary>
        ///<param name = "state"> state is a GameStateType </param>
        ///<returns> The corresponding string, to the entered GameStateType <returns>
        public static string TransformStateToString(GameStateType state)
        {
            return state switch
            {
                GameStateType.MainMenu => "MAIN_MENU",
                GameStateType.GameRunning => "GAME_RUNNING",
                GameStateType.GamePaused => "GAME_PAUSED",
                GameStateType.GameOver => "GAME_OVER",
                _ => throw new ArgumentException(String.Format("{0} is not a GameStateType", state))
            };
        }
    }
}
