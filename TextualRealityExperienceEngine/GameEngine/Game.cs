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
using System.Threading;
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
        ///
        /// The parser process tried to split the input down into the following forms.
        /// verb - noun
        /// verb - noun - preposition - noun
        /// </summary>
        public IParser Parser { get; }
        
        /// <summary>
        /// The GlobalState property represents a key / value store that you can use to store data or game state that
        /// needs to be access between rooms. This is a tidier approach than just using global or static variables.
        ///
        /// A good example for what you can use this for is storing classic RPG style player stats that can be access
        /// and modified as the game progresses.
        /// </summary>
        public IGlobalState GlobalState { get; }
        
        /// <summary>
        /// The NumberOfMoves property counts the number of moves that the player does that effects the game. It is up
        /// to the implementor how you increase this value, but it should be used as another part of the score for the player.
        ///
        /// For example, the player may want a high score, but the might want to complete the game in the least number of moves as possible.
        /// </summary>
        public int NumberOfMoves { get; set; }
        
        /// <summary>
        /// The score property represents the players overall game score. Increasing this property is the responsibility
        /// of the game implementor. Based on the difficulty level that has been set, there is a multiplier added to the
        /// score when it is incremented. The harder the difficulty, the higher the score will be increased.
        ///
        /// The multiplier is set as;
        ///
        /// Easy x 1
        /// Medium x 2
        /// Hard x 3
        /// </summary>
        public int Score { get; private set; }
        
        /// <summary>
        /// The main game object has an instance of the content management service available to allow you to decouple your
        /// game logic from the text content you display. This makes it easier to manage game text in one place.
        ///
        /// The content management systems works as a key value pair store for you to store text.
        /// </summary>
        public IContentManagement ContentManagement { get; }
        
        /// <summary>
        /// If you want your player to be able to request hints for the game, then set this flag to True.
        /// </summary>
        public bool HintSystemEnabled { get; set; }

        /// <summary>
        /// Ths Inventory object is used to store the objects that the player has collected as they navigate around
        /// different rooms. When implementing game logic that require the player to possess an object, you need to
        /// check that the object exists in this Inventory instance.
        /// </summary>
        public IInventory Inventory { get; set; }
        
        /// <summary>
        /// It is possible to set a game clock so you can use the date and time as part of a game-play mechanic. This
        /// means if you collected an object for the inventory on Tuesday (in game time) and used a time machine to go back
        /// to last Saturday, then that object would not be in your inventory if you want it to observe game time.
        /// </summary>
        public DateTime GameClock { get; set; }
        
        /// <summary>
        /// The Difficulty flag let you set whether the game is Easy, Medium or Hard. You can query this flag in game
        /// to change game-play based on difficulty. This difficulty flag is also used to change the score multipliers
        /// like the following:
        ///
        /// Easy x 1
        /// Medium x 2
        /// Hard x 3
        /// 
        /// </summary>
        public DifficultyEnum Difficulty { get; set; }
        
        private readonly ICommandQueue _commandQueue = new CommandQueue();

        /// <summary>
        /// Constructor : Set up the correct default initial game state.
        /// </summary>
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

        /// <summary>
        /// Constructor : Set up the correct default initial game state.
        /// </summary>
        /// <param name="prologue">You can inject the game prologue text into this constructor.</param>
        /// <param name="room">Set the initial starting room.</param>
        /// <exception cref="ArgumentNullException">If the prologue or initial room is null then throw the 
        /// ArgumentNullException.</exception>
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
        
        /// <summary>
        /// You have several options when it comes to offering hints to the player. You could maintain your own counter
        /// and allow the player 5 hints, for example, or you could deduct from their score and use the score as a hint
        /// currency. If you choose to do that, then this method will return the cost to deduct off of the score based
        /// on the selected difficulty level.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">ArgumentOutOfRangeException which should never get thrown.</exception>
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

        /// <summary>
        /// The ProcessCommand method is the primary way in which the player interacts with the game. When they type a command
        /// into the game, it is fed into this method which executes the parser to interpret what has been types which
        /// then results in a GameReply object that contains the broken down command ready for the game logic to use.
        /// </summary>
        /// <param name="command">The typed in command from the player.</param>
        /// <returns>The GameReply object that contains the interpreted command ready for the game to use.</returns>
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

        /// <summary>
        /// Increase the players score. The score entered into this method is then multiplied by the modifier which is
        /// dependent on the game difficulty. For example, if the score passed in is 1, and the difficulty is set to Hard,
        /// then the score is multiplied by 3.
        /// </summary>
        /// <param name="increaseBy">The non-multiplied score to increment by.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
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

        /// <summary>
        /// Decrease the players score by a set amount.
        /// </summary>
        /// <param name="decreaseBy">The amount to decrease the score by.</param>
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

        /// <summary>
        /// This method assists in saving the game state by returning a read only collection of game commands.
        ///
        /// It is then the implementors responsibility to save the data to a file or database.
        /// </summary>
        /// <returns>ReadOnlyCollection of game commands.</returns>
        public ReadOnlyCollection<ICommand> SaveGame()
        {
            return _commandQueue.Commands; 
        }

        /// <summary>
        /// Load in a list of game commands to restore the game to a known state.
        /// </summary>
        /// <param name="commands">ReadOnlyCollection of game commands.</param>
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
