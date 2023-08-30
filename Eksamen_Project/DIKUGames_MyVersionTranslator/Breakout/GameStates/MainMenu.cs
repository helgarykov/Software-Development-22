using DIKUArcade.State;

namespace Breakout.GameStates;
using DIKUArcade.Events;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Input;
using System.IO;


public class MainMenu : IBreakoutGameState
{
    public GameStateType ActiveState { get; set; } = GameStateType.MainMenu;

    public SelectedAction SelectedAction { get; set; }
    private Entity backGroundImage;

    private Text[]? menuButtons;
    private Text? arrow;
    private int activeMenuButton;
    private int maxMenuButtons;

    public MainMenu()
    {
        backGroundImage = new Entity (new DynamicShape(0f, 0f, 1f, 1f), new Image(Path.Combine("Assets", "Images", "BreakoutTitleScreen.png")));
        SelectedAction = SelectedAction.MainMenu;
        activeMenuButton = 0;
        InitializeText();
    }

    private void InitializeText()
    {
        menuButtons = new Text[] {
            new Text(("New Game"), new Vec2F(0.35f, -0.1f), new Vec2F(0.6f, 0.6f)) , 
            new Text(("Quit"), new Vec2F(0.35f, -0.2f), new Vec2F(0.6f, 0.6f)) ,
        };
        foreach (var text in menuButtons)
        {
            text.SetColor(System.Drawing.Color.Ivory);
            text.SetFont(Game.font);
        }
        maxMenuButtons = menuButtons.Length;
        
        //Arrow created
        arrow = new Text(("> "), new Vec2F(0.27f, -0.2f), new Vec2F(0.6f, 0.6f));
        arrow.SetColor(System.Drawing.Color.Brown);
        arrow.SetFont(Game.font);
    }
    public GameStateType GetNextState()
    {
        if (SelectedAction == SelectedAction.StartGame)
        {
            return GameStateType.GameRunning;
        }
        if (SelectedAction == SelectedAction.CloseGame) Environment.Exit(0);

        return ActiveState;
    }

    /// <summary>
    /// This function is what is assigned as keyHandler through DIKUArcade functionalities.
    /// the inputted KeyboardAction typically represents either KeyPress, or KeyRelease. For this function,
    /// only keyPress is needed, so it is ensured that the entered action is a KeyPress, with an if statement.
    /// If the if-statement is successful, it calls its own function KeyPress, and sends along the pressed Key. 
    /// </summary>
    ///<param name = "action, key"> action is a KeyboardAction, and key is a KeyboardKey. </param>
    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key)
    {
        if (action == KeyboardAction.KeyPress)
        {
            KeyPress(key);
        }
    }

    private void KeyPress(KeyboardKey key)
    {
        if (key == KeyboardKey.Up)
        {
            if (activeMenuButton != 0)
            {
                activeMenuButton--;
            }
        }
        else if (key == KeyboardKey.Down)
        {
            if (activeMenuButton == 0)
            {
                activeMenuButton++;
            }
        }
        else if (key == KeyboardKey.Enter)
        {
            SelectedAction = activeMenuButton switch
            {
                0 => SelectedAction.StartGame,
                1 => SelectedAction.CloseGame,
                _ => throw new ArgumentOutOfRangeException(nameof(key), key, null)
            };
        }
    }
    
    
    /// <summary>
    /// Method that sets activeMenuButton to brown and others to ivory.
    /// </summary>
    /// <param name="buttons"> Text array with two strings: NewGame & Quit </param>

    public void UpdateButtons(Text[]? buttons)
    {
        if (menuButtons != null)
            foreach (Text button in menuButtons)
            {
                if (buttons != null && button == buttons[activeMenuButton])
                {
                    button.SetColor(System.Drawing.Color.Brown);
                    arrow!.GetShape().Position.Y = menuButtons[activeMenuButton].GetShape().Position.Y;
                }
                else
                {
                    button.SetColor(System.Drawing.Color.Ivory);
                }
            }
    }
    
    /// <summary>
    /// This function renders the two menu Buttons, arrow as well as the background Image behind it.
    /// </summary>
    public void RenderState()
    {
        backGroundImage.RenderEntity();
        for (var i = 0; i < maxMenuButtons; i++)
        {
            menuButtons?[i].RenderText();
        }
        arrow?.RenderText();
    }

    public void OnEnterState(IGameState previousState)
    {
    }

    /// <summary>
    /// This function allows for the gameState to be returned to its original state. It is not
    /// particularly useful in the mainMenu, as the mainMenu does not really contain any dynamic values
    /// or entities.
    /// </summary>
    public void ResetState()
    {
    }

    /// <summary>
    /// As this state has no dynamic values, updateState is not used. However, it is part of the
    /// interface, and thus has to be here
    /// </summary>
    public void UpdateState()
    {
        UpdateButtons(menuButtons);
    }
}

    
    
            