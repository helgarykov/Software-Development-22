using System;

namespace TicTacToeTest;

using NUnit.Framework;
using TicTacToe;

public class BoardCheckerTest
{
    public Board board;
    public BoardChecker boardChecker;

    [SetUp]
    public void Setup()
    {
        board = new Board(3);
        boardChecker = new BoardChecker();
    }

    [Test]
    public void RightDiagonalWinTestIsTrueWhenThreeNaughtsOnOneDiagonal()
    {
        board.TryInsert(0, 0, PlayerIdentifier.Naught);
        board.TryInsert(1, 1, PlayerIdentifier.Naught);
        board.TryInsert(2, 2, PlayerIdentifier.Naught);

        bool result = boardChecker.IsDiagWin(board);

        Assert.True(result);
    }
    
    [Test]
    public void LeftDiagonalWinTestIsFalseWhenTwoCrossOnOneDiagonal()
    {
        board.TryInsert(0, 2, PlayerIdentifier.Cross);
        board.TryInsert(1, 1, PlayerIdentifier.Naught);
        board.TryInsert(2, 0, PlayerIdentifier.Cross);

        bool result = boardChecker.IsDiagWin(board);

        Assert.False(result);
    } 

    [Test]
    public void LeftDiagonalWinTestIsTrueWhenThreeCrossOnOneDiagonal()
    {
        board.TryInsert(0, 2, PlayerIdentifier.Cross);
        board.TryInsert(1, 1, PlayerIdentifier.Cross);
        board.TryInsert(2, 0, PlayerIdentifier.Cross);

        bool result = boardChecker.IsDiagWin(board);

        Assert.True(result);
    }

    [Test]
    public void RowWinTestIsTrueWhenThreeNaughtsOnOneRow()
    {
        board.TryInsert(1, 0, PlayerIdentifier.Naught);
        board.TryInsert(1, 1, PlayerIdentifier.Naught);
        board.TryInsert(1, 2, PlayerIdentifier.Naught);

        bool result = boardChecker.IsRowWin(board);

        Assert.True(result);
    }

    [Test]
    public void RowWinIsFalseWhenOnlyTwoNaughtsOnSameRow()
    {
        board.TryInsert(1, 0, PlayerIdentifier.Naught);
        board.TryInsert(1, 2, PlayerIdentifier.Naught);

        bool result = boardChecker.IsRowWin(board);

        Assert.False(result);
    }

    [Test]
    public void RowWinIsFalseWhenThreeNaughtsNotOnSameRow()
    {
        board.TryInsert(1, 0, PlayerIdentifier.Naught);
        board.TryInsert(2, 1, PlayerIdentifier.Naught);
        board.TryInsert(1, 2, PlayerIdentifier.Naught);

        bool result = boardChecker.IsRowWin(board);

        Assert.False(result);
    }
    [Test]
    public void RowWinIsFalseWhenNotAllOfSameKind()
    {
        board.TryInsert(1, 0, PlayerIdentifier.Naught);
        board.TryInsert(2, 1, PlayerIdentifier.Naught);
        board.TryInsert(1, 2, PlayerIdentifier.Cross);

        bool result = boardChecker.IsRowWin(board);
        
        Assert.False(result);
    }

    [Test]
    public void ColumnWinTestIsTrueWhenAllOfSameKind()
    {
        board.TryInsert(0, 0, PlayerIdentifier.Cross);
        board.TryInsert(1, 0, PlayerIdentifier.Cross);
        board.TryInsert(2, 0, PlayerIdentifier.Cross);

        bool result = boardChecker.IsColWin(board);
        
        Assert.True(result);
    }
    
    [Test]
    public void ColumnWinTestIsFalseWhenOnlyTwoNaughtsOnSameColumn()
    {
        board.TryInsert(0, 2, PlayerIdentifier.Naught);
        board.TryInsert(2, 2, PlayerIdentifier.Naught);

        bool result = boardChecker.IsColWin(board);

        Assert.False(result);
    }
    
    [Test]
    public void ColumnWinIsFalseWhenThreeNaughtsNotOnSameColumn()
    {
        board.TryInsert(0, 0, PlayerIdentifier.Naught);
        board.TryInsert(1, 1, PlayerIdentifier.Naught);
        board.TryInsert(2, 0, PlayerIdentifier.Naught);

        bool result = boardChecker.IsColWin(board);

        Assert.False(result);
    }
    
    [Test]
    public void ColumnWinIsFalseWhenNotAllOfSameKind()
    {
        board.TryInsert(0, 1, PlayerIdentifier.Naught);
        board.TryInsert(1, 1, PlayerIdentifier.Naught);
        board.TryInsert(1, 1, PlayerIdentifier.Cross);

        bool result = boardChecker.IsColWin(board);
        
        Assert.False(result);
    }

    [Test]
    public void InconclusiveTest()
    {
        board.TryInsert(0, 0, PlayerIdentifier.Cross);
        board.TryInsert(0, 2, PlayerIdentifier.Cross);
        board.TryInsert(1, 0, PlayerIdentifier.Naught);
        board.TryInsert(1, 1, PlayerIdentifier.Naught);
        board.TryInsert(1, 2, PlayerIdentifier.Cross);
        board.TryInsert(2, 0, PlayerIdentifier.Naught);
        board.TryInsert(2, 1, PlayerIdentifier.Cross);
        board.TryInsert(2, 2, PlayerIdentifier.Naught);

        BoardState result = boardChecker.CheckBoardState(board);

        Assert.AreEqual(result, BoardState.Inconclusive);
    }

    [Test]
    public void TiedWhenBoardIsFilledButNoWin()
    {
        board.TryInsert(0, 0, PlayerIdentifier.Cross);
        board.TryInsert(0, 1, PlayerIdentifier.Naught);
        board.TryInsert(0, 2, PlayerIdentifier.Cross);
        board.TryInsert(1, 0, PlayerIdentifier.Naught);
        board.TryInsert(1, 1, PlayerIdentifier.Naught);
        board.TryInsert(1, 2, PlayerIdentifier.Cross);
        board.TryInsert(2, 0, PlayerIdentifier.Naught);
        board.TryInsert(2, 1, PlayerIdentifier.Cross);
        board.TryInsert(2, 2, PlayerIdentifier.Naught);

        BoardState result = boardChecker.CheckBoardState(board);

        Assert.AreEqual(result, BoardState.Tied);

    }

    [Test]
    public void NotTiedWhenBoardIsFilledWithWinLeftDiag()
    {
        board.TryInsert(0, 0, PlayerIdentifier.Cross);
        board.TryInsert(0, 1, PlayerIdentifier.Naught);
        board.TryInsert(0, 2, PlayerIdentifier.Naught);
        board.TryInsert(1, 0, PlayerIdentifier.Naught);
        board.TryInsert(1, 1, PlayerIdentifier.Naught);
        board.TryInsert(1, 2, PlayerIdentifier.Cross);
        board.TryInsert(2, 0, PlayerIdentifier.Naught);
        board.TryInsert(2, 1, PlayerIdentifier.Cross);
        board.TryInsert(2, 2, PlayerIdentifier.Naught);

        BoardState result = boardChecker.CheckBoardState(board);

        Assert.AreNotEqual(result, BoardState.Tied);

    }
    
    [Test]
    public void NotTiedWhenBoardIsFilledWithWinRightDiag()
    {
        board.TryInsert(0, 0, PlayerIdentifier.Naught);
        board.TryInsert(0, 1, PlayerIdentifier.Naught);
        board.TryInsert(0, 2, PlayerIdentifier.Cross);
        board.TryInsert(1, 0, PlayerIdentifier.Naught);
        board.TryInsert(1, 1, PlayerIdentifier.Naught);
        board.TryInsert(1, 2, PlayerIdentifier.Cross);
        board.TryInsert(2, 0, PlayerIdentifier.Cross);
        board.TryInsert(2, 1, PlayerIdentifier.Cross);
        board.TryInsert(2, 2, PlayerIdentifier.Naught);

        BoardState result = boardChecker.CheckBoardState(board);

        Assert.AreNotEqual(result, BoardState.Tied);

    }
}
   

