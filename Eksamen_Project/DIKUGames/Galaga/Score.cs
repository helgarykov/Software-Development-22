using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;

namespace Galaga {

    /// <summary>
    /// A score class.
    /// </summart>
    public class Score : IGameEventProcessor {
        ///<summary>
        ///The int score that will be displayed.
        ///</summary>
        public int score {get; set;}
        private Text display;
        /// <summary>
        /// The constructor of the score class.
        /// </summary> 
        /// <param name = "position"> the position of the score. </param>
        /// <param name = "extent"> the extent of the score. </param>
        public Score (Vec2F position, Vec2F extent) {
            score = 0;
            display = new Text (score.ToString(), position, extent);
            display.SetColor(new Vec3F(1f, 1f, 1f));
            GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
        }
        /// <summary>
        /// Method that increases the score by 1.
        /// </summary>
        public void AddPoints () {
            score ++;
        }

        /// <summary>
        /// Method that renders the score.
        /// </summary>
        public void RenderScore () {
            display.RenderText();
        }
        /// <summary> 
        /// Methods that changes the score if an enemy dies.
        /// </summary>
        /// <param name = "gameevent"> A gameevent. </param>
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.GameStateEvent) {
                switch(gameEvent.Message) {    
                    case "ENEMY_DEAD":
                        AddPoints();
                        display.SetText(score.ToString());
                        break;
                }
            }
        }
    }
}
