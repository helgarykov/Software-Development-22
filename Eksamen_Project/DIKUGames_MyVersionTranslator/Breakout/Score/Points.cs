using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;

namespace Breakout {
    /// <summary>
    /// Point class.
    /// </summary>
    public class Points : IGameEventProcessor {
        /// <Summary>
        /// Player points that will be displayed.
        /// </summary>
        public int points {get; set;}
        private Text display;
        /// <summary>
        /// The constructor of the points class.
        /// </summary> 
        /// <param name = "position"> the position of the score. </param>
        /// <param name = "extent"> the extent of the score. </param>
        public Points (Vec2F position, Vec2F extent) {
            points = 0;
            display = new Text (points.ToString(), position, extent);
            display.SetColor(new Vec3F(1f, 1f, 1f));
            display.SetFont(Game.font);
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
        }
        /// <summary>
        /// Method that increases the points depending on the blocks value.
        /// </summary>
        private void AddPoints(int value) {
            points += value;
        }
        
        /// <summary>
        /// Sets color to the points.
        /// </summary>
        /// <param name="color"></param>
        public void SetColor(System.Drawing.Color color)
        {
            display.SetColor(color);
        }
        
        /// <summary>
        /// Sets font to the points.
        /// </summary>
        /// <param name="font"></param>
        public void SetFont(string font)
        {
            display.SetFont(font);
        }
        
        /// <summary>
        /// Method that renders the points.
        /// </summary>
        public void RenderPoints () {
            display.RenderText();
        }
        /// <summary> 
        /// Methods that changes the points if a block is destroyed.
        /// </summary>
        /// <param name = "gameevent"> A gameevent. </param>
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.GameStateEvent) {
                switch(gameEvent.Message) {    
                    case "BLOCK_DESTROYED":
                        AddPoints(gameEvent.IntArg1);
                        display.SetText(points.ToString());
                        break;
                }
            }
        }
    }
}
