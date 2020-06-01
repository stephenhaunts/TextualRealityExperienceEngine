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
using TextualRealityExperienceEngine.GameEngine.Utilities;

namespace TextualRealityExperienceEngine.Tests.Unit.Utilities
{
    [TestClass]
    public class ContentManagementTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddContentItemThrowsArgumentNullExceptionIfIdentifierNull()
        {
            var content = new ContentManagement();
            content.AddContentItem("", "");
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddContentItemThrowsArgumentNullExceptionIfContentNull()
        {
            var content = new ContentManagement();
            content.AddContentItem("MyId", "");
        }
        
        [TestMethod]        
        public void AddContentItemAddsContentToCms()
        {
            var content = new ContentManagement();
            content.AddContentItem("MyId", "This is a string");
            
            Assert.AreEqual(1, content.CountContentItems);
            Assert.AreEqual("This is a string", content.RetrieveContentItem("MyId"));
        }
        
        [TestMethod]        
        public void AddContentItemAddsMultipleContentItemsToCms()
        {
            var content = new ContentManagement();
            content.AddContentItem("MyId", "This is a string");
            content.AddContentItem("MyId2", "This is a string2");
            
            Assert.AreEqual(2, content.CountContentItems);
            Assert.AreEqual("This is a string", content.RetrieveContentItem("MyId"));
            Assert.AreEqual("This is a string2", content.RetrieveContentItem("MyId2"));
        }
        
        [TestMethod]  
        [ExpectedException(typeof(ArgumentException))]
        public void AddContentItemAddsMultipleWithSameContentId()
        {
            var content = new ContentManagement();
            content.AddContentItem("MyId", "This is a string");
            content.AddContentItem("MyId", "This is a string2");                        
        }
        
        [TestMethod]        
        public void RetrieveContentItemGetsItemsFromCms()
        {
            var content = new ContentManagement();
            content.AddContentItem("MyId", "This is a string");
            content.AddContentItem("MyId2", "This is a string2");
            
            Assert.AreEqual(2, content.CountContentItems);
            Assert.AreEqual("This is a string", content.RetrieveContentItem("MyId"));
            Assert.AreEqual("This is a string2", content.RetrieveContentItem("MyId2"));
        }
        
        [TestMethod]        
        public void ExistsReturnsTrueForItemsThatExist()
        {
            var content = new ContentManagement();
            content.AddContentItem("MyId", "This is a string");
            content.AddContentItem("MyId2", "This is a string2");
            
            Assert.AreEqual(2, content.CountContentItems);
            Assert.IsTrue(content.Exists("MyId"));
            Assert.IsTrue(content.Exists("MyId2"));            
        }
        
        [TestMethod]        
        public void ExistsReturnsFalseForItemsThatDoNotExist()
        {
            var content = new ContentManagement();
            content.AddContentItem("MyId", "This is a string");
            content.AddContentItem("MyId2", "This is a string2");
            
            Assert.AreEqual(2, content.CountContentItems);
            Assert.IsFalse(content.Exists("not exist"));
            Assert.IsFalse(content.Exists("not exist 2"));            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddCompressedContentItemThrowsArgumentNullExceptionIfIdentifierNull()
        {
            var content = new ContentManagement(true);
            content.AddContentItem("", "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddCompressedContentItemThrowsArgumentNullExceptionIfContentNull()
        {
            var content = new ContentManagement(true);
            content.AddContentItem("MyId", "");
        }

        [TestMethod]
        public void AddCompressedContentItemAddsContentToCms()
        {
            var content = new ContentManagement(true);
            content.AddContentItem("MyId", "This is a string");

            Assert.AreEqual(1, content.CountContentItems);
            Assert.AreEqual("This is a string", content.RetrieveContentItem("MyId"));
        }

        [TestMethod]
        public void AddCompressedContentItemAddsMultipleContentItemsToCms()
        {
            var content = new ContentManagement(true);
            content.AddContentItem("MyId", "This is a string");
            content.AddContentItem("MyId2", "This is a string2");

            Assert.AreEqual(2, content.CountContentItems);
            Assert.AreEqual("This is a string", content.RetrieveContentItem("MyId"));
            Assert.AreEqual("This is a string2", content.RetrieveContentItem("MyId2"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddCompressedContentItemAddsMultipleWithSameContentId()
        {
            var content = new ContentManagement(true);
            content.AddContentItem("MyId", "This is a string");
            content.AddContentItem("MyId", "This is a string2");
        }

        [TestMethod]
        public void RetrieveCompressedContentItemGetsItemsFromCms()
        {
            var content = new ContentManagement(true);
            content.AddContentItem("MyId", "This is a string");
            content.AddContentItem("MyId2", "This is a string2");

            Assert.AreEqual(2, content.CountContentItems);
            Assert.AreEqual("This is a string", content.RetrieveContentItem("MyId"));
            Assert.AreEqual("This is a string2", content.RetrieveContentItem("MyId2"));
        }

        [TestMethod]
        public void ExistsCompressedReturnsTrueForItemsThatExist()
        {
            var content = new ContentManagement(true);
            content.AddContentItem("MyId", "This is a string");
            content.AddContentItem("MyId2", "This is a string2");

            Assert.AreEqual(2, content.CountContentItems);
            Assert.IsTrue(content.Exists("MyId"));
            Assert.IsTrue(content.Exists("MyId2"));
        }

        [TestMethod]
        public void ExistsCompressedReturnsFalseForItemsThatDoNotExist()
        {
            var content = new ContentManagement(true);
            content.AddContentItem("MyId", "This is a string");
            content.AddContentItem("MyId2", "This is a string2");

            Assert.AreEqual(2, content.CountContentItems);
            Assert.IsFalse(content.Exists("not exist"));
            Assert.IsFalse(content.Exists("not exist 2"));
        }
    }
}
