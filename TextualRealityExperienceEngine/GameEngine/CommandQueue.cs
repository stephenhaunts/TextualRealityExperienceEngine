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

using System.Collections.Generic;
using System.Collections.ObjectModel;
using TextualRealityExperienceEngine.GameEngine.Interfaces;
using TextualRealityExperienceEngine.GameEngine.TextProcessing.Interfaces;

namespace TextualRealityExperienceEngine.GameEngine
{
    /// <summary>
    /// The CommandQueue object is used to store commands as they are returned from the parser. This means it stores a
    /// history of commands that the user has typed in post parsing. This command queue then forms the basis of a save
    /// and load game system.
    ///
    /// A load game mechanism works by replaying commands back into the game from the initial start game state.
    /// </summary>
    public class CommandQueue : ICommandQueue
    {
        private readonly List<ICommand> _commandQueue = new List<ICommand>();

        /// <summary>
        /// Add a command into the queue.
        /// </summary>
        /// <param name="command">A command that has already been processed and returned by the game parser.</param>
        public void AddCommand(ICommand command)
        {
            _commandQueue.Add(command);
        }

        /// <summary>
        /// Returns the number of commands in the queue.
        /// </summary>
        public int Count => _commandQueue.Count;

        /// <summary>
        /// Return a read only collection of commands that have been stored in the queue.
        /// </summary>
        public ReadOnlyCollection<ICommand> Commands => new ReadOnlyCollection<ICommand>(_commandQueue);
        
        // Clear all the commands in the queue.
        public void Clear()
        {
            _commandQueue.Clear();
        }
    }
}
