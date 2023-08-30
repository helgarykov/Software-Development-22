
namespace Breakout.BlockFunctions {
    
    /// <summary>
    /// Class used for converting from a string to an actual BlockType.
    /// </summary>
    public class BlockTypeConverter  {
        
        ///<Summary> This function uses a switch statement to convert a string to any of the 
        /// currently utilized BlockTypes. </summary>
        ///<param name = "block"> block is a string that represents a BlockType.  </param>
        ///<returns> The corresponding BlockType, to the entered string <returns>
        public static BlockType TransformStringToBlock(string block) {
            switch(block) {
                case "Hardened":
                    return BlockType.Hardened;
                case "Unbreakable":
                    return BlockType.Unbreakable;
                case "PowerUp":
                    return BlockType.PowerUp;
                default:
                    return BlockType.Standard;
            }
        }
    }
}