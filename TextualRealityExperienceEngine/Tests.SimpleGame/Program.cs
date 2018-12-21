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
using TextualRealityExperienceEngine.GameEngine;
using TextualRealityExperienceEngine.GameEngine.Interfaces;

namespace Tests.SimpleGame
{
    internal partial class Program
    {
        private static IGame _game = new Game();
        private const string Prologue = "Welcome to test adventure.You will be bedazzled with awesomeness.";
        private const string HelpText = "Your aim is to find the treasure that is hidden somewhere in the house. \r\nYou need to type commands into the game to control the player.";


        private const string OutsideName = "Outside";
        private const string OutsideDescription = "You are standing on a driveway outside of a house. It is nightime and very cold. " +
                                                    "There is frost on the ground. There is a door to the north with a plant pot next to the door mat.";

        private const string HallwayName = "Hallway";
        private const string HallwayDescription = "You are standing in a hallway that is modern, yet worn. There is a door to the west." +
                                                    "To the south the front door leads back to the driveway.";

        private const string HallwayLightsOff = "You are standing in a very dimly lit hallway. Your eyes struggle to adjust to the low light. " +
                                                   "You notice there is a swith on the wall to your left.";

        private const string LoungeName = "Lounge";
        private const string LoungeDescription = "You are stand in the lounge. There is a sofa and a TV inside. There is a door back to the hallway to the east.";


        private static IRoom _outside;
        private static IRoom _hallway;
        private static IRoom _lounge;

        private static void InitializeGame()
        {
            _game = new Game
            {
                Prologue = Prologue,
                HelpText = HelpText
            };

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
            _game.Parser.Nouns.Add("frondoor", "door");

            _outside = new Outside(OutsideName, OutsideDescription, _game);

            _hallway = new Hallway(HallwayName, HallwayDescription, _game)
            {
                LightsOn = false,
                LightsOffDescription = HallwayLightsOff
            };

            _lounge = new Lounge(LoungeName, LoungeDescription, _game);

            DoorWay doorway = new DoorWay
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

        static void Main(string[] args)
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
                    case ParserStateEnum.Playing:
                        if (!string.IsNullOrEmpty(reply.Reply))
                        {
                            Console.WriteLine();
                            ConsoleEx.WordWrap(reply.Reply);
                        }
                        continue;

                    case ParserStateEnum.Clearscreen:
                        Console.Clear();
                        continue;

                    case ParserStateEnum.Exit:
                        Console.WriteLine();
                        Console.Write("Are you sure? (y/n) : ");
                        var response = Console.ReadLine();

                        if (response != null && response.ToLower() == "y")
                        {
                            Console.WriteLine();
                            ConsoleEx.WordWrap("Have it your way.. You spontaniously combust and depart this mortal coil in a puff of smoke....");

                            Environment.Exit(0);
                        }
                        continue;

                    case ParserStateEnum.Score:
                        Console.WriteLine();
                        Console.WriteLine("You have made " + _game.NumberOfMoves + " so far.");
                        Console.WriteLine("You score is " + _game.Score);
                        Console.WriteLine();
                        continue;

                    case ParserStateEnum.Inventory:
                        if (_game.Inventory.Count() == 0)
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

                            foreach (var i in _game.Inventory.GetInventory())
                            {
                                Console.WriteLine("   " + i);
                            }

                            Console.WriteLine();
                        }
                        continue;
                    case ParserStateEnum.Help:
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
