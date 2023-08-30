using NUnit.Framework;
using System;
using System.IO;
using Galaga;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga.MovementStrategy;

namespace galagatests { 
    [TestFixture]
    public class TestMovementStrategy {
        private Enemy enemy;

        [SetUp]
        public void InitiateEnemy() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            var ds = new DynamicShape(new Vec2F(0.0f, 0.1f), new Vec2F(0.1f, 0.1f));
            var img = new Image(Path.Combine("Assets", "Images", "BlueMonster.png"));
            var img2 = new Image(Path.Combine("Assets", "Images", "RedMonster.png"));
            enemy = new Enemy(ds, img, img2);
        }
 
        [Test]
        public void TestStrategyDown() {
            var y = enemy.Shape.Position.Y;
                
            var stratdown = new StrategyDown();
            stratdown.moveEnemy(enemy);
                
            Assert.LessOrEqual(enemy.Shape.Position.Y, y);
        }

        [Test]
        public void TestStrategyNoMove() {
            var pos = enemy.Shape.Position;
                
            var stratno = new StrategyNoMove();
            stratno.moveEnemy(enemy);
                
            Assert.AreEqual(pos, enemy.Shape.Position);
        }
        
        [Test]
        public void TestStrategyZigZagDown() {
            var p = -0.045f;
            var s = -0.0003f;
            var a = -0.05f;

            var y = enemy.Shape.Position.Y;
            var x = enemy.Shape.Position.X;
                
            var stratzig = new StrategyZigZagDown();
            stratzig.moveEnemy(enemy);
                
                
            Assert.AreEqual(enemy.Shape.Position.Y, y+s);
            Assert.AreEqual(enemy.Shape.Position.X, x + a*(float)(Math.Sin(Convert.ToDouble((2*Math.PI*(y-enemy.Shape.Position.Y))/p))));
        }
    }
}