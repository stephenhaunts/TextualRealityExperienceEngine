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
    public class Parser : IParser
    {
        readonly IVerbSynonyms _verbSynonyms = new VerbSynonyms();
        readonly INounSynonyms _nounSynonyms = new NounSynonyms();


        //
        // https://www.talkenglish.com/vocabulary/top-50-prepositions.aspx
        //


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

            // to lower
            var lowerCase = command.ToLower();

            // split into list of words
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
            ICommand command = new Command();

            bool verbSet = false;
            bool nounSet = false;

            foreach (string word in commandList)
            {
                if (verbSet == false)
                {
                    var verb = _verbSynonyms.GetVerbforSynonum(word);

                    if (verb != VerbCodes.NoCommand)
                    {
                        command.Verb = verb;
                        verbSet = true;
                        continue;
                    }                                     
                }

                if (nounSet == false)
                {
                    var noun = _nounSynonyms.GetNounforSynonum(word);
                    if (!string.IsNullOrEmpty(noun))
                    {
                        command.Noun = noun;
                        nounSet = true;
                        continue;
                    }
                }
            }

            return command; 
        }
    }
}
