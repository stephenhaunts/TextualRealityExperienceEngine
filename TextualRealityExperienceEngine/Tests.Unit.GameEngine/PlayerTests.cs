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
using System;

namespace TextualRealityExperienceEngine.Tests.Unit.GameEngine
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void ConstructorCreatesInventoryObject()
        {
            var player = new Player();
            Assert.IsNotNull(player.Inventory);
        }

        [TestMethod]
        public void DefaultConstructorCreatesEmptyName()
        {
            var player = new Player();
            Assert.IsNull(player.Name);
        }

        [TestMethod]
        public void ConstructorCreatesSetsName()
        {
            var player = new Player("Geoff");
            Assert.AreEqual("Geoff", player.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Name of player can not be null.")]
        public void ConstructorCreatesThrowsArgumentNullExceptionIfNameIsNull()
        {
            var player = new Player(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Name of player can not be null.")]
        public void ConstructorCreatesThrowsArgumentNullExceptionIfNameIsEmptyString()
        {
            var player = new Player(string.Empty);
        }
    }
}
