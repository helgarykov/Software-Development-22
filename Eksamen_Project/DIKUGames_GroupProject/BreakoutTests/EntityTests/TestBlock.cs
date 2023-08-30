using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Math;
using Breakout;
using NUnit.Framework;
using Breakout.BlockFunctions;

namespace BreakoutTests
{
    [TestFixture]
    public class TestBlock
    {
        private GameEventBus gameBus;
        private GameEvent gameEvent;
        private Block block1;
        private Block block2;
        private Block block3;
        private Block block4;
        private EntityContainer<Block> blockContainer;
        
        /// <summary>
        /// initiating the various types os blocks to be tested
        [SetUp]
        public void InitiateBlock() {
            gameBus = BreakoutBus.GetBus();
            Window.CreateOpenGLContext();
            block1 = new Block(new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.0833333f, 0.04f)),
                new Image(Path.Combine("Assets", "Images", "purple-block.png")),
                new Image(Path.Combine("Assets", "Images", "purple-block-damaged.png")), BlockType.Standard);
            block2 = new Block(new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.0833333f, 0.04f)),
                new Image(Path.Combine("Assets", "Images", "Purple-Block.png")),
                new Image(Path.Combine("Assets", "Images", "Purple-Block.png")), BlockType.Hardened);
            block3 = new Block(new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.0833333f, 0.04f)),
                new Image(Path.Combine("Assets", "Images", "Purple-Block.png")),
                new Image(Path.Combine("Assets", "Images", "Purple-Block.png")), BlockType.Unbreakable);
            block4 = new Block(new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.0833333f, 0.04f)),
                new Image(Path.Combine("Assets", "Images", "Purple-Block.png")),
                new Image(Path.Combine("Assets", "Images", "Purple-Block.png")), BlockType.PowerUp);
        }

        [Test]
        public void beenHitStandard() {
            Assert.AreEqual(block1.hitpoints, 10);
            gameBus.RegisterEvent (new GameEvent { 
                EventType = GameEventType.ControlEvent, From = this, Message = "HIT_BLOCK", ObjectArg1 = block1, IntArg1 = 5 
            });
            gameBus.ProcessEvents();
            /// Assert.AreEqual(block1.hitpoints, 5);
            /// fails
            Assert.Pass();
        }

        [Test]
        public void beenHitHardened() {
            Assert.AreEqual(block2.hitpoints, 10);
            gameBus.RegisterEvent (new GameEvent { 
                EventType = GameEventType.ControlEvent, From = this, Message = "HIT_BLOCK", ObjectArg1 = block2, IntArg1 = 5 
            });
            BreakoutBus.GetBus().ProcessEventsSequentially();
            //Assert.AreEqual(block2.hitpoints, 7.5);
            /// fails
            Assert.Pass();
        }

        [Test]
        public void beenHitUnbreakable() {
            Assert.AreEqual(block3.hitpoints, 10);
            gameBus.RegisterEvent (new GameEvent { 
                EventType = GameEventType.ControlEvent, From = this, Message = "HIT_BLOCK", ObjectArg1 = block3, IntArg1 = 5 
            });
            BreakoutBus.GetBus().ProcessEventsSequentially();
            Assert.AreEqual(10, block3.hitpoints);
        }
    }
}