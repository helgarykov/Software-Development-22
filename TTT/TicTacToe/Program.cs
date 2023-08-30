﻿namespace TicTacToe;

using System;
using TicTacToe.Interfaces;
using TicTacToe.IO;


/// <summary>
/// The class that contains the Main method of the program.
/// </summary>
class Program {

    /// <summary>
    /// Method that will Clear the console and close the program.
    /// </summary>
    private static void SafeClose(object sender, ConsoleCancelEventArgs args) {
        args.Cancel = true; // Don't end process.
        Console.Clear();
        Environment.Exit(0);
    }

    static void Main(string[] args) {

        // Add delegate so the method will be executed on the CancelKeyPress.
        Console.CancelKeyPress += new ConsoleCancelEventHandler(SafeClose!);

        int size = 3;

        // Initialize the input maps for each player.
        var keyToMoveMapX = new KeyToMoveMap('w', 's', 'a', 'd', 'q', ' ');
        var keyToMoveMapO = new KeyToMoveMap('i', 'k', 'j', 'l', 'q', ' ');
        
        // Initialize the Cursors for each player.
        var inputX = new Cursor(size, keyToMoveMapX);
        var inputO = new Cursor(size, keyToMoveMapO);

        // Initialize the Players.
        var cross = new Player("Cross", PlayerIdentifier.Cross, inputX);
        var naught = new Player("Naught", PlayerIdentifier.Naught, inputO);

        // The enumerators that will be used for the drawer and the Game object.
        var players = new IPlayer[] { cross, naught };
        var symbols = new char[] { 'x', 'o' };

        // Initialize the the BoardDrawer and the BoardChecker.
        var boardDrawer = new ConsoleBoardDrawer(players, symbols);
        var boardChecker = new BoardChecker();

        // Initialize the Game and run it.
        var game = new Game(size, players, boardDrawer, boardChecker);
        game.Run();
    }
}