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
using System.Collections.ObjectModel;
using TextualRealityExperienceEngine.GameEngine.Interfaces;

namespace TextualRealityExperienceEngine.GameEngine
{
    /// <summary>
    /// The main game object that hold together the game state, inventory, score and game rooms.
    /// </summary>
    public class Game : IGame
    {
        /// <summary>
        /// The prologue contains the introductory text that is displayed to the game player before they take control of
        /// the player. This text should give some short back story and some basic instruction to get the player started.
        ///
        /// This text shouldn't be too long so that it puts the player off, but a few paragraphs saying why they are there
        /// and how to get help. Detailed back story should be revealed as part of the in game narrative as the player
        /// progresses.
        /// </summary>
        public string Prologue { get; set; }
        
        /// <summary>
        /// The help text field is somewhere you can put some assistance text for the player. You don't want to make it too
        /// long, but this is a good place to document some of the commands the player can use, and how to get hints.
        /// </summary>
        public string HelpText { get; set; }

        /// <summary>
        /// StartRoom is a reference to the starting room for the game. If you want the player to start in the small dungeon, then
        /// assign that room to this field.
        /// </summary>
        public IRoom StartRoom { get; set; }
        
        /// <summary>
        /// As the player navigates around the map, this property contains a reference to the current active room.
        /// </summary>
        public IRoom CurrentRoom { get; set; }
        
        /// <summary>
        /// The parser is the system that takes the players written input and reduces the synonyms down to command
        /// instances that the game can interpret.
        /// </summary>
        public IParser Parser { get; }
        public IGlobalState GlobalState { get; }
        public int NumberOfMoves { get; set; }
        public int Score { get; private set; }
        
        public IContentManagement ContentManagement { get; }
        
        public bool HintSystemEnabled { get; set; }

        public IInventory Inventory { get; set; }
        public DateTime GameClock { get; set; }
        
        public DifficultyEnum Difficulty { get; set; }

        private readonly ICommandQueue _commandQueue = new CommandQueue();

        public Game()
        {
            Prologue = string.Empty;
            HelpText = string.Empty;
            StartRoom = null;
            Parser = new Parser();
            GlobalState = new GlobalState();
            Inventory = new Inventory();
            Difficulty = DifficultyEnum.Easy;
            HintSystemEnabled = false;
            ContentManagement = new ContentManagement();
        }

        public Game(string prologue, IRoom room)
        {
            if (string.IsNullOrEmpty(prologue))
            {
                throw new ArgumentNullException(nameof(prologue), "The prologue can not be empty.");
            }

            Prologue = prologue;
            StartRoom = room ?? throw new ArgumentNullException(nameof(room), "The initial room state can not be null.");
            CurrentRoom = room;
            HelpText = string.Empty;
            Parser = new Parser();
            GlobalState = new GlobalState();
            Inventory = new Inventory();
            Difficulty = DifficultyEnum.Easy;
            HintSystemEnabled = false;
            ContentManagement = new ContentManagement();
        }
        
        public int HintCost
        {
            get
            {
                switch (Difficulty)
                {
                    case DifficultyEnum.Easy:
                        return 1;                        
                    case DifficultyEnum.Medium:
                        return 3;
                    case DifficultyEnum.Hard:
                        return 10;
                    default:
                        throw new ArgumentOutOfRangeException();
                }  
            }
        }

        public GameReply ProcessCommand(string command)
        {
            var reply = new GameReply();

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
            var reply = new GameReply();

            // Run the natural language parser.
            var parsedCommand = Parser.ParseCommand(command);
            _commandQueue.AddCommand(parsedCommand);

            reply.State = ParserStateEnum.Playing;
            reply.Reply = CurrentRoom.ProcessCommand(parsedCommand);

            return reply;
        }

        public void IncreaseScore(int increaseBy)
        {
            int scoreMultiplier;

            switch (Difficulty)
            {
                case DifficultyEnum.Easy:
                    scoreMultiplier = 1;
                    break;
                case DifficultyEnum.Medium:
                    scoreMultiplier = 2;
                    break;
                case DifficultyEnum.Hard:
                    scoreMultiplier = 3;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            Score = Score + increaseBy * scoreMultiplier;
        }

        public void DecreaseScore(int decreaseBy)
        {
            Score = Score - decreaseBy;
        }

        private static GameReply CheckGameSpecificCommandOverrides(string command)
        {
            var reply = new GameReply();

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
                case "help":
                case "help me":
                case "instructions":
                case "read manual":
                case "read the manual":
                case "manual":
                case "man":
                {
                    reply.State = ParserStateEnum.Help;
                    reply.Reply = lowerCase;
                    return reply;
                }
            }

            reply.State = ParserStateEnum.Playing;
            reply.Reply = "";

            return reply;
        }

        public ReadOnlyCollection<ICommand> SaveGame()
        {
            return _commandQueue.Commands; 
        }

        public void LoadGame(ReadOnlyCollection<ICommand> commands)
        {
            _commandQueue.Clear();

            foreach (var command in commands)
            {
                ProcessCommand(command.FullTextCommand);
            }
        }
    }
}
