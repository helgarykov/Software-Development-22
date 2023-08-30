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
using Breakout.LevelManagement;

namespace BreakoutTests { 

    public class TestGetMeta {


        [SetUp]
        public void InitiateWindow() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
        }    

        [Test]
        public void TestNoMeta() {
          Assert.AreEqual(Translator.getMeta("No Metadata.txt").Length, 0);  
        }
        
        [Test]
        public void TestEmptyMeta() {
            Assert.AreEqual(Translator.getMeta("Empty Metadata.txt").Length, 0);
        }

        [Test]
        public void TestInvalidMeta() {
            Assert.AreEqual(Translator.getMeta("Invalid Metadata.txt").Length, 2);
        }

        [Test]
        public void TestEmptyFile() {
            Assert.AreEqual(Translator.getMeta("Empty File.txt").Length, 0);
        }

        [Test]
        public void TestCorrectAmount() {
            Assert.AreEqual(Translator.getMeta("level3.txt").Length, 8);
        }

        [Test]
        public void TestCorrectOrder() {
            var metaList = Translator.getMeta("level3.txt");
            Assert.AreEqual(metaList[0], "Name");
            Assert.AreEqual(metaList[1], "LEVEL 3");
            Assert.AreEqual(metaList[2], "Time");
            Assert.AreEqual(metaList[3], "180");
            Assert.AreEqual(metaList[4], "PowerUp");
            Assert.AreEqual(metaList[5], "#");
            Assert.AreEqual(metaList[6], "Unbreakable");
            Assert.AreEqual(metaList[7], "Y");
        }
        [Test]
        public void TestAtypicalNames() {
            var metaList = Translator.getMeta("Atypical Metadata.txt");
            Assert.AreEqual(metaList.Length, 4);
            Assert.AreEqual(metaList[0], "123456789");
            Assert.AreEqual(metaList[3], "SUPERLONGDATAFILEWITHNOSPACESANDALSONUMBERS123456789");
        }
    }
}