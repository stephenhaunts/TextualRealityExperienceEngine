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
using System.Collections.Generic;
using TextualRealityExperienceEngine.GameEngine.Interfaces;

namespace TextualRealityExperienceEngine.GameEngine
{
    public class ContentManagement : IContentManagement
    {
        private readonly Dictionary<string, string> _content = new Dictionary<string, string>();
        
        public void AddContentItem(string identifier, string content)
        {
            if (string.IsNullOrEmpty(identifier))
            {
                throw new ArgumentNullException(nameof(identifier));    
            }
            
            if (string.IsNullOrEmpty(content))
            {
                throw new ArgumentNullException(nameof(content));    
            }
            
            _content.Add(identifier, content);
        }

        public string RetrieveContentItem(string identifier)
        {
            if (string.IsNullOrEmpty(identifier))
            {
                throw new ArgumentNullException(nameof(identifier));    
            }

            if (_content.ContainsKey(identifier))
            {
                return _content[identifier];
            }
                        
            return string.Empty;            
        }

        public int CountContentItems => _content.Count;

        public bool Exists(string identifier)
        {
            if (string.IsNullOrEmpty(identifier))
            {
                throw new ArgumentNullException(nameof(identifier));    
            }
            
            return _content.ContainsKey(identifier);
        }
    }
}
