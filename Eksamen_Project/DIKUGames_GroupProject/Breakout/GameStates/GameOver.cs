using System.Drawing;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.State;
using Image = DIKUArcade.Graphics.Image;

namespace Breakout.GameStates;

public class GameOver : IBreakoutGameState, IGameEventProcessor {
    public GameStateType ActiveState { get; set; } = GameStateType.GameOver;
    public SelectedAction SelectedAction { get; set; }
    
    private Entity _backGroundImage;
    private Text[]? _menuButtons;

    private Text? _arrow;
    private Text? _nameText;

    private Text[]? _finalTexts;
    private Text[]? _scoreTexts;
    private HighScore? _highscore;
    private List<Text>? _nameEnterText;
    private List<Text>? _highScoreText;

    private Points? _points;

    private int _activeMenuButton;
    private int _maxMenuButtons;
    private bool _newHigh;
    private bool _nameInputMode;
    private string? _name;
    private GameEventBus _gamebus;
    private Image nameSelectBg;
    

    public GameOver() {
        _gamebus = BreakoutBus.GetBus();
        _backGroundImage = new Entity(new DynamicShape(Game.leftX, Game.bottomY, 1f, 1f),
            new Image(Path.Combine("Assets", "Images", "SpaceBackground.png")));
        nameSelectBg = new Image(Path.Combine("Assets", "Images", "NameSelect.png"));
        BreakoutBus.GetBus().Subscribe(GameEventType.ControlEvent, this);
    }

    /// <summary>
    /// Method that defines position and color of GameOver buttons and Finaltexts. It also
    /// defines the text of the Enter Name popup screen.
    /// </summary>
    private void InitializeAllText() {
        if (GameRunning.Points != null) {
            _points = new Points(new Vec2F(Game.rightX / 2 + 0.25f, Game.bottomY - 0.1f),
                new Vec2F(0.4f, 0.4f)) {
                points = GameRunning.Points.points
            };
            _points.SetColor(Color.Bisque);
            _points.SetFont(Game.font);

            _menuButtons = new Text[] {
                new("Main Menu", new Vec2F(Game.rightX / 2 - 0.15f, Game.bottomY - 0.175f), new Vec2F(0.3f, 0.3f)),
                new("Exit Game", new Vec2F(Game.rightX / 2 - 0.15f, Game.bottomY - 0.225f), new Vec2F(0.3f, 0.3f)),
            };
            foreach (var text in _menuButtons){
                text.SetColor(Color.Ivory);
                text.SetFont(Game.font);
            }

            _maxMenuButtons = _menuButtons.Length;
            _finalTexts = new Text[] {
                new("GAME", new Vec2F(Game.rightX / 2 - 0.4f, Game.topY - 0.8f), new Vec2F(0.8f, 0.8f)),
                new("OVER", new Vec2F(Game.rightX / 2 - 0.0f, Game.topY - 0.8f), new Vec2F(0.8f, 0.8f))
            };
            foreach (var text in _finalTexts) {
                text.SetColor(Color.Bisque);
                text.SetFont(Game.font);
            }

            _scoreTexts = new Text[] {
                new("FINAL", new Vec2F(Game.rightX / 2 - 0.25f, Game.bottomY - 0.1f), new Vec2F(0.4f, 0.4f)),
                new("SCORE: ", new Vec2F(Game.rightX / 2 + 0.0f, Game.bottomY - 0.1f), new Vec2F(0.4f, 0.4f)),
            };
            foreach (var text in _scoreTexts) {
                text.SetColor(Color.Bisque);
                text.SetFont(Game.font);
            }

            //Arrow created
            _arrow = new Text("> ", new Vec2F(0.27f, -0.2f), new Vec2F(0.3f, 0.3f));
            _arrow.SetColor(Color.Brown);
            _arrow.SetFont(Game.font);

            //Enter Name Text
            _nameEnterText = new List<Text>();
            _nameEnterText.Add(new Text("NEW", new Vec2F(Game.rightX / 2 - 0.35f, Game.topY - 0.633f),
                new Vec2F(0.5f, 0.5f)));
            _nameEnterText.Add(new Text("HIGHSCORE", new Vec2F(Game.rightX / 2 - 0.15f, Game.topY - 0.633f),
                new Vec2F(0.5f, 0.5f)));
            _nameEnterText.Add(new Text("Enter", new Vec2F(Game.rightX / 2 - 0.25f, Game.topY - 0.775f),
                new Vec2F(0.4f, 0.4f)));
            _nameEnterText.Add(new Text("Name", new Vec2F(Game.rightX / 2, Game.topY - 0.775f), new Vec2F(0.4f, 0.4f)));
            _nameEnterText.Add(new Text("Final", new Vec2F(Game.rightX / 2 - 0.25f, Game.topY - 0.9f),
                new Vec2F(0.3f, 0.3f)));
            _nameEnterText.Add(new Text("Score: ", new Vec2F(Game.rightX / 2 - 0.08f, Game.topY - 0.9f),
                new Vec2F(0.3f, 0.3f)));
            _nameEnterText.Add(new Text(GameRunning.Points.points.ToString() ,
                new Vec2F(Game.rightX / 2 + 0.12f, Game.topY - 0.9f), new Vec2F(0.3f, 0.3f)));
        }

        if (_nameEnterText != null)
            foreach (var i in _nameEnterText) {
                i.SetColor(Color.Bisque);
                i.SetFont(Game.font);
            }

        _nameText = new Text(_name, new Vec2F(Game.rightX / 2 - 0.05f, Game.bottomY+0.155f), new Vec2F(0.3f, 0.3f));
        _nameText.SetColor(Color.Bisque);
        _nameText.SetFont(Game.font);    
    }
    
    /// <summary>
    /// Switches from GameStateType GameOver to MainMenu or Exit game.
    /// </summary>
    /// <returns></returns>
    public GameStateType GetNextState() {
        if (SelectedAction == SelectedAction.MainMenu) return GameStateType.MainMenu;
        if (SelectedAction == SelectedAction.CloseGame) Environment.Exit(0);

        return ActiveState;
    }

    public void OnEnterState(IGameState previousState) {
        ResetState();
    }

    public void ResetState() {
        SelectedAction = SelectedAction.ExitGame;
        _gamebus = BreakoutBus.GetBus();
        _nameInputMode = false;
        _backGroundImage = new Entity(new DynamicShape(Game.leftX, Game.bottomY, 1f, 1f),
            new Image(Path.Combine("Assets", "Images", "SpaceBackground.png")));
        
        _activeMenuButton = 0;
        _newHigh = false;
        _highscore = new HighScore(GameRunning.Points);
        _name = "";
        ReadHighScore();
        InitializeAllText();
    }

    /// <summary>
    /// Method that sets activeMenuButton to brown-red and others to ivory.
    /// </summary>
    /// <param name="buttons"> Text array with two strings: MainMenu & Exit game </param>
    private void UpdateButtons(Text[]? buttons)
    {
        if (_menuButtons == null) return;
        foreach (var button in _menuButtons) {
            if (buttons != null && button == buttons[_activeMenuButton]) {
                button.SetColor(Color.Brown);
                if (_arrow != null)
                    _arrow.GetShape().Position.Y = _menuButtons[_activeMenuButton].GetShape().Position.Y;
            } else {
                button.SetColor(Color.Ivory);
            }
        }
    }

    public void UpdateState() {
        UpdateButtons(_menuButtons);
    }

    /// <summary>
    /// Renders backgroundImage, menuButtons, finalTexts and Points. If namInputMode is true,
    /// it Renders the background image of the name input popup on top of everything else.
    /// </summary>
    public void RenderState() {
        _backGroundImage.RenderEntity();
        for (var i = 0; i < _maxMenuButtons; i++) {
            if (_menuButtons != null) _menuButtons[i].RenderText();
        }

        if (_finalTexts != null)
            foreach (var t in _finalTexts) {
                t.RenderText();
            }

        _arrow?.RenderText();
        if (_highScoreText != null)
            foreach (var i in _highScoreText) {
                i.RenderText();
            }

        if (_nameInputMode) {
            nameSelectBg.Render(new StationaryShape(new Vec2F(Game.leftX + 0.1f, Game.bottomY + 0.17f), new Vec2F(Game.rightX - 0.2f, Game.topY - 0.2f)));
            foreach (var i in _nameEnterText!) {
                i.RenderText();
            }
            _nameText?.RenderText();
        } 
        if (_newHigh == false) {
            _points?.RenderPoints();
            foreach (var t in _scoreTexts!) {
                t.RenderText();
            } 
        }
    }
    
    /// <summary>
    /// Reads score.txt and adds every line of text from the file as Text in a list.
    /// </summary>
    public void ReadHighScore() {
        var rawList = File.ReadAllLines(Path.Combine("Assets", "score.txt"));
        _highScoreText = new List<Text>();
        for (var i = 0; i <= rawList.Length - 1; i++) {
            _highScoreText.Add(new Text (rawList[i], 
                new Vec2F(Game.rightX / 2 - 0.117f, Game.topY - 0.4f - 0.05f*i), 
                new Vec2F(0.25f, 0.25f)));
            _highScoreText[i].SetColor(Color.Bisque);
            _highScoreText[i].SetFont(Game.font);
        }
    }
    
    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        if (action == KeyboardAction.KeyPress) KeyPress(key);
        
    }
    
    /// <summary>
    /// At pressing Up/Down button, the game switches to the MainMenu state or Exit Game.
    /// Takes 3-letter name input if a high score is reached, and requests for score.txt to be 
    /// updated with the new name.
    /// </summary>
    /// <param name="key"></param>
    private void KeyPress(KeyboardKey key) {
        if (_nameInputMode == false) {
            switch (key) {
            case KeyboardKey.Up: {
                if (_activeMenuButton != 0)
                {
                    _activeMenuButton--;
                }

                break;
            }
            case KeyboardKey.Down: {
                if (_activeMenuButton == 0) {
                    _activeMenuButton++;
                }

                break;
            }
            case KeyboardKey.Enter:
                SelectedAction = _activeMenuButton switch {
                    0 => SelectedAction.MainMenu,
                    1 => SelectedAction.CloseGame,
                    _ => throw new ArgumentOutOfRangeException(nameof(key), key, null)
                };
                break;
        }
        } else {
            switch (key) {
            case KeyboardKey.Backspace:
                if (_name != null && _name.Length > 0) {
                    _name = _name.Remove(_name.Length-1);
                    _nameText?.SetText(_name);
                }
                break;
            // If the name input is less than three letters, it fills in the rest with space.
            case KeyboardKey.Enter:
                if (_name != null && _name.Length != 3) {
                    while (_name.Length < 3) {
                        _name = _name + " ";
                    }
                } 
                _gamebus.RegisterEvent (new GameEvent { 
                    EventType = GameEventType.ControlEvent, From = this,
                    Message = "RETURN_PLAYER_NAME",
                    StringArg1 = _name
                });
                _nameInputMode = false;
                break;
            // When a letter is pressed, the convertToChar method is utilized to make sure it is 
            // an allowed letter.
            default:
                if (_name != null && _name.Length < 3) {
                    _name += convertToChar(key);
                    _nameText?.SetText(_name);
                }
                break;  
            }
        }
    }
    
    /// <summary>
    /// This method is necessary primarily to disallow special characters and KeyBoardKeys such as 
    /// Apostrophe to be entered as player names, as this would break score.txt syntax.
    /// </summary>
    private string convertToChar(KeyboardKey key) {
        
        switch(key) {
            case KeyboardKey.Space:
                return " ";
            case KeyboardKey.Num_0:
                return "0";
            case KeyboardKey.Num_1:
                return "1";
            case KeyboardKey.Num_2:
                return "2";
            case KeyboardKey.Num_3:
                return "3";
            case KeyboardKey.Num_4:
                return "4";
            case KeyboardKey.Num_5:
                return "5";
            case KeyboardKey.Num_6:
                return "6";
            case KeyboardKey.Num_7:
                return "7";
            case KeyboardKey.Num_8:
                return "8";
            case KeyboardKey.Num_9:
                return "9";
            case KeyboardKey.A:
                return "A";
            case KeyboardKey.B:
                return "B";
            case KeyboardKey.C:
                return "C";
            case KeyboardKey.D:
                return "D";
            case KeyboardKey.E:
                return "E";
            case KeyboardKey.F:
                return "F";
            case KeyboardKey.G:
                return "G";
            case KeyboardKey.H:
                return "H";
            case KeyboardKey.I:
                return "I";
            case KeyboardKey.J:
                return "J";
            case KeyboardKey.K:
                return "K";
            case KeyboardKey.L:
                return "L";
            case KeyboardKey.M:
                return "M";
            case KeyboardKey.N:
                return "N";
            case KeyboardKey.O:
                return "O";
            case KeyboardKey.P:
                return "P";
            case KeyboardKey.Q:
                return "Q";
            case KeyboardKey.R:
                return "R";
            case KeyboardKey.S:
                return "S";
            case KeyboardKey.T:
                return "T";
            case KeyboardKey.U:
                return "U";
            case KeyboardKey.V:
                return "V";
            case KeyboardKey.W:
                return "W";
            case KeyboardKey.X:
                return "X";
            case KeyboardKey.Z:
                return "z";
            default:
                return "";
        }
    }
    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.ControlEvent) {
                switch(gameEvent.Message) {    
                    case "REQUEST_PLAYER_NAME":
                        _nameInputMode = true;
                        _newHigh = true;
                        break;
                    case "RELOAD_SCORE":
                        ReadHighScore();
                        break;
                }
        }
    }
}