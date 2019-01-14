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
    /// The NounSynonyms class handles the mapping of many synonyms to a single verb. This means the parser can map and
    /// reduce different nouns entered by the player to a series of single nouns that can be used for triggering different
    /// events in a game.
    /// </summary>
    public class NounSynonyms : INounSynonyms
    {
        readonly Dictionary<string, string> _synonymsMappings = new Dictionary<string, string>();

        /// <summary>
        /// Constructor that setus up the default noun encoding for different navigation directions.
        /// </summary>
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
            
            _synonymsMappings.Add("f", "north");
            _synonymsMappings.Add("b", "south");
            _synonymsMappings.Add("right", "east");                        
            _synonymsMappings.Add("left", "west");            
            _synonymsMappings.Add("r", "east");
            _synonymsMappings.Add("l", "west");
        }

        /// <summary>
        /// Add a new synonym mapping into the dictionary.
        /// </summary>
        /// <param name="synonym">The synonym to be mapped.</param>
        /// <param name="noun">The noun that the synonym maps onto.</param>
        /// <exception cref="ArgumentNullException">If either the synonym or noun inputs are null of empty, an
        /// ArgumentNullException is thrown.</exception>
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

        /// <summary>
        /// Return the base noun by providing one of it's synonyms. This is used by the parser to reduce potential
        /// synonyms down to the base noun to add into a command
        /// </summary>
        /// <param name="synonym">The synonym to return a noun for.</param>
        /// <returns>The mapped noun</returns>
        /// <exception cref="ArgumentNullException">If the synonym is null or empty throw an ArgumentNullException.</exception>
        public string GetNounForSynonym(string synonym)
        {
            if (string.IsNullOrEmpty(synonym))
            {
                throw new ArgumentNullException(nameof(synonym));
            }

            return _synonymsMappings.ContainsKey(synonym) ? _synonymsMappings[synonym] : string.Empty;
        }
    }
}
