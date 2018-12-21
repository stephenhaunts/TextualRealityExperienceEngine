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
using TextualRealityExperienceEngine.GameEngine;
using TextualRealityExperienceEngine.GameEngine.Interfaces;
using TextualRealityExperienceEngine.GameEngine.Synonyms;

namespace Tests.SimpleGame
{
    public class Hallway : Room
    {
        public Hallway(string name, string description, IGame game) : base(name, description, game)
        {
        }

        public override string ProcessCommand(ICommand command)
        {
            string reply;
            
            if (command.ProfanityDetected)
            {
                return "There is no need to be rude.";
            }

            if (command.Verb == VerbCodes.Use)
            {
                if (command.Noun == "lightswitch")
                {
                    LightsOn = !LightsOn;

                    Game.NumberOfMoves++;
                    Game.IncreaseScore(1);


                    if (LightsOn)
                    {
                        return "You flip the lightswitch and the lights flicker for a few seconds until they illuminate the hallway. You hear a faint buzzing sound coming from the lights."
                               + Description;
                    }
                    else
                    {
                        return Description;
                    }
                }
            }

            reply = base.ProcessCommand(command);

            return reply;
        }
    }
}