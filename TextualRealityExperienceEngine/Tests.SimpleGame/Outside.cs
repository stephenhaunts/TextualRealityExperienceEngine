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

        public Outside(string name, string description, IGame game) : base(name, description, game)
        {
            _lookedAtPlantPot = false;
            
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
                            if (Game.Inventory.Exists("Key")) return Game.ContentManagement.RetrieveContentItem("ItsAPlantPot");
                            
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

                        return Game.ContentManagement.RetrieveContentItem("WhatKey");
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
}
