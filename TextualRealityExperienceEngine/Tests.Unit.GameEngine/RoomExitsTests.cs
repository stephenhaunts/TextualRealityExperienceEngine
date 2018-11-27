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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextualRealityExperienceEngine.GameEngine;

namespace TextualRealityExperienceEngine.Tests.Unit.GameEngine
{
    [TestClass]
    public class RoomExitsTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddExitThrowsArgumentNullExceptionIfRoomIsNull()
        {
            var roomExits = new RoomExits();
            roomExits.AddExit(Direction.North, null);
        }

        [TestMethod]
        public void AddExitForNorthAddsTheExit()
        {
            var room = new Room("testRoom", "this is a test room.");

            var roomExits = new RoomExits();
            roomExits.AddExit(Direction.North, room);

            var savedRoom = roomExits.GetRoomForExit(Direction.North);

            Assert.AreEqual(room, savedRoom);
            Assert.AreSame("testRoom", savedRoom.Name);
            Assert.AreSame("this is a test room.", savedRoom.Description);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddExitThrowsInvalidOperationExceptionIfSameDirectionUsedMoreThanOnce()
        {
            var room = new Room("testRoom", "this is a test room.");
            var room2 = new Room("testRoom2", "this is a test room.");

            var roomExits = new RoomExits();
            roomExits.AddExit(Direction.North, room);
            roomExits.AddExit(Direction.North, room2);

            roomExits.AddExit(Direction.South, room);
            roomExits.AddExit(Direction.South, room2);
        }

        [TestMethod]
        public void GetRoomForExitReturnsValidRoom()
        {
            var room = new Room("testRoom", "this is a test room.");
            var room2 = new Room("testRoom2", "this is a test room.");

            var roomExits = new RoomExits();
            roomExits.AddExit(Direction.North, room);
            roomExits.AddExit(Direction.South, room2);

            Assert.AreEqual(room, roomExits.GetRoomForExit(Direction.North));
            Assert.AreEqual(room2, roomExits.GetRoomForExit(Direction.South));
        }

        [TestMethod]
        public void GetRoomForExitReturnsNullForNonExistentRoom()
        {
            var room = new Room("testRoom", "this is a test room.");
            var room2 = new Room("testRoom2", "this is a test room.");

            var roomExits = new RoomExits();
            roomExits.AddExit(Direction.North, room);
            roomExits.AddExit(Direction.South, room2);

            Assert.AreEqual(null, roomExits.GetRoomForExit(Direction.East));
            Assert.AreEqual(null, roomExits.GetRoomForExit(Direction.West));
        }
    }
}
