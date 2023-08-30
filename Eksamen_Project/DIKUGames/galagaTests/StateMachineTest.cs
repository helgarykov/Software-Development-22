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
using Galaga.GalagaStates;

namespace galagatests {
    [TestFixture]
    public class StateMachineTesting : IGameEventProcessor {
        private StateMachine stateMachine;
        private GameEventBus eventBus;

        [OneTimeSetUp]
        public void InitiateStateMachine() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();

            // Initialize a galagabus with proper GameEventTypes
            eventBus = GalagaBus.GetBus();
            
            // Instantiate the StateMachine
            stateMachine = new StateMachine();

            //Subscribe the GalagaBus to proper GameEventTypes 
            //and GameEvent processors
            
            eventBus.Subscribe(GameEventType.GameStateEvent, this);
        }
        public void ProcessEvent(GameEvent gameEvent) {
            return;
        }

       
        public void TestInitialState() {
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>());
        }

        [Test]
        public void TestEventGamePaused() {
            GalagaBus.GetBus().RegisterEvent(
                new GameEvent{
                    EventType = GameEventType.GameStateEvent,
                    Message = "CHANGE_STATE",
                    StringArg1 = "GAME_PAUSED"
                }
            );
            GalagaBus.GetBus().ProcessEventsSequentially();
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<GamePaused>());
        }
    }
}