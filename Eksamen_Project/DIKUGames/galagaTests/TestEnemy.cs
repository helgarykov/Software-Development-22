using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Math;
using Galaga;
using OpenTK.Windowing.Common;
using Galaga.GalagaStates;

namespace galagatests {


    [TestFixture]
    public class TestEnemy : IGameEventProcessor {
        private Enemy enemy;
        private GameEventBus gamebus;
        

        [OneTimeSetUp]
        public void InitiateEnemy() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            gamebus = GalagaBus.GetBus();
            //gamebus.InitializeEventBus(new List<DIKUArcade.Events.GameEventType> { GameEventType.ControlEvent, GameEventType.GameStateEvent });
            var ds = new DynamicShape(new Vec2F(0.0f, 0.1f), new Vec2F(0.1f, 0.1f));
            var img = new Image(Path.Combine("Assets", "Images", "BlueMonster.png"));
            var img2 = new Image(Path.Combine("Assets", "Images", "RedMonster.png"));
            enemy = new Enemy(ds, img, img2);
        }
        
        
        [Test]
        public void TestEnemyList() {
           List<Enemy> enemyList = new List<Enemy>();
           enemyList.Add(enemy);
            Assert.AreEqual(enemy, enemyList[0]);
            Assert.True(1 == enemyList.Count);
        }

        [Test]
        public void TestDamage() {
            // 0 dmg test
            Assert.AreEqual(enemy.hitpoints, 10);
            gamebus.RegisterEvent (new GameEvent { 
                        EventType = GameEventType.ControlEvent, From = this, Message = "ENEMY_HIT", ObjectArg1 = enemy, IntArg1 = 0 
                        });
            Assert.AreEqual(enemy.hitpoints, 9);
            // low dmg test
            gamebus.RegisterEvent (new GameEvent { 
                            EventType = GameEventType.ControlEvent, From = this, Message = "ENEMY_HIT", ObjectArg1 = enemy, IntArg1 = 1 
                        });
            Assert.AreEqual(enemy.hitpoints, 9);
            // high dmg test
            gamebus.RegisterEvent (new GameEvent { 
                            EventType = GameEventType.ControlEvent, From = this, Message = "ENEMY_HIT", ObjectArg1 = enemy, IntArg1 = 8 
                        });
            Assert.AreEqual(enemy.hitpoints, 1);
            // overkill test
            gamebus.RegisterEvent (new GameEvent { 
                            EventType = GameEventType.ControlEvent, From = this, Message = "ENEMY_HIT", ObjectArg1 = enemy, IntArg1 = 5 
                        });
            Assert.AreEqual(enemy.hitpoints, -4);
        }

        [Test]
        public void TestStrategyZigZagDown() {

        }

        public void ProcessEvent(GameEvent gameEvent) {
            return;
        }
    } 
}

