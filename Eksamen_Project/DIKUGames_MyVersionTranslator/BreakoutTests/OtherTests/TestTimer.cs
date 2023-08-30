using System.Collections.Generic;
using System.IO;
using Breakout;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using NUnit.Framework;

namespace breakouttests.OtherTests;

[TestFixture]
public class TestTimer
{
    private GameEventBus gameBus;

    [OneTimeSetUp]
    public void InitiateTimer() {
        
        DIKUArcade.GUI.Window.CreateOpenGLContext();
        gameBus = BreakoutBus.GetBus();
        gameBus.InitializeEventBus(new List<GameEventType> { GameEventType.ControlEvent, GameEventType.PlayerEvent, GameEventType.StatusEvent });
    }

    [Test]
    public void checkTimeTest()
    {
        //to do : insert test
    }

    [Test]
    public void PauseTest()
    {
        //to do
    }

    [Test]
    public void ProcessEventTest()
    {
        //to do
    }

}
