using DIKUArcade.Entities;
using DIKUArcade.Events;
using Breakout;
using NUnit.Framework;

namespace BreakoutTests
{
    [TestFixture]
    public class TestPowerUpDistributor
    {
        private IGameEventProcessor eventProcessor;
        private GameEventBus gameBus;
        private GameEvent gameEvent;
        private PowerUp powerUp1;
        private PowerUp powerUp2;
        private EntityContainer<PowerUp>? _powerups;

        [SetUp]
        public void InitiatePowerDistributor()
        {
            gameBus = BreakoutBus.GetBus();
            BreakoutBus.GetBus().Subscribe(GameEventType.StatusEvent, eventProcessor);
        }

    }
}