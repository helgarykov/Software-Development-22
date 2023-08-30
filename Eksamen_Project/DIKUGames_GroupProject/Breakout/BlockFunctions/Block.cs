using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using Breakout.BlockFunctions;
using Breakout.PowerUpTypes;
using DIKUArcade.Math;


namespace Breakout {
    /// <summary>
    /// Block class contains all types of blocks.
    /// </summary>
    public class Block : Entity, IGameEventProcessor {
        private Entity entity;
        private IBaseImage image;
        private IBaseImage damagedImage;
        public int hitpoints {get; set;}
        private BlockType type;
        private PowerUpType? powerUp;
        private Entity? powerUpIcon;

        /// <summary>
        /// Block constructor that instantiates a new block object with shape, image and type. 
        /// </summary>
        public Block(DynamicShape shape, IBaseImage image1, IBaseImage damagedImage1, BlockType typeInput) : base(shape, image1) {
            entity = new Entity(shape, image1);
            hitpoints = 10;
            BreakoutBus.GetBus().Subscribe(GameEventType.ControlEvent, this); 
            type = typeInput;
            image = image1;
            damagedImage = damagedImage1;
            if (typeInput == BlockType.PowerUp) {
                powerUp = rollPowerUp();
                generatePowerUpIcon(powerUp);
            }
        }
        
        ///<Summary> This function is periodically called by DIKUArcade. It renders the block entity on the screen.
        /// It also renders the powerUpIcon inside the block </summary>
        public void Render() {
            entity.RenderEntity();
            if (powerUpIcon != null) {
                powerUpIcon.RenderEntity();
            }
        }

        ///<Summary> This function is periodically called by DIKUArcade. It checks for the event of the Block reaching
        /// below 0 hitpoints. In that case, it uses the EventBus to grant points, and spawn a falling PowerUp, before
        /// deleting the entity. </summary>
        public void Update() {
            if (hitpoints <= 0) {
                switch(type) {
                    case BlockType.Standard:
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                        EventType = GameEventType.GameStateEvent, From = this, Message = "BLOCK_DESTROYED", StringArg1 = "", IntArg1 = 1
                        });
                        break;
                    case BlockType.Hardened:
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                        EventType = GameEventType.GameStateEvent, From = this, Message = "BLOCK_DESTROYED", StringArg1 = "", IntArg1 = 2
                        });
                        break;
                    case BlockType.PowerUp:
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                        EventType = GameEventType.GameStateEvent, From = this, Message = "BLOCK_DESTROYED", StringArg1 = "", IntArg1 = 1
                        });
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                        EventType = GameEventType.StatusEvent, From = this, Message = "SPAWN_POWERUP", ObjectArg1 = powerUp, StringArg1 = (entity.Shape.Position.X + entity.Shape.Extent.X / 2).ToString("0.000"), StringArg2 = (entity.Shape.Position.Y + entity.Shape.Extent.Y / 2).ToString("0.000")
                        });
                        break;
                }
                DeleteEntity();
            }
        }
        
        /// <summary>
        /// Method that implements IGameEventProcessor.
        /// </summary>
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.ControlEvent) {
                switch(gameEvent.Message) {    
                    case "HIT_BLOCK":
                        if (gameEvent.ObjectArg1 == this) {
                            beenHit(gameEvent.IntArg1);
                        }
                        break;
                } 
            }
        }
        
        ///<Summary> Uses a switch case to reduce or prevent damage based on the block-type. </summary>
        ///<param name = "damage"> Damage is an integer input that represents the amount of damage to substract </param>
        private void beenHit(int damage) {
            switch (type) {
                case BlockType.Hardened:
                    hitpoints = hitpoints - (int) damage/2;
                    entity.Image = this.damagedImage; 
                    break;
                case BlockType.Unbreakable:
                    hitpoints = hitpoints;
                    break;
                default:
                    hitpoints = hitpoints - damage;
                    break;
            } 
        }
        /// <summary>
        /// Method for choosing a random power-up for a block which
        /// is of the type "PowerUpType". The default case should never be reached.
        /// </summary>
        private PowerUpType rollPowerUp() {
            var rand = new Random();
            var value = rand.Next(1,6);
            switch (value) {
                //Extra Life
                case 1:
                    return PowerUpType.ExtraLife;
                //More Time
                case 2:
                    return PowerUpType.MoreTime;
                //Rocket
                case 3:
                    return PowerUpType.Rocket;
                //Split
                case 4:
                    return PowerUpType.ExtraSpeed;
                //Hard Ball
                case 5:
                    return PowerUpType.HardBall;
                default:
                    return PowerUpType.ExtraLife;
            }
        }
        ///<Summary> This function instantiates a Entity located inside the Block, that shows an image based on the PowerUpType. </summary>
        ///<param name = "powerUp"> powerUp represents a PowerUpType. </param>
        private void generatePowerUpIcon(PowerUpType? powerUp) {
            switch (powerUp) {
                //Extra Life
                case PowerUpType.ExtraLife:
                    powerUpIcon = new Entity(new DynamicShape(new Vec2F(entity.Shape.Position.X+0.01f, entity.Shape.Position.Y+0.01f),
                                    new Vec2F(0.033f, 0.033f)), Game.ExtraLifeImage);
                    break;
                //More Time
                case PowerUpType.MoreTime:
                    powerUpIcon = new Entity(new DynamicShape(new Vec2F(entity.Shape.Position.X+0.01f, entity.Shape.Position.Y+0.01f),
                                    new Vec2F(0.033f, 0.033f)), Game.MoreTimeImage);
                    break;
                //Rocket
                case PowerUpType.Rocket:
                    powerUpIcon = new Entity(new DynamicShape(new Vec2F(entity.Shape.Position.X+0.01f, entity.Shape.Position.Y+0.01f),
                                    new Vec2F(0.033f, 0.033f)), Game.RocketImage);
                    break;
                //Split
                case PowerUpType.ExtraSpeed:
                    powerUpIcon = new Entity(new DynamicShape(new Vec2F(entity.Shape.Position.X+0.01f, entity.Shape.Position.Y+0.01f),
                                    new Vec2F(0.033f, 0.033f)), Game.ExtraSpeedImage);
                    break;
                //FireBall (hardBall)
                case PowerUpType.HardBall:
                    powerUpIcon = new Entity(new DynamicShape(new Vec2F(entity.Shape.Position.X+0.01f, entity.Shape.Position.Y+0.01f),
                                    new Vec2F(0.033f, 0.033f)), Game.HardBallImage);
                    break;
            }
        }
    }
}