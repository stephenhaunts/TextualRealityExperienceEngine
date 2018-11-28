/*
MIT License

Copyright(c) 2018 

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

namespace TextualRealityExperienceEngine.GameEngine
{
    public static class DirectionsHelper
    {
        public static ICommand GetDirectionCommand(string command)
        {
            ICommand toReturn = new Command();
            string lowerCaseCommand = command.ToLower();

            switch (lowerCaseCommand)
            {
                case "north":
                    toReturn.Verb = Synonyms.VerbCodes.Go;
                    toReturn.Noun = "north";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;

                case "south":
                    toReturn.Verb = Synonyms.VerbCodes.Go;
                    toReturn.Noun = "south";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;

                case "east":
                    toReturn.Verb = Synonyms.VerbCodes.Go;
                    toReturn.Noun = "east";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;

                case "west":
                    toReturn.Verb = Synonyms.VerbCodes.Go;
                    toReturn.Noun = "west";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;

                case "northeast":
                    toReturn.Verb = Synonyms.VerbCodes.Go;
                    toReturn.Noun = "northeast";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;

                case "southeast":
                    toReturn.Verb = Synonyms.VerbCodes.Go;
                    toReturn.Noun = "southeast";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;

                case "northwest":
                    toReturn.Verb = Synonyms.VerbCodes.Go;
                    toReturn.Noun = "northwest";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;

                case "southwest":
                    toReturn.Verb = Synonyms.VerbCodes.Go;
                    toReturn.Noun = "southwest";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;


                case "n":
                    toReturn.Verb = Synonyms.VerbCodes.Go;
                    toReturn.Noun = "north";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;

                case "s":
                    toReturn.Verb = Synonyms.VerbCodes.Go;
                    toReturn.Noun = "south";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;

                case "e":
                    toReturn.Verb = Synonyms.VerbCodes.Go;
                    toReturn.Noun = "east";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;

                case "w":
                    toReturn.Verb = Synonyms.VerbCodes.Go;
                    toReturn.Noun = "west";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;

                case "ne":
                    toReturn.Verb = Synonyms.VerbCodes.Go;
                    toReturn.Noun = "northeast";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;

                case "se":
                    toReturn.Verb = Synonyms.VerbCodes.Go;
                    toReturn.Noun = "southeast";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;

                case "nw":
                    toReturn.Verb = Synonyms.VerbCodes.Go;
                    toReturn.Noun = "northwest";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;

                case "sw":
                    toReturn.Verb = Synonyms.VerbCodes.Go;
                    toReturn.Noun = "southwest";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;

                case "f":
                    toReturn.Verb = Synonyms.VerbCodes.Go;
                    toReturn.Noun = "north";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;

                case "b":
                    toReturn.Verb = Synonyms.VerbCodes.Go;
                    toReturn.Noun = "south";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;

                case "forward":
                    toReturn.Verb = Synonyms.VerbCodes.Go;
                    toReturn.Noun = "north";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;

                case "backward":
                    toReturn.Verb = Synonyms.VerbCodes.Go;
                    toReturn.Noun = "south";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;

                case "forwards":
                    toReturn.Verb = Synonyms.VerbCodes.Go;
                    toReturn.Noun = "north";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;

                case "backwards":
                    toReturn.Verb = Synonyms.VerbCodes.Go;
                    toReturn.Noun = "south";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;

                case "right":
                    toReturn.Verb = Synonyms.VerbCodes.Go;
                    toReturn.Noun = "east";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;

                case "left":
                    toReturn.Verb = Synonyms.VerbCodes.Go;
                    toReturn.Noun = "west";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;

                case "r":
                    toReturn.Verb = Synonyms.VerbCodes.Go;
                    toReturn.Noun = "east";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;

                case "l":
                    toReturn.Verb = Synonyms.VerbCodes.Go;
                    toReturn.Noun = "west";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;
            }

            return toReturn;
        }
    }
}
