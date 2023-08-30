using DIKUArcade.Events;

namespace Galaga {
    public static class GalagaBus {
        private static GameEventBus eventbus;

        ///<Summary> This function checks if a Galagabus instance already exists. If it does, then it returns
        /// that instance. Otherwise, it creates a new instance, and returns that.  </summary>
        ///<returns> An Eventbus <returns>
        public static GameEventBus GetBus() {
            return GalagaBus.eventbus ?? (GalagaBus.eventbus = new GameEventBus());
        }
    }
}