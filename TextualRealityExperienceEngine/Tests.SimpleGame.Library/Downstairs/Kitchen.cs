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
using TextualRealityExperienceEngine.GameEngine.TextProcessing.Interfaces;
using TextualRealityExperienceEngine.GameEngine.TextProcessing.Synonyms;

namespace Tests.SimpleGame.Downstairs
{
    public class Kitchen : Room
    {
        private bool _openedFridge;
        private readonly IObject _chicken = new GameObject("Chicken", "It is the remains of a cooked chicken.", "You pick up the chicken.", true, 10, "health", "You eat the chicken; it was lovely.");
        private readonly IObject _cheese = new GameObject("Cheese", "It is a small block of english cheddar.", "You pick up the cheese.", true, 5, "health", "You eat the cheese.");

        public Kitchen(IGame game) : base(game)
        {
            game.ContentManagement.AddContentItem("KitchenName", "Kitchen");
            game.ContentManagement.AddContentItem("KitchenDescription", "You are standing in the kitchen. There is a small table to your left, a fridge in front of you and a cooker to the right. There is a door to the east.");

            Name = game.ContentManagement.RetrieveContentItem("KitchenName");
            Description = game.ContentManagement.RetrieveContentItem("KitchenDescription");

            game.ContentManagement.AddContentItem("ItsAFridge", "It's a fridge..");

            game.ContentManagement.AddContentItem("LookedAtFridge", "You open the fridge door.");

            game.ContentManagement.AddContentItem("LookAtChicken", "It is the remains of a cooked chicken.");
            game.ContentManagement.AddContentItem("AlreadyHaveChicken", "You already have the chicken.");

            game.ContentManagement.AddContentItem("LookAtCheese", "It is a small block of English cheddar cheese.");
            game.ContentManagement.AddContentItem("AlreadyHaveCheese", "You already have the cheese.");

            game.ContentManagement.AddContentItem("WhatFood", "What food?.");

            game.Parser.Nouns.Add("chicken", "chicken");
            game.Parser.Nouns.Add("chook", "chicken");
            game.Parser.Nouns.Add("poultry", "chicken");
            game.Parser.Nouns.Add("turkey", "chicken");

            game.Parser.Nouns.Add("cheese", "cheese");
            game.Parser.Nouns.Add("cheddar", "cheese");
            game.Parser.Nouns.Add("chedderr", "cheese");

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
                                if (!_openedFridge)
                                {
                                    return Game.ContentManagement.RetrieveContentItem("ItsAFridge");
                                }
                                else
                                {
                                    string message = "The fridge is open. ";

                                    return FridgeContents(message);
                                }
                            }
                        case "chicken":
                            {
                                if (_openedFridge)
                                {
                                    Game.IncreaseScore(1);
                                    Game.NumberOfMoves++;
                                    return Game.ContentManagement.RetrieveContentItem("LookAtChicken");
                                }
                                else
                                {
                                    if (!Game.Player.Inventory.Exists("chicken"))
                                    {
                                        Game.NumberOfMoves++;
                                        return Game.ContentManagement.RetrieveContentItem("WhatFood");
                                    }
                                    else
                                    {
                                        return Game.ContentManagement.RetrieveContentItem("LookAtChicken");
                                    }
                                }
                            }
                        case "cheese":
                            {
                                if (_openedFridge)
                                {
                                    Game.IncreaseScore(1);
                                    Game.NumberOfMoves++;
                                    return Game.ContentManagement.RetrieveContentItem("LookAtCheese");
                                }
                                else
                                {
                                    if (!Game.Player.Inventory.Exists("cheese"))
                                    {
                                        Game.NumberOfMoves++;
                                        return Game.ContentManagement.RetrieveContentItem("WhatFood");
                                    }
                                    else
                                    {
                                        return Game.ContentManagement.RetrieveContentItem("LookAtCheese");
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

                                string message = "You open the fridge door. ";

                                message = FridgeContents(message);

                                return message;
                            }
                    }
                    break;

                case VerbCodes.Take:
                    switch (command.Noun)
                    {
                        case "chicken":
                            if (_openedFridge)
                            {
                                if (!Game.Player.Inventory.Exists("chicken"))
                                {
                                    Game.Player.Inventory.Add(_chicken.Name, _chicken);
                                    Game.IncreaseScore(1);
                                    Game.NumberOfMoves++;
                                    return _chicken.PickUpMessage;
                                }

                                if (Game.Player.Inventory.Exists("chicken "))
                                {
                                    return Game.ContentManagement.RetrieveContentItem("AlreadyHaveChicken");
                                }
                            }
                            else
                            {
                                Game.NumberOfMoves++;
                                return Game.ContentManagement.RetrieveContentItem("WhatFood");
                            }
                            break;

                        case "cheese":
                            if (_openedFridge)
                            {
                                if (!Game.Player.Inventory.Exists("cheese"))
                                {
                                    Game.Player.Inventory.Add(_cheese.Name, _cheese);
                                    Game.IncreaseScore(1);
                                    Game.NumberOfMoves++;
                                    return _cheese.PickUpMessage;
                                }

                                if (Game.Player.Inventory.Exists("cheese"))
                                {
                                    return Game.ContentManagement.RetrieveContentItem("AlreadyHaveCheese");
                                }
                            }
                            else
                            {
                                Game.NumberOfMoves++;
                                return Game.ContentManagement.RetrieveContentItem("WhatFood");
                            }
                            break;
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

        private string FridgeContents(string message)
        {
            bool chicken = Game.Player.Inventory.Exists("chicken");
            bool cheese = Game.Player.Inventory.Exists("cheese");

            if ((!chicken) || (!cheese))
            {
                message += "The fridge contains the following items, ";

                if (!chicken)
                {
                    message += "Chicken ";
                }

                if (!cheese)
                {
                    message += "Cheese ";
                }
            }
            else
            {
                message += "The fridge is empty.";
            }

            return message;
        }
    }
}