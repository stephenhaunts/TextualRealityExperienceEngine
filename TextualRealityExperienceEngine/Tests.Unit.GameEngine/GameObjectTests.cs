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
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextualRealityExperienceEngine.GameEngine;
using TextualRealityExperienceEngine.GameEngine.Interfaces;

namespace TextualRealityExperienceEngine.Tests.Unit.GameEngine
{
    [TestClass]
    public class GameObjectTests
    {
        [TestMethod]
        public void ConstructorSetsNameTest()
        {
            IObject gameObject = new GameObject("objectName", "object description", "pickup message");

            Assert.AreEqual("objectName", gameObject.Name);
        }

        [TestMethod]
        public void ConstructorSetsDescriptionTest()
        {
            IObject gameObject = new GameObject("objectName", "object description", "pickup message");

            Assert.AreEqual("object description", gameObject.Description);
        }

        [TestMethod]
        public void ConstructorSetsPickupMessageTest()
        {
            IObject gameObject = new GameObject("objectName", "object description", "pickup message");

            Assert.AreEqual("pickup message", gameObject.PickUpMessage);
        }

        [TestMethod]
        public void ExtendedConstructorSetsNameTest()
        {
            IObject gameObject = new GameObject("objectName", "object description", "pickup message",
                true, 100,"health", "you have eaten the cheese");

            Assert.AreEqual("objectName", gameObject.Name);
        }

        [TestMethod]
        public void ExtendedConstructorSetsDescriptionTest()
        {
            IObject gameObject = new GameObject("objectName", "object description", "pickup message",
                true, 100, "health", "you have eaten the cheese");

            Assert.AreEqual("object description", gameObject.Description);
        }

        [TestMethod]
        public void ExtendedConstructorSetsPickupMessageTest()
        {
            IObject gameObject = new GameObject("objectName", "object description", "pickup message",
                true, 100, "health", "you have eaten the cheese");

            Assert.AreEqual("pickup message", gameObject.PickUpMessage);
        }

        [TestMethod]
        public void ExtendedConstructorSetsEatenFlagTest()
        {
            IObject gameObject = new GameObject("objectName", "object description", "pickup message",
                true, 100, "health", "you have eaten the cheese");

            Assert.IsTrue(gameObject.Edible);
        }

        [TestMethod]
        public void ExtendedConstructorSetsPointsToApplyTest()
        {
            IObject gameObject = new GameObject("objectName", "object description", "pickup message",
                true, 100, "health", "you have eaten the cheese");

            Assert.AreEqual(100, gameObject.StatsAdjustment[0].PointsToApply);
        }

        [TestMethod]
        public void ExtendedConstructorSetsStatToModifyTest()
        {
            IObject gameObject = new GameObject("objectName", "object description", "pickup message",
                true, 100, "health", "you have eaten the cheese");

            Assert.AreEqual("health", gameObject.StatsAdjustment[0].StatToModify);
        }

        [TestMethod]
        public void ExtendedConstructorSetsEatenMessageTest()
        {
            IObject gameObject = new GameObject("objectName", "object description", "pickup message",
                true, 100, "health", "you have eaten the cheese");

            Assert.AreEqual("you have eaten the cheese", gameObject.EatenMessage);
        }

        [TestMethod]
        public void SecondConstructorSetsNameTest()
        {
            List<PlayerStatsAdjustments> stats = new List<PlayerStatsAdjustments>();
            PlayerStatsAdjustments health = new PlayerStatsAdjustments("health", 10);
            PlayerStatsAdjustments skill = new PlayerStatsAdjustments("skill", 5);
            stats.Add(health);
            stats.Add(skill);

            IObject gameObject = new GameObject("objectName", "object description", "pickup message",
                true, stats, "you have eaten the cheese");

            Assert.AreEqual("objectName", gameObject.Name);
        }

        [TestMethod]
        public void SecondConstructorSetsDescriptionTest()
        {
            List<PlayerStatsAdjustments> stats = new List<PlayerStatsAdjustments>();
            PlayerStatsAdjustments health = new PlayerStatsAdjustments("health", 10);
            PlayerStatsAdjustments skill = new PlayerStatsAdjustments("skill", 5);
            stats.Add(health);
            stats.Add(skill);

            IObject gameObject = new GameObject("objectName", "object description", "pickup message",
                true, stats, "you have eaten the cheese");

            Assert.AreEqual("object description", gameObject.Description);
        }

        [TestMethod]
        public void SecondConstructorSetsPickupMessageTest()
        {
            List<PlayerStatsAdjustments> stats = new List<PlayerStatsAdjustments>();
            PlayerStatsAdjustments health = new PlayerStatsAdjustments("health", 10);
            PlayerStatsAdjustments skill = new PlayerStatsAdjustments("skill", 5);
            stats.Add(health);
            stats.Add(skill);

            IObject gameObject = new GameObject("objectName", "object description", "pickup message",
                true, stats, "you have eaten the cheese");

            Assert.AreEqual("pickup message", gameObject.PickUpMessage);
        }

        [TestMethod]
        public void SecondConstructorSetsEatenFlagTest()
        {
            List<PlayerStatsAdjustments> stats = new List<PlayerStatsAdjustments>();
            PlayerStatsAdjustments health = new PlayerStatsAdjustments("health", 10);
            PlayerStatsAdjustments skill = new PlayerStatsAdjustments("skill", 5);
            stats.Add(health);
            stats.Add(skill);

            IObject gameObject = new GameObject("objectName", "object description", "pickup message",
                true, stats, "you have eaten the cheese");

            Assert.IsTrue(gameObject.Edible);
        }


        [TestMethod]
        public void SecondExtendedConstructorStatsToApplyTest()
        {
            List<PlayerStatsAdjustments> stats = new List<PlayerStatsAdjustments>();
            PlayerStatsAdjustments health = new PlayerStatsAdjustments("health", 10);
            PlayerStatsAdjustments skill = new PlayerStatsAdjustments("skill", 5);
            stats.Add(health);
            stats.Add(skill);

            IObject gameObject = new GameObject("objectName", "object description", "pickup message",
                true, stats, "you have eaten the cheese");

            Assert.AreEqual(10, gameObject.StatsAdjustment[0].PointsToApply);
            Assert.AreEqual("health", gameObject.StatsAdjustment[0].StatToModify);

            Assert.AreEqual(5, gameObject.StatsAdjustment[1].PointsToApply);
            Assert.AreEqual("skill", gameObject.StatsAdjustment[1].StatToModify);

        }
    }
}
