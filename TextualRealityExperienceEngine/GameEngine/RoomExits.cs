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
    /// The RoomExits class contains a list of the exits assigned to a room for a given direction.
    /// </summary>
    public sealed class RoomExits : IRoomExits
    {
        private readonly Dictionary<DoorWay, IRoom> _roomMappings = new Dictionary<DoorWay, IRoom>();

        /// <summary>
        /// Add an exit to the list for a specified direction that leads to the room that is passed in.
        /// </summary>
        /// <param name="direction">The direction to assign the exit too.</param>
        /// <param name="room">The room that the exit leads too.</param>
        /// <exception cref="ArgumentNullException">If the room is null, throw an ArgumentNullException.</exception>
        /// <exception cref="InvalidOperationException">If you try to map a room to the same direction twice an
        /// InvalidOperationException will be thrown.</exception>
        public void AddExit(Direction direction, IRoom room)
        {
            if (room == null)
            {
                throw new ArgumentNullException(nameof(room));
            }

            var doorway = new DoorWay
            {
                Direction = direction,
                Locked = false,       
            };

            foreach (var entry in _roomMappings)
            {
                if (entry.Key.Direction == direction)
                {
                    throw new InvalidOperationException("A room mapping already exists for the direction <" + doorway.Direction + ">.");
                }
            }
            
            _roomMappings.Add(doorway, room);
        }

        /// <summary>
        /// Add an exit to the list for a specified direction that leads to the room that is passed in.
        /// </summary>
        /// <param name="doorway">Doorway object that encapsulates the direction.</param>
        /// <param name="room">The room that the exit leads too.</param>
        /// <param name="locked">Specify if this exit is locked.</param>
        /// <param name="objectToUnlock">The name of the object that can unlock this exit.</param>
        /// <exception cref="ArgumentNullException">If the room is null, throw an ArgumentNullException.</exception>
        /// <exception cref="InvalidOperationException">If you try to map a room to the same direction twice an
        /// InvalidOperationException will be thrown.</exception>
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

        /// <summary>
        /// Return the room assigned for a specific direction.
        /// </summary>
        /// <param name="direction">The direction to return the exist for.</param>
        /// <returns>The room that is assigned to this exit direction.</returns>
        public IRoom GetRoomForExit(Direction direction)
        {
            foreach (var entry in _roomMappings)
            {
                if (entry.Key.Direction == direction)
                {
                    return entry.Value;
                }
            }

            return null;
        }

        /// <summary>
        /// For a given exit, is that exit locked.
        /// </summary>
        /// <param name="direction">The direction to check the lock status for.</param>
        /// <returns>True if the exit is locked, False otherwise.</returns>
        public bool IsDoorLocked(Direction direction)
        {
            foreach (var entry in _roomMappings)
            {
                if (entry.Key.Direction == direction)
                {
                    return entry.Key.Locked;
                }
            }

            return false;
        }

        /// <summary>
        /// Return the door way object for the given direction.
        /// </summary>
        /// <param name="direction">The direction to get the door way object.</param>
        /// <returns>The retrieved door way.</returns>
        public DoorWay GetDoorWay(Direction direction)
        {
            foreach (var entry in _roomMappings)
            {
                if (entry.Key.Direction == direction)
                {
                    return entry.Key;
                }
            }

            return null;
        }

        /// <summary>
        /// Set the door lock status for the given direction.
        /// </summary>
        /// <param name="locked">True if you want the door locked, False otherwise.</param>
        /// <param name="direction">The direction of the exit that you want to set the lock status of.</param>
        public void SetDoorLock(bool locked, Direction direction)
        {
            foreach (var entry in _roomMappings)
            {
                if (entry.Key.Direction == direction)
                {
                    var door = entry.Key;
                    door.Locked = locked;
                }
            }
        }
    }
}
