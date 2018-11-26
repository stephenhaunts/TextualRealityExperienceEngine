using System;
using TextualRealityExperienceEngine.GameEngine.Interfaces;

namespace TextualRealityExperienceEngine.GameEngine
{
    public class Game : IGame
    {
        public Game()
        {
            Prologue = string.Empty;
            StartRoom = null;
        }

        public Game(string prologue, IRoom room)
        {
            if (string.IsNullOrEmpty(prologue))
            {
                throw new ArgumentNullException(nameof(prologue), "The prologue can not be empty.");
            }

            if (room == null)
            {
                throw new ArgumentNullException(nameof(room), "The initial room state can not be null.");
            }

            Prologue = prologue;
            StartRoom = room;
        }

        public string Prologue { get; set; }
        public IRoom StartRoom { get; set; }
    }
}
