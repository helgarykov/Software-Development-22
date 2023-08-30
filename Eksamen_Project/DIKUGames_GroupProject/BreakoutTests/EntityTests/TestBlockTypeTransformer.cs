using DIKUArcade.Events;
using Breakout;
using NUnit.Framework;
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
        }
        
        /// <summary>
        //testing that conversion takes place as expected
        /// <summary>
        [Test]
        public void TransformStringToBlockTest()
        {
            BlockType hardened = BlockTypeConverter.TransformStringToBlock("Hardened");
            Assert.AreEqual(hardened, BlockType.Hardened);
            
            BlockType unbreakable = BlockTypeConverter.TransformStringToBlock("Unbreakable");
            Assert.AreEqual(unbreakable, BlockType.Unbreakable);
            
            BlockType powerup = BlockTypeConverter.TransformStringToBlock("PowerUp");
            Assert.AreEqual(powerup, BlockType.PowerUp);
            
            BlockType standard = BlockTypeConverter.TransformStringToBlock("blablabla");
            Assert.AreEqual(standard, BlockType.Standard);
        }
    }
}