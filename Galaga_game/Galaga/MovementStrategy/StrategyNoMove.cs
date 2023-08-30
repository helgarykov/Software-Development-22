using DIKUArcade.Entities;

namespace Galaga.MovementStrategy {
    /// <summary>
    ///  Represents a simple no-move strategy for a squadron of enemy entities. 
    /// </summary>
    public class StrategyNoMove : IMovementStrategy {
        public void moveEnemy (Enemy enemy) {
            enemy.Shape.MoveY(enemy.Shape.Position.Y);
        }
        public void moveEnemies (EntityContainer<Enemy> enemies) {
                enemies.Iterate(enemy => {if (enemy.Shape.Position.Y > 0) {
                    moveEnemy(enemy);
                }   
            });
        }
    }
}