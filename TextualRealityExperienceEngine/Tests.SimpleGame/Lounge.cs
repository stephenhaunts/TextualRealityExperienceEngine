using TextualRealityExperienceEngine.GameEngine;
using TextualRealityExperienceEngine.GameEngine.Interfaces;

namespace Tests.SimpleGame
{
    public class Lounge : Room
    {
        public Lounge(string name, string description, IGame game) : base(name, description, game)
        {
            game.ContentManagement.AddContentItem("NoNeedToBeRudeLounge", "There is no need to be rude.");
        }

        public override string ProcessCommand(ICommand command)
        {
            return command.ProfanityDetected ? Game.ContentManagement.RetrieveContentItem("NoNeedToBeRudeLounge") : base.ProcessCommand(command);
        }
    }
}