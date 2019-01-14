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

namespace TextualRealityExperienceEngine.GameEngine
{
    /// <summary>
    /// ConsoleEx offers the ability to write text to the console but with the added feature of word wrapping so that
    /// words are not split across multiple lines but instead wrapped correctly.
    ///
    /// This code is based on code from https://www.codeproject.com/Articles/51488/Implementing-Word-Wrap-in-C
    /// </summary>
    public static class ConsoleEx
    {
        /// <summary>
        /// Print a paragraph of text to the console but with word wrapping enabled so that
        /// words are not split across multiple lines but instead wrapped correctly.
        /// </summary>
        /// <param name="paragraph">Text to print to the console.</param>
        public static void WordWrap(string paragraph)
        {
            const string newline = "\r\n";
            int pos, next;
            var sb = new StringBuilder();

            // Parse each line of text
            for (pos = 0; pos < paragraph.Length; pos = next)
            {
                // Find end of line
                var eol = paragraph.IndexOf(newline, pos, StringComparison.CurrentCulture);

                if (eol == -1)
                {
                    next = eol = paragraph.Length;
                }
                else
                {
                    next = eol + newline.Length;
                }

                // Copy this line of text, breaking into smaller lines as needed
                if (eol > pos)
                {
                    do
                    {
                        var len = eol - pos;

                        if (len > Console.WindowWidth)
                        {
                            len = BreakLine(paragraph, pos, Console.WindowWidth);
                        }

                        sb.Append(paragraph, pos, len);
                        sb.Append(newline);

                        // Trim whitespace following break
                        pos += len;

                        while (pos < eol && Char.IsWhiteSpace(paragraph[pos]))
                        {
                            pos++;
                        }

                    } while (eol > pos);
                }
                else
                {
                    sb.Append(newline); // Empty line
                }
            }

            Console.WriteLine(sb);
        }

        private static int BreakLine(string text, int pos, int max)
        {
            // Find last whitespace in line
            var i = max - 1;

            while (i >= 0 && !Char.IsWhiteSpace(text[pos + i]))
            {
                i--;
            }

            if (i < 0)
            {
                return max;
            }

            while (i >= 0 && Char.IsWhiteSpace(text[pos + i]))
            {
                i--;
            }

            return i + 1;
        }
    }
}
