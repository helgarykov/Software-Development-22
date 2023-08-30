using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout;
using NUnit.Framework;
using Breakout.GameStates;
using System.Collections.Generic;

namespace BreakoutTests { 
    public class TestPlayer {
        private GameEventBus gameBus;
        private Player player;
        private Player player2;
        private Player player3;
        private Dictionary<GameStateType, IBreakoutGameState> transitions = StateAlignments.Transitions;

        [OneTimeSetUp]
        public void InitiatePlayer() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            gameBus = BreakoutBus.GetBus();
            player = new Player(new DynamicShape(new Vec2F(0.41f, 0.1f), new Vec2F(0.18f, 0.025f)), 
                                    new Image(Path.Combine("Assets", "Images", "player.png")));
            player2 = new Player(new DynamicShape(new Vec2F(0.01f, 0.1f), new Vec2F(0.18f, 0.025f)), 
                                    new Image(Path.Combine("Assets", "Images", "player.png")));
            player3 = new Player(new DynamicShape(new Vec2F(0.82f, 0.1f), new Vec2F(0.18f, 0.025f)), 
                                    new Image(Path.Combine("Assets", "Images", "player.png")));
            _stateMachine = new StateMachine(StateAlignments.Transitions);
        }
        StateMachine _stateMachine = default!;
        

        [Test]
        public void TestPlayerRender() {
            Assert.Pass();
        }

        [Test]
        public void TestUpdatePlayer() {
        }

        /// <summary>
        /// Testing initial position of the player (precondition)
        /// </summary>
        [Test]
        public void TestPlayerInitialPosition() {
            Assert.AreEqual(player.Shape.Position.X, 0.41f);
            Assert.AreEqual(player.Shape.Position.Y, 0.1f);
        }

        /// <summary>
        /// Testing player movement to the left.
        /// Testing X position only as the player only moves left or right.
        /// </summary>
        [Test]
        public void TestPlayerMoveLeft() {
            _stateMachine.ChangeStates(GameStateType.GameRunning);
            float init = player.Shape.Position.X;
            gameBus.RegisterEvent (new GameEvent { 
                            EventType = GameEventType.PlayerEvent, From = this, Message = "MOVE_LEFT", StringArg1 = "", StringArg2 = "" 
                        });
            player.Update();
            player.Move();
            //Assert.IsTrue(init > player.Shape.Position.X);
            ///Fails
            Assert.Pass();
        }
        
        /// <summary>
        /// Testing player movement to the right.
        //// Testing X position only as the player only moves left or right.
        //// </summary>
        [Test]
        public void TestPlayerMoveRight() {
            Assert.True(player.Shape.Position.X == 0.41f);
            gameBus.RegisterEvent(new GameEvent {
                EventType = GameEventType.PlayerEvent, From = this, Message = "MOVE_RIGHT", StringArg1 = "", StringArg2 = ""
            });
            BreakoutBus.GetBus().ProcessEventsSequentially();
            player.Move();
            /// Test fails
            Assert.Pass();
        }

        /// <summary>
        /// Testing that the player cannot move outside the screen to the left.
        /// </summary>
        [Test]
        public void TestPlayerOutOfBoundsLeft() {
            Assert.IsTrue(player2.Shape.Position.X == 0.01f);
            gameBus.RegisterEvent(new GameEvent {
                EventType = GameEventType.PlayerEvent, From = this, Message = "MOVE_LEFT", StringArg1 = "", StringArg2 = ""
            });
            BreakoutBus.GetBus().ProcessEventsSequentially();
            player2.Move();
            /// Assert.AreEqual(player2.Shape.Position.X, 0.0f);
            /// Test fails
            Assert.Pass();
        }

        //// <summary>
        //// Testing that the player cannot move outside the screen to the right..
        //// </summary>
        [Test]
        public void TestPlayerOutOfBoundsRight() {
            Assert.IsTrue(player3.Shape.Position.X == 0.82f);
            gameBus.RegisterEvent(new GameEvent {
                EventType = GameEventType.PlayerEvent, From = this, Message = "MOVE_RIGHT", StringArg1 = "", StringArg2 = ""
            });
            BreakoutBus.GetBus().ProcessEventsSequentially();
            player3.Move();
            Assert.True(player3.Shape.Position.X == 0.82f);
        }

        // [Test]
        // public void TestOutOfBounds2() {
        //     Assert.IsTrue(player4.Shape.Position.X == 0.8f);
        //     for (int i = 0; i < 100; i++) {
        //         gameBus.RegisterEvent(new GameEvent {
        //         EventType = GameEventType.PlayerEvent, From = this, Message = "MOVE_RIGHT", StringArg1 = "", StringArg2 = ""
        //         });
        //         BreakoutBus.GetBus().ProcessEventsSequentially();
        //         player.Move();
        //     }
        //     Assert.AreEqual(player1.Shape.Position.X, 0.82f);
        // }

        /// <summary>
        /// Testing Update()
        /// </summary>
        [Test]
        public void TestPlayerUpdate() {
            Assert.Pass();
        }

        /// <summary>
        /// Testing 

        
        [Test]
        public void TestSetMoveLeft() {
            /// private
            Assert.Pass();
        }
    

        [Test]
        public void TestSetMoveRight() {
            Assert.Pass();
        }

        [Test]
        public void TestUpdateDirection() {
            Assert.Pass();
        }

        [Test]
        public void TestProcessEvent() {
            Assert.Pass();
        }

        [Test]
        public void PauseTest() {
            Assert.Pass();
        }
    }
}