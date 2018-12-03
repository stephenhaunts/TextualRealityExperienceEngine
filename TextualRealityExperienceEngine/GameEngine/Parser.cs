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
        public IPrepositionMapping Prepositions { get; }
        ParserStatesEnum parserStates = ParserStatesEnum.Verb;
        ICommand _command;

        public Parser()
        {
            Verbs = new VerbSynonyms();
            Nouns = new NounSynonyms();
            Prepositions = new PrepositionMapping();
        }

        public Parser(IVerbSynonyms verbSynonyms, INounSynonyms nounSynonyms, IPrepositionMapping prepositionMapping)
        {
            Verbs = verbSynonyms;
            Nouns = nounSynonyms;
            Prepositions = prepositionMapping;
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

            ICommand commandList = new Command();

            switch (wordList.Length)
            {
                case 0:
                    return new Command();
                case 1:
                    commandList = SingleWordCommand(wordList[0]);
                    commandList.FullTextCommand = lowerCase;
                    return commandList;
                default:
                    commandList = MultiWordCommand(wordList);
                    commandList.FullTextCommand = lowerCase;
                    return commandList;
            }                      
        }

        ICommand SingleWordCommand(string command)
        {
            return DirectionsHelper.GetDirectionCommand(command);                     
        }

        ICommand MultiWordCommand(string[] commandList)
        {
            _command = new Command();

            foreach (string word in commandList)
            {
                if (parserStates == ParserStatesEnum.Verb)
                {
                    if (ProcessVerbs(word)) continue;                                                  
                }

                if (parserStates == ParserStatesEnum.Noun)
                {
                    if (ProcessNoun1(word)) continue;
                }

                if (parserStates == ParserStatesEnum.Preposition)
                {
                    if (ProcessPreposition(word)) continue;
                }

                if (parserStates == ParserStatesEnum.Noun2)
                {
                    if (ProcessNoun2(word)) continue;
                }
            }

            parserStates = ParserStatesEnum.Verb;

            return _command; 
        }

        bool ProcessVerbs(string word)
        {
            var verb = Verbs.GetVerbforSynonum(word);
            parserStates = ParserStatesEnum.Noun;

            if (verb != VerbCodes.NoCommand)
            {
                _command.Verb = verb;
                return true;
            }

            return false;
        }

        bool ProcessNoun1(string word)
        {
            var noun = Nouns.GetNounforSynonum(word);
            if (!string.IsNullOrEmpty(noun))
            {
                _command.Noun = noun;
                parserStates = ParserStatesEnum.Preposition;

                return true;
            }

            return false;
        }

        bool ProcessPreposition(string word)
        {
            var preposition = Prepositions.GetPreposition(word);
            if (preposition != PropositionEnum.NotRecognised)
            {
                _command.Preposition = preposition;
                parserStates = ParserStatesEnum.Noun2;

                return true;
            }

            return false;
        }

        bool ProcessNoun2(string word)
        {
            var noun = Nouns.GetNounforSynonum(word);
            if (!string.IsNullOrEmpty(noun))
            {

                _command.Noun2 = noun;
                return true;
            }

            return false;
        }
    }
}
