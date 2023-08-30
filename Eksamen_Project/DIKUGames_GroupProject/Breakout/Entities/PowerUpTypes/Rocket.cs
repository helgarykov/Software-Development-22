using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;


namespace Breakout {
    
    /// <summary>
    /// A class that represents Rocket objects.
    /// </summary>
    public class Rocket : Entity {
        private Entity entity;
        
        // The constructor that instantiates a Rocket object with shape and image.
        public Rocket(DynamicShape shape, IBaseImage? image) : base(shape, image) {
            entity = new Entity(shape, image);
        }
        
        // The method to render a Rocket object on the screen.
        public void Render() {
            entity.RenderEntity();
        }
        //The method to make a Rocket moveable in a vertical direction.
        public void Move() {
            entity.Shape.MoveY(0.01f);
            entity.Shape.AsDynamicShape().ChangeDirection(new Vec2F(0f, 0.01f));
        }
    }
    
}