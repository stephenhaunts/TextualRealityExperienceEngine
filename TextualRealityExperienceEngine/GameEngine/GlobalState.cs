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
        /// Add an entry to the GlobalState store.
        /// </summary>
        /// <param name="name">Name of the property you want to store.</param>
        /// <param name="state">The actual object to store in the GlobalState.</param>
        /// <exception cref="ArgumentNullException">If either the name or the object to store are null, then throw
        ///  an ArgumentNullException.</exception>
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

            if (!Exists(name.ToLower()))
            {
                _globalState.Add(name.ToLower(), state);
            }
        }

        /// <summary>
        /// Update a value in a state object.
        /// </summary>
        /// <param name="name">Name of the state object to update.</param>
        /// <param name="state">The value to update too.</param>
        public void Update(string name, object state)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            if (Exists(name.ToLower()))
            { 
                _globalState[name.ToLower()] = state;
            }
            else
            {
                throw new InvalidOperationException("The state object <" + name + "> doesn't exist.");
            }
        }

        /// <summary>
        /// Clear all entries from the GlobalState.
        /// </summary>
        public void Clear()
        {
            _globalState.Clear();
        }

        /// <summary>
        /// Check if an entry exists in the GlobalState.
        /// </summary>
        /// <param name="name">The name of the object to check for.</param>
        /// <returns>True if the object exists, False otherwise.</returns>
        /// <exception cref="ArgumentNullException">If the name to check is either null or empty then throw an 
        /// ArgumentNullException.</exception>
        public bool Exists(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            return _globalState.ContainsKey(name);
        }

        /// <summary>
        /// Retrieve an object from the global store.
        /// </summary>
        /// <param name="name">The name of the object to retrieve.</param>
        /// <returns>The desired object from the store.</returns>
        /// <exception cref="ArgumentNullException">If the object name is null or empty then throw an ArgumentNullException.</exception>
        public object Get(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            try
            {
                return _globalState[name.ToLower()];
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }

        /// <summary>
        /// Return the number of objects in the GlobalStore.
        /// </summary>
        /// <returns>The number of objects in the GlobalStore.</returns>
        public int Count()
        {
            return _globalState.Count;
        }
    }
}