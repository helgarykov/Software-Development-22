using System.Collections.Generic;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Math;
using Galaga;
using NUnit.Framework;
using OpenTK.Windowing.Common;

namespace galagatests { 

    public class TestPlayer {
        private GameEventBus gameBus;
        private Player player;

        [SetUp]
        public void InitiatePlayer() {
            gameBus = GalagaBus.GetBus();
            player = new Player(new DynamicShape(new Vec2F(0.45f, 0.1f),
            new Vec2F(0.1f, 0.1f)), new Image(Path.Combine("Assets", "Images", "Player.png")));
        }    

        [Test]
        public void TestPlayerEntity() {
            List<Player> playerList = new List<Player>();
            playerList.Add(player);
            Assert.AreEqual(player, playerList[0]);
            Assert.AreEqual(1, playerList.Count);
        }

        [Test]
        public void TestPlayerMove() {
            var playerPos1 = player.shape.Position.X;
            var windowArgs = new WindowArgs() { Title = "Galaga TestPlayerMove" };
            var game = new Game(windowArgs);
            gameBus.RegisterEvent (new GameEvent { 
                            EventType = GameEventType.PlayerEvent, From = this, Message = "MOVE_LEFT", StringArg1 = "", StringArg2 = "" 
                        });
            var playerPos2 = player.shape.Position.X;
            gameBus.RegisterEvent (new GameEvent { 
                            EventType = GameEventType.PlayerEvent, From = this, Message = "MOVE_RIGHT", StringArg1 = "", StringArg2 = "" 
                        });
            var playerPos3 = player.shape.Position.X;
            Assert.LessOrEqual(playerPos2, playerPos1);
            Assert.AreEqual(playerPos1, playerPos3);
        }

        [Test]
        public void TestClearPlayer() {
            List<Player> playerList = new List<Player>();
            playerList.Add(player);
            Assert.AreEqual(player, playerList[0]);
            player.ClearPlayer();
            Assert.AreNotEqual(player, playerList[0]);
            Assert.AreEqual(null, playerList.Count);
        }
    }
}