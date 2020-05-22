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
using System.Text;
using TextualRealityExperienceEngine.GameEngine.TextProcessing.Interfaces;
using TextualRealityExperienceEngine.GameEngine.TextProcessing.Synonyms;

namespace TextualRealityExperienceEngine.GameEngine.TextProcessing
{
    /// <summary>
    /// The parser is the system that takes the players written input and reduces the synonyms down to command
    /// instances that the game can interpret.
    ///
    /// The parser process tries to split the input down into the following forms.
    /// verb - noun
    /// verb - noun - preposition - noun
    /// </summary>
    public class Parser : ParserBase, IParser
    {
        /// <summary>
        /// Default constructor that sets the default initial state of the parser.
        /// </summary>
        public Parser() 
        {
        }

        /// <summary>
        /// Constructor that allows you to custom set the verb, noun and preposition synonyms used by the parser. This
        /// constructor is mostly used by the unit tests.
        /// </summary>
        /// <param name="verbSynonyms">Verb synonyms to be used by the parser.</param>
        /// <param name="nounSynonyms">Noun synonyms to be used by the parser.</param>
        /// <param name="prepositionMapping">Prepositions being used by the parser.</param>
        public Parser(IVerbSynonyms verbSynonyms, INounSynonyms nounSynonyms, IPrepositionMapping prepositionMapping) : base(verbSynonyms,nounSynonyms,prepositionMapping)
        {
        }

        /// <summary>
        /// This is the method that will take a users input and parse it into a game command.
        ///
        /// The parser process tries to split the input down into the following forms.
        /// verb - noun
        /// verb - noun - preposition - noun
        ///
        /// The command that is returned by this method will reduce any verb and noun synonyms down into a basic set of
        /// default verb and nouns that the game logic can easily react too. This means if the player types any of the following, then
        /// would be mapped to the same command.
        ///
        /// Get Key
        /// Grab Key
        /// Pickup key
        /// 
        /// </summary>
        /// <param name="command">A string representing the command types in by the user.</param>
        /// <returns>An instance of a command that is passed back to the controlling room for processing.</returns>
        public ICommand ParseCommand(string command)
        {
            if (string.IsNullOrEmpty(command))
            {
                ICommand toReturn = new Command
                {
                    FullTextCommand = string.Empty
                };

                return toReturn;
            }

            SanitizeInput(command, out string lowerCase, out string[] wordList);
            CheckForProfanity(lowerCase);

            return ReduceInputToCommand(lowerCase, wordList);
        }

        private ICommand ReduceInputToCommand(string lowerCase, string[] wordList)
        {
            switch (wordList.Length)
            {
                case 0:
                    return new Command();
                case 1:
                    SingleWordCommand(wordList[0]);
                    _command.FullTextCommand = lowerCase;
                    return _command;
                default:
                    MultiWordCommand(wordList);
                    _command.FullTextCommand = lowerCase;
                    return _command;
            }
        }

        private void SingleWordCommand(string command)
        {
            var direction = DirectionsHelper.GetDirectionCommand(command);

            _command.Verb = direction.Verb;
            _command.Noun = direction.Noun;
            _command.FullTextCommand = direction.FullTextCommand;

            if (_command.Verb == VerbCodes.NoCommand)
            {
                _command.Verb = Verbs.GetVerbForSynonym(command);             
            }
        }

        private void MultiWordCommand(string[] commandList)
        {
            foreach (var word in commandList)
            {            
                switch(_parserStates)
                {
                    case ParserStatesEnum.Verb:
                        var verb = ProcessVerbs(word, ParserStatesEnum.Noun);

                        if (verb == VerbCodes.NoCommand) { continue; }

                        _command.Verb = verb;
                        break;
                    case ParserStatesEnum.Noun:
                        if (Adjectives.CheckAdjectiveExists(word))
                        {
                            _command.Adjective = word;
                            continue;
                        }

                        var noun = ProcessNoun(word, ParserStatesEnum.Preposition);

                        if (noun == string.Empty) continue;
                        _command.Noun = noun;
                        break;
                    case ParserStatesEnum.Preposition:
                        var preposition = ProcessPreposition(word, ParserStatesEnum.Noun2);

                        if (preposition == PropositionEnum.NotRecognised) { continue; }
                        _command.Preposition = preposition;
                        break;
                    case ParserStatesEnum.Noun2:
                        if (Adjectives.CheckAdjectiveExists(word))
                        {
                            _command.Adjective2 = word;
                            continue;
                        }

                        var noun2 = ProcessNoun(word, ParserStatesEnum.Preposition2);

                        if (noun2 == string.Empty) continue;
                        _command.Noun2 = noun2;
                        break;
                    case ParserStatesEnum.Preposition2:
                        var preposition2 = ProcessPreposition(word, ParserStatesEnum.Noun3);
                        if (preposition2 == PropositionEnum.NotRecognised) continue;
                        _command.Preposition2 = preposition2;
                        break;
                    case ParserStatesEnum.Noun3:
                        if (Adjectives.CheckAdjectiveExists(word))
                        {
                            _command.Adjective3 = word;
                            continue;
                        }

                        var noun3 = ProcessNoun(word, ParserStatesEnum.None);

                        if (noun3 == string.Empty) continue;
                        _command.Noun3 = noun3;
                        break;
                }
            }

            _parserStates = ParserStatesEnum.Verb;
        }           
    }
}
