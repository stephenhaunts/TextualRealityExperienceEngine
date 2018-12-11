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
using TextualRealityExperienceEngine.GameEngine;
using TextualRealityExperienceEngine.GameEngine.Interfaces;

namespace TextualRealityExperienceEngine.Tests.Unit.GameEngine.Stubs
{
    public class RoomExitsStub : IRoomExits
    {
        public int AddExitCounter { get; set; }
        public int GetRoomCounter { get; set; }

        public void AddExit(DoorWay door, IRoom room, bool locked = false, string objectToUnlock = "")
        {
            AddExitCounter++;
        }

        public void AddExit(Direction direction, IRoom room)
        {
            AddExitCounter++;
        }

        public DoorWay GetDoorWay(Direction direction)
        {
            throw new NotImplementedException();
        }

        public IRoom GetRoomForExit(Direction direction)
        {
            GetRoomCounter++;
            return new Room();
        }

        public bool IsDoorLocked(Direction direction)
        {
            return false;
        }

        public void SetDoorLock(bool locked, Direction direction)
        {
            throw new NotImplementedException();
        }
    }
}
