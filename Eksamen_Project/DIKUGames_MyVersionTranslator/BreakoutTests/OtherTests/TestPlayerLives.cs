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
public class TestPlayerLives
{
    private GameEventBus gameBus;
    private Player player;
    private Ball ball;
    private PlayerLives playerLives;

    [OneTimeSetUp]
    public void InitiatePlayerandBall() {
        
        DIKUArcade.GUI.Window.CreateOpenGLContext();
        gameBus = BreakoutBus.GetBus();
        gameBus.InitializeEventBus(new List<GameEventType> { GameEventType.ControlEvent, GameEventType.PlayerEvent, GameEventType.StatusEvent });
    }

    [Test]
    public void playerLivesTest()
    {
        //testing that the player is actually assigned 3 player lives at the beginning of each game
        Assert.AreEqual(3, playerLives);
        
        //testing that a player life is subtracted when the ball goes below y=0.
        ball = new Ball(new DynamicShape(new Vec2F(0.55f, -1.1f), new Vec2F(0.04f, 0.04f), new Vec2F(0.1f, -0.1f)),
            new Image(Path.Combine("Assets", "Images", "ball2.png")));
        Assert.AreEqual(2, playerLives);
    }

    [Test]
    public void drawHeartsTest()
    {
        //to do
    }

    [Test]
    public void checkLivesTest()
    {
    }

    [Test]
    public void ProcessEventTest()
    {
    }

}
