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
using TextualRealityExperienceEngine.GameEngine.Interfaces;
using TextualRealityExperienceEngine.GameEngine.Synonyms;

namespace TextualRealityExperienceEngine.GameEngine
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
            Verbs = new VerbSynonyms();
            Nouns = new NounSynonyms();
            Prepositions = new PrepositionMapping();
            Adjectives = new AdjectiveMapping();
            EnableProfanityFilter = true;
        }

        /// <summary>
        /// Constructor that allows you to custom set the verb, noun and preposition synonyms used by the parser. This
        /// constructor is mostly used by the unit tests.
        /// </summary>
        /// <param name="verbSynonyms">Verb synonyms to be used by the parser.</param>
        /// <param name="nounSynonyms">Noun synonyms to be used by the parser.</param>
        /// <param name="prepositionMapping">Prepositions being used by the parser.</param>
        public Parser(IVerbSynonyms verbSynonyms, INounSynonyms nounSynonyms, IPrepositionMapping prepositionMapping)
        {
            Verbs = verbSynonyms;
            Nouns = nounSynonyms;
            Prepositions = prepositionMapping;
            Adjectives = new AdjectiveMapping();
            EnableProfanityFilter = true;
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

            var lowerCase = command.ToLower();

            lowerCase = RemovePunctuation(lowerCase);

            var wordList = lowerCase.Split(' ');
            _command = new Command();

            if (EnableProfanityFilter)
            {
                var profanity = _profanityFilter.StringContainsFirstProfanity(lowerCase);
                if (!string.IsNullOrEmpty(profanity))
                {
                    _command.ProfanityDetected = true;
                    _command.Profanity = profanity;
                }
            }

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
                if (_parserStates == ParserStatesEnum.Verb)
                {
                    var verb = ProcessVerbs(word, ParserStatesEnum.Noun);

                    if (verb == VerbCodes.NoCommand) { continue; }
                    _command.Verb = verb;
                }

                if (_parserStates == ParserStatesEnum.Noun)
                {
                    if (Adjectives.CheckAdjectiveExists(word))
                    {
                        _command.Adjective = word;
                        continue;
                    }

                    var noun = ProcessNoun(word, ParserStatesEnum.Preposition);

                    if (noun == string.Empty)continue; 
                    _command.Noun = noun;                    
                }

                if (_parserStates == ParserStatesEnum.Preposition)
                {               
                    var preposition = ProcessPreposition(word, ParserStatesEnum.Noun2);

                    if (preposition == PropositionEnum.NotRecognised) { continue; }
                    _command.Preposition = preposition;
                }

                if (_parserStates == ParserStatesEnum.Noun2)
                {
                    if (Adjectives.CheckAdjectiveExists(word))
                    {
                        _command.Adjective2 = word;
                        continue;
                    }

                    var noun = ProcessNoun(word, ParserStatesEnum.Preposition2);

                    if (noun == string.Empty) continue; 
                    _command.Noun2 = noun;
                }

                if (_parserStates == ParserStatesEnum.Preposition2)
                {
                    var preposition = ProcessPreposition(word, ParserStatesEnum.Noun3);
                    if (preposition == PropositionEnum.NotRecognised) continue; 
                    _command.Preposition2 = preposition;
                }

                if (_parserStates == ParserStatesEnum.Noun3)
                {
                    if (Adjectives.CheckAdjectiveExists(word))
                    {
                        _command.Adjective3 = word;
                        continue;
                    }

                    var noun = ProcessNoun(word, ParserStatesEnum.None);

                    if (noun == string.Empty) continue; 
                    _command.Noun3 = noun;
                }
            }

            _parserStates = ParserStatesEnum.Verb;
        }

        private VerbCodes ProcessVerbs(string word, ParserStatesEnum nextState)
        {
            var verb = Verbs.GetVerbForSynonym(word);

            if (verb != VerbCodes.NoCommand)
            {
                _parserStates = nextState;
            }

            if (verb != VerbCodes.NoCommand)
            {
                return verb;
            }

            return VerbCodes.NoCommand;
        }             

        private string ProcessNoun(string word, ParserStatesEnum nextState)
        {
            var noun = Nouns.GetNounForSynonym(word);
            if (!string.IsNullOrEmpty(noun))
            {
                _parserStates = nextState;
                return noun;           
            }

            return string.Empty;
        }

        private PropositionEnum ProcessPreposition(string word, ParserStatesEnum nextState)
        {
            var preposition = Prepositions.GetPreposition(word);

            if (preposition != PropositionEnum.NotRecognised)
            {
                _parserStates = nextState;
                return preposition;
            }

            return PropositionEnum.NotRecognised;
        }              
    }
}
