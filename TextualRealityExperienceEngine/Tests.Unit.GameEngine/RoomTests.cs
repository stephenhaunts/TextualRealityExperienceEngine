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
using TextualRealityExperienceEngine.GameEngine.Synonyms;
using TextualRealityExperienceEngine.Tests.Unit.GameEngine.Stubs;

namespace TextualRealityExperienceEngine.Tests.Unit.GameEngine
{
    [TestClass]
    public class RoomTests
    {
        [TestMethod]
        public void DescriptionIsNullWhenCreatedWithDefaultConstructor()
        {
            IGame game = new Game();
            var room = new Room(game);
            Assert.AreEqual(string.Empty, room.Description);
        }

        [TestMethod]
        public void NameIsEmptyWhenCreatedWithDefaultConstructor()
        {
            IGame game = new Game();
            var room = new Room(game);
            Assert.AreEqual(string.Empty, room.Name);
        }

        [TestMethod]
        public void DescriptionmIsSetByProperty()
        {
            IGame game = new Game();
            var room = new Room(game)
            {
                Description = "description"
            };

            Assert.AreEqual("description", room.Description);
        }

        [TestMethod]
        public void NameIsSetByProperty()
        {
            IGame game = new Game();
            var room = new Room(game)
            {
                Name = "name"
            };

            Assert.AreEqual("name", room.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "The room name can not be empty.")]
        public void OveriddenConstructorThrowsArgumentNullExcpetionIfNameIsNull()
        {
            var room = new Room("", null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "The room description can not be null.")]
        public void OveriddenConstructorThrowsArgumentNullExcpetionIfDescriptionIsNull()
        {
            var room = new Room("name", null, null);
        }

        [TestMethod]
        public void OveriddenConstructorSetsName()
        {
            var room = new Room("name", "description", null);

            Assert.AreEqual("name", room.Name);
        }

        [TestMethod]
        public void OveriddenConstructorSetsDescriptionm()
        {
            var room = new Room("name", "description", null);

            Assert.AreEqual("description", room.Description);
        }

        [TestMethod]
        public void AddExitCallsAddExitOnGameExits()
        {
            IGame game = new Game();
            var roomExits = new RoomExitsStub();
            var room = new Room(roomExits, game);
            var room2 = new Room("name2", "description2", null);

            DoorWay doorway = new DoorWay
            {
                Direction = Direction.North
            };

            room.AddExit(doorway, room2, false);

            Assert.AreEqual(1, roomExits.AddExitCounter);
        }

        [TestMethod]
        public void AddExitCallsAddExitOnGameExitWithNoReturnRoute()
        {
            IGame game = new Game();
            var roomExits = new RoomExitsStub();
            var roomExits2 = new RoomExitsStub();

            var room = new Room(roomExits, game);
            var room2 = new Room(roomExits2, game);

            DoorWay doorway = new DoorWay
            {
                Direction = Direction.North
            };

            room.AddExit(doorway, room2, false);

            Assert.AreEqual(1, roomExits.AddExitCounter);
            Assert.AreEqual(0, roomExits2.AddExitCounter);
        }

        [TestMethod]
        public void AddExitCallsAddsExitWithReturn()
        {
            IGame game = new Game();
            var roomExits = new RoomExitsStub();
            var roomExits2 = new RoomExitsStub();

            var room = new Room(roomExits, game);
            var room2 = new Room(roomExits2, game);

            DoorWay doorway = new DoorWay
            {
                Direction = Direction.North
            };

            room.AddExit(doorway, room2);

            Assert.AreEqual(1, roomExits.AddExitCounter);
            Assert.AreEqual(1, roomExits2.AddExitCounter);
        }

        [TestMethod]
        public void MovingBetweenRoomsIncrementsNumberOfMoves()
        {
            IGame game = new Game();
            var room = new Room(game);
            var room2 = new Room("name2", "description2", game);

            game.CurrentRoom = room;
            game.StartRoom = room;

            DoorWay doorway = new DoorWay
            {
                Direction = Direction.North
            };

            room.AddExit(doorway, room2, true);

            game.ProcessCommand("go north");
            game.ProcessCommand("go south");

            Assert.AreEqual(2, game.NumberOfMoves);
        }

        [TestMethod]
        public void LightsOffReturnsDarkDescription()
        {
            IGame game = new Game();
            var room = new Room("TestRoom", "The room is light.", game)
            {
                LightsOn = true, LightsOffDescription = "It is dark."
            };

            Assert.AreEqual("The room is light.", room.Description);

            room.LightsOn = false;

            Assert.AreEqual("It is dark.", room.Description);
        }

        [TestMethod]
        public void AddExitCallsAddExitOnGameExits_UnLockedDoor()
        {
            IGame game = new Game();
            var roomExits = new RoomExitsStub();
            var room = new Room(roomExits, game);
            var room2 = new Room("name2", "description2", null);

            room.AddExit(Direction.North, room2, false);

            Assert.AreEqual(1, roomExits.AddExitCounter);
        }

        [TestMethod]
        public void AddExitCallsAddExitOnGameExitWithNoReturnRoute_UnLockedDoor()
        {
            IGame game = new Game();
            var roomExits = new RoomExitsStub();
            var roomExits2 = new RoomExitsStub();

            var room = new Room(roomExits, game);
            var room2 = new Room(roomExits2, game);

            room.AddExit(Direction.North, room2, false);

            Assert.AreEqual(1, roomExits.AddExitCounter);
            Assert.AreEqual(0, roomExits2.AddExitCounter);
        }

        [TestMethod]
        public void AddExitCallsAddsExitWithReturn_UnLockedDoor()
        {
            IGame game = new Game();
            var roomExits = new RoomExitsStub();
            var roomExits2 = new RoomExitsStub();

            var room = new Room(roomExits, game);
            var room2 = new Room(roomExits2, game);

            room.AddExit(Direction.North, room2);

            Assert.AreEqual(1, roomExits.AddExitCounter);
            Assert.AreEqual(1, roomExits2.AddExitCounter);
        }

        [TestMethod]
        public void MovingBetweenRoomsIncrementsNumberOfMoves_UnLockedDoor()
        {
            IGame game = new Game();
            var room = new Room(game);
            var room2 = new Room("name2", "description2", game);

            game.CurrentRoom = room;
            game.StartRoom = room;

            room.AddExit(Direction.North, room2, true);

            game.ProcessCommand("go north");
            game.ProcessCommand("go south");

            Assert.AreEqual(2, game.NumberOfMoves);
        }

        [TestMethod]
        public void DroppedObjectThatIsntInInventoryDoesNothing()
        {
            IGame game = new Game();
            var room = new Room(game);

            IObject key = new GameObject("key", "A rusty key", "You picked up the key.");

            game.CurrentRoom = room;
            game.StartRoom = room;

            // Initial state where they key is in the inventory.
            Assert.AreEqual(0, game.Inventory.Count());
            Assert.AreEqual(0, room.DroppedObjects.DroppedObjectsList.Count);
            
            Assert.IsFalse(room.DroppedObjects.DropObject(key.Name));
            
            // Key is removed from the inventory and dropped on the floor.
            Assert.AreEqual(0, game.Inventory.Count());
            Assert.AreEqual(0, room.DroppedObjects.DroppedObjectsList.Count);
        }
        
        [TestMethod]
        public void DroppedObjectAddsObjectToDroppedObjectsList()
        {
            IGame game = new Game();
            var room = new Room(game);

            IObject key = new GameObject("key", "A rusty key", "You picked up the key.");
            game.Inventory.Add("key", key);

            game.CurrentRoom = room;
            game.StartRoom = room;

            // Initial state where they key is in the inventory.
            Assert.AreEqual(1, game.Inventory.Count());
            Assert.AreEqual(0, room.DroppedObjects.DroppedObjectsList.Count);
            
            Assert.IsTrue(room.DroppedObjects.DropObject(key.Name));
            
            // Key is removed from the inventory and dropped on the floor.
            Assert.AreEqual(0, game.Inventory.Count());
            Assert.AreEqual(1, room.DroppedObjects.DroppedObjectsList.Count);
        }
        
        [TestMethod]
        public void PickupDroppedObjectAddsObjectToInventory()
        {
            IGame game = new Game();
            var room = new Room(game);

            IObject key = new GameObject("key", "A rusty key", "You picked up the key.");
            game.Inventory.Add("key", key);

            game.CurrentRoom = room;
            game.StartRoom = room;

            // Initial state where they key is in the inventory.
            Assert.AreEqual(1, game.Inventory.Count());
            Assert.AreEqual(0, room.DroppedObjects.DroppedObjectsList.Count);
            
            Assert.IsTrue(room.DroppedObjects.DropObject(key.Name));
            
            // Key is removed from the inventory and dropped on the floor.
            Assert.AreEqual(0, game.Inventory.Count());
            Assert.AreEqual(1, room.DroppedObjects.DroppedObjectsList.Count);
            
            Assert.IsTrue(room.DroppedObjects.PickUpDroppedObject(key.Name));
            
            // Key is picked up and placed in the inventory
            Assert.AreEqual(1, game.Inventory.Count());
            Assert.AreEqual(0, room.DroppedObjects.DroppedObjectsList.Count);
        }
    }
}
