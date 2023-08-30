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

namespace BreakoutTests { 
    public class TestPlayer {
        private GameEventBus gameBus;
        private Player player;

        [OneTimeSetUp]
        public void InitiatePlayer() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            gameBus = BreakoutBus.GetBus();
            player = new Player(new DynamicShape(new Vec2F(0.41f, 0.1f), new Vec2F(0.18f, 0.025f)), 
                                    new Image(Path.Combine("Assets", "Images", "player.png")));
        }

        [Test]
        public void UpdateTest()
        {
        }

        /// <summary>
        /// Testing movement of player.
        /// </summary>
        [Test]
        public void TestPlayerMoveLeft() {
            /// initial position is 0.41f.
            float init = player.Shape.Position.X;
            Assert.AreEqual(init, 0.41f);
            /// move the player left one time.
            gameBus.RegisterEvent (new GameEvent {
                EventType = GameEventType.PlayerEvent, From = this, Message = "MOVE_LEFT", StringArg1 = "", StringArg2 = ""
            });
            BreakoutBus.GetBus().ProcessEventsSequentially();
            player.Move();
            /// second position is saved.
            float snd = player.Shape.Position.X;
            /// second position should be equal to initial position - 0.01f.
            Assert.AreEqual(init - 0.01f, snd);
            /// stop the player moving left.
            gameBus.RegisterEvent (new GameEvent {
                EventType = GameEventType.PlayerEvent, From = this, Message = "STOP_MOVE_LEFT", StringArg1 = "", StringArg2 = ""
            });
            BreakoutBus.GetBus().ProcessEventsSequentially();
            /// move the player to the right one time
            gameBus.RegisterEvent (new GameEvent {
                EventType = GameEventType.PlayerEvent, From = this, Message = "MOVE_RIGHT", StringArg1 = "", StringArg2 = ""
            });
            BreakoutBus.GetBus().ProcessEventsSequentially();
            player.Move();
            /// end position is saved.
            float end = player.Shape.Position.X;
            /// end position should be equal to second position + 0.01f
            Assert.AreEqual(snd + 0.01f, end);
            /// end position should also equal the initial position, as the player has moved once left and then back.
            Assert.AreEqual(end, init);

        }

        /// <summary>
        /// Testing that the player cannot move outside the screen by moving to the left.
        /// </summary>
        [Test]
        public void TestPlayerMoveOutsideScreenToTheLeft() {

            for (int i = 0; i < 100; i++) {
                gameBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.PlayerEvent, From = this, Message = "MOVE_LEFT", StringArg1 = "", StringArg2 = ""
                });
                BreakoutBus.GetBus().ProcessEventsSequentially();
                player.Move();
            }
            Assert.AreEqual(0.00f, player.Shape.Position.X);
            gameBus.RegisterEvent (new GameEvent {
                EventType = GameEventType.PlayerEvent, From = this, Message = "STOP_MOVE_LEFT", StringArg1 = "", StringArg2 = ""
            });
            BreakoutBus.GetBus().ProcessEventsSequentially();
        }

        /// <summary>
        /// Testing that the player cannot move outside the screen by moving to the right.
        /// </summary>
        [Test]
        public void TestPlayerMoveOutsideScreenToTheRight() {
            for (int i = 0; i < 100; i++) {
                gameBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.PlayerEvent, From = this, Message = "MOVE_RIGHT", StringArg1 = "", StringArg2 = ""
                });
                BreakoutBus.GetBus().ProcessEventsSequentially();
                player.Move();
            }
            Assert.AreEqual(0.82f, player.Shape.Position.X);
        }

        [Test]
        public void SetMoveLeft()
        {
            //privat
        }

        [Test]
        public void SetMoveRight()
        {
            //privat
        }

        [Test]
        public void PauseTest()
        {
            //privat
        }

        [Test]
        public void ProcessEventTest()
        {
            //testing move left
            gameBus.RegisterEvent(new GameEvent {
                EventType = GameEventType.PlayerEvent, From = this, Message = "MOVE_LEFT", StringArg1 = "", StringArg2 = ""
            });
            BreakoutBus.GetBus().ProcessEventsSequentially();
            // ? Assert.True(...);
            
            //testing move right
            gameBus.RegisterEvent(new GameEvent {
                EventType = GameEventType.PlayerEvent, From = this, Message = "MOVE_RIGHT", StringArg1 = "", StringArg2 = ""
            });
            BreakoutBus.GetBus().ProcessEventsSequentially();
            // ? Assert.True(...);
            
            //testing stop move left
            gameBus.RegisterEvent(new GameEvent {
                EventType = GameEventType.PlayerEvent, From = this, Message = "STOP_MOVE_LEFT", StringArg1 = "", StringArg2 = ""
            });
            BreakoutBus.GetBus().ProcessEventsSequentially();
            // ? Assert.True(...);
            
            //testing stop move right
            gameBus.RegisterEvent(new GameEvent {
                EventType = GameEventType.PlayerEvent, From = this, Message = "STOP_MOVE_RIGHT", StringArg1 = "", StringArg2 = ""
            });
            BreakoutBus.GetBus().ProcessEventsSequentially();
            // ? Assert.True(...);
            
            //testing extra speed
            gameBus.RegisterEvent(new GameEvent {
                EventType = GameEventType.StatusEvent, From = this, Message = "EXTRA_SPEED", StringArg1 = "", StringArg2 = ""
            });
            BreakoutBus.GetBus().ProcessEventsSequentially();
            // ? Assert.True(...);
            
            //testing pause time
            gameBus.RegisterEvent(new GameEvent {
                EventType = GameEventType.StatusEvent, From = this, Message = "PAUSE_TIME", StringArg1 = "", StringArg2 = ""
            });
            BreakoutBus.GetBus().ProcessEventsSequentially();    
            // ? Assert.True(...);
        }
    }
}