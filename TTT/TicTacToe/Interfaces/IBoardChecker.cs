namespace TicTacToe.Interfaces;


/// <summary>
/// An interface that is used for checking the state of the board.
/// </summary>
public interface IBoardChecker {
    
    /// <summary>
    /// Method that is used to check what the state of the board is.
    /// </summary>
    /// <param name="board">Some board object.</param>
    /// <returns>The state of the board.</returns>
    BoardState CheckBoardState(Board board);
}