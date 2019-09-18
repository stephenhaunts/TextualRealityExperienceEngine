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
using TextualRealityExperienceEngine.GameEngine.Interfaces;

namespace TextualRealityExperienceEngine.GameEngine
{
    /// <summary>
    /// The Player class represents a player character in a game. 
    /// </summary>
    public class Player : IPlayer
    {
        public IInventory Inventory { get; set; }
        public string Name { get; set; }
        public GenderIdentityEnum GenderIdentity { get; set; }
        public IPlayerStats PlayerStats { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TextualRealityExperienceEngine.GameEngine.Player"/> class.
        /// </summary>
        public Player()
        {
            Inventory = new Inventory();
            GenderIdentity = GenderIdentityEnum.Other;
            PlayerStats = new PlayerStats();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TextualRealityExperienceEngine.GameEngine.Player"/> class.
        /// </summary>
        public Player(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Inventory = new Inventory();
            Name = name;
            GenderIdentity = GenderIdentityEnum.Other;
            PlayerStats = new PlayerStats();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TextualRealityExperienceEngine.GameEngine.Player"/> class.
        /// </summary>
        public Player(string name, GenderIdentityEnum genderIdentity)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Inventory = new Inventory();
            Name = name;
            GenderIdentity = genderIdentity;
            PlayerStats = new PlayerStats();
        }
    }
}
