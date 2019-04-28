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
using System.Collections.Generic;
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
            Assert.AreEqual(0, game.Player.Inventory.Count());
            Assert.AreEqual(0, room.DroppedObjects.DroppedObjectsList.Count);
            
            Assert.IsFalse(room.DroppedObjects.DropObject(key.Name));
            
            // Key is removed from the inventory and dropped on the floor.
            Assert.AreEqual(0, game.Player.Inventory.Count());
            Assert.AreEqual(0, room.DroppedObjects.DroppedObjectsList.Count);
        }
        
        [TestMethod]
        public void DroppedObjectAddsObjectToDroppedObjectsList()
        {
            IGame game = new Game();
            var room = new Room(game);

            IObject key = new GameObject("key", "A rusty key", "You picked up the key.");
            game.Player.Inventory.Add("key", key);

            game.CurrentRoom = room;
            game.StartRoom = room;

            // Initial state where they key is in the inventory.
            Assert.AreEqual(1, game.Player.Inventory.Count());
            Assert.AreEqual(0, room.DroppedObjects.DroppedObjectsList.Count);
            
            Assert.IsTrue(room.DroppedObjects.DropObject(key.Name));
            
            // Key is removed from the inventory and dropped on the floor.
            Assert.AreEqual(0, game.Player.Inventory.Count());
            Assert.AreEqual(1, room.DroppedObjects.DroppedObjectsList.Count);
        }
        
        [TestMethod]
        public void PickupDroppedObjectAddsObjectToInventory()
        {
            IGame game = new Game();
            var room = new Room(game);

            IObject key = new GameObject("key", "A rusty key", "You picked up the key.");
            game.Player.Inventory.Add("key", key);

            game.CurrentRoom = room;
            game.StartRoom = room;

            // Initial state where they key is in the inventory.
            Assert.AreEqual(1, game.Player.Inventory.Count());
            Assert.AreEqual(0, room.DroppedObjects.DroppedObjectsList.Count);
            
            Assert.IsTrue(room.DroppedObjects.DropObject(key.Name));
            
            // Key is removed from the inventory and dropped on the floor.
            Assert.AreEqual(0, game.Player.Inventory.Count());
            Assert.AreEqual(1, room.DroppedObjects.DroppedObjectsList.Count);
            
            Assert.IsTrue(room.DroppedObjects.PickUpDroppedObject(key.Name));
            
            // Key is picked up and placed in the inventory
            Assert.AreEqual(1, game.Player.Inventory.Count());
            Assert.AreEqual(0, room.DroppedObjects.DroppedObjectsList.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GotoRoomThrowsArgumentNullExceptionIfNounIsNull()
        {
            IGame game = new Game();
            var room = new Room(game);
            room.GotoRoom("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GotoRoomThrowsArgumentExceptionForInvalidNoun()
        {
            IGame game = new Game();
            var room = new Room(game);
            room.GotoRoom("floof");
        }

        [TestMethod]
        public void GotoRoomReturnsNoRoomMessageForNoExit()
        {
            IGame game = new Game();
            
            var room = new Room(game);
            var room2 = new Room("name2", "description2", game);

            game.CurrentRoom = room;
            game.StartRoom = room;

            room.AddExit(Direction.North, room2, true);

            var message = room.GotoRoom("south");
            Assert.AreEqual("There is no exit to the south.", message);
        }

        [TestMethod]
        public void GotoRoomReturnsRoom()
        {
            IGame game = new Game();

            var room = new Room(game);
            var room2 = new Room("name2", "description2", game);

            game.CurrentRoom = room;
            game.StartRoom = room;

            room.AddExit(Direction.North, room2, true);

            var message = room.GotoRoom("north");
            Assert.AreEqual("description2", message);
        }

        [TestMethod]
        public void GotoRoomSetsCurrentRoom()
        {
            IGame game = new Game();

            var room = new Room("name1", "description1", game);
            var room2 = new Room("name2", "description2", game);

            game.CurrentRoom = room;
            game.StartRoom = room;

            room.AddExit(Direction.North, room2, true);

            Assert.AreEqual("description1", game.CurrentRoom.Description);
            Assert.AreEqual("name1", game.CurrentRoom.Name);
            Assert.AreEqual("description1", game.StartRoom.Description);
            Assert.AreEqual("name1", game.StartRoom.Name);

            room.GotoRoom("north");

            Assert.AreEqual("description2", game.CurrentRoom.Description);
            Assert.AreEqual("name2", game.CurrentRoom.Name);
            Assert.AreEqual("description1", game.StartRoom.Description);
            Assert.AreEqual("name1", game.StartRoom.Name);
        }

        [TestMethod]
        public void GotoRoomSetsNewRoomAndReturnsDroppedObjectInDescription()
        {
            IGame game = new Game();
            var room = new Room("test", "description", game);


            IObject key = new GameObject("key", "A rusty key", "You picked up the key.");
            game.Player.Inventory.Add("key", key);

            game.CurrentRoom = room;
            game.StartRoom = room;

            room.AddExit(Direction.North, room, true);

            Assert.IsTrue(room.DroppedObjects.DropObject(key.Name));
            var message = room.GotoRoom("north");
            Assert.AreEqual("description\r\n\r\nThere is a key on the floor.", message);
        }

        [TestMethod]
        public void GotoRoomSetsRoom()
        {
            IGame game = new Game();

            var room = new Room(game);
            var room2 = new Room("name2", "description2", game);

            game.CurrentRoom = room;
            game.StartRoom = room;

            room.AddExit(Direction.North, room2, true);

            var message = room.GotoRoom("north");
            Assert.AreEqual("description2", message);
            Assert.IsTrue(game.VisitedRooms.CheckRoomVisited("name2"));

            var visitedRoom = game.VisitedRooms.GetRoomInstance("name2");
            Assert.AreEqual("name2", visitedRoom.Name);
            Assert.AreEqual("description2", visitedRoom.Description);
        }

        [TestMethod]
        public void GotoRoomIncrementsNumberOfMoves()
        {
            IGame game = new Game();

            var room = new Room(game);
            var room2 = new Room("name2", "description2", game);

            game.CurrentRoom = room;
            game.StartRoom = room;

            room.AddExit(Direction.North, room2, true);
        
            Assert.AreEqual(0, game.NumberOfMoves);
            room.GotoRoom("north");
            Assert.AreEqual(1, game.NumberOfMoves);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ProcessCommandThrowsArgumentNullExceptionIfCommandIsNull()
        {
            IGame game = new Game();

            var room = new Room(game);
            room.ProcessCommand(null);
        }

        [TestMethod]
        public void ProcessCommandReturnsEmptyStringForNoCommand()
        {
            IGame game = new Game();

            var room = new Room(game);

            ICommand command = new Command
            {
                Verb = VerbCodes.NoCommand
            };

            Assert.AreEqual(string.Empty, room.ProcessCommand(command));
        }

        [TestMethod]
        public void ProcessCommandReturnsOopsStringForBadNounWhenMoving()
        {
            IGame game = new Game();

            var room = new Room(game);         

            ICommand command = new Command
            {
                Verb = VerbCodes.Go,
                Noun = "banannas"
            };

            var reply = room.ProcessCommand(command);

            Assert.AreEqual("Oops", reply);
        }

        [TestMethod]
        public void ProcessCommandReturnsRoomDescriptionForRoomWithNoDroppedObjects()
        {
            IGame game = new Game();

            var room = new Room("room", "This is a standard room.", game);

            ICommand command = new Command
            {
                Verb = VerbCodes.Look
            };

            var reply = room.ProcessCommand(command);

            Assert.AreEqual("This is a standard room.", reply);
        }

        [TestMethod]
        public void ProcessCommandReturnsRoomDescriptionForRoomWithSingleDroppedObject()
        {
            IGame game = new Game();

            var room = new Room("room", "This is a standard room.", game);

            IObject key = new GameObject("key", "A rusty key", "You picked up the key.");
            game.Player.Inventory.Add("key", key);
            room.DroppedObjects.DropObject(key.Name);

            ICommand command = new Command
            {
                Verb = VerbCodes.Look
            };

            var reply = room.ProcessCommand(command);

            Assert.AreEqual("This is a standard room.\r\n\r\nThere is a key on the floor.", reply);
        }

        [TestMethod]
        public void ProcessCommandReturnsRoomDescriptionForRoomWithMultipleDroppedObject()
        {
            IGame game = new Game();

            var room = new Room("room", "This is a standard room.", game);

            IObject key = new GameObject("key", "A rusty key", "You picked up the key.");
            IObject banana = new GameObject("banana", "A yellow banana", "You picked up the banana.");

            game.Player.Inventory.Add("key", key);
            game.Player.Inventory.Add("banana", banana);

            room.DroppedObjects.DropObject(key.Name);
            room.DroppedObjects.DropObject(banana.Name);

            ICommand command = new Command
            {
                Verb = VerbCodes.Look
            };

            var reply = room.ProcessCommand(command);

            Assert.AreEqual("This is a standard room.\r\n\r\nThere is a key on the floor.\r\nThere is a banana on the floor.", reply);
        }

        [TestMethod]
        public void ProcessCommandReturnsMessageWhenYouPickUpAnObject()
        {
            IGame game = new Game();

            var room = new Room("room", "This is a standard room.", game);

            IObject key = new GameObject("key", "A rusty key", "You picked up the key.");
            game.Player.Inventory.Add("key", key);
            room.DroppedObjects.DropObject(key.Name);

            ICommand command = new Command
            {
                Verb = VerbCodes.Take,
                Noun = "key"
            };

            var reply = room.ProcessCommand(command);

            Assert.AreEqual("You pick up the key.", reply);
        }

        [TestMethod]
        public void ProcessCommandPicksUpAnObject()
        {
            IGame game = new Game();

            var room = new Room("room", "This is a standard room.", game);

            IObject key = new GameObject("key", "A rusty key", "You picked up the key.");
            game.Player.Inventory.Add("key", key);
            room.DroppedObjects.DropObject(key.Name);

            Assert.AreEqual(1, room.DroppedObjects.DroppedObjectsList.Count);

            ICommand command = new Command
            {
                Verb = VerbCodes.Take,
                Noun = "key"
            };

            var reply = room.ProcessCommand(command);

            Assert.AreEqual(0, room.DroppedObjects.DroppedObjectsList.Count);
            Assert.AreEqual("You pick up the key.", reply);
        }

        [TestMethod]
        public void ProcessCommandReturnsMessageWhenYouTryToPickUpAnInvalidObject()
        {
            IGame game = new Game();

            var room = new Room("room", "This is a standard room.", game);

            IObject key = new GameObject("key", "A rusty key", "You picked up the key.");
            game.Player.Inventory.Add("key", key);
            room.DroppedObjects.DropObject(key.Name);

            ICommand command = new Command
            {
                Verb = VerbCodes.Take,
                Noun = "cabbage"
            };

            var reply = room.ProcessCommand(command);

            Assert.AreEqual("You can not pick up a cabbage.", reply);
        }

        [TestMethod]
        public void ProcessCommandReturnsMessageWhenYouDropAnObjectFromYourInventory()
        {
            IGame game = new Game();

            var room = new Room("room", "This is a standard room.", game);

            IObject key = new GameObject("key", "A rusty key", "You picked up the key.");
            game.Player.Inventory.Add("key", key);
     
            ICommand command = new Command
            {
                Verb = VerbCodes.Drop,
                Noun = "key"
            };

            var reply = room.ProcessCommand(command);

            Assert.AreEqual("You drop the key.", reply);
        }

        [TestMethod]
        public void ProcessCommandReturnsMessageWhenYouFailToDropAnObjectInYourIventory()
        {
            IGame game = new Game();

            var room = new Room("room", "This is a standard room.", game);

            IObject key = new GameObject("key", "A rusty key", "You picked up the key.");
            game.Player.Inventory.Add("key", key);

            ICommand command = new Command
            {
                Verb = VerbCodes.Drop,
                Noun = "cabbage"
            };

            var reply = room.ProcessCommand(command);

            Assert.AreEqual("You do not have a cabbage to drop.", reply);
        }

        [TestMethod]
        public void ProcessCommandDropsAnObjectToTheDroppedObjectList()
        {
            IGame game = new Game();

            var room = new Room("room", "This is a standard room.", game);

            IObject key = new GameObject("key", "A rusty key", "You picked up the key.");
            game.Player.Inventory.Add("key", key);

            ICommand command = new Command
            {
                Verb = VerbCodes.Drop,
                Noun = "key"
            };

            Assert.AreEqual(0, room.DroppedObjects.DroppedObjectsList.Count);
            var reply = room.ProcessCommand(command);

            Assert.AreEqual("You drop the key.", reply);
            Assert.AreEqual(1, room.DroppedObjects.DroppedObjectsList.Count);
        }

        [TestMethod]
        public void ProcessCommandReturnsEmptyStringIfHintRequestWhenDisabled()
        {
            IGame game = new Game();

            var room = new Room("room", "This is a standard room.", game);

            game.HintSystemEnabled = false;

            ICommand command = new Command
            {
                Verb = VerbCodes.Hint
            };

            Assert.AreEqual(string.Empty, room.ProcessCommand(command));
        }

        [TestMethod]
        public void ProcessCommandReturnsNoHintStringIfDifficultyEasy()
        {
            IGame game = new Game();

            var room = new Room("room", "This is a standard room.", game);

            game.HintSystemEnabled = true;
            game.Difficulty = DifficultyEnum.Easy;

            ICommand command = new Command
            {
                Verb = VerbCodes.Hint
            };

            Assert.AreEqual("There are no more hints available.", room.ProcessCommand(command));
        }

        [TestMethod]
        public void ProcessCommandReturnsNoHintStringIfDifficultyMedium()
        {
            IGame game = new Game();

            var room = new Room("room", "This is a standard room.", game);

            game.HintSystemEnabled = true;
            game.Difficulty = DifficultyEnum.Medium;

            ICommand command = new Command
            {
                Verb = VerbCodes.Hint
            };

            Assert.AreEqual("There are no more hints available.", room.ProcessCommand(command));
        }

        [TestMethod]
        public void ProcessCommandReturnsNoHintStringIfDifficultyHard()
        {
            IGame game = new Game();

            var room = new Room("room", "This is a standard room.", game);

            game.HintSystemEnabled = true;
            game.Difficulty = DifficultyEnum.Hard;

            ICommand command = new Command
            {
                Verb = VerbCodes.Hint
            };

            Assert.AreEqual("Hints are not allowed for the Hard difficulty.", room.ProcessCommand(command));
        }

        [TestMethod]
        public void ProcessCommandVisitsRoom()
        {
            IGame game = new Game();

            var room = new Room("name1", "description1", game);
            var room2 = new Room("name2", "description2", game);

            game.CurrentRoom = room;
            game.StartRoom = room;

            room.AddExit(Direction.North, room2, true);

            ICommand command = new Command
            {
                Verb = VerbCodes.Go,
                Noun = "north"
            };

            Assert.AreEqual("description2", room.ProcessCommand(command));

            ICommand command2 = new Command
            {
                Verb = VerbCodes.Visit,
                Noun = "name1"
            };

            Assert.AreEqual(2, game.VisitedRooms.GetVisitedRooms().Count);
            Assert.AreEqual("description1", room.ProcessCommand(command2));

            command2.Noun = "name2";
            Assert.AreEqual("description2", room.ProcessCommand(command2));
        }

        [TestMethod]
        public void ProcessCommandCantVisitRoomThatHasNotAlreadyBeenVisited()
        {
            IGame game = new Game();

            var room = new Room("name1", "description1", game);
            var room2 = new Room("name2", "description2", game);

            game.CurrentRoom = room;
            game.StartRoom = room;

            room.AddExit(Direction.North, room2, true);

            ICommand command = new Command
            {
                Verb = VerbCodes.Visit,
                Noun = "name2"
            };

            Assert.AreEqual(1, game.VisitedRooms.GetVisitedRooms().Count);
            Assert.AreEqual("You can not visit this room as you have not previously been there.", room.ProcessCommand(command));
        }

        [TestMethod]
        public void ProcessCommandEatsEdibleObject()
        {
            IGame game = new Game();

            var room = new Room("name1", "description1", game);

            game.CurrentRoom = room;
            game.StartRoom = room;

            IObject _chicken = new GameObject("Chicken", "It is the remains of a cooked chicken.", "You pick up the chicken.", true, 10, "health", "You eat the chicken; it was lovely.");
            game.Player.Inventory.Add(_chicken.Name, _chicken);

            ICommand command = new Command
            {
                Verb = VerbCodes.Eat,
                Noun = "chicken"
            };

            var reply = room.ProcessCommand(command);

            Assert.AreEqual("You eat the chicken; it was lovely.\r\nPlayer HEALTH increased by 10 points to : 10", reply);
        }

        [TestMethod]
        public void ProcessCommandEatsEdibleObjectAddsPointsToHealthStat()
        {
            IGame game = new Game();

            var room = new Room("name1", "description1", game);

            game.CurrentRoom = room;
            game.StartRoom = room;

            IObject _chicken = new GameObject("Chicken", "It is the remains of a cooked chicken.", "You pick up the chicken.", true, 10, "health", "You eat the chicken; it was lovely.");
            game.Player.Inventory.Add(_chicken.Name, _chicken);

            ICommand command = new Command
            {
                Verb = VerbCodes.Eat,
                Noun = "chicken"
            };

            Assert.AreEqual(0, game.Player.PlayerStats.Get("health"));

            var reply = room.ProcessCommand(command);

            Assert.AreEqual("You eat the chicken; it was lovely.\r\nPlayer HEALTH increased by 10 points to : 10", reply);
            Assert.AreEqual(10, game.Player.PlayerStats.Get("health"));
        }

        [TestMethod]
        public void ProcessCommandEatsEdibleObjectRemovesFromInventory()
        {
            IGame game = new Game();

            var room = new Room("name1", "description1", game);

            game.CurrentRoom = room;
            game.StartRoom = room;

            IObject _chicken = new GameObject("Chicken", "It is the remains of a cooked chicken.", "You pick up the chicken.", true, 10, "health", "You eat the chicken; it was lovely.");
            game.Player.Inventory.Add(_chicken.Name, _chicken);

            ICommand command = new Command
            {
                Verb = VerbCodes.Eat,
                Noun = "chicken"
            };

            Assert.IsTrue(game.Player.Inventory.Exists("Chicken"));

            var reply = room.ProcessCommand(command);

            Assert.AreEqual("You eat the chicken; it was lovely.\r\nPlayer HEALTH increased by 10 points to : 10", reply);
            Assert.IsFalse(game.Player.Inventory.Exists("Chicken"));
        }

        [TestMethod]
        public void ProcessCommandEatsEdibleFailsIfNotInTheInventory()
        {
            IGame game = new Game();

            var room = new Room("name1", "description1", game);

            game.CurrentRoom = room;
            game.StartRoom = room;

            IObject _chicken = new GameObject("Chicken", "It is the remains of a cooked chicken.", "You pick up the chicken.", true, 10, "health", "You eat the chicken; it was lovely.");

            ICommand command = new Command
            {
                Verb = VerbCodes.Eat,
                Noun = "chicken"
            };

            Assert.IsFalse(game.Player.Inventory.Exists("Chicken"));

            var reply = room.ProcessCommand(command);

            Assert.AreEqual("I'm not hungry.", reply);

            Assert.IsFalse(game.Player.Inventory.Exists("Chicken"));
        }

        [TestMethod]
        public void ProcessCommandEatsEdibleObjectWithMultipleStatsToUpdate()
        {
            IGame game = new Game();

            var room = new Room("name1", "description1", game);

            game.CurrentRoom = room;
            game.StartRoom = room;

            List<PlayerStatsAdjustments> stats = new List<PlayerStatsAdjustments>();
            PlayerStatsAdjustments health = new PlayerStatsAdjustments("health", 10);
            PlayerStatsAdjustments skill = new PlayerStatsAdjustments("skill", 5);
            stats.Add(health);
            stats.Add(skill);

            IObject _chicken = new GameObject("Chicken", "It is the remains of a cooked chicken.", "You pick up the chicken.", true, stats, "You eat the chicken; it was lovely.");
            game.Player.Inventory.Add(_chicken.Name, _chicken);

            ICommand command = new Command
            {
                Verb = VerbCodes.Eat,
                Noun = "chicken"
            };

            var reply = room.ProcessCommand(command);

            Assert.AreEqual("You eat the chicken; it was lovely.\r\nPlayer HEALTH increased by 10 points to : 10\r\nPlayer SKILL increased by 5 points to : 5", reply);
        }

        [TestMethod]
        public void ProcessCommandEatsEdibleObjectAddsPointsToHealthStatWithMultipleStatsToUpdate()
        {
            IGame game = new Game();

            var room = new Room("name1", "description1", game);

            game.CurrentRoom = room;
            game.StartRoom = room;

            List<PlayerStatsAdjustments> stats = new List<PlayerStatsAdjustments>();
            PlayerStatsAdjustments health = new PlayerStatsAdjustments("health", 10);
            PlayerStatsAdjustments skill = new PlayerStatsAdjustments("skill", 5);
            stats.Add(health);
            stats.Add(skill);

            IObject _chicken = new GameObject("Chicken", "It is the remains of a cooked chicken.", "You pick up the chicken.", true, stats, "You eat the chicken; it was lovely.");
            game.Player.Inventory.Add(_chicken.Name, _chicken);

            ICommand command = new Command
            {
                Verb = VerbCodes.Eat,
                Noun = "chicken"
            };

            Assert.AreEqual(0, game.Player.PlayerStats.Get("health"));

            var reply = room.ProcessCommand(command);

            Assert.AreEqual("You eat the chicken; it was lovely.\r\nPlayer HEALTH increased by 10 points to : 10\r\nPlayer SKILL increased by 5 points to : 5", reply);
            Assert.AreEqual(10, game.Player.PlayerStats.Get("health"));
            Assert.AreEqual(5, game.Player.PlayerStats.Get("skill"));
        }

        [TestMethod]
        public void ProcessCommandEatsEdibleObjectRemovesFromInventoryWithMultipleStatsToUpdate()
        {
            IGame game = new Game();

            var room = new Room("name1", "description1", game);

            game.CurrentRoom = room;
            game.StartRoom = room;

            List<PlayerStatsAdjustments> stats = new List<PlayerStatsAdjustments>();
            PlayerStatsAdjustments health = new PlayerStatsAdjustments("health", 10);
            PlayerStatsAdjustments skill = new PlayerStatsAdjustments("skill", 5);
            stats.Add(health);
            stats.Add(skill);

            IObject _chicken = new GameObject("Chicken", "It is the remains of a cooked chicken.", "You pick up the chicken.", true, stats, "You eat the chicken; it was lovely.");
            game.Player.Inventory.Add(_chicken.Name, _chicken);

            ICommand command = new Command
            {
                Verb = VerbCodes.Eat,
                Noun = "chicken"
            };

            Assert.IsTrue(game.Player.Inventory.Exists("Chicken"));

            var reply = room.ProcessCommand(command);

            Assert.AreEqual("You eat the chicken; it was lovely.\r\nPlayer HEALTH increased by 10 points to : 10\r\nPlayer SKILL increased by 5 points to : 5", reply);
            Assert.IsFalse(game.Player.Inventory.Exists("Chicken"));
        }

        [TestMethod]
        public void ProcessCommandEatsEdibleFailsIfNotInTheInventoryWithMultipleStatsToUpdate()
        {
            IGame game = new Game();

            var room = new Room("name1", "description1", game);

            game.CurrentRoom = room;
            game.StartRoom = room;

            List<PlayerStatsAdjustments> stats = new List<PlayerStatsAdjustments>();
            PlayerStatsAdjustments health = new PlayerStatsAdjustments("health", 10);
            PlayerStatsAdjustments skill = new PlayerStatsAdjustments("skill", 5);
            stats.Add(health);
            stats.Add(skill);

            IObject _chicken = new GameObject("Chicken", "It is the remains of a cooked chicken.", "You pick up the chicken.", true, stats, "You eat the chicken; it was lovely.");

            ICommand command = new Command
            {
                Verb = VerbCodes.Eat,
                Noun = "chicken"
            };

            Assert.IsFalse(game.Player.Inventory.Exists("Chicken"));

            var reply = room.ProcessCommand(command);

            Assert.AreEqual("I'm not hungry.", reply);

            Assert.IsFalse(game.Player.Inventory.Exists("Chicken"));
        }
    }
}
