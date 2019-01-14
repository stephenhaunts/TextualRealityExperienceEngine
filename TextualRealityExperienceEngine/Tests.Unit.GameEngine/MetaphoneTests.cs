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

namespace TextualRealityExperienceEngine.Tests.Unit.GameEngine
{
    [TestClass]
    public class MetaphoneTests
    {
        [TestMethod]
        public void GenerateMetaphoneCodeForStephenAndSteven()
        {
            IPhoneticMatch phonetic = new Metaphone();
            var token1 = phonetic.CreateToken("Stephen");
            var token2 = phonetic.CreateToken("Steven");
 
            Assert.AreEqual(token1, token2);
        }
 
        [TestMethod]
        public void GenerateMetaphoneCodeForStephenAndSteffan()
        {
            IPhoneticMatch phonetic = new Metaphone();
            var token1 = phonetic.CreateToken("Stephen");
            var token2 = phonetic.CreateToken("Steffan");
 
            Assert.AreEqual(token1, token2);
        }
 
        [TestMethod]
        public void GenerateMetaphoneCodeForStephenAndGeorge()
        {
            IPhoneticMatch phonetic = new Metaphone();
            var token1 = phonetic.CreateToken("Stephen");
            var token2 = phonetic.CreateToken("George");
 
            Assert.AreNotEqual(token1, token2);
        }
    }
}