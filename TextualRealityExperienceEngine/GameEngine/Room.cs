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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TextualRealityExperienceEngine.GameEngine.Interfaces;
using TextualRealityExperienceEngine.GameEngine.Synonyms;

namespace TextualRealityExperienceEngine.GameEngine 
{
    public class Room : IRoom
    {
        private readonly IRoomExits _roomExits = new RoomExits();
        public string Name { get; set; }
        public string LightsOffDescription { get; set; }
        public IGame Game { get; set; }
        private string _description;

        public IDroppedObjects DroppedObjects;

        public bool LightsOn { get; set;}

        public Room()
        {
            Name = string.Empty;
            Description = string.Empty;
            LightsOn = true;
            DroppedObjects = new DroppedObjects(Game);
        }

        public Room(IGame game)
        {
            Name = string.Empty;
            Description = string.Empty;
            Game = game;
            LightsOn = true;
            DroppedObjects = new DroppedObjects(Game);
        }

        public Room(IRoomExits roomExits, IGame game)
        {
            Name = string.Empty;
            Description = string.Empty;
            _roomExits = roomExits;
            Game = game;
            LightsOn = true;
            DroppedObjects = new DroppedObjects(Game);
        }

        public string Description 
        {
            get => !LightsOn ? LightsOffDescription : _description;
            set => _description = value;
        }

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
        }

        public void AddExit(Direction direction, IRoom room, bool withExit = true)
        {
            _roomExits.AddExit(direction, room);

            if (!withExit)
            {
                return;
            }

            var door = new DoorWay {Locked = false, ObjectToUnlock = string.Empty};

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

        public void AddExit(DoorWay doorway, IRoom room, bool withExit = true)
        {
            _roomExits.AddExit(doorway, room);

            if (!withExit)
            {
                return;
            }

            var door = new DoorWay {Locked = false, ObjectToUnlock = string.Empty};

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

        public DoorWay GetDoorWay(Direction direction)
        {
            return _roomExits.GetDoorWay(direction);
        }

        public void SetDoorLock(bool locked, Direction direction)
        {
            _roomExits.SetDoorLock(locked, direction);
        }

        public virtual string ProcessCommand(ICommand command)
        {
            switch (command.Verb)
            {
                case Synonyms.VerbCodes.Go:
                {
                    try
                    {
                        Direction direction = (Direction)Enum.Parse(typeof(Direction), command.Noun, true);
                        var room = _roomExits.GetRoomForExit(direction);

                        if (room == null)
                            return "There is no exit to the " + command.Noun.ToLower();

                        if (_roomExits.IsDoorLocked(direction))
                        {
                                return "The door is locked.";
                        }

                        Game.CurrentRoom = room;
                        Game.NumberOfMoves++;

                        return room.Description;
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
                        return Description;
                    }
                    break;
                }
                case VerbCodes.NoCommand:
                    break;
                case VerbCodes.Take:
                    break;
                case VerbCodes.Use:
                    break;
                case VerbCodes.Drop:
                    break;
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
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return string.Empty;
        }
    }
}
