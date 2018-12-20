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
        public void ParseCommandNoCommandForGiberishString()
        {
            IParser parser = new Parser();
            var command = parser.ParseCommand("siudfhw8e7r wrymw98ym78yr 98w4myr98wn");

            Assert.AreEqual(VerbCodes.NoCommand, command.Verb);
            Assert.AreEqual("siudfhw8e7r wrymw98ym78yr 98w4myr98wn", command.FullTextCommand);
        }

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
        public void ParseSingleWordCommandReturnsValidCommandFor_North()
        {
            IParser parser = new Parser();
            var command = parser.ParseCommand("North");

            Assert.AreEqual(VerbCodes.Go, command.Verb);
            Assert.AreEqual("north", command.Noun);
            Assert.AreEqual("north", command.FullTextCommand);
        }

        [TestMethod]
        public void ParseSingleWordCommandReturnsValidCommandFor_Twatty_North()
        {
            IParser parser = new Parser();
            var command = parser.ParseCommand("Twatty North");

            Assert.AreEqual(VerbCodes.NoCommand, command.Verb);
            Assert.AreEqual("north", command.Noun);
            Assert.AreEqual("twatty north", command.FullTextCommand);
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

        [TestMethod]
        public void ParseCommandReturnsValidCommandFor_Grab_Key_From_Floor()
        {
            IParser parser = new Parser();
            parser.Nouns.Add("key", "key");
            parser.Nouns.Add("floor", "floor");

            var command = parser.ParseCommand("Grab Key from floor");

            Assert.AreEqual(VerbCodes.Take, command.Verb);
            Assert.AreEqual("key", command.Noun);
            Assert.AreEqual(PropositionEnum.From, command.Preposition);
            Assert.AreEqual("floor", command.Noun2);
            Assert.AreEqual("grab key from floor", command.FullTextCommand);
        }

        [TestMethod]
        public void ParseCommandReturnsValidCommandFor_Grab_Key_From_Ground()
        {
            IParser parser = new Parser();
            parser.Nouns.Add("key", "key");
            parser.Nouns.Add("floor", "floor");
            parser.Nouns.Add("ground", "floor");

            var command = parser.ParseCommand("Grab Key from ground");

            Assert.AreEqual(VerbCodes.Take, command.Verb);
            Assert.AreEqual("key", command.Noun);
            Assert.AreEqual(PropositionEnum.From, command.Preposition);
            Assert.AreEqual("floor", command.Noun2);
            Assert.AreEqual("grab key from ground", command.FullTextCommand);
        }

        [TestMethod]
        public void ParseCommandReturnsValidCommandFor_Obtain_Key_From_Ground()
        {
            IParser parser = new Parser();
            parser.Nouns.Add("key", "key");
            parser.Nouns.Add("floor", "floor");
            parser.Nouns.Add("ground", "floor");

            var command = parser.ParseCommand("Obtain Key from ground");

            Assert.AreEqual(VerbCodes.Take, command.Verb);
            Assert.AreEqual("key", command.Noun);
            Assert.AreEqual(PropositionEnum.From, command.Preposition);
            Assert.AreEqual("floor", command.Noun2);
            Assert.AreEqual("obtain key from ground", command.FullTextCommand);
        }

        [TestMethod]
        public void ParseCommandReturnsValidCommandFor_Obtain_Key_From_Ground2()
        {
            IParser parser = new Parser();
            parser.Nouns.Add("key", "key");
            parser.Nouns.Add("floor", "floor");
            parser.Nouns.Add("ground", "floor");

            var command = parser.ParseCommand("Obtain uiouoiuiou Key iuouoiuio from wibblebum ground");

            Assert.AreEqual(VerbCodes.Take, command.Verb);
            Assert.AreEqual("key", command.Noun);
            Assert.AreEqual(PropositionEnum.From, command.Preposition);
            Assert.AreEqual("floor", command.Noun2);
            Assert.AreEqual("obtain uiouoiuiou key iuouoiuio from wibblebum ground", command.FullTextCommand);
        }

        [TestMethod]
        public void ParseCommandReturnsValidCommandFor_Examine()
        {
            IParser parser = new Parser();
            var command = parser.ParseCommand("Examine");

            Assert.AreEqual(VerbCodes.Look, command.Verb);
            Assert.AreEqual(string.Empty, command.Noun);
            Assert.AreEqual("examine", command.FullTextCommand);
        }

        [TestMethod]
        public void ParseCommandReturnsValidCommandFor_Gaze()
        {
            IParser parser = new Parser();
            var command = parser.ParseCommand("gaze");

            Assert.AreEqual(VerbCodes.Look, command.Verb);
            Assert.AreEqual(string.Empty, command.Noun);
            Assert.AreEqual("gaze", command.FullTextCommand);
        }

        [TestMethod]
        public void ParseCommandReturnsValidCommandFor_Use_Key_On_Door()
        {
            IParser parser = new Parser();
            parser.Nouns.Add("key", "key");
            parser.Nouns.Add("floor", "floor");
            parser.Nouns.Add("door", "door");

            var command = parser.ParseCommand("use key on door");

            Assert.AreEqual(VerbCodes.Use, command.Verb);
            Assert.AreEqual("key", command.Noun);
            Assert.AreEqual(PropositionEnum.On, command.Preposition);
            Assert.AreEqual("door", command.Noun2);
            Assert.AreEqual("use key on door", command.FullTextCommand);
        }

        [TestMethod]
        public void ProfanityFilterGetsTrippedForFullCommand()
        {
            IParser parser = new Parser();
         
            var command = parser.ParseCommand("2 girls 1 cup");
            Assert.IsTrue(command.ProfanityDetected);
            Assert.AreEqual("2 girls 1 cup", command.Profanity);
        }
        
        [TestMethod]
        public void ProfanityFilterGetsTrippedForWorkInPartialCommand()
        {
            IParser parser = new Parser();
         
            var command = parser.ParseCommand("flappy cunt bananna");
            Assert.IsTrue(command.ProfanityDetected);
            Assert.AreEqual("cunt", command.Profanity);
        }
        
        [TestMethod]
        public void ProfanityFilterNotTrippedForFullCommand()
        {
            IParser parser = new Parser();
         
            var command = parser.ParseCommand("fluffy clouds");
            Assert.IsFalse(command.ProfanityDetected);
            Assert.AreSame(string.Empty, command.Profanity);
        }
        
        [TestMethod]
        public void ProfanityFilterGetsTrippedForProfanePhraseInMiddleOfCommand()
        {
            IParser parser = new Parser();
         
            var command = parser.ParseCommand("watch 2 girls 1 cup on tv");
            Assert.IsTrue(command.ProfanityDetected);
            Assert.AreEqual("2 girls 1 cup", command.Profanity);
        }
        
        [TestMethod]
        public void ProfanityFilterDoesntDetectProfanityIfDisabled()
        {
            IParser parser = new Parser();
            parser.EnableProfanityFilter = false;
         
            var command = parser.ParseCommand("flappy cunt bananna");
            Assert.IsFalse(command.ProfanityDetected);
            Assert.AreEqual(string.Empty, command.Profanity);
        }
    }
}
