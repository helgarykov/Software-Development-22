using System.Collections.Generic;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Math;

namespace Galaga.Squadron {
    /// <summary> 
    /// SquadronLine class.
    /// </summary>
    public class SquadronLine : ISquadron {
        private EntityContainer<Enemy> enemies;
        public int MaxEnemies {get;}
        private List<Image> blueStrides;
        private List<Image> redStrides;

        /// <summary>
        /// Constructor of the SquadronLine class.
        /// </summary>
        public SquadronLine(List<Image> enemyStride, List<Image> alternativeEnemyStride) {
            MaxEnemies = 8;
            blueStrides = enemyStride;
            redStrides = alternativeEnemyStride;
            enemies = new EntityContainer<Enemy>(MaxEnemies);
            /// <summary>
            /// Method that creates enemies in a line.
            /// </summary> 
            void CreateEnemies(List<Image> blueStrides, List<Image> redStrides) {
                for (int i = 0; i <MaxEnemies; i++) {
                    enemies.AddEntity(new Enemy(
                        new DynamicShape(new Vec2F(0.1F + (float)i * 0.1f, 0.9f), new Vec2F(0.1f, 0.1f)),
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