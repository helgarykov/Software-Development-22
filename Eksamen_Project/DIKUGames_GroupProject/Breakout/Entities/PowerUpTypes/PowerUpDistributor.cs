using DIKUArcade.Events;
using Breakout.PowerUpTypes;



namespace Breakout {
    
    /// <summary>
    /// A class that distributes power-ups.
    /// </summary>
    public class PowerUpDistributor : IGameEventProcessor {

        private GameEventBus gamebus;
        
        /// <summary>
        /// Constructor that subscribes the BreakoutBus to events of the type "StatusEvent"
        /// </summary>
        public PowerUpDistributor(){
            gamebus = BreakoutBus.GetBus();
            BreakoutBus.GetBus().Subscribe(GameEventType.StatusEvent, this);
        }
        /// <summary>
        /// The method for registering each power-up as an event in the BreakoutBus. 
        /// </summary>
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.StatusEvent && gameEvent.Message == "GRANT_POWERUP") {
                switch(gameEvent.ObjectArg1) {    
                    case PowerUpType.ExtraLife:
                            gamebus.RegisterEvent (new GameEvent { 
                            EventType = GameEventType.StatusEvent, From = this, Message = "GRANT_LIFE", IntArg1 = 1, StringArg2 = "" 
                            });
                        break;
                    case PowerUpType.HardBall:
                            gamebus.RegisterEvent (new GameEvent { 
                            EventType = GameEventType.StatusEvent, From = this, Message = "FIRE_BALL", StringArg1 = "TRUE", StringArg2 = "" 
                            });
                        break;
                    case PowerUpType.MoreTime:
                            gamebus.RegisterEvent (new GameEvent { 
                            EventType = GameEventType.StatusEvent, From = this, Message = "ADD_TIME", StringArg1 = "", StringArg2 = "" 
                            });
                        break;
                    case PowerUpType.Rocket:
                            gamebus.RegisterEvent (new GameEvent { 
                            EventType = GameEventType.StatusEvent, From = this, Message = "ADD_ROCKET", StringArg1 = "", StringArg2 = "" 
                            });
                        break;
                    case PowerUpType.ExtraSpeed:
                        gamebus.RegisterEvent (new GameEvent { 
                        EventType = GameEventType.StatusEvent, From = this, Message = "EXTRA_SPEED"
                        });
                        break;
                }
            }
        }
    }
}