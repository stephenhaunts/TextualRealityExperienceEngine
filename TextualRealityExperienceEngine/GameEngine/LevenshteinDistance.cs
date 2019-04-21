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
using TextualRealityExperienceEngine.GameEngine.Interfaces;

namespace TextualRealityExperienceEngine.GameEngine
{
    public class LevenshteinDistance : IStringDistance
    {
        public int Distance(string source, string target)
        {
            if (string.IsNullOrEmpty(source))
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (string.IsNullOrEmpty(target))
            {
                throw new ArgumentNullException(nameof(target));
            }

            source = source.ToLower();
            target = target.ToLower();

            var distance = new int[source.Length + 1, target.Length + 1];

            for (var i = 0; i <= source.Length; i++)
            {
                distance[i, 0] = i;
            }

            for (var j = 0; j <= target.Length; j++)
            {
                distance[0, j] = j;
            }

            for (var j = 1; j <= target.Length; j++)
            {
                for (var i = 1; i <= source.Length; i++)
                {
                    if (source[i - 1] == target[j - 1])
                    {
                        distance[i, j] = distance[i - 1, j - 1];
                    }
                    else
                    {
                        distance[i, j] = Math.Min(Math.Min(
                        distance[i - 1, j] + 1, //a deletion
                        distance[i, j - 1] + 1), //an insertion
                        distance[i - 1, j - 1] + 1 //a substitution
                        );
                    }
                }
            }

            return distance[source.Length, target.Length];
        }
    }
}
