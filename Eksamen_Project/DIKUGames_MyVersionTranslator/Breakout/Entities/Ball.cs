using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;



namespace Breakout {
    /// <summary>
    /// Ball class.
    /// </summary>
    public class Ball : Entity, IGameEventProcessor {
        public Entity entity {get;}
        public bool playing; 
        public int direction {get; set;}
        private IBaseImage ballImage;
        private IBaseImage fireBallImage;
        private GameEventBus gamebus;
        
        /// <summary> 
        /// The constructor that instantiates a ball with shape and image. 
        /// </summary>
        public Ball(DynamicShape shape, IBaseImage image) : base(shape, image) {
            entity = new Entity(shape, image);
            ballImage = image;
            gamebus = BreakoutBus.GetBus();
            var rand = new Random();
            //It is assigned an upwards angle of a random value between 80-90 degrees. If it gets exactly 90,
            //the value is changed to 89. This is due to the fact that a launch angle at 90 degrees would allow
            //the player to continously bounce the ball exactly upwards, if they do not move the paddle.
            var value = 80 + rand.Next(1,20);
            if (value == 90) {value = 89;}
            direction = value;
            fireBallImage = new ImageStride(80, ImageStride.CreateStrides(8, Path.Combine("Assets", "Images", "FireBallStrides.png")));
            BreakoutBus.GetBus().Subscribe(GameEventType.StatusEvent, this);
        }
        
        /// <summary>
        /// Method that defines the movement scope for a ball entity by using its x-coordinate and y-coordinate.
        /// </summary>
        private void Move() {
            if (playing == true) {
                entity.Shape.MoveX((float)(0.01*Math.Cos((double)direction*Math.PI/180)));
                entity.Shape.MoveY((float)(0.01*Math.Sin((double)direction*Math.PI/180)));
                entity.Shape.AsDynamicShape().ChangeDirection(new Vec2F((float)(0.01*Math.Cos((double)direction*Math.PI/180)), 
                                                                (float)(0.01*Math.Sin((double)direction*Math.PI/180))));
            }
        }

        /// <summary>
        /// The method to render a ball on the screen.
        /// </summary>
        public void Render() {
            entity.RenderEntity();
        }
        public void SetPositionX(float x) {
            if (playing == false) {
                entity.Shape.Position.X = x;
            }
        }

        /// <summary>
        /// Method to update a ball.
        /// </summary>
        public void Update() {
            Move();
            insideScreen();
        }
        /// <summary>
        /// Method that changes the image of the ordinary ball
        /// into a fireball
        /// </summary>
        private void ChangeStridestoFireBall(bool input) {
            if (input == true) {
                entity.Image = fireBallImage;
            } if (input == false) {
                entity.Image = ballImage;
            }
        }
        public void insideScreen() {
            if (entity.Shape.Position.Y <= 0f) {
                DeleteEntity();
                gamebus.RegisterEvent (new GameEvent { 
                    EventType = GameEventType.GameStateEvent, 
                    From = this, 
                    Message = "ALL_BALLS_OUT", 
                    IntArg1 = -1
            });
            } 
        }
        
        /// <summary>
        /// Method that checks if the fireball has hit the paddle. If not,
        /// then it remains a fireball. If it has hit the paddle, the
        /// ball is turned back into a regular ball.
        /// If the gameEvent message is "SHOOT", the player shoots out both a ball and a rocket.
        /// </summary>
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.StatusEvent) {
                switch(gameEvent.Message) {    
                    case "FIRE_BALL":
                        if (gameEvent.StringArg1 == "TRUE") {
                            ChangeStridestoFireBall(true);
                        } else {
                            ChangeStridestoFireBall(false);
                        }
                        break;
                    case "SHOOT":
                        playing = true;
                        break;
                }
            }
        }
    }
    
}