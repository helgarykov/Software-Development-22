using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;


namespace Galaga {
    /// <summary>
    /// A module that represents a player object.
    /// </summary>
    public class Player : IGameEventProcessor {
        private Entity entity;
        public DynamicShape shape;
        private float moveLeft = 0f;
        private float moveRight = 0f;
        private const float MOVEMENT_SPEED = 0.01f;
        
        // A player constructor that instantiates a new player object with shape and image. 
        public Player(DynamicShape shape, IBaseImage image) {
            entity = new Entity(shape, image);
            this.shape = shape;
            Game.eventBus.Subscribe(GameEventType.PlayerEvent, this); // subscribes a player entity to GameEventBus and calls its EventProcessor().
        }
        
        // The method to render a player object on the screen.
        public void Render() {
            if (entity != null) {
                entity.RenderEntity();
            }
        }
        
        // The methods that defines the movement scope for a player entity,
        // by using its x-coordinate.
        // The player entity is allowed to move within the window width,
        // in the interval [0.0; 0.9].
        public void Move() {
                if (shape.Position.X >= 0 && shape.Position.X <= 0.9) {
                    shape.Move();
                }
                if (shape.Position.X < 0.0f) {
                    shape.Position.X = 0.0f;
                }
                if (shape.Position.X > 0.9f) {
                    shape.Position.X = 0.9f;
                }
        }
        
        // The method to move the player object leftwards.
        private void SetMoveLeft(bool val) {
            if (val == true) {
                moveLeft -= MOVEMENT_SPEED; 
            }
            else {
                moveLeft = 0;
            }          
            UpdateDirection();
        }
        // The method to move the player object rightwards.  
        private void SetMoveRight(bool val) {
            if (val == true) {
                moveRight += MOVEMENT_SPEED;
            } 
            else {
                moveRight = 0;
            }
            UpdateDirection();
        }
        
        // The method to update the current position of the player
        // after it had moved to the left or to the right.
        private void UpdateDirection() {
            shape.Direction.X = moveLeft + moveRight;            
        }
        
        //The method that implements the IGameEventProcessor and its method.
        // If the gameEvent matches that of the player object, 
        // we can call player's move methods above (right, left, etc.)
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.PlayerEvent) {
                switch(gameEvent.Message) {    
                    case "MOVE_LEFT":
                        SetMoveLeft(true);
                        break;
                    case "MOVE_RIGHT":
                        SetMoveRight(true);
                        break;
                    case "STOP_MOVE_LEFT":
                        SetMoveLeft(false);
                        break;
                    case "STOP_MOVE_RIGHT":
                        SetMoveRight(false);
                        break;
                }
            }
        }
        
        // The method to remove the player object from the screen 
        // when Game.GameOver() is being called.
        public void ClearPlayer() {
            entity = null;
        }
    }
}