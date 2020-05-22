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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextualRealityExperienceEngine.GameEngine;
using TextualRealityExperienceEngine.GameEngine.Interfaces;
using TextualRealityExperienceEngine.GameEngine.TextProcessing.Interfaces;
using TextualRealityExperienceEngine.GameEngine.TextProcessing.Synonyms;

namespace Tests.Integration.GameEngine
{
    class Outside : Room
    {    
        public Outside(string name, string description, IGame game) : base(name, description, game)
        {
           
        }

        public override string ProcessCommand(ICommand command)
        {
            if (command.ProfanityDetected)
            {
                return "There is no need to be rude.";
            }
            
            switch (command.Verb)
            {                   
                case VerbCodes.Look:
                    switch (command.Noun)
                    {                       
                        case "doormat":
                            return "It's a doormat where people wipe their feet. On it is written 'There is no place like 10.0.0.1'.";
                    }

                    break;
               
                case VerbCodes.NoCommand:
                    break;
                case VerbCodes.Go:
                    break;
                case VerbCodes.Drop:
                    break;
                case VerbCodes.Take:
                    break;
                case VerbCodes.Use:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var reply = base.ProcessCommand(command);
            return reply;
        }
    }
     
    [TestClass]
    public class OneRoomProfanityFilter
    {

        private readonly IGame _game = new Game();
        private const string Prologue = "Welcome to test adventure.You will be bedazzled with awesomeness.";

        private const string OutsideName = "Outside";
        private const string OutsideDescription = "You are standing on a driveway outside of a house. It is nightime and very cold. " +
                                                    "There is frost on the ground. There is a door to the north.";

        private IRoom _outside;       

        private void InitializeGame()
        {
            _game.Prologue = Prologue;

            _outside = new Outside(OutsideName, OutsideDescription, _game);
           
            _game.StartRoom = _outside;
            _game.CurrentRoom = _outside;
        }

        [TestMethod]
        public void TestInitialState()
        {
            InitializeGame();

            Assert.AreEqual(Prologue, _game.Prologue);
            Assert.AreEqual(_outside, _game.StartRoom);
            Assert.AreEqual(_outside, _game.CurrentRoom);
            Assert.IsNotNull(_game.Parser);

            Assert.AreEqual(OutsideName, _outside.Name);
            Assert.AreEqual(OutsideDescription, _outside.Description);
           
        }

        [TestMethod]
        public void WalkAround()
        {
            InitializeGame();
            var reply = _game.ProcessCommand("Bloody Hell!");
            Assert.AreEqual("There is no need to be rude.", reply.Reply);
        }
    }
}
