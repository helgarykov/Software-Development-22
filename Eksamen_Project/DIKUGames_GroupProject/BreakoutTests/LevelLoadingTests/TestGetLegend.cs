using NUnit.Framework;
using Breakout.LevelManagement;

namespace BreakoutTests { 

    public class TestGetLegend {

        [SetUp]
        public void InitiateWindow() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
        }    
        [Test]
        public void TestNoLegend() {
          Assert.AreEqual(Translator.getLegend("No Metadata.txt").Length, 0);  
        }
        
        [Test]
        public void TestEmptyLegend() {
            Assert.AreEqual(Translator.getLegend("Empty Metadata.txt").Length, 0);
        }

        [Test]
        public void TestInvalidLegend() {
            Assert.AreEqual(Translator.getLegend("Invalid Metadata.txt").Length, 4);
        }

        [Test]
        public void TestEmptyFile() {
            Assert.AreEqual(Translator.getLegend("Empty File.txt").Length, 0);
        }

        [Test]
        public void TestCorrectAmount() {
            Assert.AreEqual(Translator.getLegend("level3.txt").Length, 10);
        }

        [Test]
        public void TestCorrectOrder() {
            var legendList = Translator.getLegend("level3.txt");
            Assert.AreEqual(legendList[0], "0");
            Assert.AreEqual(legendList[1], "orange-block.png");
            Assert.AreEqual(legendList[2], "w");
            Assert.AreEqual(legendList[3], "darkgreen-block.png");
            Assert.AreEqual(legendList[4], "#");
            Assert.AreEqual(legendList[5], "green-block.png");
            Assert.AreEqual(legendList[6], "Y");
            Assert.AreEqual(legendList[7], "brown-block.png");
            Assert.AreEqual(legendList[8], "b");
            Assert.AreEqual(legendList[9], "yellow-block.png");
        }
        [Test]
        public void TestAtypicalNames() {
            var legendList = Translator.getLegend("Atypical Metadata.txt");
            Assert.AreEqual(legendList.Length, 4);
            Assert.AreEqual(legendList[1], "STRANGE_FILE_NAME_WITH_NUMBERS_123456789.jpeg");
            Assert.AreEqual(legendList[2], "?");
        }
    }
}