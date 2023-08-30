using DIKUArcade.State;

namespace Breakout.GameStates;
using DIKUArcade.Events;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Input;
using LevelManagement;
using System.IO;
using DIKUArcade.Physics;
using System;
using PowerUpTypes;




public class GameRunning : IBreakoutGameState, IGameEventProcessor {
    //GameEventBus and Statemachine related variables
    public GameStateType ActiveState { get; set; } = GameStateType.GameRunning;
    public SelectedAction SelectedAction { get; set; }
    private GameEventBus gamebus;

    // Abstract class variables
    public static Points? Points {get; set;}
    private PlayerLives? PlayerLives {get; set;}
    private Timer? _time;

    //Entities
    private Player? Player {get; set;}
    private EntityContainer<Block>? _blocks;
    private Ball? _ball;
    private EntityContainer<PowerUp>? _powerups;

    // Rocket related variables
    private int _availableRockets;
    private Entity rocketIcon;
    private EntityContainer<Rocket>? _rocketContainer;
    private IBaseImage? _rocketStrides;
    private static AnimationContainer? _rocketExplosions;
    private static List<Image>? _explosionStrides;
    private const int ExplosionLengthMs = 500;

    //Simple variables
    private bool _fireBallActive;
    private int _currentMap;

    //Images
    private Image topBar;
    private Image backGround;

    public GameRunning() {
        gamebus = BreakoutBus.GetBus();
        BreakoutBus.GetBus().Subscribe(GameEventType.StatusEvent, this);
        BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
        topBar = new Image(Path.Combine("Assets", "Images", "topBar.png"));
        backGround = new Image(Path.Combine("Assets", "Images", "SpaceBackground.png"));
        rocketIcon = new Entity(new DynamicShape(new Vec2F(Game.rightX / 2 + 0.15f, 1f-0.05f), new Vec2F(0.05f, 0.05f)),
            new Image(Path.Combine("Assets", "Images", "RocketIcon.png")));
    }
    
    public void OnEnterState(IGameState previousState){
        if (previousState is not GamePaused)
        {
            ResetState();
        }
    }
    
    /// <summary>
    /// Method for changing between states in the game.
    /// </summary>
    public GameStateType GetNextState()
    {
        if (SelectedAction == SelectedAction.PauseGame) return GameStateType.GamePaused;
        if (SelectedAction == SelectedAction.ExitGame) return GameStateType.GameOver;
        return ActiveState;
    }

    /// <summary>
    /// A method that renders the game objects: player, blocks,
    /// ball, points, playerLives and time.
    /// </summary>
    public void RenderState() {
        backGround.Render(new StationaryShape(
            new Vec2F(Game.leftX, Game.bottomY), 
            new Vec2F(Game.rightX, Game.topY)));
        _blocks?.Iterate(block => {
            block.Render();
        });
        topBar.Render(new StationaryShape(
            new Vec2F(Game.leftX - 0.06f, 1f-0.17f), 
            new Vec2F(1.13f, 0.2f)));
        Player?.Render();
        Points?.RenderPoints();
        PlayerLives?.RenderLives();
        _time?.RenderTime();
        _ball?.Render();
        if (_availableRockets > 0) {
            rocketIcon.RenderEntity();
        }
        _rocketContainer?.Iterate(rocket => {
            rocket.Render();
        });
        _rocketExplosions?.RenderAnimations();
        _powerups?.Iterate(rocket => {
            rocket.Render();
        });
    }
    /// <summary>
    /// Method for extracting the time info from the .txt files.
    /// </summary>
    /// <param name="filename"> The file which the data is extracted from</param>
    /// <returns> Returns the time in int.</returns>
    private int GetTimeLimit(string filename) {
        var metaList = Translator.getMeta(filename);
        var metaIndex = Array.IndexOf(metaList, "Time");
        return Convert.ToInt32(metaList[metaIndex+1]);
    }

    /// <summary>
    /// A method that resets GameRunning state and
    /// sets its private variables to their initial default values.
    /// </summary>
    public void ResetState() {
        SelectedAction = SelectedAction.StartGame;
        _currentMap = 3;
        _blocks = Translator.CreateMap("level" + _currentMap + ".txt");
        Player = new Player(new DynamicShape(new Vec2F(Game.rightX / 2.0f - 0.18f, Game.bottomY + 0.1f), new Vec2F(0.18f, 0.025f)), 
            new Image(Path.Combine("Assets", "Images", "player.png")));
        _ball = new Ball(new DynamicShape(new Vec2F(Game.rightX / 2.0f - 0.04f, Game.bottomY + 0.14f), new Vec2F(0.04f, 0.04f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", "ball2.png")));
        Points = new Points(new Vec2F(Game.leftX + 0.1f, 1f-0.3f), new Vec2F(0.3f, 0.3f));
        _time = new Timer(new Vec2F(Game.rightX - 0.2f, 1f-0.3f), new Vec2F(0.3f, 0.3f), GetTimeLimit("level" + _currentMap + ".txt"));
        PlayerLives = new PlayerLives();
        _fireBallActive = false;
        _availableRockets = 0;
        _rocketContainer = new EntityContainer<Rocket>(20);
        _rocketStrides =  new ImageStride(80, ImageStride.CreateStrides(5, Path.Combine("Assets", "Images", "RocketLaunched.png")));
        _rocketExplosions = new AnimationContainer(50);
        _explosionStrides = ImageStride.CreateStrides(8, Path.Combine("Assets", "Images", "Explosion.png"));
        _powerups = new EntityContainer<PowerUp>(20);
        gamebus.RegisterEvent (new GameEvent { 
                    EventType = GameEventType.StatusEvent, From = this, Message = "PAUSE_TIME", StringArg1 = "TRUE"
                });
    }
    public void ResetMap() {
        string levelFileName = $"level{_currentMap}.txt";
        // Changes gameState to GameOver state if there are no more levels.
        if (!Translator.MapExists(levelFileName))
        {
            SelectedAction = SelectedAction.ExitGame;
            return;
        }
        
        _blocks = Translator.CreateMap(levelFileName);
        Player = new Player(new DynamicShape(new Vec2F(Game.rightX / 2.0f - 0.18f, Game.bottomY + 0.1f), new Vec2F(0.18f, 0.025f)), 
            new Image(Path.Combine("Assets", "Images", "player.png")));
        _ball = new Ball(new DynamicShape(new Vec2F(Game.rightX / 2.0f - 0.04f, Game.bottomY + 0.14f), new Vec2F(0.04f, 0.04f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", "ball2.png")));
        _time = new Timer(new Vec2F(Game.rightX - 0.2f, 1f-0.3f), new Vec2F(0.3f, 0.3f), GetTimeLimit("level" + _currentMap + ".txt"));
        _fireBallActive = false;
        _availableRockets = 0;
        _rocketContainer = new EntityContainer<Rocket>(20);
        _rocketStrides =  new ImageStride(80, ImageStride.CreateStrides(5, Path.Combine("Assets", "Images", "RocketLaunched.png")));
        _rocketExplosions = new AnimationContainer(50);
        gamebus.RegisterEvent (new GameEvent { 
                    EventType = GameEventType.StatusEvent, From = this, Message = "PAUSE_TIME", StringArg1 = "TRUE"
                });
    }

    /// <summary>
    /// A method that constantly calls the methods listed in its body.
    /// </summary>
    public void UpdateState() {
        Player?.Move();
        _ball?.Update();
        ControlBall(_ball);
        Collision(!_fireBallActive, _ball);
        _blocks?.Iterate(block => {
            block.Update();
        });
        _time?.Update();
        PlayerLives?.Update();
        _rocketContainer?.Iterate(rocket => {
            rocket.Move();
            RocketCollision(rocket);
        });
        CheckBlocks();
        Player?.Update();
        _powerups?.Iterate(powerup => {
            powerup.Update();
            CollisionPowerUp(powerup);
        });
    }
    
    /// <summary>
    /// Follows with IGameState interface, but this functionality is delegated to Game.
    /// </summary>
    /// <param name="action"></param>
    /// <param name="key"></param>
    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        if (action == KeyboardAction.KeyPress) KeyPress(key);
        if (action == KeyboardAction.KeyRelease) KeyRelease(key);
    }
    
    /// <summary>
    /// Method for registering the events which take place when specific keys are pressed.
    /// </summary>
    /// <param name="key"> The key which is being pressed</param>
    private void KeyPress(KeyboardKey key) {
        switch(key) {
            case KeyboardKey.Left:
                gamebus.RegisterEvent (new GameEvent { 
                    EventType = GameEventType.PlayerEvent, From = this, 
                    Message = "MOVE_LEFT", 
                    StringArg1 = "", 
                    StringArg2 = "" 
                });
                break;

            case KeyboardKey.Right:
                gamebus.RegisterEvent (new GameEvent { 
                    EventType = GameEventType.PlayerEvent, From = this, 
                    Message = "MOVE_RIGHT", 
                    StringArg1 = "", 
                    StringArg2 = "" 
                });
                break;
            case KeyboardKey.P:
                SelectedAction = SelectedAction.PauseGame;
                gamebus.RegisterEvent (new GameEvent { 
                    EventType = GameEventType.StatusEvent, From = this, 
                    Message = "PAUSE_TIME", 
                    StringArg1 = "TRUE"
                });
                break;
            case KeyboardKey.Escape:
                SelectedAction = SelectedAction.ExitGame;
                break;
            case KeyboardKey.Space:
                // If we have a rocket, and the ball is not by the bat, launch!
                if (Math.Abs(_ball!.Shape.Position.Y - 0.14f) > 0.001 && _availableRockets > 0) {
                    GenerateRocket();
                    _availableRockets -= 1;
                }
                else {
                    gamebus.RegisterEvent (new GameEvent { 
                        EventType = GameEventType.StatusEvent, From = this, 
                        Message = "SHOOT", 
                        StringArg1 = "BALL", 
                        StringArg2 = "" 
                    });
                    gamebus.RegisterEvent (new GameEvent { 
                        EventType = GameEventType.StatusEvent, From = this, 
                        Message = "PAUSE_TIME", 
                        StringArg1 = "FALSE"
                    });
                }
                break;
        }
    }

    /// <summary>
    /// Method for registering the events which take place when specific keys released.
    /// </summary>
    /// <param name="key"> The key which is no longer being pressed.</param>
    private void KeyRelease(KeyboardKey key) {
        switch(key) {
            case KeyboardKey.Left:
                gamebus.RegisterEvent (new GameEvent { 
                    EventType = GameEventType.PlayerEvent, From = this,
                    Message = "STOP_MOVE_LEFT", 
                    StringArg1 = "", 
                    StringArg2 = "" 
                });
                break;
            case KeyboardKey.Right:
                gamebus.RegisterEvent (new GameEvent { 
                    EventType = GameEventType.PlayerEvent, From = this, 
                    Message = "STOP_MOVE_RIGHT",
                    StringArg1 = "", 
                    StringArg2 = "" 
                });
                break;
        }
    }

    /// <summary>
    /// Distributes collision between collision targets.
    /// </summary>
    private void Collision(bool redirect, Ball? inputBall) {
        CollisionPaddle(true, inputBall);
        CollisionBlocks(redirect, inputBall);
        CollisionWalls(true, inputBall);        
    }
    
    
    /// <summary>
    /// Method for redirecting the ball's direction when it hits the paddle.
    /// </summary>
    private void CollisionPaddle(bool redirect, Ball? inputBall) {
        if (!CollisionDetection.Aabb(inputBall?.Shape.AsDynamicShape(),
                Player?.Shape).Collision) return;
            if(redirect) {
                if (Player != null)
                    if (inputBall != null)
                        inputBall.direction = -inputBall.direction + (int) Math.Round(
                            (inputBall.Shape.Position.X - inputBall.Shape.Extent.X / 2 -
                             (Player.Shape.Position.X + 0.08f) * 1.0f) * 400.0f * -1.0f);
            }        
            if (!_fireBallActive) return;
                _fireBallActive = false;
                gamebus.RegisterEvent (new GameEvent { 
                    EventType = GameEventType.StatusEvent, From = this, 
                    Message = "FIRE_BALL", 
                    StringArg1 = "FALSE", 
                    StringArg2 = "" 
        });
    }
    
    /// <summary>
    /// Method for redirecting the ball's direction when it hits the blocks.
    /// </summary>

    private void CollisionBlocks(bool redirect, Ball? inputBall) {
        _blocks?.Iterate(block => {
            if (CollisionDetection.Aabb(inputBall?.Shape.AsDynamicShape(), block.Shape).Collision != true) return;
            if (redirect) {
                            
                // collision with left side of block
                if (inputBall != null && inputBall.Shape.Position.X + inputBall.Shape.Extent.X <= block.Shape.Position.X) { 
                    inputBall.Shape.Position.X += inputBall.Shape.Extent.X/2;
                    inputBall.direction = 180 - inputBall.direction;               
                }
                // collision with right side of block
                if (inputBall != null && inputBall.Shape.Position.X >= (block.Shape.Position.X + block.Shape.Extent.X)) {
                    inputBall.Shape.Position.X -= inputBall.Shape.Extent.X/2;
                    inputBall.direction = 180 - inputBall.direction;
                }
                // collision with top side of block
                if (inputBall != null && inputBall.Shape.Position.Y >= block.Shape.Position.Y + block.Shape.Extent.Y) {
                    inputBall.Shape.Position.Y -= inputBall.Shape.Extent.Y/2;
                    inputBall.direction = 360 - inputBall.direction;     
                }
                // collision with bottom of block
                if (inputBall != null && inputBall.Shape.Position.Y + inputBall.Shape.Extent.Y <= block.Shape.Position.Y) {
                    inputBall.Shape.Position.Y += inputBall.Shape.Extent.Y/2;
                    inputBall.direction = 360 - inputBall.direction;
                }
            }
            gamebus.RegisterEvent (new GameEvent { 
                EventType = GameEventType.ControlEvent, From = this,
                Message = "HIT_BLOCK", 
                ObjectArg1 = block, 
                IntArg1 = 10
            });
        });
    }
    
    /// <summary>
    /// Method for redirecting the ball's direction when it hits the walls.
    /// </summary>
    private void CollisionWalls (bool redirect, Ball? inputBall) {
        if (redirect) {
            if (inputBall != null && inputBall.Shape.Position.X <= Game.leftX) {
                inputBall.direction = 180 - inputBall.direction;
                inputBall.Shape.Position.X = Game.leftX;
            }
            if (inputBall != null && inputBall.Shape.Position.X + inputBall.Shape.Extent.X >= Game.rightX) {
                inputBall.direction = 180 - inputBall.direction;
                inputBall.Shape.Position.X = Game.rightX - inputBall.Shape.Extent.X;
            }
            if (inputBall != null && inputBall.Shape.Position.Y + inputBall.Shape.Extent.Y >= Game.topY) {
                inputBall.direction = 360 - inputBall.direction;
            }
        }
    }

    ///<Summary> This method checks if the entered powerup is currently colliding with the paddle.
    /// If collision occurs, the powerup is deleted, and the effect is given via the gamebus. </summary>
    ///<param name = "powerup"> The entered powerup will be checked for collision with paddle </param>
    private void CollisionPowerUp(PowerUp powerup) {
        if (CollisionDetection.Aabb(powerup.Shape.AsDynamicShape(),
                Player?.Shape).Collision != true) return;
        
        BreakoutBus.GetBus().RegisterEvent(new GameEvent {
            EventType = GameEventType.StatusEvent, From = this, 
            Message = "GRANT_POWERUP", 
            ObjectArg1 = powerup.powerUp
        });
        powerup.deletePowerUp();
    }
    
    ///<Summary> This function is repeatedly called to check if the rocket collides with the top wall, 
    /// or with any blocks along the way. </summary>
    ///<param name = "rocket"> The entered rocket is checked for collision   </param>
    public void RocketCollision(Rocket rocket) {
        if (rocket.Shape.Position.Y + rocket.Shape.Extent.Y >= 1f) {
                    rocket.DeleteEntity();
                    AddExplosion(new Vec2F((rocket.Shape.Position.X + rocket.Shape.Extent.X / 2) - 0.1f, 
                        (rocket.Shape.Position.Y + rocket.Shape.Extent.Y) - 0.1f), 
                        new Vec2F(0.2f, 0.2f));
                    ExplosionCollision(rocket); 
        }
        _blocks?.Iterate(block => {
            if (CollisionDetection.Aabb(rocket.Shape.AsDynamicShape(), block.Shape).Collision) {
                rocket.DeleteEntity();
                AddExplosion(new Vec2F((rocket.Shape.Position.X + (rocket.Shape.Extent.X / 2)) - 0.1f, 
                    (rocket.Shape.Position.Y + rocket.Shape.Extent.Y) - 0.1f),
                    new Vec2F(0.2f, 0.2f));
                ExplosionCollision(rocket); 
            }
        }); 
    }
    
    ///<Summary> This method adds an animated explosion effect at the entered position, with the entered size. </summary>
    ///<param name = "position", "extent"> Position and extent are both Vec2F inputs that determine the position and size of the explosion  </param>
    private void AddExplosion(Vec2F position, Vec2F extent) {
            _rocketExplosions?.AddAnimation(new StationaryShape(position, extent), 
            ExplosionLengthMs, new ImageStride(ExplosionLengthMs/8, _explosionStrides));
        }
    
    
    ///<Summary> This function uses the position of the input Rocket and "hits"
    /// all blocks within the square field of the explosion animation </summary>
    ///<param name = "rocket"> rocket represents a Rocket.  </param>
    private void ExplosionCollision(Rocket rocket) {
        var expBottomLeftX = (rocket.Shape.Position.X + (rocket.Shape.Extent.X / 2)) - 0.1f;
        var expBottomLeftY = (rocket.Shape.Position.Y + rocket.Shape.Extent.Y) - 0.1f;
        var expTopRightX = expBottomLeftX + 0.2f;
        var expTopRightY = expBottomLeftY + 0.2f;
        _blocks?.Iterate(block => {
            var blkBottomLeftX = block.Shape.Position.X;
            var blkBottomLeftY = block.Shape.Position.Y;
            var blkTopRightX = blkBottomLeftX + block.Shape.Extent.X;
            var blkTopRightY = blkBottomLeftY + block.Shape.Extent.Y;
            if ((blkBottomLeftX > expBottomLeftX && blkBottomLeftX < expTopRightX && blkBottomLeftY > expBottomLeftY && blkBottomLeftY < expTopRightY) ||
                    (blkTopRightX > expBottomLeftX && blkTopRightX < expTopRightX && blkTopRightY > expBottomLeftY && blkTopRightY < expTopRightY)) {
                gamebus.RegisterEvent (new GameEvent { 
                            EventType = GameEventType.ControlEvent, From = this, Message = "HIT_BLOCK", ObjectArg1 = block, IntArg1 = 10
                        });
            } 
        }); 
    }
    
    
    ///<Summary> When this function is called, a rocket is added to the rocket container, and it will
    /// start flying up from the paddle. </summary>
    private void GenerateRocket() {
        if (Player != null)
            _rocketContainer?.AddEntity(new Rocket
            (new DynamicShape(new Vec2F(Player.entity.Shape.Position.X + 0.073333f, 0.14f),
                new Vec2F(0.04f, 0.04f),
                new Vec2F(0.1f, 0.1f)), _rocketStrides));
    }

    /// <summary>
    /// This function returns the ball to the paddle in the not playing state (E.g, it is stuck to
    /// the paddle position), and time is paused until it is launched again.
    /// </summary>
    private void ResetBall() {
        if (Player != null)
            _ball = new Ball(new DynamicShape(new Vec2F(Player.entity.Shape.Position.X + 0.073333f, 0.14f),
                    new Vec2F(0.04f, 0.04f),
                    new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "ball2.png")));
        _fireBallActive = false;
            gamebus.RegisterEvent (new GameEvent { 
                    EventType = GameEventType.StatusEvent, From = this, 
                    Message = "PAUSE_TIME",
                    StringArg1 = "TRUE"
                });
    }
    
    /// <summary>
    /// This method is only called if the ball is not launched yet. It locks the balls
    /// X value to the paddles x value, ensuring that the ball moves along with the paddle.
    /// </summary>
    private void ControlBall(Ball? ballinput) {
        if (Player != null) ballinput?.SetPositionX(Player.entity.Shape.Position.X + 0.073333f);
    }
    
    ///<Summary> This method continously checks if the block container is empty, and 
    /// moves to the next level if that is the case. </summary>
    private void CheckBlocks() {
        if (_blocks != null && _blocks.CountEntities()-Translator.countUnbreakable("level" + _currentMap + ".txt") == 0) {
            _currentMap = _currentMap + 1;
            ResetMap();
        }
    }
    
    ///<Summary> This function adds a floating powerup at the position requested (typically by a destroyed block),
    /// and assigns it with the correct image and powerUp Type. </summary>
    ///<param name = "x", "y", "image", "powerup"> x and y represents the position to create the floating powerUp. 
    /// image and powerUp passes along the powerUpType, and the correct image.  </param>
    private void AddPowerUp(float x, float y, IBaseImage image, PowerUpType powerup) {
        _powerups?.AddEntity(new PowerUp(new DynamicShape(new Vec2F(x, y), 
            new Vec2F(0.033f, 0.033f), 
            new Vec2F(0.1f, 0.1f)), image, powerup));
    }
    
    
    /// <summary>
    /// This method is part of the IGameEventProcessor.
    /// </summary>
    /// <param name="gameEvent"></param>
    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.StatusEvent) {
                switch(gameEvent.Message) {    
                    case "FIRE_BALL":
                        if (gameEvent.StringArg1 == "TRUE") {
                            _fireBallActive = true;
                        } 
                        break;
                    case "ADD_ROCKET":
                        _availableRockets += 1;
                        break;
                    case "SPAWN_POWERUP":
                        var image = new Image(Path.Combine("Assets", "Images", "ball2.png"));
                        var powerup = PowerUpType.ExtraLife;
                        switch(gameEvent.ObjectArg1) {
                            case PowerUpType.ExtraLife:
                                image = Game.ExtraLifeImage;
                                powerup = PowerUpType.ExtraLife;
                                break;
                            case PowerUpType.ExtraSpeed:
                                image = Game.ExtraSpeedImage;
                                powerup = PowerUpType.ExtraSpeed;
                                break;
                            case PowerUpType.HardBall:
                                image = Game.HardBallImage;
                                powerup = PowerUpType.HardBall;
                                break;
                            case PowerUpType.MoreTime:
                                image = Game.MoreTimeImage;
                                powerup = PowerUpType.MoreTime;
                                break;
                            case PowerUpType.Rocket:
                                image = Game.RocketImage;
                                powerup = PowerUpType.Rocket;
                                break;
                        }
                        /// The position of the destroyed block as passed along as stringArgs, and 
                        /// converted back into float values on arrival.
                        AddPowerUp((float) Convert.ToDouble(gameEvent.StringArg1),
                            (float) Convert.ToDouble(gameEvent.StringArg2), image, powerup);
                        break; 
                }
        }
        if (gameEvent.EventType == GameEventType.GameStateEvent) {
                switch(gameEvent.Message) {    
                    case "GAME_OVER":
                        SelectedAction = SelectedAction.ExitGame;
                        break;
                    case "ALL_BALLS_OUT":
                        ResetBall();
                        break;
                }
        }      
    }
}