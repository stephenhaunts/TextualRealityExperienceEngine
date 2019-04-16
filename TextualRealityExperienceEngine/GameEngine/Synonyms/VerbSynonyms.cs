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
    /// Mapping of verb synonyms to verb enum codes. This is where the parser will reduce the different input verb
    /// synonyms to the verbs in the enumeration.
    /// </summary>
    public class VerbSynonyms : IVerbSynonyms
    {
        readonly Dictionary<string, VerbCodes> _synonymMappings = new Dictionary<string, VerbCodes>();

        /// <summary>
        /// Constructor that maps the default set of synonyms supported by the game engine.
        /// </summary>
        public VerbSynonyms()
        {
            _synonymMappings.Add("walk", VerbCodes.Go);
            _synonymMappings.Add("go", VerbCodes.Go);
            _synonymMappings.Add("hop", VerbCodes.Go);
            _synonymMappings.Add("run", VerbCodes.Go);
            _synonymMappings.Add("shuffle", VerbCodes.Go);
            _synonymMappings.Add("crawl", VerbCodes.Go);
            _synonymMappings.Add("slide", VerbCodes.Go);
            _synonymMappings.Add("scuffle", VerbCodes.Go);
            _synonymMappings.Add("wiggle", VerbCodes.Go);
            _synonymMappings.Add("skip", VerbCodes.Go);
            _synonymMappings.Add("prance", VerbCodes.Go);
            _synonymMappings.Add("mince", VerbCodes.Go);

            _synonymMappings.Add("pick", VerbCodes.Take);
            _synonymMappings.Add("grab", VerbCodes.Take);
            _synonymMappings.Add("collect", VerbCodes.Take);
            _synonymMappings.Add("steal", VerbCodes.Take);
            _synonymMappings.Add("get", VerbCodes.Take);
            _synonymMappings.Add("accept", VerbCodes.Take);
            _synonymMappings.Add("capture", VerbCodes.Take);
            _synonymMappings.Add("earn", VerbCodes.Take);
            _synonymMappings.Add("hold", VerbCodes.Take);
            _synonymMappings.Add("reach", VerbCodes.Take);
            _synonymMappings.Add("acquire", VerbCodes.Take);
            _synonymMappings.Add("attain", VerbCodes.Take);
            _synonymMappings.Add("catch", VerbCodes.Take);
            _synonymMappings.Add("clasp", VerbCodes.Take);
            _synonymMappings.Add("clutch", VerbCodes.Take);
            _synonymMappings.Add("ensnare", VerbCodes.Take);
            _synonymMappings.Add("grasp", VerbCodes.Take);
            _synonymMappings.Add("obtain", VerbCodes.Take);
            _synonymMappings.Add("reap", VerbCodes.Take);
            _synonymMappings.Add("snag", VerbCodes.Take);
            _synonymMappings.Add("secure", VerbCodes.Take);
            _synonymMappings.Add("snatch", VerbCodes.Take);
            _synonymMappings.Add("gain", VerbCodes.Take);
            _synonymMappings.Add("gather", VerbCodes.Take);
            _synonymMappings.Add("take", VerbCodes.Take);
            _synonymMappings.Add("haul", VerbCodes.Take);

            _synonymMappings.Add("drop", VerbCodes.Drop);
            _synonymMappings.Add("abandon", VerbCodes.Drop);
            _synonymMappings.Add("release", VerbCodes.Drop);
            _synonymMappings.Add("discard", VerbCodes.Drop);
            _synonymMappings.Add("leave", VerbCodes.Drop);
            _synonymMappings.Add("desert", VerbCodes.Drop);
            _synonymMappings.Add("dismiss", VerbCodes.Drop);
            _synonymMappings.Add("reject", VerbCodes.Drop);
            _synonymMappings.Add("disown", VerbCodes.Drop);
            _synonymMappings.Add("forfeit", VerbCodes.Drop);
            _synonymMappings.Add("relinquish", VerbCodes.Drop);
            _synonymMappings.Add("renounce", VerbCodes.Drop);
            _synonymMappings.Add("resign", VerbCodes.Drop);
            _synonymMappings.Add("sacrifice", VerbCodes.Drop);
            _synonymMappings.Add("terminate", VerbCodes.Drop);

            _synonymMappings.Add("use", VerbCodes.Use);
            _synonymMappings.Add("employ", VerbCodes.Use);
            _synonymMappings.Add("apply", VerbCodes.Use);
            _synonymMappings.Add("work", VerbCodes.Use);
            _synonymMappings.Add("ply", VerbCodes.Use);
            _synonymMappings.Add("exert", VerbCodes.Use);
            _synonymMappings.Add("wield", VerbCodes.Use);
            _synonymMappings.Add("pull", VerbCodes.Use);
            _synonymMappings.Add("push", VerbCodes.Use);
            _synonymMappings.Add("flick", VerbCodes.Use);
            _synonymMappings.Add("flip", VerbCodes.Use);
            _synonymMappings.Add("turn", VerbCodes.Use);
            _synonymMappings.Add("unlock", VerbCodes.Use);
            _synonymMappings.Add("switch", VerbCodes.Use);
            _synonymMappings.Add("climb", VerbCodes.Use);
            _synonymMappings.Add("open", VerbCodes.Use);

            _synonymMappings.Add("look", VerbCodes.Look);
            _synonymMappings.Add("examine", VerbCodes.Look);
            _synonymMappings.Add("peek", VerbCodes.Look);
            _synonymMappings.Add("review", VerbCodes.Look);
            _synonymMappings.Add("stare", VerbCodes.Look);
            _synonymMappings.Add("view", VerbCodes.Look);
            _synonymMappings.Add("cast", VerbCodes.Look);
            _synonymMappings.Add("gander", VerbCodes.Look);
            _synonymMappings.Add("gaze", VerbCodes.Look);
            _synonymMappings.Add("inspect", VerbCodes.Look);
            _synonymMappings.Add("observe", VerbCodes.Look);
            _synonymMappings.Add("watch", VerbCodes.Look);
            _synonymMappings.Add("see", VerbCodes.Look);
            _synonymMappings.Add("glance", VerbCodes.Look);
            _synonymMappings.Add("lift", VerbCodes.Look);
            _synonymMappings.Add("read", VerbCodes.Look);


            _synonymMappings.Add("hint", VerbCodes.Hint);
            _synonymMappings.Add("hints", VerbCodes.Hint);
            _synonymMappings.Add("clue", VerbCodes.Hint);
            _synonymMappings.Add("clues", VerbCodes.Hint);
            _synonymMappings.Add("sos", VerbCodes.Hint);

            _synonymMappings.Add("attack", VerbCodes.Attack);
            _synonymMappings.Add("hit", VerbCodes.Attack);
            _synonymMappings.Add("barge", VerbCodes.Attack);
            _synonymMappings.Add("kick", VerbCodes.Attack);
            _synonymMappings.Add("wallop", VerbCodes.Attack);
            _synonymMappings.Add("smash", VerbCodes.Attack);
            _synonymMappings.Add("stab", VerbCodes.Attack);
            _synonymMappings.Add("slap", VerbCodes.Attack);
            _synonymMappings.Add("punch", VerbCodes.Attack);
            _synonymMappings.Add("chop", VerbCodes.Attack);

            _synonymMappings.Add("teleport", VerbCodes.Visit);
            _synonymMappings.Add("visit", VerbCodes.Visit);
            _synonymMappings.Add("goto", VerbCodes.Visit);
        }

        /// <summary>
        /// Add a new verb synonym mapping into the dictionary.
        /// </summary>
        /// <param name="synonym">The synonym to be mapped.</param>
        /// <param name="verb">The verb that the synonym maps onto.</param>
        /// <exception cref="ArgumentNullException">If either the synonym or verb inputs are null of empty, an
        /// ArgumentNullException is thrown.</exception>
        public void Add(string synonym, VerbCodes verb)
        {
            if (string.IsNullOrEmpty(synonym))
            {
                throw new ArgumentNullException(nameof(synonym));
            }

            _synonymMappings.Add(synonym, verb);
        }

        /// <summary>
        /// Return the base verb by providing one of it's synonyms. This is used by the parser to reduce potential
        /// synonyms down to the base verb to add into a command
        /// </summary>
        /// <param name="synonym">The synonym to return a verb for.</param>
        /// <returns>The mapped verb</returns>
        /// <exception cref="ArgumentNullException">If the synonym is null or empty throw an ArgumentNullException.</exception>
        public VerbCodes GetVerbForSynonym(string synonym)
        {
            if (string.IsNullOrEmpty(synonym))
            {
                throw new ArgumentNullException(nameof(synonym));
            }

            try
            {
                return _synonymMappings[synonym];
            }
            catch (KeyNotFoundException)
            {
                return VerbCodes.NoCommand;
            }
        }
    }
}
