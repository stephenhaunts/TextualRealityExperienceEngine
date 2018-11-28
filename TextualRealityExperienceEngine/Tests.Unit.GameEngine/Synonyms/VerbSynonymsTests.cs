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
    public class VerbSynonymsTests
    {    
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddThrowsArgumentNullExceptionIsSynonymIsNull()
        {
            var verbSynonyms = new VerbSynonyms();
            verbSynonyms.Add("", VerbCodes.Go);
        }

        [TestMethod]
        public void AddCreatesSynonymMappingForANoun()
        {
            var verbSynonyms = new VerbSynonyms();
            verbSynonyms.Add("get", VerbCodes.Take);

            Assert.AreEqual(VerbCodes.Take, verbSynonyms.GetVerbforSynonum("get"));
        }

        [TestMethod]

        public void GetReturnsReturnsNoCommandForNonExistingSysnonym()
        {
            var verbSynonyms = new VerbSynonyms();

            Assert.AreEqual(VerbCodes.NoCommand, verbSynonyms.GetVerbforSynonum("flop"));
        }

        [TestMethod]
        public void MultipleSynonymsCanMapToTheSameVerb()
        {
            var verbSynonyms = new VerbSynonyms();
            verbSynonyms.Add("take", VerbCodes.Take);
            verbSynonyms.Add("get", VerbCodes.Take);

            Assert.AreEqual(VerbCodes.Take, verbSynonyms.GetVerbforSynonum("take"));
            Assert.AreEqual(VerbCodes.Take, verbSynonyms.GetVerbforSynonum("get"));
        }
    }
}
