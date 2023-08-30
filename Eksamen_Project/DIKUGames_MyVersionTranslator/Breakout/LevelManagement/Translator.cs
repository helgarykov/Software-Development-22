using System.Collections.Generic;
using System.Linq;
using System;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using Breakout.BlockFunctions;
using DIKUArcade.Utilities;


namespace Breakout.LevelManagement
{
    ///<summary>
    /// Class for extracting information from the .txt files in the Assets directory.
    /// </summary>
    public static class Translator {
        private static EntityContainer<Block> blocks;
        static Translator() {
            blocks = new EntityContainer<Block>(300);
        }
        /// <summary>
        /// Method for extracting the legend data of the .txt files
        /// </summary>
        /// <param name="filename"> The file which the data are extracted from</param>
        /// <returns> Returns a list of strings with the legend info in it.</returns>
        public static string[] getLegend(string filename) {
            string[] levelStr = System.IO.File.ReadAllLines(Path.Combine("Assets", "Levels", filename));

            string[] getLegend(string filename) {
                var path = FileIO.GetProjectPath();
                string[] levelStr = System.IO.File.ReadAllLines(Path.Combine(@path, "Assets", "Levels", filename));
                string[] legendStr = { };
                //It iterates through the lines of the txt.file, until it finds the line "Legend:"
                for (var i = 0; i < levelStr.Length - 1; i++) {
                    if (levelStr[i] == "Legend:") {
                        //When Legend is found, it starts acting on the remaining lines of the file. It uses
                        //string slicing to create the new string[], in such a way that the first input will be
                        //the legend character, and the next input will be the legend effect. This is repeated
                        //for every legend input.
                        for (var j = i + 1; j < levelStr.Length - 1; j++) {
                            if (levelStr[j][1].ToString() == ")" && (levelStr[j].IndexOf(".png") != -1 ||
                                                                     levelStr[j].IndexOf(".jpeg") != -1)) {
                                legendStr = legendStr.Append(levelStr[j].Substring(0, 1)).ToArray();
                                legendStr = legendStr.Append(levelStr[j].Substring(3, levelStr[j].Length - 3))
                                    .ToArray();
                            }
                        }
                    }
                }
                return legendStr;
            }
            return getLegend(filename);
        }
        /// <summary>
        /// Method for extracting the meta data of the .txt files
        /// </summary>
        /// <param name="filename"> The file which the data are extracted from</param>
        /// <returns> Returns a list of strings with the meta info in it.</returns>
        public static string[] getMeta(string filename) {
                var path = FileIO.GetProjectPath();
                string[] levelStr = System.IO.File.ReadAllLines(Path.Combine(@path, "Assets", "Levels", filename));
                string[] metaStr = {};
                //Iterates until "Meta:" is found, and stops when "Meta/" is found.
                for (var i = 0; i < levelStr.Length - 1; i++) {
                    if (levelStr[i] == "Meta:") {
                        for (var j = i + 1; j < levelStr.Length - 1; j++) {
                            if (levelStr[j] == "Meta/") {
                                break;
                            }
                            //Between those two inputs it adds every line of the text file to metaStr, after using string
                            //slicing to seperate the string based on the position of the ":" char. The first input in the
                            //string[] will be the effect, and the following input will be the character. 
                            if (levelStr[j].IndexOf(":") != -1) {
                                metaStr = metaStr.Append(levelStr[j].Substring(0, levelStr[j].IndexOf(":"))).ToArray();
                                metaStr = metaStr.Append(levelStr[j].Substring
                                    (levelStr[j].IndexOf(":") + 2,
                                        levelStr[j].Length - levelStr[j].IndexOf(":") - 2))
                                    .ToArray();
                            }
                        }
                    }
                }
                return metaStr;
            }
    
        /// <summary>
        /// Method for converting the .txt file's information into blocks in the screen.
        /// </summary>
        /// <param name="filename"> The file which the data are extracted from</param>
        /// <returns> Returns an entity container with the blocks in it.</returns>
        public static EntityContainer<Block> CreateMap (string filename) 
        {
                blocks.ClearContainer();
                string[] legendStr = getLegend(filename);
                string[] metaStr = getMeta(filename);
                string[] levelStr = System.IO.File.ReadAllLines(Path.Combine("Assets", "Levels", filename));
                //This iterates through the lines of the text file, stopping when the line containing 
                //"Map/" is reached.
                for (var i = 1; i < levelStr.Length - 1; i++) {
                    if (levelStr[i] == "Map/") {
                        break;
                    }
                    //This iterates one step deeper, iterating through every character of the line.
                    for (var j = 0; j < levelStr[i].Length; j++) {
                        //These checks the current character in the line and compares the character 
                        //with any of the inputs in the legend or meta lists. If none are found, they
                        //return -1. 
                        var legendIndex = Array.IndexOf(legendStr, levelStr[i][j].ToString());
                        var metaIndex = Array.IndexOf(metaStr, levelStr[i][j].ToString());
                        //If the character corresponds to any of the characters in the meta list, this if statement 
                        //is true.
                        if (legendIndex != -1 && j < 12 && i < 22 && metaIndex != -1) {
                            //A block is added at the correct position, using the legend list for the correct block image,
                            //and the metaList for the correct BlockType.
                            blocks.AddEntity(new Block(
                                new DynamicShape(new Vec2F((float) j * 0.0833333f, Game.topY - ((float) i * 0.04f)),
                                    new Vec2F(0.0833333f, 0.04f)),
                                new Image(Path.Combine("Assets", "Images", legendStr[legendIndex + 1].ToString())),
                                new Image(Path.Combine("Assets", "Images", legendStr[legendIndex + 1].ToString().Substring(0, (legendStr[legendIndex + 1].Length -4)) + "-damaged.png")),
                                BlockTypeConverter.TransformStringToBlock(metaStr[metaIndex-1].ToString())));
                        }
                        if (legendIndex != -1 && j < 12 && i < 22 && metaIndex == -1) {
                            //In the case of no correspondance between the character and the meta list, a block
                            //will be created with the Standard BlockType.
                            blocks.AddEntity(new Block(
                                new DynamicShape(new Vec2F((float) j * 0.0833333f, Game.topY - ((float) i * 0.04f)),
                                    new Vec2F(0.0833333f, 0.04f)),
                                new Image(Path.Combine("Assets", "Images", legendStr[legendIndex + 1].ToString())),
                                new Image(Path.Combine("Assets", "Images", legendStr[legendIndex + 1].ToString().Substring(0, (legendStr[legendIndex + 1].Length -4)) + "-damaged.png")),
                                BlockType.Standard));
                        }
                    }
                }
                return blocks;
        }

        public static bool MapExists(string filename)
        {
            return System.IO.File.Exists(Path.Combine("Assets", "Levels", filename));
        }
        
    }
}
