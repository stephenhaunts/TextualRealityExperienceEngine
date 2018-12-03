/*
MIT License

Copyright (c) 2018 

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

using TextualRealityExperienceEngine.GameEngine.Interfaces;

namespace TextualRealityExperienceEngine.GameEngine 
{
    public class Room : IRoom
    {
        readonly IRoomExits _roomExits = new RoomExits();

        public string Name { get; set; }
        public string Description { get; set; }

        public Room()
        {
            Name = string.Empty;
            Description = string.Empty;
        }

        public Room(IRoomExits roomExits)
        {
            Name = string.Empty;
            Description = string.Empty;
            _roomExits = roomExits;
        }

        public Room(string name, string description)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(Name), "The room name can not be empty.");
            }

            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentNullException(nameof(Description), "The room description can not be empty.");
            }

            Name = name;
            Description = description;
        }

        public void AddExit(Direction direction, IRoom room)
        {
            _roomExits.AddExit(direction, room);
        }

        public virtual void ProcessCommand(ICommand command)
        {

        }
    }
}
