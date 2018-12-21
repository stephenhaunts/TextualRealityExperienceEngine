using TextualRealityExperienceEngine.GameEngine;
using TextualRealityExperienceEngine.GameEngine.Interfaces;

namespace Tests.SimpleGame
{
    public class Lounge : Room
    {
        public Lounge(string name, string description, IGame game) : base(name, description, game)
        {
        }

        public override string ProcessCommand(ICommand command)
        {
            return command.ProfanityDetected ? "There is no need to be rude." : base.ProcessCommand(command);
        }
    }
}