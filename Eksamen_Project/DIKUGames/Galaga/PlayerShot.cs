using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;


namespace Galaga {
    
    /// <summary>
    /// A module that represents player-shot objects.
    /// </summary>
    public class PlayerShot : Entity {
        private Entity entity;
        
        // The constructor that instantiates a player-shot object with shape and image.
        public PlayerShot(DynamicShape shape, IBaseImage image) : base(shape, image) {
            entity = new Entity(shape, image);
        }
        
        // The method to render a player-shot object on the screen.
        public void Render() {
            entity.RenderEntity();
        }
    }
    
}