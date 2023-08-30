using DIKUArcade.GUI;


namespace Breakout {
    class Program {
        static void Main(string[] args) {
             var windowArgs = new WindowArgs() { Title = "Breakout v0.1" };
             windowArgs.Width = 700U;
             windowArgs.Height = 700U;

             var game = new Game(windowArgs);

             game.Run();
        }
    }
}