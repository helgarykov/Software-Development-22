namespace TicTacToe.Interfaces;


/// <summary>
/// An interface that describes the needed methods for showing a board object.
/// </summary>
public interface IBoardDrawer {

    /// <summary>
    /// Method that will be used to display the the board.
    /// </summary>
    /// <param name="board">Some board object.</param>
    void Draw(Board board);

    /// <summary>
    /// Method that is used to display the winner.
    /// </summary>
    /// <param name="winner">The name of the winner.</param>
    void DrawWin(string winner);

    /// <summary>
    /// Method that is used to display when the game is in a tied state.
    /// </summary>
    void DrawTied();

}