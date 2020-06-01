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
using TextualRealityExperienceEngine.GameEngine.Utilities;
using TextualRealityExperienceEngine.Tests.SimpleGame.Library;

namespace TextualRealityExperienceEngine.Tests.SimpleGame
{
    public class Program
    {    
        public static void Main()
        {
            SimpleGameController simpleGame = new SimpleGameController();
            Console.Clear();
            simpleGame.InitializeGame();

            ConsoleEx.WordWrap(simpleGame.Prologue);
            Console.WriteLine();

            ConsoleEx.WordWrap(simpleGame.CurrentRoomDescription);
            Console.WriteLine();

            while (true)
            {
                Console.Write("> ");
                var reply = simpleGame.ProcessCommand(Console.ReadLine());

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
                        Console.Write(simpleGame.RetrieveContentItem("AreYouSure"));
                        var response = Console.ReadLine();

                        if (response != null && response.ToLower() == "y")
                        {
                            Console.WriteLine();
                            ConsoleEx.WordWrap(simpleGame.RetrieveContentItem("ExitMessage"));

                            Environment.Exit(0);
                        }
                        continue;

                    case GameStateEnum.Score:
                        Console.WriteLine();
                        Console.WriteLine("You have made " + simpleGame.NumberOfMoves + " so far.");
                        Console.WriteLine("You score is " + simpleGame.Score);
                        Console.WriteLine();
                        continue;

                    case GameStateEnum.Inventory:
                        if (simpleGame.GetInventory().Count == 0)
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

                            foreach (var i in simpleGame.GetInventory())
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

                        foreach (var location in simpleGame.GetVisitedRooms())
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
                        ConsoleEx.WordWrap(simpleGame.Help);
                        continue;
                }
            }
        }
    }
}
