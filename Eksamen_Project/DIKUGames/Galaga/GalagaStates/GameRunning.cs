using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Input;
using DIKUArcade.Events;
using System.IO;
using System.Collections.Generic;
using Galaga.MovementStrategy;
using Galaga.Squadron;
using System;


namespace Galaga.GalagaStates {
    /// <summary>
    /// Manages the game state "GameRunning". 
    /// And basically contains the whole game: Player, Enemies, etc.
    /// </summary>
    public class GameRunning : IGameState, IGameEventProcessor {
        private static GameRunning instance = null;
        private GameEventBus eventBus;
        public static int roundNO;
        private int squadronNO = 1;
        private EntityContainer<PlayerShot> playerShots;
        private IBaseImage playerShotImage;
        private static AnimationContainer enemyExplosions;
        private static List<Image> explosionStrides;
        private const int EXPLOSION_LENGTH_MS = 500;
        private EntityContainer<Enemy> enemies = new EntityContainer<Enemy>{};
        private Player player;
        public Score score {get; set;}
        public bool active {get; set;}

        /// <summary>
        /// A method that initializes GameRunning and sets the game
        /// and other Galaga objects (player, enemies, etc.) in motion.
        /// </summary>
        public void InitializeGameState () {
            // Initialises a new EventBus object.
            eventBus = GalagaBus.GetBus();
            roundNO = 0;
            squadronNO = 1;
            enemies = new EntityContainer<Enemy>{};
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
        }
        /// <summary>
        /// A method to create an instance of GameRunning state.
        /// </summary>
        /// <returns>A GameRunning object.</returns>
        public static GameRunning GetInstance() {
            if (GameRunning.instance == null) {
                GameRunning.instance = new GameRunning();
                GameRunning.instance.InitializeGameState();
            }
            return GameRunning.instance;
        }
        /// <summary>
        /// A method that resets GameRunning state and
        /// sets its private variables to their initial default values.
        /// This is useful when e.g. leaving a game and entering a new game.
        /// </summary>
        public void ResetState() {
            roundNO = 0;
            squadronNO = 1;
            enemies = new EntityContainer<Enemy>{};
            player = new Player(new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)), 
                new Image(Path.Combine("Assets", "Images", "Player.png")));
            playerShots = new EntityContainer<PlayerShot>();
            score = new Score(new Vec2F(0.9f, 0.7f), new Vec2F(0.3f, 0.3f));
            enemyExplosions = new AnimationContainer(50);
        }
        /// <summary>
        /// A method that constantly calls the methods listed in its body.
        /// </summary>
        public void UpdateState() {
            player.Move();
            this.MoveEnemies();
            this.IterateShots();
            this.GameOverCheck();
        }
        
        /// <summary>
        /// A method that renders the game objects: player, enemies,
        /// playershots, score and enemyExplosions.
        /// </summary>
        public void RenderState() {
            player.Render();
            enemies.Iterate(enemy => {
                        enemy.Render();
                        });
            playerShots.Iterate(shot => {
                        shot.Render();
                        });   
            score.RenderScore();
            enemyExplosions.RenderAnimations();
        }
        /// <summary>
        /// Each state can react to key events, delegated from the host StateMachine.
        /// </summary>
        /// <param name="action">Enumeration representing key press/release.</param>
        /// <param name="key">Enumeration representing the keyboard key.</param>
        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            if (action == KeyboardAction.KeyPress) {
                KeyPress(key);
            } if (action == KeyboardAction.KeyRelease) {
                KeyRelease(key);
            }  
        }
        /// <summary>
        /// This method is part of the IGameEventProcessor.
        /// </summary>
        public void ProcessEvent(GameEvent gameEvent) {
            return;
        }

        /// <summary> 
        /// Method that makes enemies move and selects the enemy formation. This method is continously called as it is
        /// called in the Update() method.
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

      
        /// <summary>
        /// Registers events in the GameBus that the player and state classes then reacts to.
        /// These methods are related to the movement of the player object and quitting the game.
        /// </summary>
        /// <param name="key">Enumeration representing the keyboard key.</param>
        public void KeyPress(KeyboardKey key) {
            if (active == true) {
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
                    case KeyboardKey.Escape:
                        eventBus.RegisterEvent (new GameEvent { 
                                                    EventType = GameEventType.GameStateEvent, From = this, Message = "CHANGE_STATE", StringArg1 = "GAME_PAUSED" 
                                                    });
                        active = false;
                        break;
                }
            }    
        }
       
        /// <summary>
        /// The method to process the input from the keyboard and 
        /// move the player left or right by calling its move methods through GameEventBus.
        /// Initialises a new player-shot by pressing the Space-key.
        /// </summary>
        /// <param name="key">Enumeration representing the keyboard key.</param>
        public void KeyRelease(KeyboardKey key) {
            if (active == true) {
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
                    case KeyboardKey.Space:
                        playerShots.AddEntity(new PlayerShot(
                            new DynamicShape(new Vec2F (player.shape.Position.X+0.05f,player.shape.Position.Y), 
                            new Vec2F (0.008f, 0.021f), new Vec2F (0.0f, 0.1f)), playerShotImage));
                    break;
                }
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
        // Adds an explosion to the enemyExplosions list. This list contains all active explosions, and make sure that their animations
        // are rendered as intended. 
        public static void AddExplosion(Vec2F position, Vec2F extent) {
            enemyExplosions.AddAnimation(new StationaryShape(position, extent), 
            EXPLOSION_LENGTH_MS, new ImageStride(EXPLOSION_LENGTH_MS/8, explosionStrides));
        }
        // Ends the game if the enemy objects cross the bottom wall of the screen.
        // If that returns true, Calls GameOver().
        private void GameOverCheck() {
            enemies.Iterate(enemy => {
                if (enemy.Shape.Position.Y <= 0) {
                    eventBus.RegisterEvent (new GameEvent { 
                                                    EventType = GameEventType.GameStateEvent, From = this, Message = "CHANGE_STATE", StringArg1 = "GAME_OVER" 
                                                    });
                    active = false;
                }
            });
        }
    }  
}