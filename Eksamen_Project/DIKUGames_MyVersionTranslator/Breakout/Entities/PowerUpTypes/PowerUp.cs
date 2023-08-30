using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout.PowerUpTypes;


namespace Breakout {
    
    /// <summary>
    /// A module that represents falling PowerUp entities.
    /// </summary>
    public class PowerUp : Entity {
        private Entity entity;
        public PowerUpType powerUp {get;}
        
        // The constructor of PowerUp creates the entity based on the given position and image, as well
        // as saves the provided powerUp type in a field.
        public PowerUp(DynamicShape shape, IBaseImage image, PowerUpType powerup) : base(shape, image) {
            entity = new Entity(shape, image);
            powerUp = powerup;
        }
        
        // The method to render a PowerUp object on the screen.
        public void Render() {
            entity.RenderEntity();
        }
        // PowerUp entites repeatingly moves downwards
        public void Move() {
            entity.Shape.MoveY(-0.005f);
            entity.Shape.AsDynamicShape().ChangeDirection(new Vec2F(0f, -0.005f));
        }
        // This method deletes the entity if it is outside of the screen.
        private void insideScreen() {
            if (entity.Shape.Position.Y <= 0f) {
                DeleteEntity();
            } 
        }
        // This method allows the PowerUp to be deleted by other classes.
        public void deletePowerUp() {
            DeleteEntity();
        }
        // This method is repeatingly called by DIKUArcade.
        public void Update() {
            Move();
            insideScreen();
        }
    }
    
}