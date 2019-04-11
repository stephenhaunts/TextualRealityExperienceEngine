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
using Tests.SimpleGame.Downstairs;
using Tests.SimpleGame.Crypt;
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
        private static IRoom _garage;
        private static IRoom _kitchen;

        private static void InitializeGame()
        {
            _game = new Game();

            AddContentItems();

            _game.Prologue = _game.ContentManagement.RetrieveContentItem("Prologue");
            _game.HelpText = _game.ContentManagement.RetrieveContentItem("HelpText");
            _game.HintSystemEnabled = true;

            SetupRooms();

            _game.StartRoom = _outside;
            _game.CurrentRoom = _outside;
        }

        private static void SetupRooms()
        {
            _outside = new Outside(_game);
            _garage = new Garage(_game);
            _kitchen = new Kitchen(_game);
            _lounge = new Lounge(_game);
            _hallway = new Hallway(_game)
            {
                LightsOn = false
            };

            var doorway = new DoorWay
            {
                Direction = Direction.North,
                Locked = true,
            };

            var doorwayToGarage = new DoorWay
            {
                Direction = Direction.NorthWest,
                Locked = true,
            };

            _outside.AddExit(doorway, _hallway);
            _outside.AddExit(doorwayToGarage, _garage);
            _hallway.AddExit(Direction.West, _lounge);
            _hallway.AddExit(Direction.North, _kitchen);
        }

        private static void AddContentItems()
        {
            _game.ContentManagement.AddContentItem("NoNeedToBeRude", "There is no need to be rude.");
            _game.ContentManagement.AddContentItem("Prologue", "Welcome to the test adventure from the Textual Reality Experience Engine. \r\n\r\nYou will be bedazzled with amazement at the sheer awesomeness of our graphics engine.");
                          
            _game.ContentManagement.AddContentItem("HelpText", "Your aim is to find the treasure that is hidden somewhere in the house. \r\nYou need to type commands into the game to control the player.");

            _game.ContentManagement.AddContentItem("AreYouSure", "Are you sure? (y/n) : ");
            _game.ContentManagement.AddContentItem("ExitMessage", "Have it your way.. You spontaneously combust and depart this mortal coil in a puff of smoke....");           
        }

        private static void Main(string[] args)
        {
            Console.Clear();
            InitializeGame();

            ConsoleEx.WordWrap(_game.Prologue);
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
