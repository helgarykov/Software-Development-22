using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using DIKUArcade.Timers;


namespace Breakout {
    /// <summary>
    /// A module that represents a player object.
    /// </summary>
    public class Player : Entity, IGameEventProcessor {
        private float moveLeft = 0f;
        private float moveRight = 0f;
        private float MOVEMENT_SPEED;
        private int remainingTime;
        private int endTime;
        private int startTime;
        public Entity entity {get;}
        
        /// <summary>
        /// A player constructor that instantiates a new player object with shape and image.
        /// </summary>
        public Player(DynamicShape shape, IBaseImage image) : base(shape, image) {
            entity = new Entity(shape, image);
            remainingTime = 0;
            endTime = 0;
            startTime = 0;
            MOVEMENT_SPEED = 0.01f;
            BreakoutBus.GetBus().Subscribe(GameEventType.PlayerEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.StatusEvent, this);
        }
        
        /// <summary>
        /// The method to a render a player object on the screen.
        /// </summary>
        public void Render() {
            this.RenderEntity();
        }
        public void Update() {
            if (endTime != 0) {
                remainingTime = (int) Math.Ceiling((endTime - (StaticTimer.GetElapsedSeconds()- startTime)));
            }
            if (remainingTime > 0) {
                MOVEMENT_SPEED = 0.02f;
            } else {
                MOVEMENT_SPEED = 0.01f;
                endTime = 0;
                startTime = 0;
            }
        }
        
        /// <summary>
        /// Method that defines the movement scope for a player entity by using its x-coordinate.
        /// The player is allowed to move within the window width.
        /// </summary>
        public void Move() {
                if (entity.Shape.Position.X >= 0 && entity.Shape.Position.X <= 0.9) {
                    entity.Shape.Move();
                }
                if (entity.Shape.Position.X < 0.0f) {
                    entity.Shape.Position.X = 0.0f;
                }
                if (entity.Shape.Position.X > 0.82f) {
                    entity.Shape.Position.X = 0.82f;
                }
        }
        
        /// <summary>
        /// Method that sets the player's direction to left.
        /// </summary>
        
        private void SetMoveLeft(bool val) {
            if (val == true) {
                moveLeft -= MOVEMENT_SPEED; 
            }
            else {
                moveLeft = 0;
            }          
            UpdateDirection();
        }
        
        /// <summary>
        /// Method that sets the player's direction to right.
        /// </summary>
        private void SetMoveRight(bool val) {
            if (val == true) {
                moveRight += MOVEMENT_SPEED;
            } 
            else {
                moveRight = 0;
            }
            UpdateDirection();
        }
        
        /// <summary>
        /// Method updates player's direction.
        /// </summary>
        private void UpdateDirection() {
            entity.Shape.AsDynamicShape().Direction.X = moveLeft + moveRight;            
        }
        private void Pause(bool input) {
            if (input == true) {
                StaticTimer.PauseTimer();
            }   else {
                StaticTimer.ResumeTimer();
            }
        }
        
        /// <summary>
        /// Method that implements the IGameEventProcessor and its methods.
        /// </summary>
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
            if (gameEvent.EventType == GameEventType.StatusEvent) {
                switch(gameEvent.Message) {
                    case "EXTRA_SPEED":
                        startTime = (int) Math.Ceiling(StaticTimer.GetElapsedSeconds());
                        endTime = startTime + 20;
                        break;
                    case "PAUSE_TIME":
                        if (gameEvent.StringArg1 == "TRUE") {
                            Pause(true);
                        } else {
                            Pause(false);
                        }
                        break;  
                }
            }
        }
    }
}