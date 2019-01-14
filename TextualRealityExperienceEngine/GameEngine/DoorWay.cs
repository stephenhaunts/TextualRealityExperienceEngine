/*MIT License

Copyright(c) 2019 

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
    /// DoorWay represents a physical door way between two rooms.
    /// </summary>
    public sealed class DoorWay
    {
        /// <summary>
        /// Constructor that sets a door ways initial state which is unlocked and not requiring an object to unlock it.
        /// </summary>
        public DoorWay()
        {
            Locked = false;
        }
        
        /// <summary>
        /// The Direction property sets the direction of the exit from the room that it is defined; i.e. if the doorway
        /// is from room1 to room2 then the direction specifies the direction you need to go from room1 to room2, which
        /// might be north for example.
        /// </summary>
        public Direction Direction { get; set; }
        
        /// <summary>
        /// This flag specifies if the doorway is locked. True means it is locked and false means unlocked.
        /// </summary>
        public bool Locked { get; set; }
    }
}
