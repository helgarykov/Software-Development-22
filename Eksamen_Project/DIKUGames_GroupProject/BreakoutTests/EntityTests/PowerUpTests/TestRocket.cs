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
    public class TestRocket
    {
        private Rocket rocket;
        private GameEventBus gameBus;
        
        [SetUp]
        public void InitiateRocket()
        {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            gameBus = BreakoutBus.GetBus();
            rocket = new Rocket(new DynamicShape(new Vec2F(Game.rightX / 2.0f - 0.04f, Game.bottomY + 0.14f),
                    new Vec2F(0.04f, 0.04f), new Vec2F(0.1f, 0.1f)), 
                new Image(Path.Combine("Assets", "Images", "Rocket.png")));
        }
        
        /// <summary>
        //testing that the rocket moves as expected
        /// <summary>
        [Test]
        public void TestRocketMove()
        {
            float init = rocket.Shape.Position.Y;
            Assert.AreEqual(init, Game.bottomY + 0.14f);
            rocket.Move();
            float movedRocket = rocket.Shape.Position.Y;
            Assert.IsTrue(movedRocket > init);
        }
    }
}