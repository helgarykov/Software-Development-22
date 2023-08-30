using Breakout.GameStates;
using DIKUArcade.GUI;
using NUnit.Framework;

namespace breakouttests.StateTests;

public class TestGameStates {
    
    [SetUp]
    public void Setup() {
        //Window.CreateOpenGLContext();
        //_stateMachine = new StateMachine(StateAlignments.Transitions);
    }
    StateMachine _stateMachine = default!;
    
    //Assuming the StateMachine can run without exceptions,
    // it should switch between states.
    [Test]
    public void FromMainMenuToGameRunningChangesCorrectly() {
        
    var actualState = _stateMachine.ActiveState;
    
    _stateMachine.ChangeStates(GameStateType.GameRunning);
    
    var resultState = _stateMachine.ActiveState;
    
    Assert.AreNotEqual(actualState, resultState);
    }
    
    [Test]
    public void FromGameRunningToGamePausedChangesCorrectly()
    {
        var actualState = _stateMachine.ActiveState;
        
        _stateMachine.ChangeStates(GameStateType.GameRunning);
        _stateMachine.ChangeStates(GameStateType.GamePaused);

        var resultState = _stateMachine.ActiveState;
        
        Assert.AreNotEqual(actualState, resultState);
    }
    
    [Test]
    public void FromGamePausedToGameRunningChangesCorrectly()
    {
        
        _stateMachine.ChangeStates(GameStateType.GamePaused);
        
        var actualState = _stateMachine.ActiveState;
        
        _stateMachine.ChangeStates(GameStateType.GameRunning);

        var resultState = _stateMachine.ActiveState;
        
        Assert.AreNotEqual(actualState, resultState);
    }
    
    [Test]
    public void FromGameOverToGamePausedFails()
    {
        _stateMachine.ChangeStates(GameStateType.GamePaused);
        
        var startState = _stateMachine.ActiveState;
        
        _stateMachine.ChangeStates(GameStateType.GameRunning);
        _stateMachine.ChangeStates(GameStateType.GameOver);
        _stateMachine.ChangeStates(GameStateType.GamePaused);

        var resultState = _stateMachine.ActiveState;

        Assert.IsFalse(startState == resultState );
    }

    [Test]
    public void TestMomsBeregner()
    {
        var calculator = new MomsBeregner();
        decimal priceExVat = 20.00m;

        Assert.AreEqual(5.00, calculator.GetVat(priceExVat));
        Assert.AreEqual(25.00, calculator.GetPriceWithVat(priceExVat));
    }
}

public class MomsBeregner
{
    public decimal GetVat(decimal priceExVat)
    {
        return priceExVat * 0.25m;
    }

    public decimal GetPriceWithVat(decimal priceExVat)
    {
        return priceExVat + GetVat(priceExVat);
    }
}



