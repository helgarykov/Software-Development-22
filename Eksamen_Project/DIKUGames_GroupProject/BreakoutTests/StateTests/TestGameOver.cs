using Breakout.GameStates;
using DIKUArcade.GUI;
using NUnit.Framework;
using System.Collections.Generic;
using DIKUArcade.Events;
using Breakout;

namespace breakouttests.StateTests;

public class TestGameOver {
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

     [Test]
     public void FromGameOverToMainMenu()
     {
         _stateMachine.ChangeStates(GameStateType.GameOver);

         var state1 = _stateMachine.ActiveState;

        _stateMachine.ChangeStates(GameStateType.MainMenu);

        var state2 = _stateMachine.ActiveState;

        Assert.AreNotEqual(state1, state2);
    }
}