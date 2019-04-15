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
    public class TextSubstituteTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddMacroThrowsArgumentNullExceptionIfMacroIdIsNull()
        {
            var substitute = new TextSubstitute();
            substitute.AddMacro("", "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddMacroThrowsArgumentNullExceptionIfTextIsNull()
        {
            var substitute = new TextSubstitute();
            substitute.AddMacro("$(name)", "");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void AddMacroThrowsFormatExceptionIfMacroIdIsWrongFormat()
        {
            var substitute = new TextSubstitute();
            substitute.AddMacro("w4t", "gfgfdsg");
        }

        [TestMethod]
        public void AddMacroAddsMacroToDictionary()
        {
            var substitute = new TextSubstitute();

            Assert.AreEqual(0, substitute.Count);
            substitute.AddMacro("$(macro)", "text to substitute.");
            Assert.AreEqual(1, substitute.Count);
            Assert.IsTrue(substitute.Exists("$(macro)"));
        }

        [TestMethod]
        public void AddMacroAddsMacroToDictionaryIsCaseInsensitive()
        {
            var substitute = new TextSubstitute();

            Assert.AreEqual(0, substitute.Count);
            substitute.AddMacro("$(MaCrO)", "text to substitute.");
            Assert.AreEqual(1, substitute.Count);
            Assert.IsTrue(substitute.Exists("$(macro)"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CheckMacroFormatThrowsArgumentNullExceptionIfMacroIdIsNull()
        {
            var substitute = new TextSubstitute();
            substitute.IsValidMacroFormat("");
        }

        [TestMethod]
        public void CheckMacroFormatReturnsTrueForValidMacro()
        {
            var substitute = new TextSubstitute();
            Assert.IsTrue(substitute.IsValidMacroFormat("$(macro)"));
        }

        [TestMethod]
        public void CheckMacroFormatReturnsTrueForEmptyValidMacro()
        {
            var substitute = new TextSubstitute();
            Assert.IsTrue(substitute.IsValidMacroFormat("$()"));
        }

        [TestMethod]
        public void CheckMacroFormatReturnsTrueForValidMacroWithSpaces()
        {
            var substitute = new TextSubstitute();
            Assert.IsTrue(substitute.IsValidMacroFormat("$(        )"));
        }

        [TestMethod]
        public void CheckMacroFormatReturnsTrueForValidMacroWithLeadingSpaces()
        {
            var substitute = new TextSubstitute();
            Assert.IsTrue(substitute.IsValidMacroFormat("        $(macro)"));
        }

        [TestMethod]
        public void CheckMacroFormatReturnsTrueForValidMacroWithTrailingSpaces()
        {
            var substitute = new TextSubstitute();
            Assert.IsTrue(substitute.IsValidMacroFormat("$(macro)     "));
        }

        [TestMethod]
        public void CheckMacroFormatReturnsFalseForMacroWithNoTerminator()
        {
            var substitute = new TextSubstitute();
            Assert.IsFalse(substitute.IsValidMacroFormat("$(macro"));
        }

        [TestMethod]
        public void CheckMacroFormatReturnsFalseForMacroWithNoStartCharacters()
        {
            var substitute = new TextSubstitute();
            Assert.IsFalse(substitute.IsValidMacroFormat("macro)"));
        }

        [TestMethod]
        public void CheckMacroFormatReturnsFalseForMacroWithMissingDollar()
        {
            var substitute = new TextSubstitute();
            Assert.IsFalse(substitute.IsValidMacroFormat("(macro)"));
        }

        [TestMethod]
        public void CheckMacroFormatReturnsFalseForMacroWithMissingOpeningBracket()
        {
            var substitute = new TextSubstitute();
            Assert.IsFalse(substitute.IsValidMacroFormat("$macro)"));
        }

        [TestMethod]
        public void CheckMacroFormatReturnsFalseForMacroWithJunkBeforeMacroName()
        {
            var substitute = new TextSubstitute();
            Assert.IsFalse(substitute.IsValidMacroFormat("blah blah $(macro)"));
        }

        [TestMethod]
        public void CheckMacroFormatReturnsFalseForMacroWithJunkAFterMacroName()
        {
            var substitute = new TextSubstitute();
            Assert.IsFalse(substitute.IsValidMacroFormat("$(macro) blah"));
        }

        [TestMethod]
        public void PerformSubstitutionReturnsEmptyStringWhenSourceIsEmpty()
        {
            var substitute = new TextSubstitute();

            substitute.AddMacro("$(number)", "one");
            var result = substitute.PerformSubstitution("");

            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void PerformSubstitutionReplacesSimpleSingleMacro()
        {
            var substitute = new TextSubstitute();

            substitute.AddMacro("$(number)", "one");
            var result = substitute.PerformSubstitution("My favourite number is $(number).");

            Assert.AreEqual("My favourite number is one.", result);
        }

        [TestMethod]
        public void PerformSubstitutionReplacesSimpleDualMacro()
        {
            var substitute = new TextSubstitute();

            substitute.AddMacro("$(number)", "one");
            substitute.AddMacro("$(number2)", "two");

            var result = substitute.PerformSubstitution("My favourite number is $(number) and $(number2).");

            Assert.AreEqual("My favourite number is one and two.", result);
        }

        [TestMethod]
        public void PerformSubstitutionReplacesSingleNestedlMacro()
        {
            var substitute = new TextSubstitute();

            substitute.AddMacro("$(name)", "My name is $(steve)");

            substitute.AddMacro("$(steve)", "Stephen Haunts");

            var result = substitute.PerformSubstitution("Hello, $(name).");

            Assert.AreEqual("Hello, My name is Stephen Haunts.", result);
        }

        [TestMethod]
        public void PerformSubstitutionReplacesMultipleNestedlMacros()
        {
            var substitute = new TextSubstitute();

            substitute.AddMacro("$(id)", "My name is $(steve) $(haunts)");

            substitute.AddMacro("$(steve)", "Stephen");
            substitute.AddMacro("$(haunts)", "Haunts");


            var result = substitute.PerformSubstitution("Hello, $(id).");

            Assert.AreEqual("Hello, My name is Stephen Haunts.", result);
        }


        [TestMethod]
        public void PerformSubstitutionReplacesMultipleLayerNestedlMacros()
        {
            var substitute = new TextSubstitute();

            substitute.AddMacro("$(id)", "My name is $(name)");
            substitute.AddMacro("$(name)", "$(steve) $(haunts)");
            substitute.AddMacro("$(steve)", "Stephen");
            substitute.AddMacro("$(haunts)", "Haunts");

            var result = substitute.PerformSubstitution("Hello, $(id).");

            Assert.AreEqual("Hello, My name is Stephen Haunts.", result);
        }

        [TestMethod]
        public void PerformSubstitutionReplacesMultipleLayerNestedlMacros2()
        {
            var substitute = new TextSubstitute();
            substitute.AddMacro("$(gender)", "Mr");
            substitute.AddMacro("$(haunts)", "Haunts");

            substitute.AddMacro("$(id)", "My name is $(name)");
            substitute.AddMacro("$(name)", "$(gender) $(steve) $(haunts)");
            substitute.AddMacro("$(steve)", "Stephen");

            var result = substitute.PerformSubstitution("Hello, $(id).");

            Assert.AreEqual("Hello, My name is Mr Stephen Haunts.", result);
        }
    }
}
