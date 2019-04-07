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
using System;
using System.Collections.Generic;
using TextualRealityExperienceEngine.GameEngine.Interfaces;

namespace TextualRealityExperienceEngine.GameEngine
{
    /// <summary>
    /// Visited rooms.
    /// </summary>
    public class VisitedRooms : IVisitedRooms
    {
        private readonly Dictionary<string, IRoom> _visitedRooms = new Dictionary<string, IRoom>();

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TextualRealityExperienceEngine.GameEngine.VisitedRooms"/> class.
        /// </summary>
        public VisitedRooms()
        {
            _visitedRooms = new Dictionary<string, IRoom>();
        }

        /// <summary>
        /// Adds the visited room.
        /// </summary>
        /// <param name="room">Room.</param>
        public void AddVisitedRoom(IRoom room)
        {
            if (room == null)
            {
                throw new ArgumentNullException(nameof(room));
            }

            if (!_visitedRooms.ContainsKey(room.Name.ToLower()))
            {
                _visitedRooms.Add(room.Name.ToLower(), room);
            }
        }

        /// <summary>
        /// Checks the room visited.
        /// </summary>
        /// <returns><c>true</c>, if room visited was checked, <c>false</c> otherwise.</returns>
        /// <param name="roomName">Room name.</param>
        public bool CheckRoomVisited(string roomName)
        {
            if (string.IsNullOrEmpty(roomName))
            {
                return false;
            }

            return _visitedRooms.ContainsKey(roomName.ToLower());
        }

        /// <summary>
        /// Gets the room instanve.
        /// </summary>
        /// <returns>The room instanve.</returns>
        /// <param name="roomName">Room name.</param>
        public IRoom GetRoomInstance(string roomName)
        {
            if (string.IsNullOrEmpty(roomName))
            {
                return null;
            }

            return _visitedRooms[roomName.ToLower()];
        }

        /// <summary>
        /// Gets the visited rooms.
        /// </summary>
        /// <returns>The visited rooms.</returns>
        public List<(string name, string description)> GetVisitedRooms()
        {
            var rooms = new List<(string name, string description)>();

            foreach (KeyValuePair<string, IRoom> entry in _visitedRooms)
            {
                rooms.Add((entry.Key, entry.Value.Description));
            }

            return rooms;
        }
    }
}
