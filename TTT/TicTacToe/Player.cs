namespace TicTacToe;

using TicTacToe.Interfaces;


/// <summary>
/// A basic player class.
/// </summary>
public class Player : IPlayer {

    /// <summary>
    /// An enum used to identify the player.
    /// </summary>
    public PlayerIdentifier Identifier { get; private set; }

    /// <summary>
    /// The name of the player that will be displayed in game.
    /// </summary>
    public string Name { get; }
    public IPositionInput positionInput;

    /// <summary>
    /// The constructor of the basic player class.
    /// </summary>
    /// <param name="name">The name the player uses.</param>
    /// <param name="identifier">
    /// The PlayerIdentifier the player uses to identify themselves.
    /// </param>
    ///<param name="positionInput">
    /// The class the player will use to retrieve a positions from.
    /// </param>
    public Player(string name, PlayerIdentifier identifier, IPositionInput positionInput) {
        this.positionInput = positionInput;
        
        Identifier = identifier;
        Name = name;
    }

    /// <summary>
    /// Method that receives the position on the board the player will place their identifier at.
    /// </summary>
    /// <returns>
    /// The position the player decided on.
    /// </returns>
    public Position Move() => positionInput.ReceivePosition();
}