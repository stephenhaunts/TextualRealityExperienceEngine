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

namespace TextualRealityExperienceEngine.Tests.Unit.GameEngine
{
    [TestClass]
    public class VisitedRoomsTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddVisitedRoomThrowsArgumentNullExceptionIfRoomIsNull()
        {
            var visitedRooms = new VisitedRooms();
            visitedRooms.AddVisitedRoom(null);
        }

        [TestMethod]
        public void AddVisitedRoomAddsRoomToVisitedList()
        {
            var game = new Game();
            var testRoom = new Room("TestRoom", "This is a test room.", game);

            var visitedRooms = new VisitedRooms();
            visitedRooms.AddVisitedRoom(testRoom);

            Assert.AreEqual(1, visitedRooms.GetVisitedRooms().Count);

            var rooms = visitedRooms.GetVisitedRooms();
            Assert.AreEqual("testroom", rooms[0].name);
            Assert.AreEqual("This is a test room.", rooms[0].description);
        }

        [TestMethod]
        public void AddVisitedRoomOnlyAddsRoomToListOnce()
        {
            var game = new Game();
            var testRoom = new Room("TestRoom", "This is a test room.", game);

            var visitedRooms = new VisitedRooms();
            visitedRooms.AddVisitedRoom(testRoom);
            visitedRooms.AddVisitedRoom(testRoom);

            Assert.AreEqual(1, visitedRooms.GetVisitedRooms().Count);

            var rooms = visitedRooms.GetVisitedRooms();
            Assert.AreEqual("testroom", rooms[0].name);
            Assert.AreEqual("This is a test room.", rooms[0].description);
        }

        [TestMethod]
        public void CheckRoomVisitedReturnsTrueForRoomInVisitedList()
        {
            var game = new Game();
            var testRoom = new Room("TestRoom", "This is a test room.", game);

            var visitedRooms = new VisitedRooms();
            visitedRooms.AddVisitedRoom(testRoom);

            Assert.AreEqual(1, visitedRooms.GetVisitedRooms().Count);

            Assert.IsTrue(visitedRooms.CheckRoomVisited("TestRoom"));
        }

        [TestMethod]
        public void CheckRoomVisitedReturnsFalseForRoomThatDoesntExist()
        {
            var game = new Game();
            var testRoom = new Room("TestRoom", "This is a test room.", game);

            var visitedRooms = new VisitedRooms();
            visitedRooms.AddVisitedRoom(testRoom);

            Assert.AreEqual(1, visitedRooms.GetVisitedRooms().Count);

            Assert.IsFalse(visitedRooms.CheckRoomVisited("Does not exist"));
        }

        [TestMethod]
        public void CheckRoomVisitedReturnsFalseForNullRoomName()
        {
            var game = new Game();
            var testRoom = new Room("TestRoom", "This is a test room.", game);

            var visitedRooms = new VisitedRooms();
            visitedRooms.AddVisitedRoom(testRoom);

            Assert.AreEqual(1, visitedRooms.GetVisitedRooms().Count);
            Assert.IsFalse(visitedRooms.CheckRoomVisited(null));
        }

        [TestMethod]
        public void CheckRoomVisitedReturnsFalseForEmptyStringRoomName()
        {
            var game = new Game();
            var testRoom = new Room("TestRoom", "This is a test room.", game);

            var visitedRooms = new VisitedRooms();
            visitedRooms.AddVisitedRoom(testRoom);

            Assert.AreEqual(1, visitedRooms.GetVisitedRooms().Count);
            Assert.IsFalse(visitedRooms.CheckRoomVisited(string.Empty));
        }

        [TestMethod]
        public void GetVisitedRoomsReturnsEmptyListWhenNoRoomsVisited()
        {
            var visitedRooms = new VisitedRooms();
            var visited = visitedRooms.GetVisitedRooms();

            Assert.IsNotNull(visited);
            Assert.AreEqual(0, visited.Count);
        }

        [TestMethod]
        public void GetVisitedRoomsReturnsListWhenTwoRoomsVisited()
        {
            var game = new Game();
            var testRoom = new Room("TestRoom", "This is a test room.", game);
            var testRoom2 = new Room("TestRoom2", "This is a test room2.", game);

            var visitedRooms = new VisitedRooms();
            visitedRooms.AddVisitedRoom(testRoom);
            visitedRooms.AddVisitedRoom(testRoom2);

            Assert.AreEqual(2, visitedRooms.GetVisitedRooms().Count);

            var rooms = visitedRooms.GetVisitedRooms();
            Assert.AreEqual("testroom", rooms[0].name);
            Assert.AreEqual("This is a test room.", rooms[0].description);

            Assert.AreEqual("testroom2", rooms[1].name);
            Assert.AreEqual("This is a test room2.", rooms[1].description);
        }                         
    }
}
