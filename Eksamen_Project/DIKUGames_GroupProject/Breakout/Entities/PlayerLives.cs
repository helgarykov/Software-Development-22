using Breakout.GameStates;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Entities;

namespace Breakout{
    ///<summary>
    /// Player lives class.
    /// </summary>
    public class PlayerLives : IGameEventProcessor {
        public int playerLives { get; private set; }
        private GameEventBus gamebus;
        private Entity heart1;
        private Entity heart2;
        private Entity heart3;
        private Entity heart4;
        private Entity heart5;
        private Image emptyHeart;
        private Image filledHeart;
        private Image overfilledHeart;
        private Image overfilledHeartplus;
        public SelectedAction SelectedAction { get; set; }
        /// <summary>
        /// The constructor of the PlayerLives class.
        /// </summary> 
        /// <param name = "position"> the position of the score. </param>
        /// <param name = "extent"> the extent of the score. </param>
        public PlayerLives() {
            playerLives = 3;
            gamebus = BreakoutBus.GetBus();
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.StatusEvent, this);
            emptyHeart = new Image(Path.Combine("Assets", "Images", "heart_empty.png"));
            filledHeart = new Image(Path.Combine("Assets", "Images", "heart_filled.png"));
            overfilledHeart = new Image(Path.Combine("Assets", "Images", "heart_filled_extra.png"));
            overfilledHeartplus = new Image(Path.Combine("Assets", "Images", "heart_filled_extra_plus.png"));
            var extent = 0.05f;
            var xPos = Game.rightX / 2 - extent * 5 / 2;
            var yPos = 1f-extent;
            
            heart1 = new Entity(new DynamicShape(new Vec2F(xPos, yPos),
                                    new Vec2F(extent, extent)), emptyHeart);
            heart2 = new Entity(new DynamicShape(new Vec2F(xPos+extent*1, yPos),
                                    new Vec2F(extent, extent)), emptyHeart);
            heart3 = new Entity(new DynamicShape(new Vec2F(xPos+extent*2, yPos),
                                    new Vec2F(extent, extent)), emptyHeart);
            heart4 = new Entity(new DynamicShape(new Vec2F(xPos+extent*3, yPos),
                                    new Vec2F(extent, extent)), emptyHeart);
            heart5 = new Entity(new DynamicShape(new Vec2F(xPos+extent*4, yPos),
                                    new Vec2F(extent, extent)), emptyHeart);
        }
        ///<summary>
        /// Method that changes the PlayerLives.
        ///</summary>
        ///<param name = "value"> the value PlayerLives changes with. </param>
        private void ChangePlayerLives(int value) {
            playerLives += value;
        }
      
        /// <summary>
        /// Method that renders PlayerLives.
        /// </summary>
        public void RenderLives() {
            heart1.RenderEntity();
            heart2.RenderEntity();
            heart3.RenderEntity();
            heart4.RenderEntity();
            heart5.RenderEntity();
        }

        /// <summary>
        /// This method could potentially have been formatted as a series of if loops, but
        /// seeing as there is a maximum of 11 cases, it has been formatted as hard coded
        /// switch statement instead. It updates the five heart entities into the correct
        /// combination of images, based on the amount of lives left.
        /// </summary>
        private void drawHearts() {
            switch (playerLives) {
                case 0:
                    heart1.Image = emptyHeart;
                    heart2.Image = emptyHeart;
                    heart3.Image = emptyHeart;
                    heart4.Image = emptyHeart;
                    heart5.Image = emptyHeart;
                    break;
                case 1:
                    heart1.Image = filledHeart;
                    heart2.Image = emptyHeart;
                    heart3.Image = emptyHeart;
                    heart4.Image = emptyHeart;
                    heart5.Image = emptyHeart;
                    break;
                case 2:
                    heart1.Image = filledHeart;
                    heart2.Image = filledHeart;
                    heart3.Image = emptyHeart;
                    heart4.Image = emptyHeart;
                    heart5.Image = emptyHeart;
                    break;
                case 3:
                    heart1.Image = filledHeart;
                    heart2.Image = filledHeart;
                    heart3.Image = filledHeart;
                    heart4.Image = emptyHeart;
                    heart5.Image = emptyHeart;
                    break;
                case 4:
                    heart1.Image = filledHeart;
                    heart2.Image = filledHeart;
                    heart3.Image = filledHeart;
                    heart4.Image = filledHeart;
                    heart5.Image = emptyHeart;
                    break;
                case 5:
                    heart1.Image = filledHeart;
                    heart2.Image = filledHeart;
                    heart3.Image = filledHeart;
                    heart4.Image = filledHeart;
                    heart5.Image = filledHeart;
                    break;
                case 6:
                    heart1.Image = overfilledHeart;
                    heart2.Image = filledHeart;
                    heart3.Image = filledHeart;
                    heart4.Image = filledHeart;
                    heart5.Image = filledHeart;
                    break;
                case 7:
                    heart1.Image = overfilledHeart;
                    heart2.Image = overfilledHeart;
                    heart5.Image = filledHeart;
                    heart5.Image = filledHeart;
                    heart5.Image = filledHeart;
                    break;
                case 8:
                    heart1.Image = overfilledHeart;
                    heart2.Image = overfilledHeart;
                    heart3.Image = overfilledHeart;
                    heart5.Image = filledHeart;
                    heart5.Image = filledHeart;
                    break;
                case 9:
                    heart1.Image = overfilledHeart;
                    heart2.Image = overfilledHeart;
                    heart3.Image = overfilledHeart;
                    heart4.Image = overfilledHeart;
                    heart5.Image = filledHeart;
                    break;
                case 10:
                    heart1.Image = overfilledHeart;
                    heart2.Image = overfilledHeart;
                    heart3.Image = overfilledHeart;
                    heart4.Image = overfilledHeart;
                    heart5.Image = overfilledHeart;
                    break;
                default:
                    heart1.Image = overfilledHeart;
                    heart2.Image = overfilledHeart;
                    heart3.Image = overfilledHeart;
                    heart4.Image = overfilledHeart;
                    heart5.Image = overfilledHeartplus;
                    break;                                    
            }
        }
        public void Update() {
            drawHearts();
            CheckLives();
        }

        /// <summary>
        /// If 0 lives is reached, a signal is sent to GameRunning that changes the state to GameOver.
        /// </summary>
        private void CheckLives() {
            if (playerLives <= 0){
                gamebus.RegisterEvent (new GameEvent { 
                            EventType = GameEventType.GameStateEvent, From = this, Message = "GAME_OVER"
                        });
            }
        }
        /// <summary>
        /// Method that changes the PlayerLives if the ball is out or if the player is granted a life.
        /// </summary>
        ///<param name = "gameEvent"> the event whose type and message determines the outcome. </param>
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.GameStateEvent) {
                switch (gameEvent.Message) {
                    case "ALL_BALLS_OUT":
                        ChangePlayerLives(gameEvent.IntArg1);
                        break;
                }
            }
            if (gameEvent.EventType == GameEventType.StatusEvent) {
                switch (gameEvent.Message) {
                    case "GRANT_LIFE":
                        ChangePlayerLives(gameEvent.IntArg1);
                        break;
                }
            }
        }
    }
}