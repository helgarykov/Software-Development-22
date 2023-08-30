namespace TicTacToe;

using System;
using System.Linq;
using System.Collections.Generic;
using TicTacToe.Interfaces;


/// <summary>
/// Class used to run a game of TicTacToe.
/// </summary>
public class Game {
    
    private IEnumerable<IPlayer> players;
    private Board board;
    private int size;
    private IBoardDrawer drawer;
    private IBoardChecker boardChecker;


    /// <summary>
    /// The constructor needs to know about the board size, which players are in the game, how the
    /// game will be drawn and how to check for what the state of the board is.
    /// </summary>
    /// <param name="size">The board is a square, so the size is a side length of a square.</param>
    /// <param name="players">An enumerator of players.</param>
    /// <param name="drawer">An object that can be used to draw the game.</param>
    /// <param name="boardChecker">
    /// An object that can be used to determine the state of the game.
    /// </param>
    public Game(int size,
                IEnumerable<IPlayer> players,
                IBoardDrawer drawer,
                IBoardChecker boardChecker) {

        if (players.Count() == 0) {
            throw new ArgumentException("The amount of players passed must be more than zero.");
        }

        this.size = size;
        this.players = players;
        this.drawer = drawer;
        this.boardChecker = boardChecker;
        
        board = new Board(size);
    }

    /// <summary>
    /// Method that moves the first player to the last place in the enumerator.
    /// </summary>
    private void NextPlayer() {
        var current = players!.First();
        players = players.Skip(1).Append(current);
    }

    /// <summary>
    /// Method that determines if the game is over using the board checker. If the game is in a win 
    /// state or tied state then draw the appropriate graphics.
    /// </summary>
    /// <returns>
    /// True if the game is over else false.
    /// </returns>
    private bool IsGameDone() {

        switch (boardChecker.CheckBoardState(board)) {
            case BoardState.Winner:
                drawer.DrawWin(players!.Last().Name);
                return true;
            case BoardState.Tied:
                drawer.DrawTied();
                return true;
            case BoardState.Inconclusive:
                return false;
        }
        throw new Exception("An unaccounted for BoardState was returned from the BoardChecker.");
    }

    /// <summary>
    /// Method that will run the game. It will get an input from the player in form of a Position 
    /// and if that position can be used to insert the players identifier on the board then its the
    /// next players turn. Else it wil keep trying to get a position from the current player.
    /// </summary>
    public void Run() {
        drawer.Draw(board);

        while (!IsGameDone()) {
            var current = players!.First();
            var move = current.Move();

            if (!board.TryInsert(move.X, move.Y, current.Identifier)) {
                continue;
            }

            drawer.Draw(board);
            NextPlayer();
        }
    }
}