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

namespace BreakoutTests { 
    [TestFixture]
    public class TestPoints {
        private GameEventBus gameBus;
        private Block block;
        private EntityContainer<Block> blocks;

        [SetUp]
        public void InitiateBlocks() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            gameBus = BreakoutBus.GetBus();
            gameBus.InitializeEventBus(new List<GameEventType> { GameEventType.ControlEvent, GameEventType.PlayerEvent, GameEventType.StatusEvent });
        }    

        [Test]
        public void pointsTest()
        {
            //testing that points are 0 when no blocks have been hit.
            blocks = new EntityContainer<Block>(20);
            blocks.AddEntity(new Block( new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.0833333f, 0.04f)),
                new Image(Path.Combine("Assets", "Images", "Purple-Block.png")), new Image(Path.Combine("Assets", "Images", "Purple-Block.png")),  BlockType.Standard));
            Assert.AreEqual(block.hitpoints, 0);
            
            //testing that points are incremented when a block is hit
            gameBus.RegisterEvent (new GameEvent { 
                EventType = GameEventType.ControlEvent, From = this, Message = "HIT_BLOCK", ObjectArg1 = block, IntArg1 = 5 
            });
            BreakoutBus.GetBus().ProcessEventsSequentially();
            Assert.AreEqual(block.hitpoints, 5);
        }

        [Test]
        public void ProcessEventTest()
        {
        }
    }
}
