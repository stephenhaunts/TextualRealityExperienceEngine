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

namespace TextualRealityExperienceEngine.Tests.Unit.GameEngine
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void StartRoomIsNullWhenCreatedWithDefaultConstructor()
        {
            var game = new Game();
            Assert.IsNull(game.StartRoom);
        }

        [TestMethod]
        public void PrologueIsEmptyWhenCreatedWithDefaultConstructor()
        {
            var game = new Game();
            Assert.AreEqual(string.Empty, game.Prologue);
        }

        [TestMethod]
        public void StartRoomIsSetByProperty()
        {
            var game = new Game();
            var room = new Room(game);
            game.StartRoom = room;

            Assert.AreEqual(room, game.StartRoom);
        }

        [TestMethod]
        public void CurrentRoomIsSetByProperty()
        {
            var game = new Game();
            var room = new Room(game);
            game.CurrentRoom = room;

            Assert.AreEqual(room, game.CurrentRoom);
        }

        [TestMethod]
        public void PrologueIsSetByProperty()
        {
            var game = new Game();
            var prologue = "This is the default prologue.";

            game.Prologue = prologue;

            Assert.AreEqual(prologue, game.Prologue);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "The prologue can not be empty.")]
        public void OveriddenConstructorThrowsArgumentNullExcpetionIfProgueIsNull()
        {
            var game = new Game("",null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "The initial room state can not be null.")]
        public void OveriddenConstructorThrowsArgumentNullExcpetionIfStartRoomIsNull()
        {
            var game = new Game("This is a progue", null);
        }

        [TestMethod]      
        public void OveriddenConstructorSetsPrologue()
        {
            var startRoom = new Room();
            var prologue = "This is a progue";
            var game = new Game(prologue, startRoom);

            Assert.AreEqual(prologue, game.Prologue);
        }

        [TestMethod]
        public void OveriddenConstructorSetsStartRoom()
        {
    
            var startRoom = new Room();
            var prologue = "This is a progue";
            var game = new Game(prologue, startRoom);

            Assert.AreEqual(startRoom, game.StartRoom);
        }

        [TestMethod]
        public void OveriddenConstructorSetsCurrentRoom()
        {
            var startRoom = new Room();
            var prologue = "This is a progue";
            var game = new Game(prologue, startRoom);

            Assert.AreEqual(startRoom, game.CurrentRoom);
        }

        [TestMethod]
        public void NumberOfMovesInitializedToZero()
        {
            var game = new Game();

            Assert.AreEqual(0, game.NumberOfMoves);
        }

        [TestMethod]
        public void ScoreInitializedToZero()
        {
            var game = new Game();

            Assert.AreEqual(0, game.Score);
        }
    }
}
