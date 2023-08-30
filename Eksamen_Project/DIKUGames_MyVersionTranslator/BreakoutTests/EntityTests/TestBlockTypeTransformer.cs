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
    public class TestBlockTypeConverter
    {
        private GameEventBus gameBus;

        [SetUp]
        public void InitiateBlockTypeConverter()
        {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            gameBus = BreakoutBus.GetBus();
            gameBus.InitializeEventBus(new List<GameEventType>
                {GameEventType.ControlEvent, GameEventType.PlayerEvent, GameEventType.StatusEvent});
        }

        [Test]
        public void TransformStringToBlockTest()
        {
            BlockType hardened = BlockTypeConverter.TransformStringToBlock("Hardened");
            Assert.AreEqual(hardened,BlockType.Hardened);
            
            BlockType unbreakable = BlockTypeConverter.TransformStringToBlock("Unbreakable");
            Assert.AreEqual(unbreakable,BlockType.Unbreakable);
            
            BlockType powerup = BlockTypeConverter.TransformStringToBlock("PowerUp");
            Assert.AreEqual(powerup,BlockType.PowerUp);
            
            BlockType standard = BlockTypeConverter.TransformStringToBlock("blablabla");
            Assert.AreEqual(standard,BlockType.Standard);
        }
    }
}