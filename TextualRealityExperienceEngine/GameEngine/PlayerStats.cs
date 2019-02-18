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
    /// The PlayerStats object is a key/value pair store that you can use to record stats for a player.
    /// </summary>
    public class PlayerStats : IPlayerStats
    {
        private readonly Dictionary<string, int> _playerStats = new Dictionary<string, int>();

        /// <summary>
        /// Add an entry to the PlayerStats object.
        /// </summary>
        /// <param name="name">Name of the stat you want to store.</param>
        /// <param name="value">The actual value to for a stat.</param>
        /// <exception cref="ArgumentNullException">If either the name or the object to store are null, then throw
        ///  an ArgumentNullException.</exception>
        public void Add(string name, int value)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (!Exists(name))
            {
                _playerStats.Add(name, value);
            }
        }

        /// <summary>
        /// Clear all entries from the PlayerStats.
        /// </summary>
        public void Clear()
        {
            _playerStats.Clear();
        }

        /// <summary>
        /// Check if an entry exists in the PlayerStats.
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

            return _playerStats.ContainsKey(name);
        }

        /// <summary>
        /// Retrieve an object from the player stats.
        /// </summary>
        /// <param name="name">The name of the object to retrieve.</param>
        /// <returns>The desired object from the stats.</returns>
        /// <exception cref="ArgumentNullException">If the object name is null or empty then throw an ArgumentNullException.</exception>
        public int Get(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            return _playerStats[name];
        }

        /// <summary>
        /// Return the number of objects in the PlayerStats.
        /// </summary>
        /// <returns>The number of objects in the PlayerStats.</returns>
        public int Count()
        {
            return _playerStats.Count;
        }

        public int AddTo(string name, int addTo)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            _playerStats[name] = _playerStats[name] + addTo;

            return _playerStats[name];
        }

        public int SubtractFrom(string name, int subtractFrom)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            _playerStats[name] = _playerStats[name] - subtractFrom;

            return _playerStats[name];
        }
    }
}