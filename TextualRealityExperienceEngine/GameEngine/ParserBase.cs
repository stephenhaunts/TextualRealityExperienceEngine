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
using TextualRealityExperienceEngine.GameEngine.Interfaces;
using TextualRealityExperienceEngine.GameEngine.Synonyms;

namespace TextualRealityExperienceEngine.GameEngine
{
    public class ParserBase
    {
        /// <summary>
        /// Retrieve the verb synonyms being used by the parser.
        /// </summary>
        public IVerbSynonyms Verbs { get; protected set; }

        /// <summary>
        /// Retrieve the noun synonyms being used by the parser.
        /// </summary>
        public INounSynonyms Nouns { get; protected set; }

        /// <summary>
        /// If this flag is set to True, then the users input passed into the parser will also be scanned for profanity.
        /// It is not the intention of this engine to perform any censorship, but it can be useful to know if the player
        /// ius a potty mouthed little so and so. You can even use this fact as part of the narrative.
        /// </summary>
        public bool EnableProfanityFilter { get; set; }

        /// <summary>
        /// Retrieve the prepositions being used by the parser.
        /// </summary>
        public IPrepositionMapping Prepositions { get; protected set; }

        /// <summary>
        /// Gets or sets the adjectives.
        /// </summary>
        /// <value>The adjectives.</value>
        public IAdjectiveMapping Adjectives { get; protected set; }

        protected ParserStatesEnum _parserStates = ParserStatesEnum.Verb;
        protected ICommand _command;
        protected readonly IProfanityFilter _profanityFilter = new ProfanityFilter();

        protected string RemovePunctuation(string s)
        {
            var result = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                if (char.IsWhiteSpace(s[i]))
                {
                    result.Append(" ");
                }
                else if (!char.IsLetter(s[i]) && !char.IsNumber(s[i])) { }
                else
                {
                    result.Append(s[i]);
                }
            }

            return result.ToString();
        }
    }
}
