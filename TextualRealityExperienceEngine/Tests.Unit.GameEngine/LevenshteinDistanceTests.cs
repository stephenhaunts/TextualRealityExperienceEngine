/*
MIT License

Copyright(c) 2019 

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
    public class LevenshteinDistanceTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowArgumentNullExceptionIfSourceIsNull()
        {
            IStringDistance distance = new LevenshteinDistance();
            distance.Distance(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowArgumentNullExceptionIfTargetIsNull()
        {
            IStringDistance distance = new LevenshteinDistance();
            distance.Distance("name", null);
        }

        [TestMethod]
        public void CalculateDistanceOfKittenAndSitten()
        {
            IStringDistance distance = new LevenshteinDistance();
            Assert.AreEqual(3, distance.Distance("kitten", "sitting"));
        }

        [TestMethod]
        public void CalculateDistanceOfKittenAndSittenWithDifferentCases()
        {
            IStringDistance distance = new LevenshteinDistance();
            Assert.AreEqual(3, distance.Distance("Kitten", "siTTing"));
        }

        [TestMethod]
        public void CalculateDistanceOfGetAndGrtWithDifferentCases()
        {
            IStringDistance distance = new LevenshteinDistance();
            Assert.AreEqual(1, distance.Distance("Get", "Grt"));
        }

        [TestMethod]
        public void CalculateDistanceWithSourceLongerThanTarget()
        {
            IStringDistance distance = new LevenshteinDistance();
            int actual = distance.Distance("Stephen", "Steve");
            Assert.AreEqual(3, actual);
        }

        [TestMethod]
        public void CalculateDistanceWithSourceShorterThanTarget()
        {
            IStringDistance distance = new LevenshteinDistance();
            int actual = distance.Distance("Steve", "Stephen");
            Assert.AreEqual(3, actual);
        }

        [TestMethod]
        public void CalculateDistanceWithSourceShorterThanTargetAndTotallyUnrelated()
        {
            IStringDistance distance = new LevenshteinDistance();
            int actual = distance.Distance("Mat", "Geoffery");
            Assert.AreEqual(8, actual);
        }

        [TestMethod]
        public void CalculateDistanceWithSingleCharacters()
        {
            IStringDistance distance = new LevenshteinDistance();
            int actual = distance.Distance("a", "b");
            Assert.AreEqual(1, actual);
        }
    }
}
