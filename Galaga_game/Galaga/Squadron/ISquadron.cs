using DIKUArcade.Entities;

/// <summary>
/// Squadron interface.
/// </summary>

namespace Galaga.Squadron {
    public interface ISquadron {
        EntityContainer<Enemy> Enemies {get;}
        int MaxEnemies {get;}
    }
}