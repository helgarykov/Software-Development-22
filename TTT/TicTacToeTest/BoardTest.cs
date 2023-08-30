namespace TicTacToeTest;

using NUnit.Framework;
using TicTacToe;


public class BoardTest {

    Board board;

    [SetUp]
    public void Setup() {
        board = new Board(3);
    }

    [Test]
    public void EveryCellIsNullTest() {
        var everyCellIsNull = true;
        for (int i = 0; i < board.Size; i++) {
            for (int j = 0; j < board.Size; j++) {
                everyCellIsNull &= !board.IsTaken(i, j);
            }
        }
        
        Assert.True(everyCellIsNull);
    }

    [Test]
    public void CanBeFullTest() {
        for (int i = 0; i < board.Size; i++) {
            for (int j = 0; j < board.Size; j++) {
                Assert.True(board.TryInsert(i, j, PlayerIdentifier.Cross));
                if (i != 2 && j != 2) {
                    Assert.False(board.IsFull());
                }
            }
        }
        Assert.True(board.IsFull());
    }
}