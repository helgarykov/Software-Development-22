using DIKUArcade.Entities;
using System;

namespace Galaga.MovementStrategy {
    /// <summary>
    ///  Represents a complex zigzag moving strategy for a squadron of enemy entities. 
    /// </summary>
    public class StrategyZigZagDown : IMovementStrategy {
        //The method to move a single object in a zigzag manner by changing its x- and y-coordinates.
        public void moveEnemy (Enemy enemy) {
            var p = -0.045f;
            var s = -0.0003f;
            var a = -0.05f;
            enemy.Shape.Position.Y = enemy.Shape.Position.Y + s; // moves an object's position by changing its y-coordinate
            enemy.Shape.Position.X = enemy.init_X + a*(float)(Math.Sin(Convert.ToDouble((2*Math.PI*(enemy.init_Y-enemy.Shape.Position.Y))/p))); // moves an object's position by changing its x-koordinate
            // the object's change of position in a zigzag way when speed goes up.
            if (enemy.movementspeed >= 1.5f) {
                enemy.Shape.Position.Y = enemy.Shape.Position.Y + s;
                enemy.Shape.Position.X = enemy.init_X + a*(float)(Math.Sin(Convert.ToDouble((2*Math.PI*(enemy.init_Y-enemy.Shape.Position.Y))/p)));
            }       
        }
        // The method that implements Iterate() through a list of enemy objects
        // and calls moveEnemy() to move all of them in a zigzag manner.
        public void moveEnemies (EntityContainer<Enemy> enemies) {
            enemies.Iterate(enemy => {if (enemy.Shape.Position.Y > 0) {
                moveEnemy(enemy);
                }   
            });
        }
    }
}