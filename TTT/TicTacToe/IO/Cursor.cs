using System.Drawing;
using System.Reflection.Metadata.Ecma335;

namespace TicTacToe.IO;

using System;
using TicTacToe.Interfaces;


/// <summary>
/// Object that uses the console cursor to retrieve a position input.
/// </summary>
public class Cursor : IPositionInput {
    
    int min, max;
    public Position position;
    private ICharToInputType charToInputType;

    /// <summary>
    /// A property that makes it possible to use a short hand for modifing the X position.
    /// </summary>
    private int X {
        get => position.X;
        set => position.X = value;
    }

    /// <summary>
    /// A property that makes it possible to use a short hand for modifing the Y position.
    /// </summary>
    private int Y {
        get => position.Y;
        set => position.Y = value;
    }

    /// <summary>
    /// The Cursor object needs to know the size of board and how it should convert keyboard
    /// keys to an InputType.
    /// </summary>
    /// <param name="size">The board is a square, so the size is a side length of a square.</param>
    /// <param name="charToInputType">
    /// An object that can convert the keyboard keys to InputType.
    /// </param>
    public Cursor(int size, ICharToInputType charToInputType) {
        this.charToInputType = charToInputType;
        
        // sets x and y to 0.
        X = Y = 0;
        min = 0;
        max = size - 1;
    }

    /// <summary>
    /// Moves the cursor one position up.
    /// </summary>
    public void MoveUp()
    {
        if (Y > min)
        {
            // The condition for the top wall.
            Y -= 1;
        }
    }

    /// <summary>
    /// Moves the cursor one position down.
    /// </summary>
    public void MoveDown()
    {
        // Condition for the bottom wall.
        if (Y < max)
        {
            Y += 1;
        }
    }

    /// <summary>
    /// Moves the cursor one position left.
    /// </summary>
    public void MoveLeft()
    {
        // The left-hand wall.
        if (X > min)
        {
            X -= 1;
        }
    }

    /// <summary>
    /// Moves the cursor one position right.
    /// </summary>
    public void MoveRight()
    {
        // The right hand wall.
        if (X < max)
        {
            X += 1;
        }
    }

    /// <summary>
    /// Quits the game.
    /// </summary>
    public void Quit() {
        Console.Clear();
        Environment.Exit(0);
    }

    /// <summary>
    /// Method that will perform some game action depending on the TypeInput.
    /// </summary>
    /// <param name="inputType">
    /// An InputType that defines the action the player will be performing.
    /// </param>
    /// <returns>
    /// False if the InputType was PerformMove else true.
    /// </returns>
    private bool MoveCursor(InputType inputType)
    {
        switch (inputType)
        {
            case InputType.Undefined:
                return true;
            case InputType.Down:
                MoveDown();
                return true;
            case InputType.Exit:
                Quit();
                return true;
            case InputType.Left:
                MoveLeft();
                return true;
            case InputType.Right:
                MoveRight();
                return true;
            case InputType.Up:
                MoveUp();
                return true;
            case InputType.PerformMove:
                return false;
        }
        return false;
    } 

    /// <summary>
    /// Method that will perform some game action depending on the TypeInput.
    /// </summary>
    /// <returns>
    /// The InputType enum that describes what action the player performed.
    /// </returns>
    public InputType GetMove() {
        return charToInputType.Convert(Console.ReadKey(true).KeyChar);
    }

    /// <summary>
    /// Method that will convert the internal position to a position in relation to the board.
    /// </summary>
    /// <param name="positionIn">
    /// Some position in relation to the cursor class.
    /// </param>
    /// <returns>
    /// The resulting position in relation to the board.
    /// </returns>
    private Position ToBoardPosition(Position positionIn) {
        return new Position(positionIn.Y, positionIn.X);
    }

    /// <summary>
    /// Sets the cursors console position.
    /// </summary>
    private void SetCursorPosition() => Console.SetCursorPosition(X + 1, Y + 1);

    /// <summary>
    /// Keeps retrieving an input until the player decides to perform that action.
    /// </summary>
    /// <returns>
    /// The players position when it has been decided.
    /// </returns>
    public Position ReceivePosition() {
        do {
            // Updates cursor position.
            SetCursorPosition();
            // Kør moveCurser på outputet fra GetMove (laver karakter om til en inputType)
            // Kør indtil MoveCursor returnerer false.
        } while (MoveCursor(GetMove()));

        return ToBoardPosition(position);
    }

}