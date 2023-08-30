namespace TicTacToe.IO;

using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Interfaces;


/// <summary>
/// This object is used for drawing the TicTacToe board via the console.
/// </summary>
public class ConsoleBoardDrawer : IBoardDrawer {

    private Dictionary<PlayerIdentifier, char> map;

    /// <summary>
    /// To construct this object you need two enumerators, one with players and one with chars.
    /// The first player will map to the first char, the second player and will map to the 
    /// second char and so on. This makes it important that the two enumerators have the same 
    /// amount of elements. The symbols are the one hat will be printed.
    /// </summary>
    /// <param name="players">An enumerator of N players.</param>
    /// <param name="symbols">An enumerator of N chars.</param>
    public ConsoleBoardDrawer(IEnumerable<IPlayer> players,
                              IEnumerable<char> symbols) {

        if (players.Count() != symbols.Count()) {
            throw new ArgumentException("The two enumerables must have the same amount of items");
        }

        map = new Dictionary<PlayerIdentifier, char>();
        
        foreach (var pair in players.Zip(symbols)) {
            map.Add(pair.First.Identifier, pair.Second);
        } 
    }

    /// <summary>
    /// This method will clear the console and draw the board to console.
    /// </summary>
    /// <param name="board">Some board object.</param>
    public void Draw(Board board) {
        
        Console.Clear();
        Console.SetCursorPosition(0, 0);
        
        Console.WriteLine("+---+");
        for (var i = 0; i < board.Size; i++) {
            Console.Write("|");
            for (var j = 0; j < board.Size; j++) {
                Console.Write(board.Get(i, j).HasValue ? map[board.Get(i, j)!.Value] : ' ');
            }
            Console.WriteLine("|");
        }
        Console.WriteLine("+---+");
    }

    /// <summary>
    /// Method that will write the winner to the console.
    /// </summary>
    /// <param name="winner">The name of the winner.</param>
    public void DrawWin(string winner) {
        Console.SetCursorPosition(0, 11);
        Console.WriteLine($"{winner} Wins!");
    }

    /// <summary>
    /// Method that will write the game ended in a tie to the console.
    /// </summary>
    public void DrawTied() {
        Console.SetCursorPosition(0, 11);
        Console.WriteLine("The game was a tie!");
    }
}