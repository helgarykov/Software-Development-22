namespace Breakout.GameStates;
using DIKUArcade.Events;
using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Input;
using System.IO;
/// <summary>
/// Class responsible for the state the game is in when it is paused.
/// </summary>
public class GamePaused : IBreakoutGameState, IGameEventProcessor {
    public GameStateType ActiveState { get; set; } = GameStateType.GamePaused;
    
    public SelectedAction SelectedAction { get; set; }
    private GameEventBus gamebus;

    private List<Text> gamePausedText;
    private Image background;

    public GamePaused()
    {
        gamebus = BreakoutBus.GetBus();
        background = new Image(Path.Combine("Assets", "Images", "Paused.png"));
        SelectedAction = SelectedAction.PauseGame;
        BreakoutBus.GetBus().Subscribe(GameEventType.StatusEvent, this);
        gamePausedText = new List<Text>(50);
        InitializeText();
    }
    /// <summary>
    /// Method for creating the text which tells the player that the game is paused.
    /// </summary>
    private void InitializeText()
    {
        gamePausedText.Add(new Text ("Game", new Vec2F(Game.rightX / 2 - 0.3f, Game.topY - 0.6f), new Vec2F(0.5f, 0.5f)));
        gamePausedText.Add(new Text ("Paused", new Vec2F(Game.rightX / 2 - 0.05f, Game.topY - 0.6f), new Vec2F(0.5f, 0.5f)));
        readHighScore();
        foreach(var i in gamePausedText) {
            i.SetColor(System.Drawing.Color.Ivory);
            i.SetFont(Game.font);
        }  
    }
    private void readHighScore() {
        var rawlist = System.IO.File.ReadAllLines(Path.Combine("Assets", "score.txt"));
        for (var i = 0; i <= rawlist.Length - 1; i++) {
            gamePausedText.Add(new Text (rawlist[i], new Vec2F(Game.rightX / 2 - 0.117f, Game.topY - 0.45f - 0.05f*i), new Vec2F(0.25f, 0.25f)));
        }
    }
    /// <summary>
    /// Method for allowing the player to change out of the paused state.
    /// </summary>
    public GameStateType GetNextState()
    {
        if (SelectedAction == SelectedAction.StartGame) return GameStateType.GameRunning;

        return ActiveState;
    }

    public void OnEnterState(IGameState previousState)
    {
    }

    public void ResetState()
    {
    }

    public void UpdateState()
    {
    }

    /// <summary>
    /// Method for rendering the text which tells the player that the game is paused.
    /// </summary>
    public void RenderState()
    {
        StateAlignments.Transitions[GameStateType.GameRunning].RenderState();
        background.Render(new StationaryShape(new Vec2F(Game.leftX + 0.1f, Game.bottomY + 0.17f), new Vec2F(Game.rightX - 0.2f, Game.topY - 0.2f)));
        foreach(var i in gamePausedText) {
            i.RenderText();
        } 
    }

    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key)
    {
        if (action == KeyboardAction.KeyPress) {
            KeyPress(key);
        }
    }
    
    public void KeyPress(KeyboardKey key)
    {
        SelectedAction = SelectedAction.StartGame;
        gamebus.RegisterEvent (new GameEvent { 
                    EventType = GameEventType.StatusEvent, From = this, Message = "PAUSE_TIME", StringArg1 = "FALSE"
                });
    }
    public void ProcessEvent(GameEvent gameEvent){
        return;
    }
}