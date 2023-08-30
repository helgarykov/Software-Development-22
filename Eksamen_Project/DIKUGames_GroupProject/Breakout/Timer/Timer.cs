using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Timers;

namespace Breakout {
    
    /// <summary>
    /// Timer class.
    /// </summary>
    public class Timer : IGameEventProcessor {
        
        /// <Summary>
        /// Timer that will be displayed.
        /// </summary>
        private double startTime;
        private double endTime;
        private Text display;
        public int remainingTime { get; private set; }
        private GameEventBus gamebus;
        
        /// <summary>
        /// The constructor of the timer class.
        /// </summary> 
        /// <param name = "position"> the position of the timer. </param>
        /// <param name = "extent"> the extent of the timer. </param>
        public Timer (Vec2F position, Vec2F extent, double EndTime) {
            startTime = StaticTimer.GetElapsedSeconds();
            endTime = EndTime;
            display = new Text (remainingTime.ToString(), position, extent);
            display.SetColor(new Vec3F(1f, 1f, 1f));
            gamebus = BreakoutBus.GetBus();
            BreakoutBus.GetBus().Subscribe(GameEventType.StatusEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
        }
        /// <summary>
        /// Method that increases the time.
        /// </summary>
        private void AddTime() {
            endTime = endTime + 60;
        }

        public void SetColor(System.Drawing.Color color)
        {
            display.SetColor(color);
        }

        public void SetFont(string font)
        {
            display.SetFont(font);
        }

        
        /// <summary>
        /// Method that renders the timer.
        /// </summary>
        public void RenderTime () {
            display.RenderText();
        }
        /// <summary>
        /// Method that updates the timer and lets the player know
        /// when time is up.
        /// </summary>
        public void Update() {
            remainingTime = (int) Math.Ceiling((endTime - (StaticTimer.GetElapsedSeconds()- startTime)));
            checkTime();
            display.SetText(remainingTime.ToString());
            display.SetFont(Game.font);
        }
        public void checkTime() {
            if (remainingTime <= 0) {
                gamebus.RegisterEvent (new GameEvent { 
                            EventType = GameEventType.GameStateEvent, 
                            From = this, 
                            Message = "GAME_OVER"
                        });
            }
        }
        private static void Pause(bool input) {
            if (input) {
                StaticTimer.PauseTimer();
            }   else {
                StaticTimer.ResumeTimer();
            }
        }
        /// <summary> 
        /// Methods that add time.
        /// </summary>
        /// <param name = "gameevent"> A gameevent. </param>
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.StatusEvent) {
                switch(gameEvent.Message) {    
                    case "ADD_TIME":
                        AddTime();
                        break;
                    case "PAUSE_TIME":
                        if (gameEvent.StringArg1 == "TRUE") {
                            Pause(true);
                        } else {
                            Pause(false);
                        }
                        break;
                }
            }
        }
    }
}