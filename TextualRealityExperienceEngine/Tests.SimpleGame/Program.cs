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
    class Program
    {
        private readonly static IGame _game = new Game();

        private static void InitializeGame()
        {
            _game.Prologue = "Welcome to test adventure. You will be bedazzled with awesomeness.";

            var outside = new Room("Outside", "You are standing on a driveway outside of a house. It is nightime and very cold. " +
                                              "There is frost on the ground. There is a door to the north.", _game);
                

            var hallway = new Room("Hallway", "You are standing in a hallway that is modern, yet worn. There is a door to the west." +
            "To the south the front door leads back to the driveway.", _game);

            var lounge = new Room("Lounge", "You are stand in the lounge. There is a sofa and a TV inside. There is a door back to the hallway to the east.", _game);

            outside.AddExit(Direction.North, hallway);
            hallway.AddExit(Direction.West, lounge);

            _game.StartRoom = outside;
            _game.CurrentRoom = outside;
        }

        static void Main(string[] args)
        {
            InitializeGame();

            Console.WriteLine(_game.Prologue);
            Console.WriteLine();
            Console.WriteLine(_game.StartRoom.Description);
            Console.WriteLine();

            while (true)
            {
                Console.Write("> ");
                string reply = _game.ProcessCommand(Console.ReadLine());

                if (!string.IsNullOrEmpty(reply))
                {
                    Console.WriteLine();
                    Console.WriteLine(reply);
                }
            }
        }
    }
}
