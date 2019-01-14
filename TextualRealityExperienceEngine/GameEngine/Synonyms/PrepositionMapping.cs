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
using System.Collections.Generic;
using TextualRealityExperienceEngine.GameEngine.Interfaces;

namespace TextualRealityExperienceEngine.GameEngine.Synonyms
{
    /// <summary>
    /// Enumeration of supported prepositions in the parser.
    /// </summary>
    public enum PropositionEnum
    {
        NotRecognised = 0,
        Into = 1,
        Against = 2,
        To = 3,
        In = 4,
        On = 5,
        Through = 6,
        Over = 7,
        Under = 8,
        Across = 9,
        Behind = 10,
        At = 11,
        Up = 12,
        From = 13
    }

    /// <summary>
    /// Mapping of strings to prepositions used by the parser.
    /// </summary>
    public class PrepositionMapping : IPrepositionMapping
    {
        readonly Dictionary<string, PropositionEnum> _prepositionMappings = new Dictionary<string, PropositionEnum>();

        /// <summary>
        /// Constructor that loads in the initial supported prepositions.
        /// </summary>
        public PrepositionMapping()
        {
            _prepositionMappings.Add("into", PropositionEnum.Into);
            _prepositionMappings.Add("against", PropositionEnum.Against);
            _prepositionMappings.Add("to", PropositionEnum.To);
            _prepositionMappings.Add("in", PropositionEnum.In);
            _prepositionMappings.Add("on", PropositionEnum.On);

            _prepositionMappings.Add("through", PropositionEnum.Through);
            _prepositionMappings.Add("over", PropositionEnum.Over);
            _prepositionMappings.Add("under", PropositionEnum.Under);
            _prepositionMappings.Add("across", PropositionEnum.Across);
            _prepositionMappings.Add("behind", PropositionEnum.Behind);
            _prepositionMappings.Add("at", PropositionEnum.At);
            _prepositionMappings.Add("up", PropositionEnum.Up);
            _prepositionMappings.Add("from", PropositionEnum.From);
        }

        /// <summary>
        /// Add a preposition mapping.
        /// </summary>
        /// <param name="inputProposition">String to map to a preposition.</param>
        /// <param name="preposition">The preposition being mapped.</param>
        /// <exception cref="ArgumentNullException">If the preposition being mapped is null or empty, then throw an
        /// ArgumentNullException.</exception>
        public void Add(string inputProposition, PropositionEnum preposition)
        {
            if (string.IsNullOrEmpty(inputProposition))
            {
                throw new ArgumentNullException(nameof(inputProposition));
            }

            _prepositionMappings.Add(inputProposition, preposition);
        }

        /// <summary>
        /// Get a preposition enum based on an input mapping string.
        /// </summary>
        /// <param name="preposition">The string to map.</param>
        /// <returns>The enum entry for the mapped preposition.</returns>
        /// <exception cref="ArgumentNullException">If the preposition being mapped is null or empty, then throw an
        /// ArgumentNullException.</exception>
        public PropositionEnum GetPreposition(string preposition)
        {
            if (string.IsNullOrEmpty(preposition))
            {
                throw new ArgumentNullException(nameof(preposition));
            }
          
            try
            {
                return _prepositionMappings[preposition];
            }
            catch (KeyNotFoundException)
            {
                return PropositionEnum.NotRecognised;
            }
        }
    }
}
