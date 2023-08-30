using System.Collections.Generic;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Math;

namespace Galaga.Squadron {
    /// <summary>
    /// SquadronArrow class
    /// </summary>
    public class SquadronArrow : ISquadron {
        private EntityContainer<Enemy> enemies;
        public int MaxEnemies {get;}
        private List<Image> blueStrides;
        private List<Image> redStrides;
        /// <summary> 
        /// The constructor of the squadronarrow class.
        /// </summary> 
        public SquadronArrow(List<Image> enemyStride, List<Image> alternativeEnemyStride) {
            MaxEnemies = 8;
            blueStrides = enemyStride;
            redStrides = alternativeEnemyStride;
            enemies = new EntityContainer<Enemy>(MaxEnemies);
            /// <summary>
            /// Method that creates enemies in an arrow formation.
            /// </summary>
            void CreateEnemies(List<Image> blueStrides, List<Image> redStrides) {
                for (int i = 0; i < MaxEnemies/2; i++) {
                    enemies.AddEntity(new Enemy(
                        new DynamicShape(new Vec2F(0.1F + (float)i * 0.1f, 0.9f-(float)i*0.05f), new Vec2F(0.1f, 0.1f)),
                        new ImageStride(80, blueStrides), new ImageStride(80, redStrides)));
                }
                for (int j = MaxEnemies/2; j < MaxEnemies; j++) {
                    enemies.AddEntity(new Enemy(
                        new DynamicShape(new Vec2F(0.1F + (float)j * 0.1f, (0.9f-(float)MaxEnemies/2*0.05f)+(float)(j-MaxEnemies/2)*0.05f+0.05f), new Vec2F(0.1f, 0.1f)),
                        new ImageStride(80, blueStrides), new ImageStride(80, redStrides)));
                }
            }
            CreateEnemies(blueStrides, redStrides);
        }
        public EntityContainer<Enemy> Enemies {
            get {return enemies;}
        }
        
    }
}