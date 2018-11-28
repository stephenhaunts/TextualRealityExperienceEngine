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
    public class VerbSynonyms : IVerbSynonyms
    {
        readonly Dictionary<string, VerbCodes> _synonymMappings = new Dictionary<string, VerbCodes>();

        public VerbSynonyms()
        {
            _synonymMappings.Add("walk", VerbCodes.Go);
            _synonymMappings.Add("go", VerbCodes.Go);
            _synonymMappings.Add("run", VerbCodes.Go);
            _synonymMappings.Add("shuffle", VerbCodes.Go);
            _synonymMappings.Add("crawl", VerbCodes.Go);
            _synonymMappings.Add("hop", VerbCodes.Go);
            _synonymMappings.Add("slide", VerbCodes.Go);

            _synonymMappings.Add("pick", VerbCodes.Take);
        }

        public void Add(string synonym, VerbCodes verb)
        {
            if (string.IsNullOrEmpty(synonym))
            {
                throw new ArgumentNullException(nameof(synonym));
            }

            _synonymMappings.Add(synonym, verb);
        }

        public VerbCodes GetVerbforSynonum(string synonym)
        {
            if (string.IsNullOrEmpty(synonym))
            {
                throw new ArgumentNullException(nameof(synonym));
            }

            try
            {
                var verb = _synonymMappings[synonym];
            }
            catch (KeyNotFoundException)
            {
                return VerbCodes.NoCommand;
            }

            return _synonymMappings[synonym];
        }
    }
}
