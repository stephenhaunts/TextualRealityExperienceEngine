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
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextualRealityExperienceEngine.GameEngine;
using TextualRealityExperienceEngine.GameEngine.Interfaces;

namespace TextualRealityExperienceEngine.Tests.Unit.GameEngine
{
    [TestClass]
    public class RoomExitsTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddExitThrowsArgumentNullExceptionIfRoomIsNull()
        {
            RoomExits roomExits = new RoomExits();
            DoorWay doorway = new DoorWay
            {
                Direction = Direction.North
            };

            roomExits.AddExit(doorway, null);
        }

        [TestMethod]
        public void AddExitForNorthAddsTheExit()
        {
            var game = new Game();
            var room = new Room("testRoom", "this is a test room.", game);

            var roomExits = new RoomExits();

            DoorWay doorway = new DoorWay
            {
                Direction = Direction.North
            };

            roomExits.AddExit(doorway, room);

            var savedRoom = roomExits.GetRoomForExit(Direction.North);

            Assert.AreEqual(room, savedRoom);
            Assert.AreSame("testRoom", savedRoom.Name);
            Assert.AreSame("this is a test room.", savedRoom.Description);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddExitThrowsInvalidOperationExceptionIfSameDirectionUsedMoreThanOnce()
        {
            var game = new Game();
            var room = new Room("testRoom", "this is a test room.", game);
            var room2 = new Room("testRoom2", "this is a test room.", game);

            var roomExits = new RoomExits();

            DoorWay doorway = new DoorWay
            {
                Direction = Direction.North
            };

            DoorWay doorway2 = new DoorWay
            {
                Direction = Direction.South
            };

            roomExits.AddExit(doorway, room);
            roomExits.AddExit(doorway, room2);

            roomExits.AddExit(doorway2, room);
            roomExits.AddExit(doorway2, room2);
        }

        [TestMethod]
        public void GetRoomForExitReturnsValidRoom()
        {
            var game = new Game();
            var room = new Room("testRoom", "this is a test room.", game);
            var room2 = new Room("testRoom2", "this is a test room.", game);

            var roomExits = new RoomExits();

            DoorWay doorway = new DoorWay
            {
                Direction = Direction.North
            };

            DoorWay doorway2 = new DoorWay
            {
                Direction = Direction.South
            };
            roomExits.AddExit(doorway, room);
            roomExits.AddExit(doorway2, room2);

            Assert.AreEqual(room, roomExits.GetRoomForExit(Direction.North));
            Assert.AreEqual(room2, roomExits.GetRoomForExit(Direction.South));
        }

        [TestMethod]
        public void GetRoomForExitReturnsNullForNonExistentRoom()
        {
            var game = new Game();
            var room = new Room("testRoom", "this is a test room.", game);
            var room2 = new Room("testRoom2", "this is a test room.", game);

            var roomExits = new RoomExits();

            DoorWay doorway = new DoorWay
            {
                Direction = Direction.North
            };

            DoorWay doorway2 = new DoorWay
            {
                Direction = Direction.South
            };

            roomExits.AddExit(doorway, room);
            roomExits.AddExit(doorway2, room2);

            Assert.AreEqual(null, roomExits.GetRoomForExit(Direction.East));
            Assert.AreEqual(null, roomExits.GetRoomForExit(Direction.West));
        }

        [TestMethod]
        public void AddExitForNorthAddsTheExit_UnLockedDoor()
        {
            var game = new Game();
            var room = new Room("testRoom", "this is a test room.", game);

            var roomExits = new RoomExits();

            roomExits.AddExit(Direction.North, room);

            var savedRoom = roomExits.GetRoomForExit(Direction.North);

            Assert.AreEqual(room, savedRoom);
            Assert.AreSame("testRoom", savedRoom.Name);
            Assert.AreSame("this is a test room.", savedRoom.Description);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddExitThrowsInvalidOperationExceptionIfSameDirectionUsedMoreThanOnce_UnLockedDoor()
        {
            var game = new Game();
            var room = new Room("testRoom", "this is a test room.", game);
            var room2 = new Room("testRoom2", "this is a test room.", game);

            var roomExits = new RoomExits();

            roomExits.AddExit(Direction.North, room);
            roomExits.AddExit(Direction.North, room2);

            roomExits.AddExit(Direction.South, room);
            roomExits.AddExit(Direction.South, room2);
        }

        [TestMethod]
        public void GetRoomForExitReturnsValidRoom_UnLockedDoor()
        {
            var game = new Game();
            var room = new Room("testRoom", "this is a test room.", game);
            var room2 = new Room("testRoom2", "this is a test room.", game);

            var roomExits = new RoomExits();

            roomExits.AddExit(Direction.North, room);
            roomExits.AddExit(Direction.South, room2);

            Assert.AreEqual(room, roomExits.GetRoomForExit(Direction.North));
            Assert.AreEqual(room2, roomExits.GetRoomForExit(Direction.South));
        }

        [TestMethod]
        public void GetRoomForExitReturnsNullForNonExistentRoom_UnLockedDoor()
        {
            var game = new Game();
            var room = new Room("testRoom", "this is a test room.", game);
            var room2 = new Room("testRoom2", "this is a test room.", game);

            var roomExits = new RoomExits();

            roomExits.AddExit(Direction.North, room);
            roomExits.AddExit(Direction.South, room2);

            Assert.AreEqual(null, roomExits.GetRoomForExit(Direction.East));
            Assert.AreEqual(null, roomExits.GetRoomForExit(Direction.West));
        }
    }
}
