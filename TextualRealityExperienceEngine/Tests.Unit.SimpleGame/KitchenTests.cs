using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.SimpleGame.Library;
using TextualRealityExperienceEngine.GameEngine.TextProcessing.Interfaces;

namespace Tests.Unit.SimpleGame
{
    [TestClass]
    public class KitchenTests
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
            controller.ProcessCommand("go north");

            return controller.Game.SaveGame();
        }

        [TestMethod]
        public void StartInKitchen()
        {
            SimpleGameController controller = new SimpleGameController();
            controller.InitializeGame();

            controller.Game.LoadGame(CompleteOutsideAndHallWaySection());

            Assert.AreEqual("Kitchen", controller.Game.CurrentRoom.Name);
            Assert.AreEqual(controller.RetrieveContentItem("KitchenDescription"), controller.Game.CurrentRoom.Description);
        }

        [TestMethod]
        public void LookAtTheFridge()
        {
            SimpleGameController controller = new SimpleGameController();
            controller.InitializeGame();

            controller.Game.LoadGame(CompleteOutsideAndHallWaySection());

            Assert.AreEqual(controller.RetrieveContentItem("ItsAFridge"), controller.ProcessCommand("look at the fridge.").Reply);
        }

        [TestMethod]
        public void OpenTheFridge()
        {
            SimpleGameController controller = new SimpleGameController();
            controller.InitializeGame();

            controller.Game.LoadGame(CompleteOutsideAndHallWaySection());

            Assert.AreEqual(controller.RetrieveContentItem("ItsAFridge"), controller.ProcessCommand("look at the fridge.").Reply);
            Assert.IsTrue(controller.ProcessCommand("open the fridge.").Reply.StartsWith(controller.RetrieveContentItem("LookedAtFridge"), System.StringComparison.Ordinal));
        }

        [TestMethod]
        public void LookAtFoodInTheFridge()
        {
            SimpleGameController controller = new SimpleGameController();
            controller.InitializeGame();

            controller.Game.LoadGame(CompleteOutsideAndHallWaySection());

            Assert.AreEqual(controller.RetrieveContentItem("ItsAFridge"), controller.ProcessCommand("look at the fridge.").Reply);
            Assert.IsTrue(controller.ProcessCommand("open the fridge.").Reply.StartsWith(controller.RetrieveContentItem("LookedAtFridge"), System.StringComparison.Ordinal));

            Assert.AreEqual(controller.RetrieveContentItem("LookAtChicken"), controller.ProcessCommand("look at the chicken.").Reply);
            Assert.AreEqual(controller.RetrieveContentItem("LookAtCheese"), controller.ProcessCommand("look at the cheese.").Reply);
        }

   

        [TestMethod]
        public void CanExitToHallway()
        {
            SimpleGameController controller = new SimpleGameController();
            controller.InitializeGame();

            controller.Game.LoadGame(CompleteOutsideAndHallWaySection());

            Assert.AreEqual("Kitchen", controller.Game.CurrentRoom.Name);

            Assert.AreEqual(controller.RetrieveContentItem("HallwayDescription"), controller.ProcessCommand("go south").Reply);
            Assert.AreEqual("Hallway", controller.Game.CurrentRoom.Name);
        }

        [TestMethod]
        public void CanExitToHallwayAndGoBackToKitchen()
        {
            SimpleGameController controller = new SimpleGameController();
            controller.InitializeGame();

            controller.Game.LoadGame(CompleteOutsideAndHallWaySection());

            Assert.AreEqual("Kitchen", controller.Game.CurrentRoom.Name);

            Assert.AreEqual(controller.RetrieveContentItem("HallwayDescription"), controller.ProcessCommand("go south").Reply);
            Assert.AreEqual("Hallway", controller.Game.CurrentRoom.Name);

            Assert.AreEqual(controller.RetrieveContentItem("KitchenDescription"), controller.ProcessCommand("go north").Reply);
            Assert.AreEqual("Kitchen", controller.Game.CurrentRoom.Name);
        }

    }
}
