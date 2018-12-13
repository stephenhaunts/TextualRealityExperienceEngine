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
using TextualRealityExperienceEngine.GameEngine.Interfaces;

namespace TextualRealityExperienceEngine.GameEngine
{

    public class Game : IGame
    {
        public string Prologue { get; set; }
        public IRoom StartRoom { get; set; }
        public IRoom CurrentRoom { get; set; }
        public IParser Parser { get; }
        public int NumberOfMoves { get; set; }
        public int Score { get; set; }
        public IInventory Inventory { get; set; }

        ICommandQueue _commandqueue = new CommandQueue();

        public Game()
        {
            Prologue = string.Empty;
            StartRoom = null;
            Parser = new Parser();
            Inventory = new Inventory();
        }

        public Game(string prologue, IRoom room)
        {
            if (string.IsNullOrEmpty(prologue))
            {
                throw new ArgumentNullException(nameof(prologue), "The prologue can not be empty.");
            }

            if (room == null)
            {
                throw new ArgumentNullException(nameof(room), "The initial room state can not be null.");
            }

            Prologue = prologue;
            StartRoom = room;
            CurrentRoom = room;
            Parser = new Parser();
            Inventory = new Inventory();
        }

        public GameReply ProcessCommand(string command)
        {
            GameReply reply = new GameReply();

            if (!string.IsNullOrEmpty(command))
            {
                // Check for game specific override commands
                reply = CheckGameSpecificCommandOverrides(command);

                if (string.IsNullOrEmpty(reply.Reply))
                {
                    reply = RunParser(command);
                }

                return reply;
            }

            reply.State = ParserStateEnum.Playing;
            reply.Reply = string.Empty;
            return reply;
        }

        private GameReply RunParser(string command)
        {
            GameReply reply = new GameReply();

            // Run the natural language parser.
            var parsedCommand = Parser.ParseCommand(command);
            _commandqueue.AddCommand(parsedCommand);

            reply.State = ParserStateEnum.Playing;
            reply.Reply = CurrentRoom.ProcessCommand(parsedCommand);

            return reply;
        }

        private GameReply CheckGameSpecificCommandOverrides(string command)
        {
            GameReply reply = new GameReply();

            var lowerCase = command.ToLower();

            switch (lowerCase)
            {
                case "clear":
                case "cls":
                case "clearscreen":
                case "clear screen":
                {
                    reply.State = ParserStateEnum.Clearscreen;
                    reply.Reply = lowerCase;
                    return reply;
                }
                case "quit":
                case "exit":
                case "run away":
                case "kill yourself":
                case "kill your self":
                {
                    reply.State = ParserStateEnum.Exit;
                    reply.Reply = lowerCase;
                    return reply;
                }
                case "show score":
                case "score":
                case "view score":
                case "see score":
                case "what is my score":
                {
                    reply.State = ParserStateEnum.Score;
                    reply.Reply = lowerCase;
                    return reply;
                }
                case "inventory":
                case "view inventory":
                {
                    reply.State = ParserStateEnum.Inventory;
                    reply.Reply = lowerCase;
                    return reply;
                }
            }

            reply.State = ParserStateEnum.Playing;
            reply.Reply = "";

            return reply;
        }
    }
}
