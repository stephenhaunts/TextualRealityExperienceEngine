/*
MIT License

Copyright (c) 2018 

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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextualRealityExperienceEngine.GameEngine;
using TextualRealityExperienceEngine.GameEngine.Interfaces;

namespace TextualRealityExperienceEngine.Tests.Unit.GameEngine
{
    [TestClass]
    public class InventoryTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "name")]
        public void AddThrowsArgumentNullExceptionIfNameIsNullOrEmpty()
        {
            IInventory inventory = new Inventory();
            inventory.Add("", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "name")]
        public void AddThrowsArgumentNullExceptionIfGameObjectIsNull()
        {
            IInventory inventory = new Inventory();
            inventory.Add("pizza", null);
        }

        [TestMethod]
        public void AddInsertsObjectInGameInventory()
        {
            IInventory inventory = new Inventory();

            IObject key = new GameObject("key", "The key of fire mountain.", "You picked up the golden key.");
            inventory.Add(key.Name, key);

            Assert.AreEqual(1, inventory.Count());
            Assert.AreEqual(key, inventory.Get(key.Name));
        }

        [TestMethod]
        public void ExistsReturnsObjectFromInventory()
        {
            IInventory inventory = new Inventory();

            IObject key = new GameObject("key", "The key of fire mountain.", "You picked up the golden key.");
            inventory.Add(key.Name, key);

            Assert.AreEqual(key, inventory.Get(key.Name));
        }

        [TestMethod]
        public void ExistsReturnsTrueForObjectThatExists()
        {
            IInventory inventory = new Inventory();

            IObject key = new GameObject("key", "The key of fire mountain.", "You picked up the golden key.");
            inventory.Add(key.Name, key);

            Assert.AreEqual(true, inventory.Exists(key.Name));
        }

        [TestMethod]
        public void ExistsReturnsFalseForObjectThatDoesntExists()
        {
            IInventory inventory = new Inventory();

            Assert.AreEqual(false, inventory.Exists("pizza"));
        }

        [TestMethod]
        public void GetReturnsObjectFromInventory()
        {
            IInventory inventory = new Inventory();

            IObject key = new GameObject("key", "The key of fire mountain.", "You picked up the golden key.");
            inventory.Add(key.Name, key);

            Assert.AreEqual(key, inventory.Get(key.Name));
        }

        [TestMethod]
        public void GetReturnsObjectFromInventory2()
        {
            IInventory inventory = new Inventory();

            Assert.AreEqual(null, inventory.Get("pizza"));
        }

        [TestMethod]
        public void CountReturnsZeroForNewInventory()
        {
            IInventory inventory = new Inventory();

            Assert.AreEqual(0, inventory.Count());
        }

        [TestMethod]
        public void CountReturnsCorrectNumberOfObjects()
        {
            IInventory inventory = new Inventory();

            IObject key = new GameObject("key", "The key of fire mountain.", "You picked up the golden key.");
            inventory.Add(key.Name, key);

            IObject key2 = new GameObject("key2", "The key of fire mountain.", "You picked up the golden key.");
            inventory.Add(key2.Name, key2);

            Assert.AreEqual(2, inventory.Count());
        }

        [TestMethod]
        public void ClearEmptiesInventory()
        {
            IInventory inventory = new Inventory();

            IObject key = new GameObject("key", "The key of fire mountain.", "You picked up the golden key.");
            inventory.Add(key.Name, key);

            IObject key2 = new GameObject("key2", "The key of fire mountain.", "You picked up the golden key.");
            inventory.Add(key2.Name, key2);

            Assert.AreEqual(2, inventory.Count());

            inventory.Clear();

            Assert.AreEqual(0, inventory.Count());
        }

        [TestMethod]
        public void GetInventoryReturnsReadOnlyList()
        {
            IInventory inventory = new Inventory();

            IObject key = new GameObject("key", "The key of fire mountain.", "You picked up the golden key.");
            inventory.Add(key.Name, key);

            IObject key2 = new GameObject("key2", "The key of fire mountain.", "You picked up the golden key.");
            inventory.Add(key2.Name, key2);

            Assert.AreEqual(2, inventory.Count());

            var inventoryList = inventory.GetInventory();

            Assert.AreEqual(2, inventoryList.Count);
            Assert.AreEqual("key : The key of fire mountain.", inventoryList[0]);
            Assert.AreEqual("key2 : The key of fire mountain.", inventoryList[1]);
        }
    }
}
