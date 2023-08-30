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
using System;

namespace BreakoutTests { 

    public class TestCreateMap
    {
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