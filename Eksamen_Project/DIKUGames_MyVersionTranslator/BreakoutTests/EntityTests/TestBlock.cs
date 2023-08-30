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
    public class TestBlock {
        private GameEventBus gameBus;
        private GameEvent gameEvent;
        private Block block1;
        private Block block2;
        private Block block3;
        private Block block4;
        private EntityContainer<Block> blockContainer;

        [SetUp]
        public void InitiateBlock() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            gameBus = BreakoutBus.GetBus();
            gameBus.InitializeEventBus(new List<GameEventType> { GameEventType.ControlEvent, GameEventType.PlayerEvent, GameEventType.StatusEvent });
            block1 = new Block( new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.0833333f, 0.04f)),
                new Image(Path.Combine("Assets", "Images", "Purple-Block.png")), new Image(Path.Combine("Assets", "Images", "Purple-Block.png")),  BlockType.Standard);
            block2 = new Block( new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.0833333f, 0.04f)),
                new Image(Path.Combine("Assets", "Images", "Purple-Block.png")), new Image(Path.Combine("Assets", "Images", "Purple-Block.png")),  BlockType.Hardened);
            block3 = new Block( new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.0833333f, 0.04f)),
                new Image(Path.Combine("Assets", "Images", "Purple-Block.png")), new Image(Path.Combine("Assets", "Images", "Purple-Block.png")),  BlockType.Unbreakable);
            block4 = new Block( new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.0833333f, 0.04f)),
                new Image(Path.Combine("Assets", "Images", "Purple-Block.png")), new Image(Path.Combine("Assets", "Images", "Purple-Block.png")),  BlockType.PowerUp);
            blockContainer.AddEntity(block1);
            blockContainer.AddEntity(block2);
            blockContainer.AddEntity(block3);
        }
        [Test]
        public void BlockConstructorTest()
        {
            block1 = new Block( new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.0833333f, 0.04f)),
                new Image(Path.Combine("Assets", "Images", "Purple-Block.png")), new Image(Path.Combine("Assets", "Images", "Purple-Block.png")),  BlockType.PowerUp);
            //? Assert.AreEqual(block.GetType(), BlockType.PowerUp);
        }

        [Test]
        public void RenderTest()
        {
            //to do
        }

        [Test]
        public void UpdateTest()
        {
            Assert.AreEqual(blockContainer.CountEntities(), 3);
            
            //testing that standard block is destroyed when hitpoints <= 0.
            gameBus.RegisterEvent (new GameEvent { 
                EventType = GameEventType.ControlEvent, From = this, Message = "HIT_BLOCK", ObjectArg1 = block1, IntArg1 = 10
            });
            gameBus.RegisterEvent (new GameEvent { 
                EventType = GameEventType.ControlEvent, From = this, Message = "HIT_BLOCK", ObjectArg1 = block1, IntArg1 = 10
            });
            BreakoutBus.GetBus().ProcessEventsSequentially();
            //? Assert.IsTrue(gameEvent.Message == "BLOCK_DESTROYED");
            //? Assert.AreEqual(blockContainer.CountEntities(), 2);
            
            //testing that hardened block is destroyed when hitpoints <= 0.
            gameBus.RegisterEvent (new GameEvent { 
                EventType = GameEventType.ControlEvent, From = this, Message = "HIT_BLOCK", ObjectArg1 = block2, IntArg1 = 10
            });
            gameBus.RegisterEvent (new GameEvent { 
                EventType = GameEventType.ControlEvent, From = this, Message = "HIT_BLOCK", ObjectArg1 = block2, IntArg1 = 10
            });
            gameBus.RegisterEvent (new GameEvent { 
                EventType = GameEventType.ControlEvent, From = this, Message = "HIT_BLOCK", ObjectArg1 = block2, IntArg1 = 10
            });
            gameBus.RegisterEvent (new GameEvent { 
                EventType = GameEventType.ControlEvent, From = this, Message = "HIT_BLOCK", ObjectArg1 = block2, IntArg1 = 10
            });
            BreakoutBus.GetBus().ProcessEventsSequentially();
            //? Assert.IsTrue(gameEvent.Message == "BLOCK_DESTROYED");
            //? Assert.AreEqual(blockContainer.CountEntities(), 1);
            
            //testing that powerup block is destroyed when hitpoints <= 0.
            gameBus.RegisterEvent (new GameEvent { 
                EventType = GameEventType.ControlEvent, From = this, Message = "HIT_BLOCK", ObjectArg1 = block4, IntArg1 = 10
            });
            gameBus.RegisterEvent (new GameEvent { 
                EventType = GameEventType.ControlEvent, From = this, Message = "HIT_BLOCK", ObjectArg1 = block4, IntArg1 = 10
            });
            BreakoutBus.GetBus().ProcessEventsSequentially();
            //? Assert.IsTrue(gameEvent.Message == "SPAWN_POWERUP");
            //? Assert.AreEqual(blockContainer.CountEntities(), 0);
        }

        [Test]
        public void ProcessEventTest()
        {
        }

        [Test]
        public void beenHitTest() {
            Assert.AreEqual(block1.hitpoints, 10);
            
            //testing standard block
            gameBus.RegisterEvent (new GameEvent { 
                EventType = GameEventType.ControlEvent, From = this, Message = "HIT_BLOCK", ObjectArg1 = block1, IntArg1 = 5 
            });
            BreakoutBus.GetBus().ProcessEventsSequentially();
            Assert.AreEqual(block1.hitpoints, 5);
            
            //testing hardened block
            gameBus.RegisterEvent (new GameEvent { 
                EventType = GameEventType.ControlEvent, From = this, Message = "HIT_BLOCK", ObjectArg1 = block2, IntArg1 = 5 
            });
            BreakoutBus.GetBus().ProcessEventsSequentially();
            Assert.AreEqual(block2.hitpoints, 7.5);
            
            //testing unbreakable block
            gameBus.RegisterEvent (new GameEvent { 
                EventType = GameEventType.ControlEvent, From = this, Message = "HIT_BLOCK", ObjectArg1 = block3, IntArg1 = 5 
            });
            BreakoutBus.GetBus().ProcessEventsSequentially();
            Assert.AreEqual(block3.hitpoints, 10);
        }

        [Test]
        public void rollPowerUpTest()
        {
            //no clue
        }
        [Test]
        public void generatePowerUpIconTest()
        {
            //no clue
        }
    }
}