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
        public void VisitedRoomsIsNotNullWhenCreatedWithDefaultConstructor()
        {
            var game = new Game();
            Assert.IsNotNull(game.VisitedRooms);
        }

        [TestMethod]
        public void VisitedRoomsIsEmptyWithDefaultConstructor()
        {
            var game = new Game();
            Assert.AreEqual(0, game.VisitedRooms.GetVisitedRooms().Count);
        }

        [TestMethod]
        public void PrologueIsEmptyWhenCreatedWithDefaultConstructor()
        {
            var game = new Game();
            Assert.AreEqual(string.Empty, game.Prologue);
        }

        [TestMethod]
        public void HelpTextIsEmptyWhenCreatedWithDefaultConstructor()
        {
            var game = new Game();
            Assert.AreEqual(string.Empty, game.HelpText);
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
        public void HelpTextIsSetByProperty()
        {
            var game = new Game();
            var help = "This is yoru help.";

            game.HelpText = help;

            Assert.AreEqual(help, game.HelpText);
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

        [TestMethod]
        public void ConstructorInitializesInventory()
        {
            var game = new Game();

            Assert.IsNotNull(game.Player.Inventory);
            Assert.AreEqual(0, game.Player.Inventory.Count());
        }
        
        [TestMethod]
        public void IncreaseScoreForEasyDifficultyUsesMultiplierOf1()
        {
            var game = new Game {Difficulty = DifficultyEnum.Easy};

            Assert.AreEqual(0, game.Score);          
            game.IncreaseScore(1);            
            Assert.AreEqual(1, game.Score);
        }
        
        [TestMethod]
        public void IncreaseScoreForMediumDifficultyUsesMultiplierOf2()
        {
            var game = new Game {Difficulty = DifficultyEnum.Medium};

            Assert.AreEqual(0, game.Score);           
            game.IncreaseScore(1);            
            Assert.AreEqual(2, game.Score);
        }
        
        [TestMethod]
        public void IncreaseScoreForHardDifficultyUsesMultiplierOf3()
        {
            var game = new Game {Difficulty = DifficultyEnum.Hard};

            Assert.AreEqual(0, game.Score);           
            game.IncreaseScore(1);           
            Assert.AreEqual(3, game.Score);
        }

        [TestMethod]
        public void HintCostReturns1ForEasyDifficulty()
        {
            var game = new Game {Difficulty = DifficultyEnum.Easy};
            Assert.AreEqual(1, game.HintCost);
        }
        
        [TestMethod]
        public void HintCostReturns3ForMediumDifficulty()
        {
            var game = new Game {Difficulty = DifficultyEnum.Medium};
            Assert.AreEqual(3, game.HintCost);
        }
        
        [TestMethod]
        public void HintCostReturns10ForHardDifficulty()
        {
            var game = new Game {Difficulty = DifficultyEnum.Hard};
            Assert.AreEqual(10, game.HintCost);
        }
    }
}
