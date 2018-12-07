/*MIT License

Copyright(c) 2018 

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
    public sealed class RoomExits : IRoomExits
    {
        readonly Dictionary<DoorWay, IRoom> _roomMappings = new Dictionary<DoorWay, IRoom>();

        public void AddExit(DoorWay doorway, IRoom room, bool locked = false, string objectToUnlock = "")
        {
            if (room == null)
            {
                throw new ArgumentNullException(nameof(room));
            }

            if (!_roomMappings.ContainsKey(doorway))
            {
                _roomMappings.Add(doorway, room);
            }
            else
            {
                throw new InvalidOperationException("A room mapping already exists for the direction <" + doorway.Direction + ">.");
            }
        }

        public IRoom GetRoomForExit(Direction direction)
        {
            foreach (KeyValuePair<DoorWay, IRoom> entry in _roomMappings)
            {
                if (entry.Key.Direction == direction)
                {
                    return entry.Value;
                }
            }

            return null;
        }
    }
}
