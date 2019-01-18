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
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using TextualRealityExperienceEngine.GameEngine.Interfaces;

namespace TextualRealityExperienceEngine.GameEngine
{
    /// <summary>
    /// The metaphone class is used to generate a phonetic token for an input string. This will be used to try and detect
    /// spelling mistakes.
    /// </summary>
    public class Metaphone : IPhoneticMatch
    {
        private static readonly Regex NonLetters = new Regex("[^A-Z]", RegexOptions.Compiled);
        private static readonly Regex Vowels = new Regex("[AEIOU]", RegexOptions.Compiled);
        private static readonly Regex Frontv = new Regex("[EIY]", RegexOptions.Compiled);
        private static readonly Regex Varson = new Regex("[CSPTG]", RegexOptions.Compiled);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public string CreateToken(string word)
        {
            var strippedNonLetters = new StringBuilder(NonLetters.Replace(word.ToUpper(), ""));
            var metaphoneKey = new StringBuilder();

            if (strippedNonLetters.Length >= 2) strippedNonLetters.ToString().Substring(0, 2);

            if (strippedNonLetters[0] == 'X') strippedNonLetters[0] = 'S';

            var length = strippedNonLetters.Length;

            for (var i = 0; i < length; i++)
            {
                var c = strippedNonLetters[i];
                if (c != 'C' && i > 0 && strippedNonLetters[i - 1] == c) continue;

                if (ProcessVowelIfAtBeginning(c, i, metaphoneKey)) continue;

                ProcessCharactersInWord(c, i, length, strippedNonLetters, metaphoneKey);
            }

            return metaphoneKey.ToString();
        }

        private static bool ProcessVowelIfAtBeginning(char c, int i, StringBuilder metaphoneKey)
        {
            if (Vowels.IsMatch(c.ToString(CultureInfo.InvariantCulture)) && i == 0)
            {
                metaphoneKey.Append(c);
                return true;
            }

            return false;
        }

        private static void ProcessCharactersInWord(char c, int i, int length, StringBuilder strippedNonLetters,
            StringBuilder metaphoneKey)
        {
            switch (c)
            {
                case 'B':
                    // Drop 'B' if after 'M' at the end of the word.
                    ProcessIfCharacterIsB(i, length, strippedNonLetters, metaphoneKey, c);
                    break;

                case 'C':
                    // 'C' transforms to 'X' if followed by 'IA' or 'H' (unless in latter case, it is part of '-SCH-', in which case it transforms to 'K').
                    // 'C' transforms to 'S' if followed by 'I', 'E', or 'Y'. Otherwise, 'C' transforms to 'K'.
                    ProcessIfCharacterIsC(i, strippedNonLetters, length, metaphoneKey);
                    break;

                case 'D':
                    // 'D' transforms to 'J' if followed by 'GE', 'GY', or 'GI'. Otherwise, 'D' transforms to 'T'.
                    ProcessIfCharacterIsD(i, length, strippedNonLetters, metaphoneKey);
                    break;

                case 'G':
                    // Drop 'G' if followed by 'H' and 'H' is not at the end or before a vowel. Drop 'G' if followed by 'N' or 'NED' and is at the end.
                    ProcessIfCharacterIsG(i, length, strippedNonLetters, metaphoneKey);
                    break;

                case 'H':
                    // Drop 'H' if after vowel and not before a vowel.
                    ProcessIfCharacterIsH(i, length, strippedNonLetters, metaphoneKey);
                    break;

                case 'F':
                case 'J':
                case 'L':
                case 'M':
                case 'N':
                case 'R':
                    metaphoneKey.Append(c);
                    break;

                case 'K':
                    // 'CK' transforms to 'K'.
                    ProcessIfCharacterIsK(i, strippedNonLetters, metaphoneKey);
                    break;

                case 'P':
                    // 'PH' transforms to 'F'.
                    ProcessIfCharacterIsPH(i, length, strippedNonLetters, metaphoneKey);
                    break;

                case 'Q':
                    metaphoneKey.Append("K");
                    break;

                case 'S':
                    // 'S' transforms to 'X' if followed by 'H', 'IO', or 'IA'.
                    ProcessIfCharacterIsS(i, length, strippedNonLetters, metaphoneKey);

                    break;

                case 'T':
                    //'T' transforms to 'X' if followed by 'IA' or 'IO'. 'TH' transforms to '0'. Drop 'T' if followed by 'CH'.
                    ProcessIfCharacterIsT(i, length, strippedNonLetters, metaphoneKey);

                    break;

                case 'V':
                    metaphoneKey.Append("F");
                    break;

                case 'W':
                case 'Y':
                    ProcessIfCharacterIsY(c, i, length, strippedNonLetters, metaphoneKey);
                    break;

                case 'X':
                    metaphoneKey.Append("KS");
                    break;

                case 'Z':
                    metaphoneKey.Append("S");
                    break;
            }
        }

        private static void ProcessIfCharacterIsY(char c, int i, int length, StringBuilder strippedNonLetters,
            StringBuilder metaphoneKey)
        {
            if (i + 1 < length && Vowels.IsMatch(strippedNonLetters[i + 1].ToString(CultureInfo.InvariantCulture)))
                metaphoneKey.Append(c);
        }

        private static void ProcessIfCharacterIsT(int i, int length, StringBuilder strippedNonLetters,
            StringBuilder metaphoneKey)
        {
            if (i > 0 && i + 2 < length && strippedNonLetters[i + 1] == 'I' && strippedNonLetters[i + 2] == 'O' &&
                strippedNonLetters[i + 2] == 'A')
            {
                metaphoneKey.Append("X");
            }
            else if (i + 1 < length && strippedNonLetters[i + 1] == 'H')
            {
                if (!(i > 0 && strippedNonLetters[i - 1] == 'T')) metaphoneKey.Append("0");
            }
            else if (i + 2 < length && strippedNonLetters[i + 1] == 'C' && strippedNonLetters[i + 2] == 'H')
            {
            }
            else
            {
                metaphoneKey.Append("T");
            }
        }

        private static void ProcessIfCharacterIsS(int i, int length, StringBuilder strippedNonLetters,
            StringBuilder metaphoneKey)
        {
            if (i > 0 && i + 2 < length && strippedNonLetters[i + 1] == 'I' &&
                (strippedNonLetters[i + 2] == 'O' || strippedNonLetters[i + 2] == 'A'))
                metaphoneKey.Append("X");
            else if (i + 1 < length && strippedNonLetters[i + 1] == 'H')
                metaphoneKey.Append("X");
            else
                metaphoneKey.Append("S");
        }

        private static void ProcessIfCharacterIsPH(int i, int length, StringBuilder strippedNonLetters,
            StringBuilder metaphoneKey)
        {
            if (i + 1 < length && strippedNonLetters[i + 1] == 'H')
                metaphoneKey.Append("F");
            else
                metaphoneKey.Append("P");
        }

        private static void ProcessIfCharacterIsK(int i, StringBuilder strippedNonLetters, StringBuilder metaphoneKey)
        {
            if (i > 0 && strippedNonLetters[i - 1] != 'C')
                metaphoneKey.Append("K");
            else if (i == 0) metaphoneKey.Append("K");
        }

        private static void ProcessIfCharacterIsH(int i, int length, StringBuilder strippedNonLetters,
            StringBuilder metaphoneKey)
        {
            if (i + 1 == length ||
                i > 0 && Varson.IsMatch(strippedNonLetters[i - 1].ToString(CultureInfo.InvariantCulture))) return;
            if (Vowels.IsMatch(strippedNonLetters[i + 1].ToString(CultureInfo.InvariantCulture)))
                metaphoneKey.Append("H");
        }

        private static void ProcessIfCharacterIsG(int i, int length, StringBuilder strippedNonLetters,
            StringBuilder metaphoneKey)
        {
            if (i + 2 < length && strippedNonLetters[i + 1] == 'H' &&
                !Vowels.IsMatch(strippedNonLetters[i + 2].ToString(CultureInfo.InvariantCulture)))
                return;
            if (i + 1 < length && strippedNonLetters[i + 1] == 'N' ||
                i + 3 < length && strippedNonLetters[i + 1] == 'N' && strippedNonLetters[i + 2] == 'E' &&
                strippedNonLetters[i + 3] == 'D')
                return;
            if (i > 0 && i + 1 < length && strippedNonLetters[i - 1] == 'D' &&
                Frontv.IsMatch(strippedNonLetters[i + 1].ToString(CultureInfo.InvariantCulture))) return;
            if (i > 0 && strippedNonLetters[i - 1] == 'G') return;

            if (i + 1 < length && Frontv.IsMatch(strippedNonLetters[i + 1].ToString()))
                metaphoneKey.Append("J");

            else
                metaphoneKey.Append("K");
        }

        private static void ProcessIfCharacterIsD(int i, int length, StringBuilder strippedNonLetters,
            StringBuilder MetaphoneKey)
        {
            if (i + 2 < length && strippedNonLetters[i + 1] == 'G' &&
                Frontv.IsMatch(strippedNonLetters[i + 2].ToString(CultureInfo.InvariantCulture)))
                MetaphoneKey.Append("J");
            else
                MetaphoneKey.Append("T");
        }

        private static void ProcessIfCharacterIsC(int i, StringBuilder strippedNonLetters, int length,
            StringBuilder MetaphoneKey)
        {
            if (i > 0 && strippedNonLetters[i - 1] == 'S' && i + 1 < length &&
                Frontv.IsMatch(strippedNonLetters[i + 1].ToString(CultureInfo.InvariantCulture))) return;
            if (i + 2 < length && strippedNonLetters.ToString().Substring(i, 3) == "CIA")
            {
                MetaphoneKey.Append("X");
            }

            else if (i + 1 < length && Frontv.IsMatch(strippedNonLetters[i + 1].ToString(CultureInfo.InvariantCulture)))
            {
                MetaphoneKey.Append("S");
            }
            else if (i > 0 && i + 1 < length && strippedNonLetters[i - 1] == 'S' && strippedNonLetters[i + 1] == 'H')
            {
                MetaphoneKey.Append("K");
            }

            else if (i + 1 < length && strippedNonLetters[i + 1] == 'H')
            {
                if (i == 0 && i + 2 < length &&
                    !Vowels.IsMatch(strippedNonLetters[i + 2].ToString(CultureInfo.InvariantCulture)))
                    MetaphoneKey.Append("K");
                else
                    MetaphoneKey.Append("X");
            }
            else
            {
                MetaphoneKey.Append("K");
            }
        }

        private static void ProcessIfCharacterIsB(int i, int length, StringBuilder strippedNonLetters,
            StringBuilder metaphoneKey, char c)
        {
            if (i == length - 1 && strippedNonLetters[length - 2] == 'M') return;
            metaphoneKey.Append(c);
        }
    }
}