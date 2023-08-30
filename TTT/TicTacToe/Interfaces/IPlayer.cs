namespace TicTacToe.Interfaces;


/// <summary>
/// An interface that describes the needed methods and properties a player has.
/// </summary>
public interface IPlayer {

    /// <summary>
    /// An enum used to identify the player.
    /// </summary>
    PlayerIdentifier Identifier { get; }

    /// <summary>
    /// The name of the player that will be used to display in game.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Method that will create some board position. 
    /// </summary>
    /// <returns>
    /// A position struct that describes the position of the player.
    /// </returns>
    Position Move();
}