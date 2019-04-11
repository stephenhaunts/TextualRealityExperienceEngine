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

using System.Net;
using System.Xml.Schema;
using TextualRealityExperienceEngine.GameEngine;
using TextualRealityExperienceEngine.GameEngine.Interfaces;
using TextualRealityExperienceEngine.GameEngine.Synonyms;

namespace Tests.SimpleGame
{
    public class Outside : Room
    {
        private readonly IObject _key = new GameObject("Key", "Its is a small brass key.", "You pick up the key.");

        private bool _lookedAtPlantPot;
        private bool _doorUnlocked = false;
        private bool _keyPickedUp = false;

        public Outside(IGame game) : base(game)
        {
            _lookedAtPlantPot = false;

            game.ContentManagement.AddContentItem("OutsideName", "Outside");
            game.ContentManagement.AddContentItem("OutsideDescription", "You are standing on a driveway outside of a house. It is nighttime and very cold. " +
                                                                         "There is frost on the ground. There is a door to the north with a plant pot next to the door mat." +
                                                                         "There is also a white metal garage door to the north west.");

            Name = game.ContentManagement.RetrieveContentItem("OutsideName");
            Description = game.ContentManagement.RetrieveContentItem("OutsideDescription");

            game.ContentManagement.AddContentItem("TurnKey", "You turn the key in the lock and you hear a THUNK of the door unlocking.");
            game.ContentManagement.AddContentItem("DoNotHaveKey", "You do not have a key.");
            game.ContentManagement.AddContentItem("ItsAPlantPot", "It's a plant pot. Quite unremarkable.");
            game.ContentManagement.AddContentItem("MovePlantPot", "You move the plant pot and find a key sitting under it.");
            game.ContentManagement.AddContentItem("DoorMat", "It's a doormat where people wipe their feet. On it is written 'There is no place like 10.0.0.1'.");
            game.ContentManagement.AddContentItem("AlreadyHaveKey", "You already have the key.");
            game.ContentManagement.AddContentItem("WhatKey", "What Key?");
            game.ContentManagement.AddContentItem("NotEnoughHintPoints", "You do not have enough points to buy a hint.");
            game.ContentManagement.AddContentItem("InterestingPlantPot", "The plant pot looks interesting.");
            game.ContentManagement.AddContentItem("UsefulKey", "That key looks like it might be useful.");
            game.ContentManagement.AddContentItem("IWonderIfKey", "I wonder if the key you picked up will unlock the front door.");
            game.ContentManagement.AddContentItem("GrabKey", "You pick up the key.");
            game.ContentManagement.AddContentItem("LookDoor", "The door is made of a black plastic that resembles a wooden texture. The lock looks very sturdy and as if it will hold back a good kick or shoulder barge.");
            game.ContentManagement.AddContentItem("DoorLocked", "The door is locked.");
            game.ContentManagement.AddContentItem("AttackDoor", "You shoulder barge the door as hard as you can, but the door does not budge. You feel an agonizing pain run through your shoulder and down your back. You are not as young as you used to be.");
            game.ContentManagement.AddContentItem("SoloAttack", "You have the power to pick up a flower; weakling!!!");

            game.Parser.Nouns.Add("plantpot", "plantpot");
            game.Parser.Nouns.Add("plant", "plantpot");
            game.Parser.Nouns.Add("pot", "plantpot");

            game.Parser.Nouns.Add("key", "key");
            game.Parser.Nouns.Add("keys", "key");

            game.Parser.Nouns.Add("doormat", "doormat");
            game.Parser.Nouns.Add("mat", "doormat");

            game.Parser.Nouns.Add("door", "door");
            game.Parser.Nouns.Add("frontdoor", "door");
        }

        public override string ProcessCommand(ICommand command)
        {            
            switch (command.Verb)
            {
                case VerbCodes.Attack:
                    switch (command.Noun)
                    {
                        case "door":
                            return Game.ContentManagement.RetrieveContentItem("AttackDoor");
                        default:
                            return Game.ContentManagement.RetrieveContentItem("SoloAttack");
                    }

                case VerbCodes.Use:
                    switch (command.Noun)
                    {
                        case "key" when (command.Noun2 == "door") && Game.Player.Inventory.Exists("Key"):
                            SetDoorLock(false, Direction.North);
                            _doorUnlocked = true;
                            return Game.ContentManagement.RetrieveContentItem("TurnKey");
                        case "door" when Game.Player.Inventory.Exists("Key"):
                            _doorUnlocked = true;
                            SetDoorLock(false, Direction.North);
                            return Game.ContentManagement.RetrieveContentItem("TurnKey");
                        case "door" when !Game.Player.Inventory.Exists("Key"):
                            return Game.ContentManagement.RetrieveContentItem("DoorLocked");
                        default:
                            return Game.ContentManagement.RetrieveContentItem("DoNotHaveKey");
                    }

                case VerbCodes.Look:
                    switch (command.Noun)
                    {
                        case "plantpot":
                        {
                            _lookedAtPlantPot = true;
                            if (Game.Player.Inventory.Exists("Key")) return Game.ContentManagement.RetrieveContentItem("ItsAPlantPot");
                            
                            Game.NumberOfMoves++;
                            return Game.ContentManagement.RetrieveContentItem("MovePlantPot");

                        }
                        case "doormat":
                            return Game.ContentManagement.RetrieveContentItem("DoorMat");
                        case "door":
                            return Game.ContentManagement.RetrieveContentItem("LookDoor");
                    }

                    break;
                case VerbCodes.Take:
                    if (command.Noun == "key")
                    {
                        if (_lookedAtPlantPot)
                        {
                            if (!Game.Player.Inventory.Exists("Key") && !_keyPickedUp)
                            {
                                Game.Player.Inventory.Add(_key.Name, _key);
                                Game.IncreaseScore(1);
                                Game.NumberOfMoves++;
                                _keyPickedUp = true;
                                return _key.PickUpMessage;
                            }                            

                            if (Game.Player.Inventory.Exists("Key"))
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

                    if (_lookedAtPlantPot && !Game.Player.Inventory.Exists("Key"))
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
                return Game.ContentManagement.RetrieveContentItem("NoNeedToBeRude");
            }
            
            var reply = base.ProcessCommand(command);
            return reply;
        }
    }
}
