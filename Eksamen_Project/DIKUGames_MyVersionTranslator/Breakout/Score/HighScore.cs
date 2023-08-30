using System;
using System.Linq;
using DIKUArcade.Events;

namespace Breakout {
    /// <summary>
    /// Class not currently in use.
    /// </summary>
    public class HighScore : IGameEventProcessor {

        private int playerPoints;
        private string[] rawlist;
        private string path;
        private List<string> nameList;
        List<string> pointList;
        private int newHighScorePos;
        private GameEventBus gamebus;
        public HighScore(Points? point) {
            gamebus = BreakoutBus.GetBus();
            path = Path.Combine("Assets", "score.txt");
            rawlist = System.IO.File.ReadAllLines(path);
            if (point != null) playerPoints = point.points;
            BreakoutBus.GetBus().Subscribe(GameEventType.ControlEvent, this);
            nameList = new List<string>{};
            pointList = new List<string>{};
            checkList();
        }
        private void checkList() {
            List<string> rawList = rawlist.ToList();
            var found = false;
            //Read score.txt and converts to another stringlist containing only numbers
            //And a similar list with scoreholder names.
            for (var i = 0; i <= rawList.Count - 1; i++) {
                    pointList.Add(rawList[i].Substring
                                    (rawList[i].IndexOf("-") + 2,
                                        rawList[i].Length - (int) rawList[i].IndexOf("-") - 2));
                    nameList.Add(rawList[i].Substring(0,3));
                }
            for (var i = 0; i <= pointList.Count - 1; i++) {
                    if ((playerPoints > Convert.ToInt32(pointList[i]) || nameList[i] == "N/A") && found == false) {
                        gamebus.RegisterEvent (new GameEvent { 
                        EventType = GameEventType.ControlEvent, From = this, Message = "REQUEST_PLAYER_NAME"
                        });
                        newHighScorePos = i;
                        found = true;
                    }
                }
        }
        private void resetScore() {
            rawlist = new string[] {"N/A - 0", "N/A - 0", "N/A - 0", "N/A - 0", "N/A - 0", "N/A - 0", "N/A - 0", "N/A - 0", "N/A - 0", "N/A - 0", "N/A - 0"};
        }
        //Inserts the 
        private void insertAndMove(int index, string name) {
            for (var i = pointList.Count - 1; i > index; i--) {
                pointList[i] = pointList[i-1];
                nameList[i] = nameList[i-1];
            }
            pointList[index] = playerPoints.ToString();
            nameList[index] = name;
            WriteToFile();
        }
        private void WriteToFile() {
            List<string> newList = new List<string>{};
            for (var i = 0; i <= pointList.Count - 1; i++) {
                    newList.Add(nameList[i] + " - " + pointList[i]);
                }
            File.WriteAllLines(path, newList.ToArray());
            rawlist = newList.ToArray();
            gamebus.RegisterEvent (new GameEvent { 
                        EventType = GameEventType.ControlEvent, From = this, Message = "RELOAD_SCORE"
                        });
        }
        public void ProcessEvent(GameEvent gameEvent){
            if (gameEvent.EventType == GameEventType.ControlEvent) {
                switch(gameEvent.Message) {    
                    case "RETURN_PLAYER_NAME":
                        insertAndMove(newHighScorePos, gameEvent.StringArg1);
                        break;
                }
        }
        }
    }
}