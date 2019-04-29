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
using Tests.SimpleGame.Crypt;
using Tests.SimpleGame.Downstairs;
using Tests.SimpleGame.UpStairs;
using TextualRealityExperienceEngine.GameEngine;
using TextualRealityExperienceEngine.GameEngine.Interfaces;

namespace Tests.SimpleGame.Library
{
    public class SimpleGameController
    {
        public IGame Game { get; private set; }

        private IRoom Outside;
        private IRoom Hallway;
        private IRoom Lounge;
        private IRoom Garage;
        private IRoom Kitchen;
        private IRoom DiningRoom;
        private IRoom UpstairsHallway;
        private IRoom LargeBedroom;
        private IRoom SmallBedroom;
        private IRoom Bathroom;

        public string Prologue
        {
            get
            {
                return Game.Prologue;
            }
        }

        public string CurrentRoomDescription
        {
            get
            {
                return Game.CurrentRoom.Description;
            }
        }

        public void InitializeGame()
        {
            Game = new Game();

            AddContentItems();

            Game.Prologue = Game.ContentManagement.RetrieveContentItem("Prologue");
            Game.HelpText = Game.ContentManagement.RetrieveContentItem("HelpText");
            Game.HintSystemEnabled = true;

            SetupRooms();

            Game.StartRoom = Outside;
            Game.CurrentRoom = Outside;
        }

        private void SetupRooms()
        {
            Outside = new Outside(Game);
            Garage = new Garage(Game);
            Kitchen = new Kitchen(Game);
            DiningRoom = new DiningRoom(Game);
            UpstairsHallway = new UpstairsHallway(Game);
            LargeBedroom = new LargeBedroom(Game);
            SmallBedroom = new SmallBedroom(Game);
            Bathroom = new Bathroom(Game);

            Lounge = new Lounge(Game);
            Hallway = new Hallway(Game)
            {
                LightsOn = false
            };

            var doorway = new DoorWay
            {
                Direction = Direction.North,
                Locked = true,
            };

            var doorwayToGarage = new DoorWay
            {
                Direction = Direction.NorthWest,
                Locked = true,
            };

            Outside.AddExit(doorway, Hallway);
            Outside.AddExit(doorwayToGarage, Garage);
            Hallway.AddExit(Direction.West, Lounge);
            Hallway.AddExit(Direction.North, Kitchen);
            Hallway.AddExit(Direction.NorthEast, UpstairsHallway);
            UpstairsHallway.AddExit(Direction.East, LargeBedroom);
            UpstairsHallway.AddExit(Direction.West, SmallBedroom);
            UpstairsHallway.AddExit(Direction.North, Bathroom);

            Kitchen.AddExit(Direction.East, DiningRoom);
        }

        private void AddContentItems()
        {
            Game.ContentManagement.AddContentItem("NoNeedToBeRude", "There is no need to be rude.");
            Game.ContentManagement.AddContentItem("Prologue", "Welcome to the test adventure from the Textual Reality Experience Engine. \r\n\r\nYou will be bedazzled with amazement at the sheer awesomeness of our graphics engine.");

            Game.ContentManagement.AddContentItem("HelpText", "Your aim is to find the treasure that is hidden somewhere in the house. \r\nYou need to type commands into the game to control the player.");

            Game.ContentManagement.AddContentItem("AreYouSure", "Are you sure? (y/n) : ");
            Game.ContentManagement.AddContentItem("ExitMessage", "Have it your way.. You spontaneously combust and depart this mortal coil in a puff of smoke....");
        }

        public GameReply ProcessCommand(string command)
        {
            return Game.ProcessCommand(command);
        }

        public string RetrieveContentItem(string item)
        {
            return Game.ContentManagement.RetrieveContentItem(item);
        }

        public int NumberOfMoves
        {
            get
            {
                return Game.NumberOfMoves;
            }
        }

        public int Score
        {
            get
            {
                return Game.Score;
            }
        }

        public ReadOnlyCollection<string> GetInventory()
        {
            return Game.Player.Inventory.GetInventory();
        }

        public List<(string name, string description)> GetVisitedRooms()
        {
            return Game.VisitedRooms.GetVisitedRooms();
        }

        public string Help
        {
            get
            {
                return Game.HelpText;
            }
        }
    }
}
