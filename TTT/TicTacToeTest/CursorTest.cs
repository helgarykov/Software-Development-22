namespace TicTacToeTest;

using NUnit.Framework;
using TicTacToe;
using TicTacToe.IO;


public class CursorTest {
    private Cursor cursor;

    [SetUp]
    public void Setup() {
        var keyToMoveMap = new KeyToMoveMap('i', 'k', 'j', 'l', 'q', ' ');
        cursor = new Cursor(3, keyToMoveMap);
        cursor.MoveDown();
        cursor.MoveRight();
    }

    [Test] 
    public void CursorCenterTest()
    {

        Assert.True(cursor.position.X == 1 && cursor.position.Y == 1);
    }

    [Test]
    public void MoveUpTest()  
    {
        cursor.MoveUp();
        int posY = cursor.position.Y;
        System.Console.WriteLine(cursor.position.Y);
        cursor.MoveUp();
        Assert.True(condition:posY == cursor.position.Y , message: "The cursor didn't move up!");
    }

    [Test]
    public void MoveDownTest() {
        cursor.MoveDown();
        int posY = cursor.position.Y;
        cursor.MoveDown();
        Assert.True(condition:posY == cursor.position.Y, message: "The cursor didn't move down!");
    }
    [Test]
    public void MoveLeftTest() {
        cursor.MoveLeft();
        int posX = cursor.position.X;
        cursor.MoveLeft();
        Assert.True(condition:posX == cursor.position.X, message: "The cursor didn't move left!");
    }

    [Test]
    public void MoveRightTest() {
        cursor.MoveRight();
        int posX = cursor.position.X;
        cursor.MoveRight();
        Assert.True(condition:posX == cursor.position.X, message: "The cursor didn't move right!");
    }   
}
