using System;
namespace TextualRealityExperienceEngine.GameEngine.Interfaces
{
    public interface IGame
    {
        string Prologue { get; set; }
        IRoom StartRoom { get; set; }
    }
}
