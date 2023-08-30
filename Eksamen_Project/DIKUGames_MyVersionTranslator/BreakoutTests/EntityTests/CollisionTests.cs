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

namespace BreakoutTests { 
    public class TestCollision {
        //private GameEventBus gameBus;
        private Player player;
        private Ball ball;

        [OneTimeSetUp]
        public void InitiatePlayerandBall() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            //gameBus = BreakoutBus.GetBus();
            player = new Player(new DynamicShape(new Vec2F(0.41f, 0.1f), new Vec2F(0.18f, 0.025f)), 
            new Image(Path.Combine("Assets", "Images", "player.png")));
            ball = new Ball(new DynamicShape(new Vec2F(0.55f, 0.14f), new Vec2F(0.04f, 0.04f), new Vec2F(0.1f, 0.1f)),
                    new Image(Path.Combine("Assets", "Images", "ball2.png")));
        }

        /// <summary>
        /// Testing collision when ball and player is initialized.
        /// Do not expect collision.
        /// </summary>

        [Test]
        public void CollisionTest() {
            bool a = DIKUArcade.Physics.CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), 
                        player.Shape).Collision;
            Assert.AreEqual(a, false);
            
        }
        /// <summary>
        /// Direction of the ball when the game is initiated is expected to be between 80 and 100.
        /// </summary>
        [Test]
        public void DirectionTest() {
            if (ball.direction >= 80 && ball.direction <= 100) {
                Assert.Pass();
            }
            
        }

        /// <summary>
        /// Direction of ball after collision.
        /// </summary>


    }
}