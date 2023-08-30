using DIKUArcade.Entities;
namespace Galaga.MovementStrategy {
    
    /// <summary>
    /// Represents a simple moving-down strategy for a squadron of enemy entities. 
    /// </summary>
    public class StrategyDown : IMovementStrategy {
        // The method that moves a single enemy object down on the Y-axis. 
        public void moveEnemy (Enemy enemy) {
            enemy.Shape.MoveY(-0.0005f * enemy.movementspeed);
        }
        // The method that implements Iterate() through a list of enemy objects
        // and calls moveEnemy() to move all of them downwards.
        public void moveEnemies (EntityContainer<Enemy> enemies) {
            enemies.Iterate(enemy => {if (enemy.Shape.Position.Y > 0) {
                moveEnemy(enemy);
                }   
            });
        }
    }
}