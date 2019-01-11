/*MIT License

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
using System.Collections.ObjectModel;
using TextualRealityExperienceEngine.GameEngine.Interfaces;

namespace TextualRealityExperienceEngine.GameEngine
{
    public class DroppedObjects : IDroppedObjects
    {
        public DroppedObjects(IGame gameObject)
        {
            _game = gameObject;
        }

        private readonly IGame _game;
        private readonly List<IObject> _droppedObjects = new List<IObject>();

        public ReadOnlyCollection<IObject> DroppedObjectsList => new ReadOnlyCollection<IObject>(_droppedObjects);
        public bool DropObject(string objectName)
        {
            if (string.IsNullOrEmpty(objectName))
            {
                throw new ArgumentNullException(nameof(objectName));
            }
            
            objectName = objectName.ToLower();
            
            if (_game.Inventory.Exists(objectName))
            {
                var droppedObject = _game.Inventory.Get(objectName);
                _game.Inventory.RemoveObject(objectName);
                _droppedObjects.Add(droppedObject);

                return true;
            }

            return false;
        }

        public bool PickUpDroppedObject(string objectName)
        {
            if (string.IsNullOrEmpty(objectName))
            {
                throw new ArgumentNullException(nameof(objectName));
            }

            objectName = objectName.ToLower();
            
            foreach (var i in _droppedObjects)
            {
                if (i.Name.ToLower() == objectName)
                {                  
                    _droppedObjects.Remove(i);
                    _game.Inventory.Add(i.Name, i);
            
                    return true;
                }
            }
            
            return false;
        }
    }
}