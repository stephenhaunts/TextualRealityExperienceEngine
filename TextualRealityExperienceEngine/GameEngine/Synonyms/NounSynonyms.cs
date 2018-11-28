/*
MIT License

Copyright(c) 2018 

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
using TextualRealityExperienceEngine.GameEngine.Interfaces;

namespace TextualRealityExperienceEngine.GameEngine.Synonyms
{
    public class NounSynonyms : INounSynonyms
    {
        readonly Dictionary<string, string> _synonymsMappings = new Dictionary<string, string>();

        public NounSynonyms()
        {
            _synonymsMappings.Add("n", "north");
            _synonymsMappings.Add("s", "south");
            _synonymsMappings.Add("e", "east");
            _synonymsMappings.Add("w", "west");

            _synonymsMappings.Add("ne", "northeast");
            _synonymsMappings.Add("se", "southeast");
            _synonymsMappings.Add("sw", "southwest");
            _synonymsMappings.Add("nw", "northwest");

            _synonymsMappings.Add("northeast", "northeast");
            _synonymsMappings.Add("southeast", "southeast");
            _synonymsMappings.Add("southwest", "southwest");
            _synonymsMappings.Add("northwest", "northwest");

            _synonymsMappings.Add("north", "north");
            _synonymsMappings.Add("south", "south");
            _synonymsMappings.Add("east", "east");
            _synonymsMappings.Add("west", "west");

            _synonymsMappings.Add("forward", "north");
            _synonymsMappings.Add("backward", "south");
            _synonymsMappings.Add("forwards", "north");
            _synonymsMappings.Add("backwards", "south");

            _synonymsMappings.Add("right", "east");
            _synonymsMappings.Add("left", "west");

            _synonymsMappings.Add("bum", "bum");

        }

        public void Add(string synonym, string noun)
        {
            if (string.IsNullOrEmpty(synonym))
            {
                throw new ArgumentNullException(nameof(synonym));
            }

            if (string.IsNullOrEmpty(noun))
            {
                throw new ArgumentNullException(nameof(noun));
            }

            _synonymsMappings.Add(synonym, noun);
        }

        public string GetNounforSynonum(string synonym)
        {
            if (string.IsNullOrEmpty(synonym))
            {
                throw new ArgumentNullException(nameof(synonym));
            }

            return _synonymsMappings.ContainsKey(synonym) ? _synonymsMappings[synonym] : string.Empty;
        }
    }
}
