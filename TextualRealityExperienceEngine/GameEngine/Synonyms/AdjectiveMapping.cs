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
    public class AdjectiveMapping : IAdjectiveMapping
    {
        readonly Dictionary<string, string> _adjectiveMapping = new Dictionary<string, string>();

        public AdjectiveMapping()
        {
            // Appearance
            Add("bald");
            Add("beautiful");
            Add("chubby");
            Add("clean");
            Add("dazzling");
            Add("drab");
            Add("elegant");
            Add("fancy");
            Add("fit");
            Add("flabby");
            Add("glamorous");
            Add("gorgeous");
            Add("handsome");
            Add("magnificent");
            Add("muscular");
            Add("plain");
            Add("plump");
            Add("scruffy");
            Add("shapely");
            Add("skinny");
            Add("stock");
            Add("unkempt");
            Add("unsightly");

            // Positive Personality
            Add("agreeable");
            Add("ambitious");
            Add("brave");
            Add("calm");
            Add("delightful");
            Add("eager");
            Add("faithful");
            Add("gentle");
            Add("happy");
            Add("jolly");
            Add("kind");
            Add("lively");
            Add("nice");
            Add("obedient");
            Add("polite");
            Add("proud");
            Add("silly");
            Add("thankful");
            Add("victorious");
            Add("witty");
            Add("wonderful");
            Add("zealous");

            // Negative Personality
            Add("angry");
            Add("bewildered");
            Add("clumsy");
            Add("defeated");
            Add("embarrassed");
            Add("fierce");
            Add("grumpy");
            Add("helpless");
            Add("itchy");
            Add("jealous");
            Add("lazy");
            Add("mysterious");
            Add("nervous");
            Add("obnoxious");
            Add("panicky");
            Add("pitiful");
            Add("repulsive");
            Add("scary");
            Add("thoughtless");
            Add("uptight");
            Add("worried");

            // Size
            Add("big");
            Add("colossal");
            Add("fat");
            Add("gigantic");
            Add("great");
            Add("huge");
            Add("immense");
            Add("large");
            Add("little");
            Add("mammoth");
            Add("massive");
            Add("microscopic");
            Add("miniature");
            Add("petite");
            Add("puny");
            Add("scrawny");
            Add("short");
            Add("small");
            Add("tall");
            Add("teeny");
            Add("tiny");
        }

        public void Add(string adjective)
        {
            if (string.IsNullOrEmpty(adjective))
            {
                throw new ArgumentNullException(nameof(adjective));
            }

            _adjectiveMapping.Add(adjective, adjective);
        }

        public bool CheckAdjectiveExists(string adjective)
        {
            if (string.IsNullOrEmpty(adjective))
            {
                return false;
            }

            if (_adjectiveMapping.ContainsKey(adjective))
            {
                return true;
            }

            return false;
        }
    }
}
