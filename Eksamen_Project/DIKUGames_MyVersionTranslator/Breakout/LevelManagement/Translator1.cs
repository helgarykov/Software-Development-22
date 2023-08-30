using System.Collections.Generic;
using System.Linq;
using System;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using Breakout.BlockFunctions;


namespace Breakout.LevelManagement {
    public class Translator {
        public EntityContainer<Block> blocks;
        public Translator() {
            blocks = new EntityContainer<Block>(300);
        }

        public string[] GetBlock(string[] body, string tagName) // gets the array of strings that are tagged as fx Legend or Map or Meta. It Kind of cuts the whole array down to the part of we are interested in
        {
            for (int i = 0; i < body.Length; i++)
            {
                if (body[i] == $"{tagName}:")
                {
                    for (int j = i + 1; j < body.Length; j++)
                    {
                        if (body[j] == $"{tagName}/")
                        {
                            if (i >= j) return Array.Empty<string>();
                            return body[(i + 1)..(j - 1)]; // returns an array of strings beginning with the first line after the title (i=Legend, so we skip "Legend") and until ”/” : [ %) blue-block.png, 0) grey-block.png, 1) orange-block.png, a) purple-block.png]; Legend/ is not included
                        }
                    }

                    throw new Exception($"Missing end tag for tag {tagName}.");
                }
            }

            throw new Exception($"No tag {tagName} present in body.");
        }
        /// <summary>
        /// GetLegend er ansvarlig for 2 ting. Det er ikke så godt. Den faktiske input
        /// burde være string[] levelContentLines. Dictionary (key, value).
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetLegend(string filename)
        {
            // Det faktiske Input.
            string[] levelContentLines = System.IO.File.ReadAllLines(Path.Combine("Assets", "Levels", filename)); // returns an array og strings, where each string is the whole line (fx -000-%%-000-)
            //Output.
            Dictionary<string, string> results = new();

            string[] legendSection = GetBlock(levelContentLines, "Legend");    // bruger method form above

            foreach (string line in legendSection)
            {
                bool startsWithSingleLetter = line.Substring(1, 1) == ")";
                bool containsImageFileName = line.Contains(".png") || line.Contains(".jpeg");

                if (startsWithSingleLetter && containsImageFileName)
                {
                    string letterKey = line.Substring(0, 1);
                    string imageFileNameValue = line.Substring(3); // the start index is 3 and it gets the rest from the 3.index to the end of the line 
                
                    results.Add(letterKey, imageFileNameValue);        // a string array of tuples, with 1. string as a key as 2. as a value referred to by a key fx "level1.txt" [(%, blue-block.png), (0, grey-block.png), (1, orange-block.png), (a, purple-block.png), ]
                }    
            }

            return results;
        } 
        
        /** public string[] getLegend (string filename)
        {
            string[] levelStr = System.IO.File.ReadAllLines(Path.Combine("Assets", "Levels", filename));
            string[] legendStr = {};
            for (var i = 0; i < levelStr.Length-1 ; i++) {
                if (levelStr[i] == "Legend:") {
                    for (var j = i+1; j < levelStr.Length-1; j++) {
                        if (levelStr[j][1].ToString() == ")" && (levelStr[j].IndexOf(".png") != -1 || levelStr[j].IndexOf(".jpeg") != -1 )) {
                            legendStr = legendStr.Append(levelStr[j].Substring(0, 1)).ToArray();
                            legendStr = legendStr.Append(levelStr[j].Substring(3, levelStr[j].Length-3)).ToArray();
                        }
                    }
                }
            }
            return legendStr;
        }
        
        public string[] getMeta (string filename) {
            string[] levelStr = System.IO.File.ReadAllLines(Path.Combine("Assets", "Levels", filename));
            string[] metaStr = {};
            for (var i = 0; i < levelStr.Length-1 ; i++) {
                if (levelStr[i] == "Meta:") {
                    for (var j = i+1; j < levelStr.Length-1; j++) {
                        if (levelStr[j] == "Meta/") {
                            break;
                        }
                        if (levelStr[j].IndexOf(":") != -1) {
                            metaStr = metaStr.Append(levelStr[j].Substring(0, levelStr[j].IndexOf(":"))).ToArray();
                            metaStr = metaStr.Append(levelStr[j].Substring
                                                    (levelStr[j].IndexOf(":")+2, levelStr[j].Length-levelStr[j].IndexOf(":")-2)).ToArray();
                        }
                    }
                }
            }
            return metaStr;
        } #1#
        
        // Burde laves om så den bruger GetBlock().
        public EntityContainer<Block> CreateMap (string filename) {
            blocks.ClearContainer();
            string[] legendStr = getLegend(filename);
            string[] levelStr = System.IO.File.ReadAllLines(Path.Combine("Assets", "Levels", filename));
            for (var i = 1; i < levelStr.Length-1 ; i++)  {
                if (levelStr[i] == "Map/") {
                    break;
                }

                for (var j = 0; j < levelStr[i].Length ; j ++) {
                    var legendIndex = Array.IndexOf(legendStr, levelStr[i][j].ToString());
                    if (legendIndex != -1 && j < 12 && i < 22) {
                        blocks.AddEntity(new Block(
                            new DynamicShape(new Vec2F((float)j*0.0833333f, 
                            1-((float)i*0.04f)), new Vec2F(0.0833333f, 0.04f)),
                                new Image(Path.Combine("Assets", "Images", 
                                legendStr[legendIndex+1].ToString())),
                                 BlockTypeConverter.TransformStringToBlock(legendStr[legendIndex+1].ToString())));
                    }
                }
            }
            return blocks;
        }
    }
}*/