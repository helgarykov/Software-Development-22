using System;
using Breakout;
using DIKUArcade.Events;
using DIKUArcade.Math;
using NUnit.Framework;

namespace breakouttests.OtherTests;

[TestFixture]
public class TestTimer
{
    private GameEventBus gameBus;
    private Timer timer1;
    public int remainingTime { get; private set; }

    [OneTimeSetUp]
    public void InitiateTimer()
    {

        DIKUArcade.GUI.Window.CreateOpenGLContext();
        gameBus = BreakoutBus.GetBus();
        timer1 = new Timer(new Vec2F(Game.rightX - 0.2f, 1f-0.3f), new Vec2F(0.3f, 0.3f), 180);
    }

    [Test]
    public void checkTimeTest()
    {
        remainingTime = (int) Math.Ceiling(180 - 190.0);
        timer1.checkTime();
        //Assert.IsTrue(GameStateType.GameOver);
    }

    [Test]
    public void PauseTest()
    {
        //to do
    }
}