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

using TextualRealityExperienceEngine.GameEngine.Interfaces;
using TextualRealityExperienceEngine.GameEngine.Synonyms;

namespace TextualRealityExperienceEngine.GameEngine
{
    /// <summary>
    /// This object represents a command that is returned from the parser to the game object. The values in the
    /// command are used to drive how decisions are made in the game logic. By the time this object is populated the
    /// users input has already been sanitized and all synonyms mapped down to their base verb, noun and preposition forms.
    /// </summary>
    public class Command : ICommand
    {
        /// <summary>
        /// The full command that was entered by the player.
        /// </summary>
        public string FullTextCommand { get; set; }
        
        /// <summary>
        /// The verb that was specified by the player.
        /// </summary>
        public VerbCodes Verb { get; set; }

        /// <summary>
        /// The first adjective that was specified by the player.
        /// </summary>
        public string Adjective { get; set; }

        /// <summary>
        /// The first noun that was specified by the player.
        /// </summary>
        public string Noun { get; set; }
        
        /// <summary>
        /// The first preposition that was specified by the player after the first verb and noun combination.
        /// </summary>
        public PropositionEnum Preposition { get; set; }

        /// <summary>
        /// The second adjective that was specified by the player.
        /// </summary>
        public string Adjective2 { get; set; }

        /// <summary>
        /// The second noun that was specified by the player.
        /// </summary>
        public string Noun2 { get; set; }

        /// <summary>
        /// The second preposition that was specified by the player after the first verb and noun combination.
        /// </summary>
        public PropositionEnum Preposition2 { get; set; }

        /// <summary>
        /// The third adjective that was specified by the player.
        /// </summary>
        public string Adjective3 { get; set; }

        /// <summary>
        /// The third noun that was specified by the player.
        /// </summary>
        public string Noun3 { get; set; }

        /// <summary>
        /// If the profanity filter detects any profane language, then this flag is set to true. It is not the purpose
        /// of the engine to perform censorship, but the developer of the game can decide what do to, with censor the language,
        /// or use the profanity as part of the game play logic and narrative.
        /// </summary>
        public bool ProfanityDetected { get; set; }

        /// <summary>
        /// If the profanity filter detects any profane language, then this field will contain what profane words were detected.
        /// </summary>
        public string Profanity { get; set; }

        /// <summary>
        /// Constructor to setup a default empty command.
        /// </summary>
        public Command()
        {
            Verb = VerbCodes.NoCommand;
            Adjective = string.Empty;
            Noun = string.Empty;
            Preposition = PropositionEnum.NotRecognised;
            Adjective2 = string.Empty;
            Noun2 = string.Empty;
            Preposition2 = PropositionEnum.NotRecognised;
            Adjective3 = string.Empty;
            Noun3 = string.Empty;
            ProfanityDetected = false;
            Profanity = string.Empty;
        }
    }
}
