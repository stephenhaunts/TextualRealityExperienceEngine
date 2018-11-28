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

namespace TextualRealityExperienceEngine.Tests.Unit.GameEngine
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void ParseCommandNoCommandForEmptyString()
        {
            IParser parser = new Parser();
            var command = parser.ParseCommand("");

            Assert.AreEqual(VerbCodes.NoCommand, command.Verb);
            Assert.AreEqual("", command.FullTextCommand);
        }

        [TestMethod]
        public void ParseCommandReturnsValidCommandFor_Go_North()
        {
            IParser parser = new Parser();
            var command = parser.ParseCommand("Go North");

            Assert.AreEqual(VerbCodes.Go, command.Verb);
            Assert.AreEqual("north", command.Noun);
            Assert.AreEqual("go north", command.FullTextCommand);
        }

        [TestMethod]
        public void ParseCommandReturnsValidCommandFor_Run_NorthEast()
        {
            IParser parser = new Parser();
            var command = parser.ParseCommand("Run NorthEast");

            Assert.AreEqual(VerbCodes.Go, command.Verb);
            Assert.AreEqual("northeast", command.Noun);
            Assert.AreEqual("run northeast", command.FullTextCommand);
        }

        [TestMethod]
        public void ParseCommandReturnsValidCommandFor_Slide_SW()
        {
            IParser parser = new Parser();
            var command = parser.ParseCommand("Slide SW");

            Assert.AreEqual(VerbCodes.Go, command.Verb);
            Assert.AreEqual("southwest", command.Noun);
            Assert.AreEqual("slide sw", command.FullTextCommand);
        }

        [TestMethod]
        public void ParseSingleWordCommandReturnsValidCommandFor_NE()
        {
            IParser parser = new Parser();
            var command = parser.ParseCommand("NE");

            Assert.AreEqual(VerbCodes.Go, command.Verb);
            Assert.AreEqual("northeast", command.Noun);
            Assert.AreEqual("ne", command.FullTextCommand);
        }

        [TestMethod]
        public void ParseSingleWordCommandReturnsValidCommandFor_L()
        {
            IParser parser = new Parser();
            var command = parser.ParseCommand("L");

            Assert.AreEqual(VerbCodes.Go, command.Verb);
            Assert.AreEqual("west", command.Noun);
            Assert.AreEqual("l", command.FullTextCommand);
        }

    }
}
