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
    public class GlobalStateTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "name")]
        public void AddThrowsArgumentNullExceptionIfNameIsNullOrEmpty()
        {
            IGlobalState state = new GlobalState();
            state.Add("", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "name")]
        public void AddThrowsArgumentNullExceptionIfObjectIsNull()
        {
            IGlobalState state = new GlobalState();
            state.Add("pizza", null);
        }

        [TestMethod]
        public void AddInsertsObjectInGameState()
        {
            IGlobalState state = new GlobalState();

            int testNumber = 8;
            state.Add("test", testNumber);

            Assert.AreEqual(1, state.Count());
            Assert.AreEqual(testNumber, (int)state.Get("test"));
        }

        [TestMethod]
        public void ExistsReturnsTrueForObjectThatExists()
        {
            IGlobalState state = new GlobalState();

            int testNumber = 8;
            state.Add("test", testNumber);

            Assert.AreEqual(true, state.Exists("test"));
        }

        [TestMethod]
        public void ExistsReturnsFalseForObjectThatDoesntExists()
        {
            IGlobalState state = new GlobalState();

            Assert.AreEqual(false, state.Exists("pizza"));
        }

        [TestMethod]
        public void GetReturnsObjectFromGlobalState()
        {
            IGlobalState state = new GlobalState();

            int testNumber = 8;
            state.Add("test", testNumber);

            Assert.AreEqual(testNumber, (int)state.Get("test"));
            Assert.AreEqual(8, (int)state.Get("test"));
        }

        [TestMethod]
        public void CountReturnsZeroForNewGlobalState()
        {
            IGlobalState state = new GlobalState();

            Assert.AreEqual(0, state.Count());
        }

        [TestMethod]
        public void CountReturnsCorrectNumberOfObjects()
        {
            IGlobalState state = new GlobalState();

            state.Add("test", 8);
            state.Add("test2", "Steve");

            Assert.AreEqual(2, state.Count());
        }

        [TestMethod]
        public void GetReturnsObjectFromGlobalStateForDifferentTypes()
        {
            IGlobalState state = new GlobalState();

            state.Add("test", 8);
            state.Add("test2", "Steve");

            Assert.AreEqual(8, (int)state.Get("test"));
            Assert.AreEqual("Steve", (string)state.Get("test2"));
        }

        [TestMethod]
        public void ClearEmptiesGlobalState()
        {
            IGlobalState state = new GlobalState();

            state.Add("test", 8);
            state.Add("test2", "Steve");

            Assert.AreEqual(2, state.Count());

            state.Clear();

            Assert.AreEqual(0, state.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "name")]
        public void UpdateThrowsArgumentNullExceptionIfNameIsNullOrEmpty()
        {
            IGlobalState state = new GlobalState();
            state.Update("", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "name")]
        public void UpdateThrowsArgumentNullExceptionIfObjectIsNull()
        {
            IGlobalState state = new GlobalState();
            state.Update("pizza", null);
        }

        [TestMethod]
        public void UpdateValueInStateObject()
        {
            IGlobalState state = new GlobalState();

            state.Add("counter", 0);

            Assert.AreEqual(0, (int)state.Get("counter"));

            var counter = (int)state.Get("counter");
            counter++;
            state.Update("counter", counter);

            Assert.AreEqual(1, (int)state.Get("counter"));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void UpdateThrowsInvalidOperationExceptionIfStateObjectDoesntExist()
        {
            IGlobalState state = new GlobalState();
            state.Update("counter", 0);
        }
    }
}
