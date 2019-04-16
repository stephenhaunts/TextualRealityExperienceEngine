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
using TextualRealityExperienceEngine.GameEngine.Interfaces;

namespace TextualRealityExperienceEngine.GameEngine
{
    /// <summary>
    /// Represents a game object that can be picked up and used by the player. An example could be a key, or weapon.
    /// </summary>
    public class GameObject : IObject
    {
        /// <summary>
        /// This is the name of the object. This name could be displayed by the game.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// This is the description of the object that can be displayed in game.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// This is the long description of the object that can be displayed in game.
        /// </summary>
        public string LongDescription { get; set; }

        /// <summary>
        /// This is a message that cen be displayed when an object is collected, something like "You put the key in your 
        /// pocket".
        /// </summary>
        public string PickUpMessage { get; set; }
        
        /// <summary>
        /// You can set a DateTime of when this object is picked up.
        /// </summary>
        public DateTime PickedUpDateTime { get; set; }

        /// <summary>
        /// Constructor that sets an objects initial state.
        /// </summary>
        /// <param name="name">The name of the object.</param>
        /// <param name="description">A description of the object.</param>
        /// <param name="pickUpMessage">The message that is displayed when an object is picked up.</param>
        public GameObject(string name, string description, string pickUpMessage)
        {
            Name = name;
            Description = description;
            PickUpMessage = pickUpMessage;
        }
    }
}
