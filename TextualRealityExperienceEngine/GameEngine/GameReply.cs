/*
MIT License

Copyright (c) 2019 

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

namespace TextualRealityExperienceEngine.GameEngine
{
    /// <summary>
    /// When the parser is executed a textual reply is sent back to the caller to be displayed on the screen. This could
    /// be the description of the room or any string that represents the current game state to be displayed to the player.
    /// </summary>
    public class GameReply
    {
        /// <summary>
        /// The State of the game.
        ///
        /// Playing
        /// Inventory
        /// Exit
        /// Help
        /// Score
        /// Clearscreen
        ///
        /// </summary>
        public GameStateEnum State { get; set; }
        
        /// <summary>
        /// The text reply that is sent back to the calling application.
        /// </summary>
        public string Reply { get; set; }
    }
}
