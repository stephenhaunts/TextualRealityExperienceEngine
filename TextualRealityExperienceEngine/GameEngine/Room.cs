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
using System.Collections.ObjectModel;
using TextualRealityExperienceEngine.GameEngine.Interfaces;
using TextualRealityExperienceEngine.GameEngine.Synonyms;

namespace TextualRealityExperienceEngine.GameEngine 
{
    /// <summary>
    /// An adventure game is comprised of multiple rooms that are linked together that the player can navigate around.
    /// This class represents one of those rooms and the exits that link to other rooms.
    /// </summary>
    public class Room : IRoom
    {
        private readonly IRoomExits _roomExits = new RoomExits();
        private string _description;

        /// <summary>
        /// The name of the room.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// The description that is returned when the LightsOn flag us set to False.
        /// </summary>
        public string LightsOffDescription { get; set; }
        
        /// <summary>
        /// An internal reference to the main Game object.
        /// </summary>
        public IGame Game { get; set; }
        
        /// <summary>
        /// A list of objects that have been dropped in a room.
        /// </summary>
        public IDroppedObjects DroppedObjects { get; set; }

        /// <summary>
        /// This flag indicates if the lights are on in the room. You can use this as a game play mechanic so metaphorically
        /// dim the lights.
        /// </summary>
        public bool LightsOn { get; set;}

        /// <summary>
        /// Gets or sets the visited rooms.
        /// </summary>
        /// <value>The visited rooms.</value>
        public IVisitedRooms VisitedRooms { get; set; }

        /// <summary>
        /// Default Constructor to setup the initial room state.
        /// </summary>
        public Room()
        {
            Name = string.Empty;
            Description = string.Empty;
            LightsOn = true;
            DroppedObjects = new DroppedObjects(Game);
        }

        /// <summary>
        /// Constructor to setup the initial room state.
        /// </summary>
        /// <param name="game">An instance of the main Game object.</param>
        public Room(IGame game)
        {
            Name = string.Empty;
            Description = string.Empty;
            Game = game;
            LightsOn = true;
            DroppedObjects = new DroppedObjects(Game);
        }

        /// <summary>
        /// Constructor to setup the initial room state.
        /// </summary>
        /// <param name="roomExits">This object contains the references to the exits for the room which define what
        /// rooms the exits point too.</param>
        /// <param name="game">An instance of the main Game object.</param>
        public Room(IRoomExits roomExits, IGame game)
        {
            Name = string.Empty;
            Description = string.Empty;
            _roomExits = roomExits;
            Game = game;
            LightsOn = true;
            DroppedObjects = new DroppedObjects(Game);
        }

        /// <summary>
        /// Constructor to setup the initial room state.
        /// </summary>
        /// <param name="name">Name of the room.</param>
        /// <param name="description">Description of the room.</param>
        /// <param name="game">Reference to the game object.</param>
        /// <exception cref="ArgumentNullException">If the name or description are null or empty then throw
        /// an ArgumentNullException.</exception>
        public Room(string name, string description, IGame game)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(Name), "The room name can not be empty.");
            }

            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentNullException(nameof(Description), "The room description can not be empty.");
            }

            Name = name;
            Description = description;
            Game = game;
            LightsOn = true;
            DroppedObjects = new DroppedObjects(Game);
        }
        
        /// <summary>
        /// Get and Set the description for the room. The Getter will return a descriptin based on whether the lightsOn 
        /// flag is set.
        /// </summary>
        public string Description 
        {
            get => !LightsOn ? LightsOffDescription : _description;
            set => _description = value;
        }

        /// <summary>
        /// Add an exit to the the room by specifying the direction and the room to exit too.
        /// </summary>
        /// <param name="direction">The direction that the exit is linked too.</param>
        /// <param name="room">The room that the exit leads too.</param>
        /// <param name="withExit">If this is set to True, then the room you specify will have an exit back to this current 
        /// room.</param>
        public void AddExit(Direction direction, IRoom room, bool withExit = true)
        {
            _roomExits.AddExit(direction, room);

            if (!withExit)
            {
                return;
            }

            var door = new DoorWay {Locked = false};

            switch (direction)
            {
                case Direction.North:
                    room.AddExit(Direction.South, this, false);
                    break;

                case Direction.South:
                    room.AddExit(Direction.North, this, false);
                    break;

                case Direction.East:
                    room.AddExit(Direction.West, this, false);
                    break;

                case Direction.West:
                    room.AddExit(Direction.East, this, false);
                    break;

                case Direction.NorthEast:
                    room.AddExit(Direction.SouthWest, this, false);
                    break;

                case Direction.SouthEast:
                    room.AddExit(Direction.NorthWest, this, false);
                    break;

                case Direction.NorthWest:
                    room.AddExit(Direction.SouthEast, this, false);
                    break;

                case Direction.SouthWest:
                    room.AddExit(Direction.NorthEast, this, false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        /// <summary>
        /// Add an exit by specifying a DoorWay object.
        /// </summary>
        /// <param name="doorway">A doorway definition object.</param>
        /// <param name="room">The room that the exit leads too.</param>
        /// <param name="withExit">If this is set to True, then the room you specify will have an exit back to this current 
        /// room.</param>
        public void AddExit(DoorWay doorway, IRoom room, bool withExit = true)
        {
            _roomExits.AddExit(doorway, room);

            if (!withExit)
            {
                return;
            }

            var door = new DoorWay {Locked = false};

            switch (doorway.Direction)
            {
                case Direction.North:
                    door.Direction = Direction.South;
                    room.AddExit(door, this, false);
                    break;

                case Direction.South:
                    door.Direction = Direction.North;
                    room.AddExit(door, this, false);
                    break;

                case Direction.East:
                    door.Direction = Direction.West;
                    room.AddExit(door, this, false);
                    break;

                case Direction.West:
                    door.Direction = Direction.East;
                    room.AddExit(door, this, false);
                    break;

                case Direction.NorthEast:
                    door.Direction = Direction.SouthWest;
                    room.AddExit(door, this, false);
                    break;

                case Direction.SouthEast:
                    door.Direction = Direction.NorthWest;
                    room.AddExit(door, this, false);
                break;

                case Direction.NorthWest:
                    door.Direction = Direction.SouthEast;
                    room.AddExit(door, this, false);
                break;

                case Direction.SouthWest:
                    door.Direction = Direction.NorthEast;
                    room.AddExit(door, this, false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Return a door way assigned to this room for a specific direction.
        /// </summary>
        /// <param name="direction">The direction to get the door way for.</param>
        /// <returns>The doorway assigned to the room for the specified direction.</returns>
        public DoorWay GetDoorWay(Direction direction)
        {
            return _roomExits.GetDoorWay(direction);
        }

        /// <summary>
        /// Set whether you want the door locked for a specific direction.
        /// </summary>
        /// <param name="locked">True if you want to door locked, or False otherwise.</param>
        /// <param name="direction">The direction you want to set the door lock for.</param>
        public void SetDoorLock(bool locked, Direction direction)
        {
            _roomExits.SetDoorLock(locked, direction);
        }

        /// <summary>
        /// Goto a room by specifying the direction noun, ie north, south, northeast etc.
        /// </summary>
        /// <returns>The room description message to display when switching rooms.</returns>
        /// <param name="noun">Direction noun, ie north, south, northeast etc.</param>
        public string GotoRoom(string noun)
        {
            if (string.IsNullOrEmpty(noun))
            {
                throw new ArgumentNullException(nameof(noun));
            }

            var direction = (Direction)Enum.Parse(typeof(Direction), noun, true);
            var room = _roomExits.GetRoomForExit(direction);

            if (room == null)
                return "There is no exit to the " + noun.ToLower() + ".";

            if (_roomExits.IsDoorLocked(direction))
            {
                return "The door is locked.";
            }

            Game.CurrentRoom = room;
            Game.NumberOfMoves++;
            Game.VisitedRooms.AddVisitedRoom(room);

            if (string.IsNullOrEmpty(Game.Parser.Nouns.GetNounForSynonym(room.Name.ToLower())))
            {
                Game.Parser.Nouns.Add(room.Name.ToLower(), room.Name.ToLower());
            }

            var roomDescription = room.Description;

            if (room.DroppedObjects.DroppedObjectsList.Count > 0)
            {
                roomDescription += "\r\n";
            }

            foreach (var item in room.DroppedObjects.DroppedObjectsList)
            {
                roomDescription += "\r\nThere is a " + item.Name + " on the floor.";
            }

            return roomDescription;
        }

        /// <summary>
        /// The ProcessCommand method is called by the main game once the parser has run. This method will handle the following
        /// functions:
        ///   - Navigation between rooms.
        ///   - Examining the room.
        ///   - Taking an object.
        ///   - Dropping an object.
        ///   - Giving hints.
        ///
        /// If you want to add custom logic for a room you should create a superclass of Room and the override ProcessCommand 
        /// to add in customer handlers.
        /// 
        /// </summary>
        /// <param name="command">The command object that was created by the parser.</param>
        /// <returns>A text string to return to main caller.</returns>
        public virtual string ProcessCommand(ICommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            switch (command.Verb)
            {
                case VerbCodes.Go:
                {
                    try
                    {
                        return GotoRoom(command.Noun);
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine();
                        return "Oops";
                    }
                }
                case VerbCodes.Look:
                {
                    if (string.IsNullOrEmpty(command.Noun))
                    {
                        string roomDescription = Description;

                        if (DroppedObjects.DroppedObjectsList.Count > 0)
                        {
                            roomDescription += "\r\n";

                            foreach (var item in DroppedObjects.DroppedObjectsList)
                            {
                                roomDescription += "\r\nThere is a " + item.Name + " on the floor.";
                            }
                        }

                        return roomDescription;
                    }
                    else 
                    { 
                        if (Game.Player.Inventory.Exists(command.Noun))
                        {
                            var inventoryObject = Game.Player.Inventory.Get(command.Noun);
                            return inventoryObject.Description;
                        }
                    }
                    break;
                }
                case VerbCodes.NoCommand:
                    break;
                case VerbCodes.Take:
                    if (DroppedObjects.PickUpDroppedObject(command.Noun))
                    {
                        return "You pick up the " + command.Noun + ".";
                    }
                    else
                    {
                        return "You can not pick up a " + command.Noun + ".";
                    }
                case VerbCodes.Use:
                    break;
                case VerbCodes.Drop:
                    if (DroppedObjects.DropObject(command.Noun))
                    {
                        return "You drop the " + command.Noun + ".";
                    }
                    else
                    {
                        return "You do not have a " + command.Noun + " to drop.";
                    }
                case VerbCodes.Hint:
                    if (Game.HintSystemEnabled)
                    {
                        switch (Game.Difficulty)
                        {
                            case DifficultyEnum.Easy:
                                return "There are no more hints available.";                                
                            case DifficultyEnum.Medium:
                                return "There are no more hints available.";                                
                            case DifficultyEnum.Hard:
                                return "Hints are not allowed for the Hard difficulty.";                                
                            default:
                                throw new ArgumentOutOfRangeException();
                        }                        
                    }
                    break;
                case VerbCodes.Visit:
                    if (Game.VisitedRooms.CheckRoomVisited(command.Noun))
                    {
                        var room = Game.VisitedRooms.GetRoomInstance(command.Noun);
                        Game.CurrentRoom = room;

                        string roomDescription = room.Description;

                        if (room.DroppedObjects.DroppedObjectsList.Count > 0)
                        {
                            roomDescription += "\r\n";

                            foreach (var item in room.DroppedObjects.DroppedObjectsList)
                            {
                                roomDescription += "\r\nThere is a " + item.Name + " on the floor.";
                            }
                        }
                        return roomDescription;
                    }
                    else
                    {
                        return "You can not visit this room as you have not previously been there.";
                    }
                   
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return string.Empty;
        }
    }
}
