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
using TextualRealityExperienceEngine.GameEngine;
using TextualRealityExperienceEngine.GameEngine.Interfaces;
using TextualRealityExperienceEngine.GameEngine.Synonyms;

namespace Tests.SimpleGame
{
    public class Hallway : Room
    {
        public Hallway(IGame game) : base(game)
        {
            game.ContentManagement.AddContentItem("HallwayName", "Hallway");
            game.ContentManagement.AddContentItem("HallwayDescription", "You are standing in a hallway that is modern, yet worn. There is a door to the west, and a door to the north." +
                                                                         "To the south the front door leads back to the driveway.");
            game.ContentManagement.AddContentItem("HallwayLightsOff", "You are standing in a very dimly lit hallway. Your eyes struggle to adjust to the low light. " +
                                                           "You notice there is a switch on the wall to your left.");

            Name = game.ContentManagement.RetrieveContentItem("HallwayName");
            Description = game.ContentManagement.RetrieveContentItem("HallwayDescription");
            LightsOffDescription = game.ContentManagement.RetrieveContentItem("HallwayLightsOff");

            game.ContentManagement.AddContentItem("FlipLightSwitch",
                "You flip the light switch and the lights flicker for a few seconds until they illuminate the hallway. You hear a faint buzzing sound coming from the lights.");
           
            game.Parser.Nouns.Add("light", "lightswitch");
            game.Parser.Nouns.Add("lights", "lightswitch");
            game.Parser.Nouns.Add("lightswitch", "lightswitch");
            game.Parser.Nouns.Add("switch", "lightswitch");
        }

        public override string ProcessCommand(ICommand command)
        {
            string reply;
            
            if (command.ProfanityDetected)
            {
                return Game.ContentManagement.RetrieveContentItem("NoNeedToBeRude");
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
                        return Game.ContentManagement.RetrieveContentItem("FlipLightSwitch")
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