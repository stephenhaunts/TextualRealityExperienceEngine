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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextualRealityExperienceEngine.GameEngine;
using TextualRealityExperienceEngine.GameEngine.Interfaces;
using TextualRealityExperienceEngine.GameEngine.Synonyms;

namespace Tests.Integration.GameEngine
{
    [TestClass]
    public class ThreeRoomsDarkHallway
    {

        private readonly IGame _game = new Game();
        private const string _prologue = "Welcome to test adventure.You will be bedazzled with awesomeness.";

        private const string _outside_name = "Outside";
        private const string _outside_description = "You are standing on a driveway outside of a house. It is nightime and very cold. " +
                                                    "There is frost on the ground. There is a door to the north.";

        private const string _hallway_name = "Hallway";
        private const string _hallway_description = "You are standing in a hallway that is modern, yet worn. There is a door to the west." +
                                                    "To the south the front door leads back to the driveway.";

        private const string _hallway_lights_off = "You are standing in a very dimly lit hallway. Your eyes struggle to adjust to the low light. " +
                                           "You notice there is a swith on the wall to your left.";

        private const string _lounge_name = "Lounge";
        private const string _lounge_description = "You are stand in the lounge. There is a sofa and a TV inside. There is a door back to the hallway to the east.";

        public class Hallway : Room
        {
            public Hallway(string name, string description, IGame game) : base(name, description, game)
            {
            }

            public override string ProcessCommand(ICommand command)
            {
                string reply;

                if (command.Verb == VerbCodes.Use)
                {
                    LightsOn = !LightsOn;

                    Game.NumberOfMoves++;
                    Game.Score++;


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

                reply = base.ProcessCommand(command);

                return reply;
            }
        }

        private IRoom _outside;
        private IRoom _hallway;
        private IRoom _lounge;

        private void InitializeGame()
        {
            _game.Prologue = _prologue;

            _game.Parser.Nouns.Add("light", "lightswitch");
            _game.Parser.Nouns.Add("lightswitch", "lightswitch");
            _game.Parser.Nouns.Add("switch", "lightswitch");

            _outside = new Room(_outside_name, _outside_description, _game);
            _hallway = new Hallway(_hallway_name, _hallway_description, _game);
            _hallway.LightsOn = false;

            _hallway.LightsOffDescription = _hallway_lights_off;

            _lounge = new Room(_lounge_name, _lounge_description, _game);

            _outside.AddExit(Direction.North, _hallway);
            _hallway.AddExit(Direction.West, _lounge);

            _game.StartRoom = _outside;
            _game.CurrentRoom = _outside;
        }

        [TestMethod]
        public void TestInitialState()
        {
            InitializeGame();

            Assert.AreEqual(_prologue, _game.Prologue);
            Assert.AreEqual(_outside, _game.StartRoom);
            Assert.AreEqual(_outside, _game.CurrentRoom);
            Assert.IsNotNull(_game.Parser);

            Assert.AreEqual(_outside_name, _outside.Name);
            Assert.AreEqual(_outside_description, _outside.Description);

            Assert.AreEqual(_hallway_name, _hallway.Name);
            Assert.IsTrue(_hallway.Description.StartsWith("You are standing in a very dimly lit hallway.", StringComparison.Ordinal));


            Assert.AreEqual(_lounge_name, _lounge.Name);
            Assert.AreEqual(_lounge_description, _lounge.Description);
        }

        [TestMethod]
        public void WalkAround()
        {
            InitializeGame();

            var reply = _game.ProcessCommand("go north");
            Assert.AreEqual(_hallway, _game.CurrentRoom);

            Assert.IsTrue(reply.Reply.StartsWith("You are standing in a very dimly lit hallway.", StringComparison.Ordinal));

            // Turn lights on
            reply = _game.ProcessCommand("use switch");
            Assert.IsTrue(reply.Reply.StartsWith("You flip the lightswitch and the lights flicker for a few seconds", StringComparison.Ordinal));


            // Turn lights off
            reply = _game.ProcessCommand("use switch");
            Assert.IsTrue(reply.Reply.StartsWith("You are standing in a very dimly lit hallway.", StringComparison.Ordinal));

            reply = _game.ProcessCommand("go west");
            Assert.AreEqual(_lounge_description, reply.Reply);
            Assert.AreEqual(_lounge, _game.CurrentRoom);

            reply = _game.ProcessCommand("go east");
            Assert.IsTrue(reply.Reply.StartsWith("You are standing in a very dimly lit hallway.", StringComparison.Ordinal));

            // Turn lights on
            reply = _game.ProcessCommand("use switch");
            Assert.IsTrue(reply.Reply.StartsWith("You flip the lightswitch and the lights flicker for a few seconds", StringComparison.Ordinal));

            Assert.AreEqual(_hallway, _game.CurrentRoom);

            reply = _game.ProcessCommand("go south");
            Assert.AreEqual(_outside_description, reply.Reply);
            Assert.AreEqual(_outside, _game.CurrentRoom);
        }
    }
}
