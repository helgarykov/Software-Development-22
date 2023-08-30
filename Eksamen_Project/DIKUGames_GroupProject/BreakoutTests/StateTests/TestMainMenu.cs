using Breakout.GameStates;
using DIKUArcade.GUI;
using NUnit.Framework;
using System.Collections.Generic;
using DIKUArcade.Events;
using Breakout;

namespace breakouttests.StateTests;

public class TestMainMenu {
    private Dictionary<GameStateType, IBreakoutGameState> transitions = StateAlignments.Transitions;
    private GameEventBus gameBus;

    [SetUp]
    public void Setup() {
        if (BreakoutBus.GetBus == null) {
            gameBus = BreakoutBus.GetBus();
            //initialise bus
            gameBus.InitializeEventBus(new List<GameEventType>
                    {GameEventType.ControlEvent, GameEventType.PlayerEvent, GameEventType.StatusEvent, GameEventType.GameStateEvent});
        }
        
        Window.CreateOpenGLContext();
        _stateMachine = new StateMachine(StateAlignments.Transitions);
    }

    StateMachine _stateMachine = default!;

    /// <summary>
    /// Precondition. ActiveState = MainMenu.
    /// </summary>
    [Test]
    public void StartStateTest() {

        var actualState = _stateMachine.ActiveState;
        Assert.AreEqual(actualState, transitions[GameStateType.MainMenu]);
    }

    /// <summary>
    /// Changes state correctly from MainMenu to GameRunning.
    /// </summary>
    [Test]
    public void FromMainMenuToGameRunningChangesCorrectly() {
        var actualState = _stateMachine.ActiveState;
        _stateMachine.ChangeStates(GameStateType.GameRunning);
        var resultState = _stateMachine.ActiveState;
        Assert.AreNotEqual(actualState, resultState);
        Assert.AreEqual(resultState, transitions[GameStateType.GameRunning]);
    }


    [Test]
    public void TestGetNextState() {
        Assert.Pass();
    }

    [Test]
    public void TestHandleKeyEvent() {
        Assert.Pass();
    }

    [Test]
    public void TestKeyPress() {
        Assert.Pass();
    }

    [Test]
    public void TestUpdateButtons() {
        Assert.Pass();
    }

}

    



