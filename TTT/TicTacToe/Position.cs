namespace TicTacToe;


/// <summary>
/// A struct used to model a position of XY-coordinates.
/// </summary>
public struct Position {

    /// <summary>
    /// A property used to get/set the X value.
    /// </summary>
    public int X { get; set; }

    /// <summary>
    /// A property used to get/set the Y value.
    /// </summary>
    public int Y { get; set; }

    /// <summary>
    /// The constructor for a position.
    /// </summary>
    /// <param name="x">The X value.</param>
    /// <param name="y">The Y value.</param>
    public Position(int x, int y) {
        X = x;
        Y = y;
    }

    /// <summary>
    /// Method used to convert a position to a string. It is mainly used for printing.
    /// </summary>
    /// <returns>
    /// A string that could for example be "(1, 2)".
    /// </returns>
    public override string ToString() => $"({X}, {Y})";
}
