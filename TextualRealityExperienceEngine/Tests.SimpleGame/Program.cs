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
using TextualRealityExperienceEngine.GameEngine;
using TextualRealityExperienceEngine.GameEngine.Interfaces;

namespace Tests.SimpleGame
{
    internal class Program
    {
        private static IGame _game = new Game(); 
        private static IRoom _outside;
        private static IRoom _hallway;
        private static IRoom _lounge;

        private static void InitializeGame()
        {
            _game = new Game();

            AddContentItems();

            _game.Prologue = _game.ContentManagement.RetrieveContentItem("Prologue");
            _game.HelpText = _game.ContentManagement.RetrieveContentItem("HelpText");
            _game.Parser.Nouns.Add("light", "lightswitch");
            _game.Parser.Nouns.Add("lights", "lightswitch");
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
            _game.Parser.Nouns.Add("frontdoor", "door");

            _outside = new Outside(_game.ContentManagement.RetrieveContentItem("OutsideName"), 
                               _game.ContentManagement.RetrieveContentItem("OutsideDescription"), _game);

            _hallway = new Hallway(_game.ContentManagement.RetrieveContentItem("HallwayName"), 
                               _game.ContentManagement.RetrieveContentItem("HallwayDescription"), _game)
            {
                LightsOn = false,
                LightsOffDescription = _game.ContentManagement.RetrieveContentItem("HallwayLightsOff")
            };

            _lounge = new Lounge(_game.ContentManagement.RetrieveContentItem("LoungeName"), 
                             _game.ContentManagement.RetrieveContentItem("LoungeDescription"), _game);
            
            _game.HintSystemEnabled = true;

            var doorway = new DoorWay
            {
                Direction = Direction.North,
                Locked = true,                
            };

            _outside.AddExit(doorway, _hallway);
            _hallway.AddExit(Direction.West, _lounge);

            _game.StartRoom = _outside;
            _game.CurrentRoom = _outside;
        }

        private static void AddContentItems()
        {
            _game.ContentManagement.AddContentItem("Prologue","Welcome to the test adventure from the Textual Reality Experience Engine. You will be bedazzled with amazement at the sheer awesomeness of our graphics engine.");
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
            _game.ContentManagement.AddContentItem("HelpText", "Your aim is to find the treasure that is hidden somewhere in the house. \r\nYou need to type commands into the game to control the player.");

            _game.ContentManagement.AddContentItem("AreYouSure", "Are you sure? (y/n) : ");
            _game.ContentManagement.AddContentItem("ExitMessage", "Have it your way.. You spontaneously combust and depart this mortal coil in a puff of smoke....");           
        }

        private static void Main(string[] args)
        {
            Console.Clear();
            InitializeGame();

            Console.WriteLine(_game.Prologue);
            Console.WriteLine();

            ConsoleEx.WordWrap(_game.StartRoom.Description);
            Console.WriteLine();

            while (true)
            {
                Console.Write("> ");
                var reply = _game.ProcessCommand(Console.ReadLine());

                switch (reply.State)
                {
                    case GameStateEnum.Playing:
                        if (!string.IsNullOrEmpty(reply.Reply))
                        {
                            Console.WriteLine();                            
                            ConsoleEx.WordWrap(reply.Reply);
                        }
                        continue;

                    case GameStateEnum.Clearscreen:
                        Console.Clear();
                        continue;

                    case GameStateEnum.Exit:
                        Console.WriteLine();
                        Console.Write(_game.ContentManagement.RetrieveContentItem("AreYouSure"));
                        var response = Console.ReadLine();

                        if (response != null && response.ToLower() == "y")
                        {
                            Console.WriteLine();
                            ConsoleEx.WordWrap(_game.ContentManagement.RetrieveContentItem("ExitMessage"));

                            Environment.Exit(0);
                        }
                        continue;

                    case GameStateEnum.Score:
                        Console.WriteLine();
                        Console.WriteLine("You have made " + _game.NumberOfMoves + " so far.");
                        Console.WriteLine("You score is " + _game.Score);
                        Console.WriteLine();
                        continue;

                    case GameStateEnum.Inventory:
                        if (_game.Player.Inventory.Count() == 0)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Your inventory is empty.");
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("Inventory");
                            Console.WriteLine("---------");

                            foreach (var i in _game.Player.Inventory.GetInventory())
                            {
                                Console.WriteLine("   " + i);
                            }

                            Console.WriteLine();
                        }
                        continue;
                    case GameStateEnum.Visited:
                        Console.WriteLine();
                        Console.WriteLine("Visited Locations");
                        Console.WriteLine("-----------------");
                        Console.WriteLine();
                        Console.WriteLine("You have visited the following locations.");
                        Console.WriteLine();

                        foreach (var location in _game.VisitedRooms.GetVisitedRooms())
                        {
                            Console.WriteLine("    " + location.name);
                        }

                        Console.WriteLine();
                        Console.WriteLine("To visit an already visited location type goto or visit and then the location name.");
                        continue;
                    case GameStateEnum.Help:
                        Console.WriteLine();
                        Console.WriteLine("Game Manual");
                        Console.WriteLine("-----------");
                        Console.WriteLine();
                        ConsoleEx.WordWrap(_game.HelpText);
                        continue;
                }
            }
        }
    }
}
