namespace TicTacToe;

using System;

/// <summary>
/// An object that describes the board.
/// </summary>
/// <remarks>
/// This object should probably have been based on some abstraction like the Cursor and Player is.
/// </remarks>
public class Board {
    private Nullable<PlayerIdentifier>[,] board;
    private int max_placements;
    private int placement_count = 0;

    /// <summary>
    /// The side length of the square board.
    /// </summary>
    public int Size { get; }

    /// <summary>
    /// The constructor that is used to construct the board.
    /// </summary>
    /// <param name="size">The board is a square, so the size is a side length of a square.</param>
    public Board(int size) {
        board = new Nullable<PlayerIdentifier>[size, size];
        max_placements = size * size;
        Size = size;
    }

    /// <summary>
    /// Increment the number of cells that are taken on the board, this is used to check for ties.
    /// </summary>
    private void IncrementPlacementCount() => placement_count++;

    /// <summary>
    /// Method used to dertermine if there are more usable cells.
    /// </summary>
    /// <returns> True if all cells on the board are taken else false. </returns>
    public bool IsFull() => max_placements <= placement_count;

    /// <summary>
    /// Method used to determine if the cell is not null and therefore taken.
    /// </summary>
    /// <param name="i">First index.</param>
    /// <param name="j">Second index.</param>
    /// <returns> True if the cell is taken else false. </returns>
    public bool IsTaken(int i, int j) => board[i, j] is not null;

    /// <summary>
    /// Method used to retrieve the cells information at the given indexes.
    /// </summary>
    /// <param name="i">First index.</param>
    /// <param name="j">Second index.</param>
    /// <returns>
    /// Null for a not taken position else it will be some PlayerIdentifier that describes which
    /// player has taken the cell.
    /// </returns>
    public Nullable<PlayerIdentifier> Get(int i, int j) => board[i, j];

    /// <summary>
    /// Method that tries to insert a PlayerIdentifier at the given indexes.
    /// </summary>
    /// <param name="i">First index.</param>
    /// <param name="j">Second index.</param>
    /// <param name="identifier">
    /// The player identifier that might be inserted at the indexes.
    /// </param>
    /// <returns>
    /// If true then the identifier was inserted succesfully, if not then the identifier was not
    /// inserted since the cell was taken. 
    /// </returns>
    public bool TryInsert(int i, int j, PlayerIdentifier identifier) {
        if (IsTaken(i, j)) {
            return false;
        }

        board[i, j] = identifier;
        IncrementPlacementCount();
        return true;
    }

}