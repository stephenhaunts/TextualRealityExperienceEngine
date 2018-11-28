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
using TextualRealityExperienceEngine.GameEngine.Synonyms;

namespace TextualRealityExperienceEngine.Tests.Unit.GameEngine
{
    [TestClass]
    public class NounSynonymsTests
    {    
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddThrowsArgumentNullExceptionIsSynonymIsNull()
        {
            var nounSynonyms = new NounSynonyms();
            nounSynonyms.Add("", "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddThrowsArgumentNullExceptionIsNounIsNull()
        {
            var nounSynonyms = new NounSynonyms();
            nounSynonyms.Add("test", "");
        }

        [TestMethod]
        public void AddCreatesSynonymMappingForANoun()
        {
            var nounSynonyms = new NounSynonyms();
            nounSynonyms.Add("golden key", "key");

            Assert.AreEqual("key", nounSynonyms.GetNounforSynonum("golden key"));
        }

        [TestMethod]
        public void GetReturnsEmptyStringForNonExistingSysnonym()
        {
            var nounSynonyms = new NounSynonyms();

            Assert.AreEqual(string.Empty, nounSynonyms.GetNounforSynonum("golden key"));
        }

        [TestMethod]
        public void MultipleSynonymsCanMapToTheSameNoun()
        {
            var nounSynonyms = new NounSynonyms();
            nounSynonyms.Add("golden key", "key");
            nounSynonyms.Add("rusty key", "key");

            Assert.AreEqual("key", nounSynonyms.GetNounforSynonum("golden key"));
            Assert.AreEqual("key", nounSynonyms.GetNounforSynonum("rusty key"));
        }
    }
}
