using System.Collections.Generic;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga.Squadron;
using Galaga.MovementStrategy;

namespace Galaga {
    /// <summary>
    /// A high-level module that represents a game entity.
    /// Instantiates different entities (Player, Enemy, PlayerShots, Score)
    /// and calls their individual methods through GameEventBus.
    /// </summary>
    public class Game : DIKUGame, IGameEventProcessor {
        private Player player;
        public static GameEventBus eventBus {get; set;}
        private EntityContainer<Enemy> enemies = new EntityContainer<Enemy>{};
        private Score score;
        private EntityContainer<PlayerShot> playerShots;
        private IBaseImage playerShotImage;
        private static AnimationContainer enemyExplosions;
        private static List<Image> explosionStrides;
        public static int roundNO;
        private bool gameOverStatus = false;
        private const int EXPLOSION_LENGTH_MS = 500;
        private int squadronNO = 1;
        private Text gameOvertext;
        private void KeyHandler(KeyboardAction action, KeyboardKey key) {
            if (action == KeyboardAction.KeyPress) {
                KeyPress(key);
            } if (action == KeyboardAction.KeyRelease) {
                KeyRelease(key);
            }
        }
        
        // A game constructor that instantiates a new game object with windowArgs.
        public Game(WindowArgs windowArgs) : base(windowArgs) {
            roundNO = 0;
            eventBus = new GameEventBus();
            
            // Creates a new list of entity events: input, player, window, enemy, score.
            eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent, GameEventType.PlayerEvent, GameEventType.WindowEvent,
                                                                    GameEventType.ControlEvent, GameEventType.GameStateEvent });

            // Allows to process inputs from the keyboard.
            window.SetKeyEventHandler(KeyHandler);
            
            //Subscribes a game entity to a GameEventBus.
            eventBus.Subscribe(GameEventType.WindowEvent, this);
            
            //Subscribes a score entity to a GameEventBus.
            eventBus.Subscribe(GameEventType.GameStateEvent, this);

            // Initialises a new player object with DynamicShape and Image.
            player = new Player(new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)), 
            new Image(Path.Combine("Assets", "Images", "Player.png")));
            
            // Initialises a new list of player-shot objects.
            playerShots = new EntityContainer<PlayerShot>();
            
            // Assigns a player-shot image to a BulletRed2.png.
            playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));

            // Initialises a new score object with position and extent.
            score = new Score(new Vec2F(0.9f, 0.7f), new Vec2F(0.3f, 0.3f));
            
            // Initialises a list of animations of enemyExplosions with size.
            enemyExplosions = new AnimationContainer(50);
            
            // Initialises a list of 8 explosion strides with Explosion.png image.
            explosionStrides = ImageStride.CreateStrides(8,
                Path.Combine("Assets", "Images", "Explosion.png"));
            
            // Initialises an object Text with "Game over", position on the screen and object extent.
            // Colors the text "Game over" white.
            gameOvertext = new Text ("GAME OVER", new Vec2F (0.25f, 0.1f), new Vec2F (0.5f, 0.5f));
            gameOvertext.SetColor(new Vec3F(1f,1f,1f));
            
        }
            
        // The method that moves the player object to the right and to the left
        // by calling Player's methods through GameEventBus.     
        public void KeyPress(KeyboardKey key) {
            switch(key) {
                case KeyboardKey.Left:
                    eventBus.RegisterEvent (new GameEvent { 
                        EventType = GameEventType.PlayerEvent, From = this, Message = "MOVE_LEFT", StringArg1 = "", StringArg2 = "" 
                    });
                    break;

                case KeyboardKey.Right:
                    eventBus.RegisterEvent (new GameEvent { 
                        EventType = GameEventType.PlayerEvent, From = this, Message = "MOVE_RIGHT", StringArg1 = "", StringArg2 = "" 
                    });
                    break;
            }
        }
        /// <summary> 
        /// Method that makes enemies move and selects the enemy formation.
        /// </summary>
        private void MoveEnemies() {
            var images = ImageStride.CreateStrides(4, Path.Combine("Assets", "Images", "BlueMonster.png"));
            var redImages = ImageStride.CreateStrides(2, Path.Combine("Assets", "Images", "RedMonster.png"));
            var stratdown = new StrategyDown();
            var stratzig = new StrategyZigZagDown();
                if (enemies.CountEntities() == 0) {
                    if (squadronNO == 7) {squadronNO = 1;}
                    switch(squadronNO) {
                        case 1:
                            var squadron1 = new SquadronLine(images, redImages);
                            enemies = squadron1.Enemies;
                            roundNO ++;
                            squadronNO ++;
                            break;
                        case 2:
                            var squadron2 = new SquadronArrow(images, redImages);
                            enemies = squadron2.Enemies;
                            roundNO ++;
                            squadronNO ++;
                            break;
                        case 3:
                            var squadron3 = new SquadronRandom(images, redImages);
                            enemies = squadron3.Enemies;
                            roundNO ++;
                            squadronNO ++;
                            break;
                        case 4:
                            var squadron4 = new SquadronLine(images, redImages);
                            enemies = squadron4.Enemies;
                            roundNO ++;
                            squadronNO ++;
                            break;
                        case 5:
                            var squadron5 = new SquadronArrow(images, redImages);
                            enemies = squadron5.Enemies;
                            roundNO ++;
                            squadronNO ++;
                            break;
                        case 6:
                            var squadron6 = new SquadronRandom(images, redImages);
                            enemies = squadron6.Enemies;
                            roundNO ++;
                            squadronNO ++;
                            break;
                }
             } 
                if (squadronNO-1 <= 3) {
                    stratdown.moveEnemies(enemies);
                } else {
                    stratzig.moveEnemies(enemies);
                }
            }
        
        // Iterates through a list of player-shots and changes their position
        // with respect to their y-coordinate, i.e. makes them move upwards.
        private void IterateShots() {
            playerShots.Iterate(shot => {
                shot.Shape.MoveY(0.02f);
                // if the position of the shot is higher than the top wall of the window,
                // the shot object is deleted.
                if (shot.Shape.Position.Y > 1.0f) {
                    shot.DeleteEntity();
                } 
                // if the 2 objects (enemy and shot) collide, the shot object is deleted
                // and the EventBus receives the message that the enemy is hit with 4 hit-points.
                else {
                    enemies.Iterate(enemy => {
                        if (DIKUArcade.Physics.CollisionDetection.Aabb(shot.Shape.AsDynamicShape(), 
                        enemy.Shape).Collision == true) {
                            shot.DeleteEntity();
                            eventBus.RegisterEvent (new GameEvent { 
                                EventType = GameEventType.ControlEvent, From = this, Message = "HIT_ENEMY", ObjectArg1 = enemy, IntArg1 = 4 
                                });
                        }
                    });
                }
            });
        }
        
        // Adds a list of explosion objects (8 images).
        public static void AddExplosion(Vec2F position, Vec2F extent) {
            enemyExplosions.AddAnimation(new StationaryShape(position, extent), 
            EXPLOSION_LENGTH_MS, new ImageStride(EXPLOSION_LENGTH_MS/8, explosionStrides));
        } 
        
        // The method to process the input from the keyboard and 
        // move the player left or right by calling its move methods
        // through GameEventBus.
        // Closes the window by pressing Escape.
        // Initialises a new player-shot by pressing the Space-key.
        public void KeyRelease(KeyboardKey key) {
            switch(key) {
                case KeyboardKey.Left:
                    eventBus.RegisterEvent (new GameEvent { 
                            EventType = GameEventType.PlayerEvent, From = this, Message = "STOP_MOVE_LEFT", StringArg1 = "", StringArg2 = "" 
                        });
                        break;

                case KeyboardKey.Right:
                    eventBus.RegisterEvent (new GameEvent { 
                            EventType = GameEventType.PlayerEvent, From = this, Message = "STOP_MOVE_RIGHT", StringArg1 = "", StringArg2 = "" 
                        });
                        break;

                case KeyboardKey.Escape:
                    eventBus.RegisterEvent (new GameEvent { 
                                EventType = GameEventType.WindowEvent, From = this, Message = "CLOSE_WINDOW", StringArg1 = "", StringArg2 = "" 
                            });
                            break;

                case KeyboardKey.Space:
                playerShots.AddEntity(new PlayerShot(
                    new DynamicShape(new Vec2F (player.shape.Position.X+0.05f,player.shape.Position.Y), 
                    new Vec2F (0.008f, 0.021f), new Vec2F (0.0f, 0.1f)), playerShotImage));
                break;
            }
        }
        
        // Game object's implementation of the interface IGameEventProcessor and its method.
        public void ProcessEvent(GameEvent gameEvent) {
            // the window is closed if gameEvent matches the game object
            if (gameEvent.EventType == GameEventType.WindowEvent) {
                switch(gameEvent.Message) {    
                    case "CLOSE_WINDOW":
                        window.CloseWindow();
                    break;
                }
            }
            //if eventType is that of GameStateEvent, the game is over.
            if (gameEvent.EventType == GameEventType.GameStateEvent) {
                switch(gameEvent.Message) {
                    case "GAME_OVER":
                        GameOver();
                        break;
                }
            }
            
        }   
        
        // Renders all entities (player, enemies, shots,
        // score and text "Game Over") on the screen.
        public override void Render() {
            player.Render();
            enemies.Iterate(enemy => {
                        enemy.Render();
                        });
            playerShots.Iterate(shot => {
                        shot.Render();
                        });   
            score.RenderScore();
            enemyExplosions.RenderAnimations();
            if (gameOverStatus == true) {
                gameOvertext.RenderText();
            }
        }
        
        // Constantly updates the functionalities of different Galaga entities.
        public override void Update() {
            window.PollEvents();
            eventBus.ProcessEventsSequentially();
            player.Move();
            this.MoveEnemies();
            this.IterateShots();
            this.GameOverCheck();
        }
        
        // Ends the game if the enemy objects cross the bottom wall of the screen.
        // If that returns true, Calls GameOver().
        private void GameOverCheck() {
            enemies.Iterate(enemy => {
                if (enemy.Shape.Position.Y <= 0) {
                    gameOverStatus = true;
                }
            });
            if (gameOverStatus == true) {
                GameOver();
            }
        }
        
        // Clears the screen off all Galaga objects, when GameOverStatus is TRUE.
        public void GameOver() {
            enemies.ClearContainer();
            player.ClearPlayer();
            playerShots.ClearContainer();
        }
        
    }
}

