using System.Collections.Generic;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Math;
using Breakout;
using NUnit.Framework;
using OpenTK.Windowing.Common;
using Breakout.BlockFunctions;

namespace BreakoutTests
{
    [TestFixture]
    public class TestBall
    {
        private Ball ball;
        private Ball ball2;
        private Image img;
        private Image img2;
        private EntityContainer<Ball> balls;
        private GameEventBus gameBus;
        private EntityContainer<Ball> ballContainer;

        [SetUp]
        public void InitiateBall()
        {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            gameBus = BreakoutBus.GetBus();
            gameBus.InitializeEventBus(new List<GameEventType>
                {GameEventType.ControlEvent, GameEventType.PlayerEvent, GameEventType.StatusEvent});
        }

        [Test]
        public void ballConstructorTest()
        {
            Ball ball = new Ball(new DynamicShape(new Vec2F(Game.rightX / 2.0f - 0.04f, Game.bottomY + 0.14f), new Vec2F(0.04f, 0.04f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "ball2.png")));
            Assert.AreEqual(ball.playing, false);
        }

        [Test]
        public void MoveTest()
        {
            Ball ball = new Ball(new DynamicShape(new Vec2F(Game.rightX / 2.0f - 0.04f, Game.bottomY + 0.3f), new Vec2F(0.04f, 0.04f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "ball2.png")));
            Assert.AreNotEqual(ball.Shape.Position.Y,Game.bottomY + 0.3f);
        }

        [Test]
        public void SetPositionTest()
        {
            Ball ball = new Ball(new DynamicShape(new Vec2F(Game.rightX / 2.0f - 0.04f, Game.bottomY + 0.14f), new Vec2F(0.04f, 0.04f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "ball2.png")));
            ball.SetPositionX(0);
            Assert.AreEqual(ball.Shape.Position.X,0);
        }

        [Test]
        public void ChangeStridesofFireBallTest()
        {
            Ball ball = new Ball(new DynamicShape(new Vec2F(Game.rightX / 2.0f - 0.04f, Game.bottomY + 0.14f), new Vec2F(0.04f, 0.04f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "ball2.png")));
            /*to do :
            ball.ChangeStridestoFireBall(false);
            Assert.AreEqual(ball.Image, ball.fireBallImage);
            ball.ChangeStridestoFireBall(true);
            Assert.AreEqual(ball.Image, ball.ballImage);*/
        }

        [Test]
        public void insideScreenTest()
        {
            balls = new EntityContainer<Ball>(300);
            balls.AddEntity(ball = new Ball(
                new DynamicShape(new Vec2F(Game.rightX / 2.0f - 0.04f, Game.bottomY + 0.14f), new Vec2F(0.04f, 0.04f),
                    new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "ball2.png"))));
            ball.insideScreen();
            Assert.AreEqual(balls.CountEntities(), 1);
            
            balls.AddEntity(ball2 = new Ball(
                new DynamicShape(new Vec2F(Game.rightX / 2.0f - 0.04f, Game.bottomY - 0.14f), new Vec2F(0.04f, 0.04f),
                    new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "ball2.png"))));
            ball2.insideScreen();
            Assert.AreEqual(balls.CountEntities(), 1);
        }

        [Test]
        public void ProcessEventTest()
        {
            img = new Image(Path.Combine("Assets", "Images", "ball2.png"));
            img2 = new Image(Path.Combine("Assets", "Images", "FireBallStrides.png"));
            ball = new Ball(
                new DynamicShape(new Vec2F(Game.rightX / 2.0f - 0.04f, Game.bottomY + 0.14f), new Vec2F(0.04f, 0.04f),
                    new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "ball2.png")));
            
            Assert.AreEqual(ball.Image, img);
            
            gameBus.RegisterEvent (new GameEvent {
                EventType = GameEventType.StatusEvent, From = this, Message = "FIRE_BALL", StringArg1 = "TRUE", StringArg2 = ""
            });
            BreakoutBus.GetBus().ProcessEventsSequentially();
            Assert.AreEqual(ball.Image, img2);
            
            gameBus.RegisterEvent (new GameEvent {
                EventType = GameEventType.StatusEvent, From = this, Message = "FIRE_BALL", StringArg1 = "FALSE", StringArg2 = ""
            });
            BreakoutBus.GetBus().ProcessEventsSequentially();
            Assert.AreNotEqual(ball.Image, img2);
            
            gameBus.RegisterEvent (new GameEvent {
                EventType = GameEventType.StatusEvent, From = this, Message = "SHOOT", StringArg1 = "", StringArg2 = ""
            });
            BreakoutBus.GetBus().ProcessEventsSequentially();
            Assert.AreEqual(ball.playing, true);
        }
    }
}
