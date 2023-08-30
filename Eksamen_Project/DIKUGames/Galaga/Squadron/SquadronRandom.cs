using System.Collections.Generic;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using System;

namespace Galaga.Squadron {

    /// <summary>
    /// SquadronRandom class.
    /// </summary>
    public class SquadronRandom : ISquadron {
        private EntityContainer<Enemy> enemies;
        public int MaxEnemies {get;}
        private List<Image> blueStrides;
        private List<Image> redStrides;
        /// <summary>
        /// Constructor of the SquadronRandom class.
        /// </summary>
        public SquadronRandom(List<Image> enemyStride, List<Image> alternativeEnemyStride) {
            MaxEnemies = 8;
            blueStrides = enemyStride;
            redStrides = alternativeEnemyStride;
            enemies = new EntityContainer<Enemy>(MaxEnemies);

            /// <summary>
            /// Method that creates random enemies in 3 lines.
            /// </summary>
            void CreateEnemies(List<Image> blueStrides, List<Image> redStrides) {
                var rand = new Random();
                for (int i = 0; i <MaxEnemies; i++) {
                    if (rand.Next(1, 11) <= 5) {
                        enemies.AddEntity(new Enemy(
                            new DynamicShape(new Vec2F(0.1F + (float)i * 0.1f, 0.9f), new Vec2F(0.1f, 0.1f)),
                            new ImageStride(80, blueStrides), new ImageStride(80, redStrides)));
                    }
                }
                for (int i = 0; i <MaxEnemies; i++) {
                    if (rand.Next(1, 11) <= 5) {
                        enemies.AddEntity(new Enemy(
                            new DynamicShape(new Vec2F(0.1F + (float)i * 0.1f, 0.8f), new Vec2F(0.1f, 0.1f)),
                            new ImageStride(80, blueStrides), new ImageStride(80, redStrides)));
                    }
                }
                for (int i = 0; i <MaxEnemies; i++) {
                    if (rand.Next(1, 11) <= 5) {
                        enemies.AddEntity(new Enemy(
                            new DynamicShape(new Vec2F(0.1F + (float)i * 0.1f, 0.7f), new Vec2F(0.1f, 0.1f)),
                            new ImageStride(80, blueStrides), new ImageStride(80, redStrides)));
                    }
                }
            }
            CreateEnemies(blueStrides, redStrides);
        }
        public EntityContainer<Enemy> Enemies {
            get {return enemies;}
        }
        
    }
}