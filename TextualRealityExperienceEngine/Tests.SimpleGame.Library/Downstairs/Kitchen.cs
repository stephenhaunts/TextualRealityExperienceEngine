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
using TextualRealityExperienceEngine.GameEngine;
using TextualRealityExperienceEngine.GameEngine.Interfaces;
using TextualRealityExperienceEngine.GameEngine.Synonyms;

namespace Tests.SimpleGame.Downstairs
{
    public class Kitchen : Room
    {
        private bool _openedFridge = false;
        private readonly IObject _food = new GameObject("Food", "It is the remains of a cooked chicken.", "You pick up the chicken.");

        public Kitchen(IGame game) : base(game)
        {
            game.ContentManagement.AddContentItem("KitchenName", "Kitchen");
            game.ContentManagement.AddContentItem("KitchenDescription", "You are standing in the kitchen. There is a small table to your left, a fridge in front of you and a cooker to the right. There is a door to the east.");

            Name = game.ContentManagement.RetrieveContentItem("KitchenName");
            Description = game.ContentManagement.RetrieveContentItem("KitchenDescription");

            game.ContentManagement.AddContentItem("LookedAtFridge", "You open the fridge and there about half a cooked chicken in the fridge covered in plastic film.");
            game.ContentManagement.AddContentItem("LookedAtFridgeNoFood", "You open the fridge and it is empty.");
            game.ContentManagement.AddContentItem("LookAtFood", "It is the remains of a cooked chicken.");
            game.ContentManagement.AddContentItem("WhatFood", "What food?.");
            game.ContentManagement.AddContentItem("AlreadyHaveFood", "You already have the chicken.");

            game.Parser.Nouns.Add("chicken", "food");
            game.Parser.Nouns.Add("food", "food");

            game.Parser.Nouns.Add("fridge", "fridge");
            game.Parser.Nouns.Add("icebox", "fridge");
            game.Parser.Nouns.Add("freezer", "fridge");
        }

        public override string ProcessCommand(ICommand command)
        {
            switch (command.Verb)
            {
                case VerbCodes.Look:
                    switch (command.Noun)
                    {
                        case "fridge":
                            {
                                Game.IncreaseScore(1);
                                Game.NumberOfMoves++;
                                _openedFridge = true;

                                if (!Game.Player.Inventory.Exists("food"))
                                {
                                    return Game.ContentManagement.RetrieveContentItem("LookedAtFridge");
                                }
                                else
                                {
                                    return Game.ContentManagement.RetrieveContentItem("LookedAtFridgeNoFood");
                                }
                            }
                        case "food":
                            {
                                if (_openedFridge)
                                {
                                    Game.IncreaseScore(1);
                                    Game.NumberOfMoves++;
                                    return Game.ContentManagement.RetrieveContentItem("LookAtFood");
                                }
                                else
                                {
                                    if (!Game.Player.Inventory.Exists("food"))
                                    {
                                        Game.NumberOfMoves++;
                                        return Game.ContentManagement.RetrieveContentItem("WhatFood");
                                    }
                                    else
                                    {
                                        return Game.ContentManagement.RetrieveContentItem("LookAtFood");
                                    }
                                }
                            }
                    }
                    break;

                case VerbCodes.Use:
                    switch (command.Noun)
                    {
                        case "fridge":
                            {
                                Game.IncreaseScore(1);
                                Game.NumberOfMoves++;
                                _openedFridge = true;

                                if (!Game.Player.Inventory.Exists("food"))
                                {
                                    return Game.ContentManagement.RetrieveContentItem("LookedAtFridge");
                                }
                                else
                                {
                                    return Game.ContentManagement.RetrieveContentItem("LookedAtFridgeNoFood");
                                }
                            }
                    }
                    break;

                case VerbCodes.Take:
                    if (command.Noun == "food")
                    {
                        if (_openedFridge)
                        {
                            if (!Game.Player.Inventory.Exists("food"))
                            {
                                Game.Player.Inventory.Add(_food.Name, _food);
                                Game.IncreaseScore(1);
                                Game.NumberOfMoves++;
                                return _food.PickUpMessage;
                            }

                            if (Game.Player.Inventory.Exists("food"))
                            {
                                return Game.ContentManagement.RetrieveContentItem("AlreadyHaveFood");
                            }
                        }
                        else
                        {
                            Game.NumberOfMoves++;
                            return Game.ContentManagement.RetrieveContentItem("WhatFood");
                        }
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