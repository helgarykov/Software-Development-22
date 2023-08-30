namespace TicTacToe.Interfaces;


/// <summary>
/// An interface that is used to decribe an objects ability to give some input.
/// </summary>
public interface IPositionInput {
    
    /// <summary>
    /// A method that will create a given position.
    /// </summary>
    /// <returns>
    /// A position struct.
    /// </returns>
    Position ReceivePosition();
}