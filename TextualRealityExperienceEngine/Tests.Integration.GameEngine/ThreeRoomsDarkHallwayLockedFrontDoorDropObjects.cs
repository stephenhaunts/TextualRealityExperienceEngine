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
using System.Diagnostics.SymbolStore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextualRealityExperienceEngine.GameEngine;
using TextualRealityExperienceEngine.GameEngine.Interfaces;
using TextualRealityExperienceEngine.GameEngine.Synonyms;

namespace Tests.Integration.GameEngine
{
    [TestClass]
    public class ThreeRoomsDarkHallwayLockedFrontDoorDropObjects
    {
        private IGame _game = new Game();

        private class Outside : Room
        {
            private readonly IObject _key = new GameObject("Key", "Its is a small brass key.", "You pick up the key.");

            private bool _lookedAtPlantPot;
            private bool _doorUnlocked = false;
            private bool _keyPickedUp = false;
            
            public Outside(string name, string description, IGame game) : base(name, description, game)
            {
                _lookedAtPlantPot = false;

                game.ContentManagement.AddContentItem("TurnKey",
                    "You turn the key in the lock and you hear a THUNK of the door unlocking.");
                game.ContentManagement.AddContentItem("DoNotHaveKey", "You do not have a key.");
                game.ContentManagement.AddContentItem("ItsAPlantPot", "It's a plant pot. Quite unremarkable.");
                game.ContentManagement.AddContentItem("MovePlantPot",
                    "You move the plant pot and find a key sitting under it.");
                game.ContentManagement.AddContentItem("DoorMat",
                    "It's a doormat where people wipe their feet. On it is written 'There is no place like 10.0.0.1'.");
                game.ContentManagement.AddContentItem("AlreadyHaveKey", "You already have the key.");
                game.ContentManagement.AddContentItem("WhatKey", "What Key?");
                game.ContentManagement.AddContentItem("NotEnoughHintPoints",
                    "You do not have enough points to buy a hint.");
                game.ContentManagement.AddContentItem("InterestingPlantPot", "The plant pot looks interesting.");
                game.ContentManagement.AddContentItem("UsefulKey", "That key looks like it might be useful.");
                game.ContentManagement.AddContentItem("IWonderIfKey",
                    "I wonder if the key you picked up will unlock the front door.");
                game.ContentManagement.AddContentItem("NoNeedToBeRudeOutside", "There is no need to be rude.");
            }

            public override string ProcessCommand(ICommand command)
            {
                switch (command.Verb)
                {
                    case VerbCodes.Use:
                        switch (command.Noun)
                        {
                            case "key" when (command.Noun2 == "door") && Game.Inventory.Exists("Key"):
                                SetDoorLock(false, Direction.North);
                                _doorUnlocked = true;
                                return Game.ContentManagement.RetrieveContentItem("TurnKey");
                            case "door" when Game.Inventory.Exists("Key"):
                                _doorUnlocked = true;
                                SetDoorLock(false, Direction.North);
                                return Game.ContentManagement.RetrieveContentItem("TurnKey");
                            default:
                                return Game.ContentManagement.RetrieveContentItem("DoNotHaveKey");
                        }

                    case VerbCodes.Look:
                        switch (command.Noun)
                        {
                            case "plantpot":
                            {
                                _lookedAtPlantPot = true;
                                if (Game.Inventory.Exists("Key"))
                                    return Game.ContentManagement.RetrieveContentItem("ItsAPlantPot");

                                Game.NumberOfMoves++;
                                return Game.ContentManagement.RetrieveContentItem("MovePlantPot");

                            }
                            case "doormat":
                                return Game.ContentManagement.RetrieveContentItem("DoorMat");
                        }

                        break;
                    case VerbCodes.Take:
                        if (command.Noun == "key")
                        {
                            if (_lookedAtPlantPot)
                            {
                                if (!Game.Inventory.Exists("Key") && !_keyPickedUp)
                                {
                                    Game.Inventory.Add(_key.Name, _key);
                                    Game.IncreaseScore(1);
                                    Game.NumberOfMoves++;
                                    _keyPickedUp = true;
                                    return _key.PickUpMessage;
                                }                            

                                if (Game.Inventory.Exists("Key"))
                                {
                                    return Game.ContentManagement.RetrieveContentItem("AlreadyHaveKey");
                                }                            
                            }                    
                        }

                        break;
                    case VerbCodes.Hint:
                        if (Game.Score - Game.HintCost < 0)
                        {
                            return Game.ContentManagement.RetrieveContentItem("NotEnoughHintPoints");
                        }

                        if (!_lookedAtPlantPot)
                        {
                            Game.DecreaseScore(Game.HintCost);
                            return Game.ContentManagement.RetrieveContentItem("InterestingPlantPot");
                        }

                        if (_lookedAtPlantPot && !Game.Inventory.Exists("Key"))
                        {
                            Game.DecreaseScore(Game.HintCost);
                            return Game.ContentManagement.RetrieveContentItem("UsefulKey");
                        }

                        if (!_doorUnlocked)
                        {
                            return Game.ContentManagement.RetrieveContentItem("IWonderIfKey");
                        }

                        break;
                }

                if (command.ProfanityDetected)
                {
                    return Game.ContentManagement.RetrieveContentItem("NoNeedToBeRudeOutside");
                }

                var reply = base.ProcessCommand(command);
                return reply;
            }
        }
    

        public class Hallway : Room
        {
            public Hallway(string name, string description, IGame game) : base(name, description, game)
            {
                game.ContentManagement.AddContentItem("NoNeedToBeRude", "There is no need to be rude.");
                game.ContentManagement.AddContentItem("FlipLightSwitch",
                    "You flip the light switch and the lights flicker for a few seconds until they illuminate the hallway. You hear a faint buzzing sound coming from the lights.");
            }

            public override string ProcessCommand(ICommand command)
            {
                string reply;
            
                if (command.ProfanityDetected)
                {
                    return Game.ContentManagement.RetrieveContentItem("NoNeedToBeRude");
                }

                if (command.Verb == VerbCodes.Use)
                {
                    if (command.Noun == "lightswitch")
                    {
                        LightsOn = !LightsOn;

                        Game.NumberOfMoves++;
                        Game.IncreaseScore(1);


                        if (LightsOn)
                        {
                            return Game.ContentManagement.RetrieveContentItem("FlipLightSwitch")
                                   + Description;
                        }
                        else
                        {
                            return Description;
                        }
                    }
                }

                reply = base.ProcessCommand(command);

                return reply;
            }
        }

        private IRoom _outside;
        private IRoom _hallway;
        private IRoom _lounge;

        private void InitializeGame()
        {
            _game = new Game();
            
            AddContentItems();

            _game.Prologue = _game.ContentManagement.RetrieveContentItem("Prologue");
            
            _game.Parser.Nouns.Add("light", "lightswitch");
            _game.Parser.Nouns.Add("lightswitch", "lightswitch");
            _game.Parser.Nouns.Add("switch", "lightswitch");
            _game.Parser.Nouns.Add("plantpot", "plantpot");
            _game.Parser.Nouns.Add("plant", "plantpot");
            _game.Parser.Nouns.Add("pot", "plantpot");

            _game.Parser.Nouns.Add("key", "key");
            _game.Parser.Nouns.Add("keys", "key");

            _game.Parser.Nouns.Add("doormat", "doormat");
            _game.Parser.Nouns.Add("mat", "doormat");

            _game.Parser.Nouns.Add("door", "door");
            _game.Parser.Nouns.Add("frondoor", "door");

            _game.HintSystemEnabled = true;
            
            _outside = new Outside(_game.ContentManagement.RetrieveContentItem("OutsideName"), 
                    _game.ContentManagement.RetrieveContentItem("OutsideDescription"), _game);
            
            _hallway = new Hallway(_game.ContentManagement.RetrieveContentItem("HallwayName"), 
                               _game.ContentManagement.RetrieveContentItem("HallwayDescription"), _game)
            {
                LightsOn = false, LightsOffDescription = _game.ContentManagement.RetrieveContentItem("HallwayLightsOff")
            };


            _lounge = new Room(_game.ContentManagement.RetrieveContentItem("LoungeName"), 
                _game.ContentManagement.RetrieveContentItem("LoungeDescription"), _game);

            var doorway = new DoorWay
            {
                Direction = Direction.North,
                Locked = true,
                ObjectToUnlock = "key"
            };

            _outside.AddExit(doorway, _hallway);
            _hallway.AddExit(Direction.West, _lounge);

            _game.StartRoom = _outside;
            _game.CurrentRoom = _outside;           
        }

        private void AddContentItems()
        {
            _game.ContentManagement.AddContentItem("Prologue","Welcome to test adventure.You will be bedazzled with awesomeness.");
            _game.ContentManagement.AddContentItem("OutsideName", "Outside");
            _game.ContentManagement.AddContentItem("OutsideDescription", "You are standing on a driveway outside of a house. It is nighttime and very cold. " +
                                                                         "There is frost on the ground. There is a door to the north with a plant pot next to the door mat.");
            
            _game.ContentManagement.AddContentItem("HallwayName", "Hallway");
            _game.ContentManagement.AddContentItem("HallwayDescription", "You are standing in a hallway that is modern, yet worn. There is a door to the west." +
                                                                         "To the south the front door leads back to the driveway.");
            
            _game.ContentManagement.AddContentItem("HallwayLightsOff", "You are standing in a very dimly lit hallway. Your eyes struggle to adjust to the low light. " + 
                                                                       "You notice there is a switch on the wall to your left.");
            
            _game.ContentManagement.AddContentItem("LoungeName", "Lounge");
            _game.ContentManagement.AddContentItem("LoungeDescription", "You are stand in the lounge. There is a sofa and a TV inside. There is a door back to the hallway to the east.");
        }

        [TestMethod]
        public void TestInitialState()
        {
            InitializeGame();

            Assert.AreEqual(_game.ContentManagement.RetrieveContentItem("Prologue"), _game.Prologue);
            Assert.AreEqual(_outside, _game.StartRoom);
            Assert.AreEqual(_outside, _game.CurrentRoom);
            Assert.IsNotNull(_game.Parser);

            Assert.AreEqual(_game.ContentManagement.RetrieveContentItem("OutsideName"), _outside.Name);
            Assert.AreEqual(_game.ContentManagement.RetrieveContentItem("OutsideDescription"), _outside.Description);

            Assert.AreEqual(_game.ContentManagement.RetrieveContentItem("HallwayName"), _hallway.Name);

            Assert.IsTrue(_hallway.Description.StartsWith("You are standing in a very dimly lit hallway.", StringComparison.Ordinal));
            Assert.AreEqual(_game.ContentManagement.RetrieveContentItem("LoungeName"), _lounge.Name);
            Assert.AreEqual(_game.ContentManagement.RetrieveContentItem("LoungeDescription"), _lounge.Description);
        }
        
             [TestMethod]
        public void DropKey()
        {
            InitializeGame();
            _game.IncreaseScore(100);

            var reply = _game.ProcessCommand("look at plant pot");
            Assert.IsTrue(reply.Reply.StartsWith("You move the plant pot and find a key sitting under it.", StringComparison.Ordinal));

            
            reply = _game.ProcessCommand("go north");
            Assert.AreEqual("The door is locked.", reply.Reply);

            reply = _game.ProcessCommand("use key on door");
            Assert.AreEqual("You do not have a key.", reply.Reply);

            reply = _game.ProcessCommand("pick up key");
            Assert.AreEqual("You pick up the key.", reply.Reply);
            Assert.AreEqual(1, _game.Inventory.Count());
            Assert.IsTrue(_game.Inventory.Exists("Key"));                        
            
            reply = _game.ProcessCommand("use key on door");
            Assert.AreEqual("You turn the key in the lock and you hear a THUNK of the door unlocking.", reply.Reply);
                   
            reply = _game.ProcessCommand("go north");
            Assert.AreEqual(_hallway, _game.CurrentRoom);

            Assert.IsTrue(reply.Reply.StartsWith("You are standing in a very dimly lit hallway.", StringComparison.Ordinal));
            
            
            reply = _game.ProcessCommand("drop key");
            Assert.IsTrue(reply.Reply.StartsWith("You drop the key", StringComparison.Ordinal));
            
            Assert.AreEqual(0, _game.Inventory.Count());
            Assert.IsFalse(_game.Inventory.Exists("Key"));   
            
            _game.ProcessCommand("go south");
            Assert.AreEqual(_outside, _game.CurrentRoom);

            Assert.AreEqual(0, _game.Inventory.Count());
            Assert.IsFalse(_game.Inventory.Exists("Key"));   
            
            reply = _game.ProcessCommand("pick up key");
            Assert.IsTrue(reply.Reply.StartsWith("You can not pick up a key", StringComparison.Ordinal));
            
            _game.ProcessCommand("go north");
            Assert.AreEqual(_hallway, _game.CurrentRoom);
            
            reply = _game.ProcessCommand("pick up key");
            Assert.IsTrue(reply.Reply.StartsWith("You pick up the key", StringComparison.Ordinal));
            Assert.AreEqual(1, _game.Inventory.Count());
            Assert.IsTrue(_game.Inventory.Exists("Key")); 
            
            // Turn lights on
            reply = _game.ProcessCommand("use switch");
            Assert.IsTrue(reply.Reply.StartsWith("You flip the light switch and the lights flicker for a few seconds", StringComparison.Ordinal));

            reply = _game.ProcessCommand("go west");
            Assert.AreEqual(_game.ContentManagement.RetrieveContentItem("LoungeDescription"), reply.Reply);
            Assert.AreEqual(_lounge, _game.CurrentRoom);

            reply = _game.ProcessCommand("go east");
            Assert.IsTrue(reply.Reply.StartsWith("You are standing in a hallway", StringComparison.Ordinal));

            reply = _game.ProcessCommand("go south");
            Assert.AreEqual(_game.ContentManagement.RetrieveContentItem("OutsideDescription"), reply.Reply);
            Assert.AreEqual(_outside, _game.CurrentRoom);
        }

        [TestMethod]
        public void TryToCollectTheKeyMultipleTimes()
        {
            InitializeGame();

            var reply = _game.ProcessCommand("look at plant pot");
            Assert.IsTrue(reply.Reply.StartsWith("You move the plant pot and find a key sitting under it.", StringComparison.Ordinal));

            reply = _game.ProcessCommand("pick up key");
            Assert.AreEqual("You pick up the key.", reply.Reply);
            Assert.AreEqual(1, _game.Inventory.Count());
            Assert.IsTrue(_game.Inventory.Exists("Key"));

            reply = _game.ProcessCommand("look at plant pot");
            Assert.AreEqual("It's a plant pot. Quite unremarkable.", reply.Reply);

            reply = _game.ProcessCommand("pick up key");
            Assert.AreEqual("You already have the key.", reply.Reply);
        }

        [TestMethod]
        public void WalkAround()
        {
            InitializeGame();
            _game.IncreaseScore(100);

            var reply = _game.ProcessCommand("hint");
            Assert.AreEqual("The plant pot looks interesting.", reply.Reply);

            reply = _game.ProcessCommand("look at plant pot");
            Assert.IsTrue(reply.Reply.StartsWith("You move the plant pot and find a key sitting under it.", StringComparison.Ordinal));

            reply = _game.ProcessCommand("hint");
            Assert.AreEqual("That key looks like it might be useful.", reply.Reply);
            
            reply = _game.ProcessCommand("go north");
            Assert.AreEqual("The door is locked.", reply.Reply);

            reply = _game.ProcessCommand("use key on door");
            Assert.AreEqual("You do not have a key.", reply.Reply);

            reply = _game.ProcessCommand("pick up key");
            Assert.AreEqual("You pick up the key.", reply.Reply);
            Assert.AreEqual(1, _game.Inventory.Count());
            Assert.IsTrue(_game.Inventory.Exists("Key"));                        
            
            reply = _game.ProcessCommand("hint");
            Assert.AreEqual("I wonder if the key you picked up will unlock the front door.", reply.Reply);

            reply = _game.ProcessCommand("look at doormat");
            Assert.IsTrue(reply.Reply.StartsWith("It's a doormat where people wipe their feet.", StringComparison.Ordinal));

            reply = _game.ProcessCommand("use key on door");
            Assert.AreEqual("You turn the key in the lock and you hear a THUNK of the door unlocking.", reply.Reply);
            
            reply = _game.ProcessCommand("hint");
            Assert.AreEqual("There are no more hints available.", reply.Reply);

            reply = _game.ProcessCommand("go north");
            Assert.AreEqual(_hallway, _game.CurrentRoom);

            Assert.IsTrue(reply.Reply.StartsWith("You are standing in a very dimly lit hallway.", StringComparison.Ordinal));

            // Turn lights on
            reply = _game.ProcessCommand("use switch");
            Assert.IsTrue(reply.Reply.StartsWith("You flip the light switch and the lights flicker for a few seconds", StringComparison.Ordinal));

            reply = _game.ProcessCommand("go west");
            Assert.AreEqual(_game.ContentManagement.RetrieveContentItem("LoungeDescription"), reply.Reply);
            Assert.AreEqual(_lounge, _game.CurrentRoom);

            reply = _game.ProcessCommand("go east");
            Assert.IsTrue(reply.Reply.StartsWith("You are standing in a hallway", StringComparison.Ordinal));

            reply = _game.ProcessCommand("go south");
            Assert.AreEqual(_game.ContentManagement.RetrieveContentItem("OutsideDescription"), reply.Reply);
            Assert.AreEqual(_outside, _game.CurrentRoom);
        }
        
        [TestMethod]
        public void SaveLoadGame()
        {
            InitializeGame();

            var reply = _game.ProcessCommand("look at plant pot");
            Assert.IsTrue(reply.Reply.StartsWith("You move the plant pot and find a key sitting under it.", StringComparison.Ordinal));

            reply = _game.ProcessCommand("go north");
            Assert.AreEqual("The door is locked.", reply.Reply);

            reply = _game.ProcessCommand("use key on door");
            Assert.AreEqual("You do not have a key.", reply.Reply);

            reply = _game.ProcessCommand("pick up key");
            Assert.AreEqual("You pick up the key.", reply.Reply);
            Assert.AreEqual(1, _game.Inventory.Count());
            Assert.IsTrue(_game.Inventory.Exists("Key"));

            reply = _game.ProcessCommand("look at doormat");
            Assert.IsTrue(reply.Reply.StartsWith("It's a doormat where people wipe their feet.", StringComparison.Ordinal));

            reply = _game.ProcessCommand("use key on door");
            Assert.AreEqual("You turn the key in the lock and you hear a THUNK of the door unlocking.", reply.Reply);
            

            reply = _game.ProcessCommand("go north");
            Assert.AreEqual(_hallway, _game.CurrentRoom);

            Assert.IsTrue(reply.Reply.StartsWith("You are standing in a very dimly lit hallway.", StringComparison.Ordinal));

            // Turn lights on
            reply = _game.ProcessCommand("use switch");
            Assert.IsTrue(reply.Reply.StartsWith("You flip the light switch and the lights flicker for a few seconds", StringComparison.Ordinal));

            reply = _game.ProcessCommand("go west");
            Assert.AreEqual(_game.ContentManagement.RetrieveContentItem("LoungeDescription"), reply.Reply);
            Assert.AreEqual(_lounge, _game.CurrentRoom);

            reply = _game.ProcessCommand("go east");
            Assert.IsTrue(reply.Reply.StartsWith("You are standing in a hallway", StringComparison.Ordinal));

            reply = _game.ProcessCommand("go south");
            Assert.AreEqual(_game.ContentManagement.RetrieveContentItem("OutsideDescription"), reply.Reply);
            Assert.AreEqual(_outside, _game.CurrentRoom);

            var saveGame = _game.SaveGame();
            InitializeGame();
            _game.LoadGame(saveGame);     
        }
    }
}


