using DIKUArcade.Entities;
using Galaga.Squadron;

namespace Galaga.MovementStrategy {
    
    /// <summary>
    /// Interface for squadron classes to implement move methods.
    /// </summary>
    public interface IMovementStrategy {
        void moveEnemy (Enemy enemy);
        void moveEnemies (EntityContainer<Enemy> enemies);
    }
}