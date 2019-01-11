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
using TextualRealityExperienceEngine.GameEngine.Synonyms;

namespace TextualRealityExperienceEngine.Tests.Unit.GameEngine
{
    [TestClass]
    public class DirectionsHelperTests
    {
        [TestMethod]
        public void GetDirectionCommandReturnsValidCommandFor_North()
        {
            ICommand command = DirectionsHelper.GetDirectionCommand("north");

            Assert.AreEqual(VerbCodes.Go, command.Verb);
            Assert.AreEqual("north", command.Noun);
            Assert.AreEqual(PropositionEnum.NotRecognised, command.Preposition);
            Assert.AreEqual(string.Empty, command.Noun2);
            Assert.AreEqual("north", command.FullTextCommand);
        }

        [TestMethod]
        public void GetDirectionCommandReturnsValidCommandFor_South()
        {
            ICommand command = DirectionsHelper.GetDirectionCommand("south");

            Assert.AreEqual(VerbCodes.Go, command.Verb);
            Assert.AreEqual("south", command.Noun);
            Assert.AreEqual(PropositionEnum.NotRecognised, command.Preposition);
            Assert.AreEqual(string.Empty, command.Noun2);
            Assert.AreEqual("south", command.FullTextCommand);
        }

        [TestMethod]
        public void GetDirectionCommandReturnsValidCommandFor_East()
        {
            ICommand command = DirectionsHelper.GetDirectionCommand("east");

            Assert.AreEqual(VerbCodes.Go, command.Verb);
            Assert.AreEqual("east", command.Noun);
            Assert.AreEqual(PropositionEnum.NotRecognised, command.Preposition);
            Assert.AreEqual(string.Empty, command.Noun2);
            Assert.AreEqual("east", command.FullTextCommand);
        }

        [TestMethod]
        public void GetDirectionCommandReturnsValidCommandFor_West()
        {
            ICommand command = DirectionsHelper.GetDirectionCommand("west");

            Assert.AreEqual(VerbCodes.Go, command.Verb);
            Assert.AreEqual("west", command.Noun);
            Assert.AreEqual(PropositionEnum.NotRecognised, command.Preposition);
            Assert.AreEqual(string.Empty, command.Noun2);
            Assert.AreEqual("west", command.FullTextCommand);
        }

        [TestMethod]
        public void GetDirectionCommandReturnsValidCommandFor_NorthEast()
        {
            ICommand command = DirectionsHelper.GetDirectionCommand("northeast");

            Assert.AreEqual(VerbCodes.Go, command.Verb);
            Assert.AreEqual("northeast", command.Noun);
            Assert.AreEqual(PropositionEnum.NotRecognised, command.Preposition);
            Assert.AreEqual(string.Empty, command.Noun2);
            Assert.AreEqual("northeast", command.FullTextCommand);
        }

        [TestMethod]
        public void GetDirectionCommandReturnsValidCommandFor_SouthEast()
        {
            ICommand command = DirectionsHelper.GetDirectionCommand("southeast");

            Assert.AreEqual(VerbCodes.Go, command.Verb);
            Assert.AreEqual("southeast", command.Noun);
            Assert.AreEqual(PropositionEnum.NotRecognised, command.Preposition);
            Assert.AreEqual(string.Empty, command.Noun2);
            Assert.AreEqual("southeast", command.FullTextCommand);
        }

        [TestMethod]
        public void GetDirectionCommandReturnsValidCommandFor_SouthWest()
        {
            ICommand command = DirectionsHelper.GetDirectionCommand("southwest");

            Assert.AreEqual(VerbCodes.Go, command.Verb);
            Assert.AreEqual("southwest", command.Noun);
            Assert.AreEqual(PropositionEnum.NotRecognised, command.Preposition);
            Assert.AreEqual(string.Empty, command.Noun2);
            Assert.AreEqual("southwest", command.FullTextCommand);
        }

        [TestMethod]
        public void GetDirectionCommandReturnsValidCommandFor_NorthWest()
        {
            ICommand command = DirectionsHelper.GetDirectionCommand("northwest");

            Assert.AreEqual(VerbCodes.Go, command.Verb);
            Assert.AreEqual("northwest", command.Noun);
            Assert.AreEqual(PropositionEnum.NotRecognised, command.Preposition);
            Assert.AreEqual(string.Empty, command.Noun2);
            Assert.AreEqual("northwest", command.FullTextCommand);
        }

        [TestMethod]
        public void GetDirectionCommandReturnsValidCommandFor_N()
        {
            ICommand command = DirectionsHelper.GetDirectionCommand("n");

            Assert.AreEqual(VerbCodes.Go, command.Verb);
            Assert.AreEqual("north", command.Noun);
            Assert.AreEqual(PropositionEnum.NotRecognised, command.Preposition);
            Assert.AreEqual(string.Empty, command.Noun2);
            Assert.AreEqual("n", command.FullTextCommand);
        }

        [TestMethod]
        public void GetDirectionCommandReturnsValidCommandFor_S()
        {
            ICommand command = DirectionsHelper.GetDirectionCommand("s");

            Assert.AreEqual(VerbCodes.Go, command.Verb);
            Assert.AreEqual("south", command.Noun);
            Assert.AreEqual(PropositionEnum.NotRecognised, command.Preposition);
            Assert.AreEqual(string.Empty, command.Noun2);
            Assert.AreEqual("s", command.FullTextCommand);
        }

        [TestMethod]
        public void GetDirectionCommandReturnsValidCommandFor_E()
        {
            ICommand command = DirectionsHelper.GetDirectionCommand("e");

            Assert.AreEqual(VerbCodes.Go, command.Verb);
            Assert.AreEqual("east", command.Noun);
            Assert.AreEqual(PropositionEnum.NotRecognised, command.Preposition);
            Assert.AreEqual(string.Empty, command.Noun2);
            Assert.AreEqual("e", command.FullTextCommand);
        }

        [TestMethod]
        public void GetDirectionCommandReturnsValidCommandFor_W()
        {
            ICommand command = DirectionsHelper.GetDirectionCommand("w");

            Assert.AreEqual(VerbCodes.Go, command.Verb);
            Assert.AreEqual("west", command.Noun);
            Assert.AreEqual(PropositionEnum.NotRecognised, command.Preposition);
            Assert.AreEqual(string.Empty, command.Noun2);
            Assert.AreEqual("w", command.FullTextCommand);
        }

        [TestMethod]
        public void GetDirectionCommandReturnsValidCommandFor_NE()
        {
            ICommand command = DirectionsHelper.GetDirectionCommand("ne");

            Assert.AreEqual(VerbCodes.Go, command.Verb);
            Assert.AreEqual("northeast", command.Noun);
            Assert.AreEqual(PropositionEnum.NotRecognised, command.Preposition);
            Assert.AreEqual(string.Empty, command.Noun2);
            Assert.AreEqual("ne", command.FullTextCommand);
        }

        [TestMethod]
        public void GetDirectionCommandReturnsValidCommandFor_SE()
        {
            ICommand command = DirectionsHelper.GetDirectionCommand("se");

            Assert.AreEqual(VerbCodes.Go, command.Verb);
            Assert.AreEqual("southeast", command.Noun);
            Assert.AreEqual(PropositionEnum.NotRecognised, command.Preposition);
            Assert.AreEqual(string.Empty, command.Noun2);
            Assert.AreEqual("se", command.FullTextCommand);
        }

        [TestMethod]
        public void GetDirectionCommandReturnsValidCommandFor_SW()
        {
            ICommand command = DirectionsHelper.GetDirectionCommand("sw");

            Assert.AreEqual(VerbCodes.Go, command.Verb);
            Assert.AreEqual("southwest", command.Noun);
            Assert.AreEqual(PropositionEnum.NotRecognised, command.Preposition);
            Assert.AreEqual(string.Empty, command.Noun2);
            Assert.AreEqual("sw", command.FullTextCommand);
        }

        [TestMethod]
        public void GetDirectionCommandReturnsValidCommandFor_NW()
        {
            ICommand command = DirectionsHelper.GetDirectionCommand("nw");

            Assert.AreEqual(VerbCodes.Go, command.Verb);
            Assert.AreEqual("northwest", command.Noun);
            Assert.AreEqual(PropositionEnum.NotRecognised, command.Preposition);
            Assert.AreEqual(string.Empty, command.Noun2);
            Assert.AreEqual("nw", command.FullTextCommand);
        }

        [TestMethod]
        public void GetDirectionCommandReturnsValidCommandFor_F()
        {
            ICommand command = DirectionsHelper.GetDirectionCommand("f");

            Assert.AreEqual(VerbCodes.Go, command.Verb);
            Assert.AreEqual("north", command.Noun);
            Assert.AreEqual(PropositionEnum.NotRecognised, command.Preposition);
            Assert.AreEqual(string.Empty, command.Noun2);
            Assert.AreEqual("f", command.FullTextCommand);
        }

        [TestMethod]
        public void GetDirectionCommandReturnsValidCommandFor_B()
        {
            ICommand command = DirectionsHelper.GetDirectionCommand("b");

            Assert.AreEqual(VerbCodes.Go, command.Verb);
            Assert.AreEqual("south", command.Noun);
            Assert.AreEqual(PropositionEnum.NotRecognised, command.Preposition);
            Assert.AreEqual(string.Empty, command.Noun2);
            Assert.AreEqual("b", command.FullTextCommand);
        }

        [TestMethod]
        public void GetDirectionCommandReturnsValidCommandFor_Forward()
        {
            ICommand command = DirectionsHelper.GetDirectionCommand("forward");

            Assert.AreEqual(VerbCodes.Go, command.Verb);
            Assert.AreEqual("north", command.Noun);
            Assert.AreEqual(PropositionEnum.NotRecognised, command.Preposition);
            Assert.AreEqual(string.Empty, command.Noun2);
            Assert.AreEqual("forward", command.FullTextCommand);
        }

        [TestMethod]
        public void GetDirectionCommandReturnsValidCommandFor_Backward()
        {
            ICommand command = DirectionsHelper.GetDirectionCommand("backward");

            Assert.AreEqual(VerbCodes.Go, command.Verb);
            Assert.AreEqual("south", command.Noun);
            Assert.AreEqual(PropositionEnum.NotRecognised, command.Preposition);
            Assert.AreEqual(string.Empty, command.Noun2);
            Assert.AreEqual("backward", command.FullTextCommand);
        }

        [TestMethod]
        public void GetDirectionCommandReturnsValidCommandFor_L()
        {
            ICommand command = DirectionsHelper.GetDirectionCommand("l");

            Assert.AreEqual(VerbCodes.Go, command.Verb);
            Assert.AreEqual("west", command.Noun);
            Assert.AreEqual(PropositionEnum.NotRecognised, command.Preposition);
            Assert.AreEqual(string.Empty, command.Noun2);
            Assert.AreEqual("l", command.FullTextCommand);
        }

        [TestMethod]
        public void GetDirectionCommandReturnsValidCommandFor_R()
        {
            ICommand command = DirectionsHelper.GetDirectionCommand("r");

            Assert.AreEqual(VerbCodes.Go, command.Verb);
            Assert.AreEqual("east", command.Noun);
            Assert.AreEqual(PropositionEnum.NotRecognised, command.Preposition);
            Assert.AreEqual(string.Empty, command.Noun2);
            Assert.AreEqual("r", command.FullTextCommand);
        }

        [TestMethod]
        public void GetDirectionCommandReturnsValidCommandFor_Left()
        {
            ICommand command = DirectionsHelper.GetDirectionCommand("left");

            Assert.AreEqual(VerbCodes.Go, command.Verb);
            Assert.AreEqual("west", command.Noun);
            Assert.AreEqual(PropositionEnum.NotRecognised, command.Preposition);
            Assert.AreEqual(string.Empty, command.Noun2);
            Assert.AreEqual("left", command.FullTextCommand);
        }

        [TestMethod]
        public void GetDirectionCommandReturnsValidCommandFor_Right()
        {
            ICommand command = DirectionsHelper.GetDirectionCommand("right");

            Assert.AreEqual(VerbCodes.Go, command.Verb);
            Assert.AreEqual("east", command.Noun);
            Assert.AreEqual(PropositionEnum.NotRecognised, command.Preposition);
            Assert.AreEqual(string.Empty, command.Noun2);
            Assert.AreEqual("right", command.FullTextCommand);
        }
    }
}
