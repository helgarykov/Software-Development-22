using NUnit.Framework;
using Breakout.LevelManagement;

namespace BreakoutTests { 

    public class TestCreateMap { 
        [OneTimeSetUp]
        public void InitiateWindow() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
        }    

        [Test]
        public void TestEmptyMap() {
            Assert.AreEqual(Translator.CreateMap("Empty Metadata.txt").CountEntities(), 0);
        }

        [Test]
        public void TestInvalidMap() {
            Assert.AreEqual(Translator.CreateMap("Invalid Metadata.txt").CountEntities(), 28);
        }

        [Test]
        public void TestEmptyFile() {
            Assert.AreEqual(Translator.CreateMap("Empty File.txt").CountEntities(), 0);
        }

        [Test]
        public void TestCorrectAmount() {
            Assert.AreEqual(Translator.CreateMap("level3.txt").CountEntities(), 76);
        }
        [Test]
        public void TestWrongLegend() {
            Assert.AreEqual(Translator.CreateMap("WrongLegend.txt").CountEntities(), 0);
        }
    }
}