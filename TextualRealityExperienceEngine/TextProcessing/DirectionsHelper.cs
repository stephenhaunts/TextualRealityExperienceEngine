/*
MIT License

Copyright(c) 2019 

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

using TextualRealityExperienceEngine.GameEngine.TextProcessing.Interfaces;
using TextualRealityExperienceEngine.GameEngine.TextProcessing.Synonyms;

namespace TextualRealityExperienceEngine.GameEngine.TextProcessing
{
    /// <summary>
    /// A helper class that returns a ICommand instance with a valid command for a specified direction.
    /// </summary>
    public static class DirectionsHelper
    {
        /// <summary>
        /// Returns an ICommand instance with a valid command for a specified direction.
        /// </summary>
        /// <param name="command">A text representation of a direction to follow, such as north, south, east or west.</param>
        /// <returns>An instance of an ICommand that is a valid game command for the engine to process.</returns>
        public static ICommand GetDirectionCommand(string command)
        {
            var toReturn = new Command();
            var lowerCaseCommand = command.ToLower();

            switch (lowerCaseCommand)
            {
                case "north":
                    toReturn.Verb = VerbCodes.Go;
                    toReturn.Noun = "north";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;
                case "south":
                    toReturn.Verb = VerbCodes.Go;
                    toReturn.Noun = "south";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;
                case "east":
                    toReturn.Verb = VerbCodes.Go;
                    toReturn.Noun = "east";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;
                case "west":
                    toReturn.Verb = VerbCodes.Go;
                    toReturn.Noun = "west";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;
                case "northeast":
                    toReturn.Verb = VerbCodes.Go;
                    toReturn.Noun = "northeast";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;
                case "southeast":
                    toReturn.Verb = VerbCodes.Go;
                    toReturn.Noun = "southeast";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;
                case "northwest":
                    toReturn.Verb = VerbCodes.Go;
                    toReturn.Noun = "northwest";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;
                case "southwest":
                    toReturn.Verb = VerbCodes.Go;
                    toReturn.Noun = "southwest";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;
                case "n":
                    toReturn.Verb = VerbCodes.Go;
                    toReturn.Noun = "north";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;
                case "s":
                    toReturn.Verb = VerbCodes.Go;
                    toReturn.Noun = "south";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;
                case "e":
                    toReturn.Verb = VerbCodes.Go;
                    toReturn.Noun = "east";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;
                case "w":
                    toReturn.Verb = VerbCodes.Go;
                    toReturn.Noun = "west";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;
                case "ne":
                    toReturn.Verb = VerbCodes.Go;
                    toReturn.Noun = "northeast";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;
                case "se":
                    toReturn.Verb = VerbCodes.Go;
                    toReturn.Noun = "southeast";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;
                case "nw":
                    toReturn.Verb = VerbCodes.Go;
                    toReturn.Noun = "northwest";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;
                case "sw":
                    toReturn.Verb = VerbCodes.Go;
                    toReturn.Noun = "southwest";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;
                case "f":
                    toReturn.Verb = VerbCodes.Go;
                    toReturn.Noun = "north";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;
                case "b":
                    toReturn.Verb = VerbCodes.Go;
                    toReturn.Noun = "south";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;
                case "forward":
                    toReturn.Verb = VerbCodes.Go;
                    toReturn.Noun = "north";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;
                case "backward":
                    toReturn.Verb = VerbCodes.Go;
                    toReturn.Noun = "south";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;
                case "forwards":
                    toReturn.Verb = VerbCodes.Go;
                    toReturn.Noun = "north";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;
                case "backwards":
                    toReturn.Verb = VerbCodes.Go;
                    toReturn.Noun = "south";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;
                case "right":
                    toReturn.Verb = VerbCodes.Go;
                    toReturn.Noun = "east";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;
                case "left":
                    toReturn.Verb = VerbCodes.Go;
                    toReturn.Noun = "west";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;
                case "r":
                    toReturn.Verb = VerbCodes.Go;
                    toReturn.Noun = "east";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;
                case "l":
                    toReturn.Verb = VerbCodes.Go;
                    toReturn.Noun = "west";
                    toReturn.FullTextCommand = lowerCaseCommand;
                    break;
            }

            return toReturn;
        }
    }
}
