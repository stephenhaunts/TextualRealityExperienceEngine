﻿/*
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
    /// <summary>
    /// The GlobalState object is a key/value pair store that you can save data too that needs to be shared across rooms.
    ///
    /// This is a neater solution than using C# global variables. Examples for this object could be storing player stats
    /// like health, luck etc.
    ///
    /// Data stored in the GlobalState is passed in as the .NET object base object. This means as the game implementor
    /// you need to be aware of the types you are passing so you can cast them back to their original types on retrieval.
    /// </summary>
    public class GlobalState : IGlobalState 
    {
        private readonly Dictionary<string, object> _globalState = new Dictionary<string, object>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="state"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Add(string name, object state)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            if (!Exists(name))
            {
                _globalState.Add(name, state);
            }
        }

        public void Clear()
        {
            _globalState.Clear();
        }

        public bool Exists(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            return _globalState.ContainsKey(name);
        }

        public object Get(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            try
            {
                return _globalState[name];
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }

        public int Count()
        {
            return _globalState.Count;
        }
    }
}
