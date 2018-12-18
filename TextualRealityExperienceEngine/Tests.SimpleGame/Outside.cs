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
using TextualRealityExperienceEngine.GameEngine;
using TextualRealityExperienceEngine.GameEngine.Interfaces;
using TextualRealityExperienceEngine.GameEngine.Synonyms;

namespace Tests.SimpleGame
{
    public class Outside : Room
    {
        private readonly IObject _key = new GameObject("Key", "Its is a small brass key.", "You pick up the key.");

        private bool _lookedAtPlantPot;

        public Outside(string name, string description, IGame game) : base(name, description, game)
        {
            _lookedAtPlantPot = false;
        }

        public override string ProcessCommand(ICommand command)
        {
            switch (command.Verb)
            {
                case VerbCodes.Use:
                    if ((command.Noun == "key") && (command.Noun2 == "door"))
                    {
                        if (Game.Inventory.Exists("Key"))
                        {
                            SetDoorLock(false, Direction.North);
                            return "You turn the key in the lock and you hear a THUNK of the door unlocking.";
                        }
                    }

                    if ((command.Noun == "door"))
                    {
                        if (Game.Inventory.Exists("Key"))
                        {
                            SetDoorLock(false, Direction.North);
                            return "You turn the key in the lock and you hear a THUNK of the door unlocking.";
                        }
                    }

                    return "You do not have a key.";

                case VerbCodes.Look:
                    switch (command.Noun)
                    {
                        case "plantpot":
                        {
                            _lookedAtPlantPot = true;
                            if (Game.Inventory.Exists("Key")) return "It's a plant pot. Quite unremarkable.";
                            
                            Game.NumberOfMoves++;
                            return "You move the plant pot and find a key sitting under it.";

                        }
                        case "doormat":
                            return "It's a doormat where people wipe their feet. On it is written 'There is no place like 10.0.0.1'.";
                    }

                    break;
                case VerbCodes.Take:
                    if (command.Noun == "key")
                    {
                        if (_lookedAtPlantPot)
                        {
                            if (!Game.Inventory.Exists("Key"))
                            {
                                Game.Inventory.Add(_key.Name, _key);
                                Game.Score++;
                                Game.NumberOfMoves++;
                                return _key.PickUpMessage;
                            }

                            return "You already have the key.";
                        }

                        return "What key?";
                    }
                    break;

            }

            var reply = base.ProcessCommand(command);
            return reply;
        }
    }
}
