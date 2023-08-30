using NUnit.Framework;
using Galaga.GalagaStates;

namespace galagatests {
    public class TestStateTranformer
    {

        [Test]
        public void TestTransformStringToState()
        {
            GameStateType one = StateTransformer.TransformStringToState("GAME_RUNNING");
            GameStateType two = StateTransformer.TransformStringToState("GAME_PAUSED");
            GameStateType three = StateTransformer.TransformStringToState("MAIN_MENU");
            bool test = (one == GameStateType.GameRunning)
                        && (two == GameStateType.GamePaused)
                        && (three == GameStateType.MainMenu);

            Assert.True(test);
        }

        [Test]
        public void TestTransformStateToString()
        {
            string one = StateTransformer.TransformStateToString(GameStateType.GameRunning);
            string two = StateTransformer.TransformStateToString(GameStateType.GamePaused);
            string three = StateTransformer.TransformStateToString(GameStateType.MainMenu);
            bool test = (one == "GAME_RUNNING")
                        && (two == "GAME_PAUSED")
                        && (three == "MAIN_MENU");

            Assert.True(test);
        }
    }
}