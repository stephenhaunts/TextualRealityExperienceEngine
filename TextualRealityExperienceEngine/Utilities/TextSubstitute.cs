/*MIT License

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
using System.Collections.Generic;
using TextualRealityExperienceEngine.GameEngine.Utilities.Interfaces;

namespace TextualRealityExperienceEngine.GameEngine.Utilities
{
    /// <summary>
    /// Text substitute.
    /// </summary>
    public class TextSubstitute : ITextSubstitute
    {
        private const int RECURSION_GUARD = 10;
        private readonly Dictionary<string, string> _macros = new Dictionary<string, string>();

        /// <summary>
        /// Adds the macro.
        /// </summary>
        /// <param name="macroId">Macro identifier.</param>
        /// <param name="text">Text.</param>
        public void AddMacro(string macroId, string text)
        {
            if (string.IsNullOrEmpty(macroId))
            {
                throw new ArgumentNullException(nameof(macroId));
            }

            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException(nameof(text));
            }

            macroId = macroId.ToLower();

            if (!IsValidMacroFormat(macroId))
            {
                throw new FormatException(nameof(macroId));
            }

            _macros.Add(macroId, text);
        }

        /// <summary>
        /// Performs the substitution.
        /// </summary>
        /// <returns>The substitution.</returns>
        /// <param name="sourceText">Source text.</param>
        public string PerformSubstitution(string sourceText)
        {
            if (string.IsNullOrEmpty(sourceText))
            {
                return string.Empty;
            }

            sourceText = PerformSubstitution(sourceText, 0);

            return sourceText;
        }

        private string PerformSubstitution(string sourceText, int recursionCounter)
        {
            int detectedOccurances = 0;
            int counter = recursionCounter;

            if (recursionCounter == RECURSION_GUARD)
            {
                return sourceText;
            }

            foreach (var macro in _macros)
            {
                if (sourceText.Contains(macro.Key))
                {
                    detectedOccurances++;
                    sourceText = sourceText.Replace(macro.Key, macro.Value);
                }
            }

            if (detectedOccurances > 0)
            {
                counter++;
                sourceText = PerformSubstitution(sourceText, counter);
            }

            return sourceText;
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
        public int Count
        {
            get
            {
                return _macros.Count;
            }
        }

        /// <summary>
        /// Exists the specified macroId.
        /// </summary>
        /// <returns>The exists.</returns>
        /// <param name="macroId">Macro identifier.</param>
        public bool Exists(string macroId)
        {
            if (string.IsNullOrEmpty(macroId))
            {
                throw new ArgumentNullException(nameof(macroId));
            }

            return _macros.ContainsKey(macroId);

        }

        /// <summary>
        /// Ises the valid macro format.
        /// </summary>
        /// <returns><c>true</c>, if valid macro format was ised, <c>false</c> otherwise.</returns>
        /// <param name="macroId">Macro identifier.</param>
        public bool IsValidMacroFormat(string macroId)
        {
            if (string.IsNullOrEmpty(macroId))
            {
                throw new ArgumentNullException(nameof(macroId));
            }

            macroId = macroId.TrimStart(' ');
            macroId = macroId.TrimEnd(' ');

            if (macroId.StartsWith("$(", StringComparison.Ordinal))
            {
                if (macroId.EndsWith(")", StringComparison.Ordinal))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
