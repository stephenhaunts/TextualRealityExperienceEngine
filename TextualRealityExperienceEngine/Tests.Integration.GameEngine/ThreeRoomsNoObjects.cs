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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextualRealityExperienceEngine.GameEngine;
using TextualRealityExperienceEngine.GameEngine.Interfaces;

namespace Tests.Integration.GameEngine
{
    [TestClass]
    public class ThreeRoomsNoObject
    {

        private readonly IGame _game = new Game();
        private const string _prologue = "Welcome to test adventure.You will be bedazzled with awesomeness.";

        private const string _outside_name = "Outside";
        private const string _outside_description = "You are standing on a driveway outside of a house. It is nightime and very cold. " +
                                                    "There is frost on the ground. There is a door to the north.";

        private const string _hallway_name = "Hallway";
        private const string _hallway_description = "You are standing in a hallway that is modern, yet worn. There is a door to the west." +
                                                    "To the south the front door leads back to the driveway.";

        private const string _lounge_name = "Lounge";
        private const string _lounge_description = "You are stand in the lounge. There is a sofa and a TV inside. There is a door back to the hallway to the east.";


        private IRoom _outside;
        private IRoom _hallway;
        private IRoom _lounge;

        private void InitializeGame()
        {
            _game.Prologue = _prologue;

            _outside = new Room(_outside_name, _outside_description, _game);
            _hallway = new Room(_hallway_name, _hallway_description, _game);
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
            Assert.AreEqual(_hallway_description, _hallway.Description);

            Assert.AreEqual(_lounge_name, _lounge.Name);
            Assert.AreEqual(_lounge_description, _lounge.Description);
        }

        [TestMethod]
        public void WalkAround()
        {
            InitializeGame();

            var reply = _game.ProcessCommand("go north");
            Assert.AreEqual(_hallway_description, reply.Reply);
            Assert.AreEqual(_hallway, _game.CurrentRoom);

            reply = _game.ProcessCommand("go west");
            Assert.AreEqual(_lounge_description, reply.Reply);
            Assert.AreEqual(_lounge, _game.CurrentRoom);

            reply = _game.ProcessCommand("go east");
            Assert.AreEqual(_hallway_description, reply.Reply);
            Assert.AreEqual(_hallway, _game.CurrentRoom);

            reply = _game.ProcessCommand("go south");
            Assert.AreEqual(_outside_description, reply.Reply);
            Assert.AreEqual(_outside, _game.CurrentRoom);
        }
    }
}
