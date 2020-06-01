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
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextualRealityExperienceEngine.GameEngine.Utilities;
using TextualRealityExperienceEngine.GameEngine.Utilities.Interfaces;

namespace TextualRealityExperienceEngine.Tests.Unit.Utilities
{
    [TestClass]
    public class GZipCompressionTests
    {
        [TestMethod]
        public void HappyPathTestForCompressingAndDecompressingText()
        {
            string test = "Mary had a little lamb, it's fleece was as white as snow.";
            test += test;
            test += test;
            test += test;

            IGZipCompression compress = new GZipCompression();

            var compressed = compress.Compress(test);
            var decompressed = compress.Decompress(compressed);

            Assert.AreEqual(test, decompressed);
            Assert.IsTrue(compressed.Length <= test.Length);
        }

        [TestMethod]
        public void HappyPathTestForCompressingAndDecompressingByteArrays()
        {
            string test = "Mary had a little lamb, it's fleece was as white as snow.";
            test += test;
            test += test;
            test += test;

            IGZipCompression compress = new GZipCompression();

            var compressed = compress.CompressBytes(Encoding.UTF8.GetBytes(test));
            var decompressed = compress.DecompressBytes(compressed);

            var original = Encoding.UTF8.GetBytes(test);
            Assert.IsTrue(Compare(original, decompressed));
            Assert.IsTrue(compressed.Length <= test.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CompressStringThrowsArgumentNullExceptionIfInputIsNull()
        {
            IGZipCompression compress = new GZipCompression();
            compress.Compress("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeCompressStringThrowsArgumentNullExceptionIfInputIsNull()
        {
            IGZipCompression compress = new GZipCompression();
            compress.Decompress("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CompressByteArrayThrowsArgumentNullExceptionIfInputIsNull()
        {
            IGZipCompression compress = new GZipCompression();
            compress.Compress(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DecompressByteArrauThrowsArgumentNullExceptionIfInputIsNull()
        {
            IGZipCompression compress = new GZipCompression();
            compress.Decompress(null);
        }

        private static bool Compare(byte[] array1, byte[] array2)
        {
            var result = array1.Length == array2.Length;

            for (var i = 0; i < array1.Length && i < array2.Length; ++i)
            {
                result &= array1[i] == array2[i];
            }

            return result;
        }

    }
}
