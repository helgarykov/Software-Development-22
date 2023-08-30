using DIKUArcade.Events;

namespace Breakout {
    public static class BreakoutBus {
        private static GameEventBus? _eventbus;
        public static GameEventBus CheckBus {get{return _eventbus;}}
        
        /// <summary>
        /// Static method that acts as a private default constructor.
        /// It returns the global instance of the bus IF IT'S has already been initiated.
        /// Otherwise - creates a new instance of the bus.
        /// Whenever that method is called, the same object is always returned.
        /// </summary>
        /// <returns>
        /// An EventBus.
        /// </returns>
        public static GameEventBus GetBus() {
            return _eventbus ??= new GameEventBus();
        }
    }
}