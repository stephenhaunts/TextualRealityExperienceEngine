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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextualRealityExperienceEngine.GameEngine;
using TextualRealityExperienceEngine.GameEngine.Interfaces;
using TextualRealityExperienceEngine.GameEngine.TextProcessing;
using TextualRealityExperienceEngine.GameEngine.TextProcessing.Interfaces;
using TextualRealityExperienceEngine.GameEngine.TextProcessing.Synonyms;

namespace TextualRealityExperienceEngine.Tests.Unit.GameEngine
{
    [TestClass]
    public class CommandQueueTests
    {
        [TestMethod]
        public void CommandQueueConstructorCreatesEmptyQueue()
        {
            ICommandQueue commandQueue = new CommandQueue();
            var queue = commandQueue.Commands;

            Assert.AreEqual(0, queue.Count);
        }

        [TestMethod]
        public void AddCommandAddsCommandsToQueue()
        {
            ICommandQueue commandQueue = new CommandQueue();

            ICommand command1 = new Command
            {
                Verb = VerbCodes.Go,
                Noun = "west",
                FullTextCommand = "walk west"
            };

            ICommand command2 = new Command
            {
                Verb = VerbCodes.Go,
                Noun = "north",
                FullTextCommand = "flop forwqard"
            };

            ICommand command3 = new Command
            {
                Verb = VerbCodes.Go,
                Noun = "east",
                FullTextCommand = "scuffle east"
            };

            commandQueue.AddCommand(command1);
            commandQueue.AddCommand(command2);
            commandQueue.AddCommand(command3);

            var queue = commandQueue.Commands;

            Assert.AreEqual(3, queue.Count);

            Assert.AreEqual(command1, queue[0]);
            Assert.AreEqual(command2, queue[1]);
            Assert.AreEqual(command3, queue[2]);
        }
    }
}
