using System.Collections.ObjectModel;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.SimpleGame.Library;
using TextualRealityExperienceEngine.GameEngine.Interfaces;

namespace Tests.Unit.SimpleGame
{
    [TestClass]
    public class LoungeTests
    {

        private ReadOnlyCollection<ICommand> CompleteOutsideAndHallWaySection()
        {
            SimpleGameController controller = new SimpleGameController();
            controller.InitializeGame();
                       
            controller.ProcessCommand("look at doormat");
            controller.ProcessCommand("look at plant pot");
            controller.ProcessCommand("pick up the key");
            controller.ProcessCommand("unlock the door");
            controller.ProcessCommand("go north");
            controller.ProcessCommand("flip slight switch");
            controller.ProcessCommand("go west");

            return controller.Game.SaveGame();
        }

        [TestMethod]
        public void StartInLounge()
        {
            SimpleGameController controller = new SimpleGameController();
            controller.InitializeGame();

            controller.Game.LoadGame(CompleteOutsideAndHallWaySection());

            Assert.AreEqual("Lounge", controller.Game.CurrentRoom.Name);
            Assert.AreEqual(controller.RetrieveContentItem("LoungeDescription"), controller.Game.CurrentRoom.Description);
        }

        [TestMethod]
        public void LookAtTheFirePlace()
        {
            SimpleGameController controller = new SimpleGameController();
            controller.InitializeGame();

            controller.Game.LoadGame(CompleteOutsideAndHallWaySection());

            Assert.AreEqual(controller.RetrieveContentItem("MentlePieceDescription"), controller.ProcessCommand("look at fire place.").Reply);
        }

        [TestMethod]
        public void LookAtTheFirePlaceAndReadNote()
        {
            SimpleGameController controller = new SimpleGameController();
            controller.InitializeGame();

            controller.Game.LoadGame(CompleteOutsideAndHallWaySection());

            Assert.AreEqual(controller.RetrieveContentItem("MentlePieceDescription"), controller.ProcessCommand("look at fire place.").Reply);
            Assert.AreEqual(controller.RetrieveContentItem("LetterDescription"), controller.ProcessCommand("read the letter.").Reply);
        }

        [TestMethod]
        public void GetTheLetter()
        {
            SimpleGameController controller = new SimpleGameController();
            controller.InitializeGame();

            controller.Game.LoadGame(CompleteOutsideAndHallWaySection());

            Assert.AreEqual(controller.RetrieveContentItem("MentlePieceDescription"), controller.ProcessCommand("look at fire place.").Reply);
            Assert.AreEqual(controller.RetrieveContentItem("LetterDescription"), controller.ProcessCommand("read the letter.").Reply);

            Assert.IsFalse(controller.Game.Player.Inventory.Exists("Letter"));
            Assert.AreEqual("You pick up the letter.", controller.ProcessCommand("pick up the letter.").Reply);
            Assert.IsTrue(controller.Game.Player.Inventory.Exists("Letter"));
        }


       [TestMethod]
        public void TryToGetTheLetterTwice()
        {
            SimpleGameController controller = new SimpleGameController();
            controller.InitializeGame();

            controller.Game.LoadGame(CompleteOutsideAndHallWaySection());

            Assert.AreEqual(controller.RetrieveContentItem("MentlePieceDescription"), controller.ProcessCommand("look at fire place.").Reply);
            Assert.AreEqual(controller.RetrieveContentItem("LetterDescription"), controller.ProcessCommand("read the letter.").Reply);

            Assert.IsFalse(controller.Game.Player.Inventory.Exists("Letter"));
            Assert.AreEqual("You pick up the letter.", controller.ProcessCommand("pick up the letter.").Reply);
            Assert.IsTrue(controller.Game.Player.Inventory.Exists("Letter"));

            Assert.AreEqual(controller.RetrieveContentItem("AlreadyHaveLetter"), controller.ProcessCommand("pick up the letter.").Reply);
        }

        [TestMethod]
        public void CanExitToHallway()
        {
            SimpleGameController controller = new SimpleGameController();
            controller.InitializeGame();

            controller.Game.LoadGame(CompleteOutsideAndHallWaySection());

            Assert.AreEqual("Lounge", controller.Game.CurrentRoom.Name);

            Assert.AreEqual(controller.RetrieveContentItem("HallwayDescription"), controller.ProcessCommand("go east").Reply);
            Assert.AreEqual("Hallway", controller.Game.CurrentRoom.Name);
        }

        [TestMethod]
        public void CanExitToHallwayAndGoBackToLounge()
        {
            SimpleGameController controller = new SimpleGameController();
            controller.InitializeGame();

            controller.Game.LoadGame(CompleteOutsideAndHallWaySection());

            Assert.AreEqual("Lounge", controller.Game.CurrentRoom.Name);

            Assert.AreEqual(controller.RetrieveContentItem("HallwayDescription"), controller.ProcessCommand("go east").Reply);
            Assert.AreEqual("Hallway", controller.Game.CurrentRoom.Name);

            Assert.AreEqual(controller.RetrieveContentItem("LoungeDescription"), controller.ProcessCommand("go west").Reply);
            Assert.AreEqual("Lounge", controller.Game.CurrentRoom.Name);
        }

    }
}
