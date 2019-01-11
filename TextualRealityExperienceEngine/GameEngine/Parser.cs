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

using TextualRealityExperienceEngine.GameEngine.Interfaces;
using TextualRealityExperienceEngine.GameEngine.Synonyms;

namespace TextualRealityExperienceEngine.GameEngine
{
    public enum ParserStatesEnum
    {
        Verb = 0,
        Noun = 2,
        Preposition = 3,
        Noun2 = 4
    }

    public class Parser : IParser
    {
        public IVerbSynonyms Verbs { get; }
        public INounSynonyms Nouns { get; }
        public bool EnableProfanityFilter { get; set; }
        public IPrepositionMapping Prepositions { get; }
        private ParserStatesEnum _parserStates = ParserStatesEnum.Verb;
        private ICommand _command;
        private readonly IProfanityFilter _profanityFilter = new ProfanityFilter();

        public Parser()
        {
            Verbs = new VerbSynonyms();
            Nouns = new NounSynonyms();
            Prepositions = new PrepositionMapping();
            EnableProfanityFilter = true;
        }

        public Parser(IVerbSynonyms verbSynonyms, INounSynonyms nounSynonyms, IPrepositionMapping prepositionMapping)
        {
            Verbs = verbSynonyms;
            Nouns = nounSynonyms;
            Prepositions = prepositionMapping;
            EnableProfanityFilter = true;
        }

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
            _command = DirectionsHelper.GetDirectionCommand(command);

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
                    if (ProcessVerbs(word)) continue;                                                  
                }

                if (_parserStates == ParserStatesEnum.Noun)
                {
                    if (ProcessNoun1(word)) continue;
                }

                if (_parserStates == ParserStatesEnum.Preposition)
                {
                    if (ProcessPreposition(word)) continue;
                }

                if (_parserStates == ParserStatesEnum.Noun2)
                {
                    ProcessNoun2(word);
                }
            }

            _parserStates = ParserStatesEnum.Verb;
        }

        private bool ProcessVerbs(string word)
        {
            var verb = Verbs.GetVerbForSynonym(word);
            _parserStates = ParserStatesEnum.Noun;

            if (verb != VerbCodes.NoCommand)
            {
                _command.Verb = verb;
                return true;
            }

            return false;
        }

        private bool ProcessNoun1(string word)
        {
            var noun = Nouns.GetNounForSynonym(word);
            if (!string.IsNullOrEmpty(noun))
            {
                _command.Noun = noun;
                _parserStates = ParserStatesEnum.Preposition;

                return true;
            }

            return false;
        }

        private bool ProcessPreposition(string word)
        {
            var preposition = Prepositions.GetPreposition(word);
            
            if (preposition != PropositionEnum.NotRecognised)
            {
                _command.Preposition = preposition;
                _parserStates = ParserStatesEnum.Noun2;
                return true;
            }

            return false;
        }

        private bool ProcessNoun2(string word)
        {
            var noun = Nouns.GetNounForSynonym(word);
            
            if (!string.IsNullOrEmpty(noun))
            {
                _command.Noun2 = noun;
                return true;
            }

            return false;
        }
    }
}
