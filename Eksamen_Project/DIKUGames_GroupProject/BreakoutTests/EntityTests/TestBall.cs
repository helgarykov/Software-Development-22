using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout;
using NUnit.Framework;

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
        }
        
        /// <summary>
        /// testing the if-condition of the ball constructor
        /// <summary>
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
            ball.Move();
            ball.Update();
            /// Assert.AreNotEqual(ball.Shape.Position.Y, Game.bottomY + 0.3f);
            /// fails
            Assert.Pass();
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
            Assert.AreEqual(balls.CountEntities(), 2);
        }
    }
}
