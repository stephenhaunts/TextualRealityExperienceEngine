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

namespace TextualRealityExperienceEngine.Tests.Unit.GameEngine
{
    [TestClass]
    public class PlayerStatsTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "name")]
        public void AddThrowsArgumentNullExceptionIfNameIsNullOrEmpty()
        {
            IPlayerStats stats = new PlayerStats();
            stats.Add("", 0);
        }

        [TestMethod]
        public void AddInsertsObjectInGameState()
        {
            IPlayerStats stats = new PlayerStats();

            int testNumber = 8;
            stats.Add("test", testNumber);

            Assert.AreEqual(1, stats.Count());
            Assert.AreEqual(testNumber, (int)stats.Get("test"));
        }

        [TestMethod]
        public void ExistsReturnsTrueForObjectThatExists()
        {
            IPlayerStats stats = new PlayerStats();

            int testNumber = 8;
            stats.Add("test", testNumber);

            Assert.AreEqual(true, stats.Exists("test"));
        }

        [TestMethod]
        public void ExistsReturnsFalseForObjectThatDoesntExists()
        {
            IPlayerStats stats = new PlayerStats();

            Assert.AreEqual(false, stats.Exists("pizza"));
        }

        [TestMethod]
        public void GetReturnsObjectFromGlobalState()
        {
            IPlayerStats stats = new PlayerStats();

            int testNumber = 8;
            stats.Add("test", testNumber);

            Assert.AreEqual(testNumber, (int)stats.Get("test"));
            Assert.AreEqual(8, (int)stats.Get("test"));
        }

       [TestMethod]
        public void CountReturnsZeroForNewGlobalState()
        {
            IPlayerStats stats = new PlayerStats();

            Assert.AreEqual(0, stats.Count());
        }

        [TestMethod]
        public void CountReturnsCorrectNumberOfObjects()
        {
            IPlayerStats stats = new PlayerStats();

            stats.Add("test", 8);
            stats.Add("test2", 3);

            Assert.AreEqual(2, stats.Count());
        }

        [TestMethod]
        public void GetReturnsObjectFromGlobalStateForDifferentTypes()
        {
            IPlayerStats stats = new PlayerStats();

            stats.Add("test", 8);
            stats.Add("test2", 2);

            Assert.AreEqual(8, stats.Get("test"));
            Assert.AreEqual(2, stats.Get("test2"));
        }

        [TestMethod]
        public void ClearEmptiesInventory()
        {
            IPlayerStats stats = new PlayerStats();

            stats.Add("test", 8);
            stats.Add("test2", 2);

            Assert.AreEqual(2, stats.Count());

            stats.Clear();

            Assert.AreEqual(0, stats.Count());
        } 
    }
}
