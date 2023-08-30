using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using Galaga.GalagaStates;
using System;


namespace Galaga {
    /// <summary>
    /// A module that represents enemy objects.
    /// </summary>
    public class Enemy : Entity, IGameEventProcessor {
        private Entity entity; 
        public int hitpoints {get; set;}
        public float movementspeed {get; set;}
        private IBaseImage greenImages;
        private IBaseImage redImages;
        public float init_X {get;}
        public float init_Y {get;}
        
        // An enemy constructor that instantiates a new enemy object with shape and greenImage. 
        public Enemy(DynamicShape shape, IBaseImage greenImage, IBaseImage redImage) : base(shape, greenImage) {
            entity = new Entity(shape, greenImage);
            hitpoints = 10;
            greenImages = greenImage;
            redImages = redImage;
            movementspeed = 1.0f + ((float)GameRunning.roundNO)/10f;
            init_X = shape.Position.X;
            init_Y = shape.Position.Y;
            GalagaBus.GetBus().Subscribe(GameEventType.ControlEvent, this); // subscribes an enemy object to GameEventBus
        }
        // The method to render an enemy object on the screen.
        public void Render() {
            entity.RenderEntity();
        }
        // The method that implements the IGameEventProcessor and its method.
        // If the gameEvent is that of the enemy object, send the message "HIT_ENEMY".
        // If gaveEvent matches the enemy, call beenHit(). 
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.ControlEvent) {
                switch(gameEvent.Message) {    
                    case "HIT_ENEMY":
                        if (gameEvent.ObjectArg1 == this) {
                            beenHit(gameEvent.IntArg1);
                        }
                        break;
                }
            }
        }
        // The method that implements the logic behind hitting an enemy object.
        // After having received 7 hit-points, increases enemy speed with 4 and
        // changes its image to enraged (red).
        // After having received max hit-points, calls DeleteEntity(), AddExplosion()
        // and calls RegisterEvent() and increases score with 1.
        private void beenHit(int damage) {
            hitpoints = hitpoints - damage;
            if (hitpoints < 3 ) {
                entity.Image = this.redImages;
                movementspeed = movementspeed*4;
            } if (hitpoints < 0) {
                DeleteEntity();
                GameRunning.AddExplosion(entity.Shape.Position, entity.Shape.Extent);
                GalagaBus.GetBus().RegisterEvent (new GameEvent { 
                            EventType = GameEventType.GameStateEvent, From = this, Message = "ENEMY_DEAD", StringArg1 = "", IntArg1 = 1 
                        });
            }
        }
    }
}