using System.Collections.ObjectModel;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.SimpleGame.Library;
using TextualRealityExperienceEngine.GameEngine.Interfaces;

namespace Tests.Unit.SimpleGame
{
    [TestClass]
    public class HallWayTests
    {

        private ReadOnlyCollection<ICommand> CompleteOutsideSection()
        {
            SimpleGameController controller = new SimpleGameController();
            controller.InitializeGame();
                       
            controller.ProcessCommand("look at doormat");
            controller.ProcessCommand("look at plant pot");
            controller.ProcessCommand("pick up the key");
            controller.ProcessCommand("unlock the door");
            controller.ProcessCommand("go north");

            return controller.Game.SaveGame();
        }

        [TestMethod]
        public void StartInHallWay()
        {
            SimpleGameController controller = new SimpleGameController();
            controller.InitializeGame();

            controller.Game.LoadGame(CompleteOutsideSection());

            Assert.AreEqual("Hallway", controller.Game.CurrentRoom.Name);
            Assert.AreEqual(controller.RetrieveContentItem("HallwayLightsOff"), controller.Game.CurrentRoom.Description);
        }

        [TestMethod]
        public void TurnOnTheLights()
        {
            SimpleGameController controller = new SimpleGameController();
            controller.InitializeGame();

            controller.Game.LoadGame(CompleteOutsideSection());

            Assert.AreEqual("Hallway", controller.Game.CurrentRoom.Name);
            Assert.AreEqual(controller.RetrieveContentItem("HallwayLightsOff"), controller.Game.CurrentRoom.Description);

            Assert.IsTrue(controller.ProcessCommand("flip light switch").Reply.StartsWith(controller.RetrieveContentItem("FlipLightSwitch"), System.StringComparison.Ordinal));
        }

        [TestMethod]
        public void CanExitToOutside()
        {
            SimpleGameController controller = new SimpleGameController();
            controller.InitializeGame();

            controller.Game.LoadGame(CompleteOutsideSection());

            Assert.AreEqual("Hallway", controller.Game.CurrentRoom.Name);
            Assert.AreEqual(controller.RetrieveContentItem("HallwayLightsOff"), controller.Game.CurrentRoom.Description);

            Assert.IsTrue(controller.ProcessCommand("flip light switch").Reply.StartsWith(controller.RetrieveContentItem("FlipLightSwitch"), System.StringComparison.Ordinal));

            Assert.AreEqual(controller.RetrieveContentItem("OutsideDescription"), controller.ProcessCommand("go south").Reply);
            Assert.AreEqual("Outside", controller.Game.CurrentRoom.Name);
        }

        [TestMethod]
        public void CanExitToOutsideAndGoBackToHallWay()
        {
            SimpleGameController controller = new SimpleGameController();
            controller.InitializeGame();

            controller.Game.LoadGame(CompleteOutsideSection());

            Assert.AreEqual("Hallway", controller.Game.CurrentRoom.Name);
            Assert.AreEqual(controller.RetrieveContentItem("HallwayLightsOff"), controller.Game.CurrentRoom.Description);

            Assert.IsTrue(controller.ProcessCommand("flip light switch").Reply.StartsWith(controller.RetrieveContentItem("FlipLightSwitch"), System.StringComparison.Ordinal));

            Assert.AreEqual(controller.RetrieveContentItem("OutsideDescription"), controller.ProcessCommand("go south").Reply);
            Assert.AreEqual("Outside", controller.Game.CurrentRoom.Name);

            Assert.AreEqual(controller.RetrieveContentItem("HallwayDescription"), controller.ProcessCommand("go north").Reply);
            Assert.AreEqual("Hallway", controller.Game.CurrentRoom.Name);
        }

        [TestMethod]
        public void StillHasKeyInInventory()
        {
            SimpleGameController controller = new SimpleGameController();
            controller.InitializeGame();

            controller.Game.LoadGame(CompleteOutsideSection());

            Assert.AreEqual("Hallway", controller.Game.CurrentRoom.Name);
            Assert.AreEqual(controller.RetrieveContentItem("HallwayLightsOff"), controller.Game.CurrentRoom.Description);

            Assert.IsTrue(controller.ProcessCommand("flip light switch").Reply.StartsWith(controller.RetrieveContentItem("FlipLightSwitch"), System.StringComparison.Ordinal));

            Assert.IsTrue(controller.Game.Player.Inventory.Exists("Key"));
        }

        [TestMethod]
        public void DropAndPickUpKey()
        {
            SimpleGameController controller = new SimpleGameController();
            controller.InitializeGame();

            controller.Game.LoadGame(CompleteOutsideSection());

            Assert.AreEqual("Hallway", controller.Game.CurrentRoom.Name);
            Assert.AreEqual(controller.RetrieveContentItem("HallwayLightsOff"), controller.Game.CurrentRoom.Description);

            Assert.IsTrue(controller.ProcessCommand("flip light switch").Reply.StartsWith(controller.RetrieveContentItem("FlipLightSwitch"), System.StringComparison.Ordinal));

            Assert.IsTrue(controller.Game.Player.Inventory.Exists("Key"));

            Assert.AreEqual("You drop the key.", controller.ProcessCommand("drop the key").Reply);

            Assert.IsFalse(controller.Game.Player.Inventory.Exists("Key"));

            Assert.AreEqual(controller.RetrieveContentItem("GrabKey"), controller.ProcessCommand("pick up the key").Reply);

            Assert.IsTrue(controller.Game.Player.Inventory.Exists("Key"));
        }

        [TestMethod]
        public void CanExitToLounge()
        {
            SimpleGameController controller = new SimpleGameController();
            controller.InitializeGame();

            controller.Game.LoadGame(CompleteOutsideSection());

            Assert.AreEqual("Hallway", controller.Game.CurrentRoom.Name);
            Assert.AreEqual(controller.RetrieveContentItem("HallwayLightsOff"), controller.Game.CurrentRoom.Description);

            Assert.IsTrue(controller.ProcessCommand("flip light switch").Reply.StartsWith(controller.RetrieveContentItem("FlipLightSwitch"), System.StringComparison.Ordinal));

            Assert.AreEqual(controller.RetrieveContentItem("LoungeDescription"), controller.ProcessCommand("go west").Reply);
            Assert.AreEqual("Lounge", controller.Game.CurrentRoom.Name);
        }

        [TestMethod]
        public void CanExitToLoungeAndGoBackToHallWay()
        {
            SimpleGameController controller = new SimpleGameController();
            controller.InitializeGame();

            controller.Game.LoadGame(CompleteOutsideSection());

            Assert.AreEqual("Hallway", controller.Game.CurrentRoom.Name);
            Assert.AreEqual(controller.RetrieveContentItem("HallwayLightsOff"), controller.Game.CurrentRoom.Description);

            Assert.IsTrue(controller.ProcessCommand("flip light switch").Reply.StartsWith(controller.RetrieveContentItem("FlipLightSwitch"), System.StringComparison.Ordinal));

            Assert.AreEqual(controller.RetrieveContentItem("LoungeDescription"), controller.ProcessCommand("go west").Reply);
            Assert.AreEqual("Lounge", controller.Game.CurrentRoom.Name);

            Assert.AreEqual(controller.RetrieveContentItem("HallwayDescription"), controller.ProcessCommand("go east").Reply);
            Assert.AreEqual("Hallway", controller.Game.CurrentRoom.Name);
        }

        [TestMethod]
        public void CanExitToKitchen()
        {
            SimpleGameController controller = new SimpleGameController();
            controller.InitializeGame();

            controller.Game.LoadGame(CompleteOutsideSection());

            Assert.AreEqual("Hallway", controller.Game.CurrentRoom.Name);
            Assert.AreEqual(controller.RetrieveContentItem("HallwayLightsOff"), controller.Game.CurrentRoom.Description);

            Assert.IsTrue(controller.ProcessCommand("flip light switch").Reply.StartsWith(controller.RetrieveContentItem("FlipLightSwitch"), System.StringComparison.Ordinal));

            Assert.AreEqual(controller.RetrieveContentItem("LoungeDescription"), controller.ProcessCommand("go west").Reply);
            Assert.AreEqual("Lounge", controller.Game.CurrentRoom.Name);
        }

        [TestMethod]
        public void CanExitToKitchenAndGoBackToHallWay()
        {
            SimpleGameController controller = new SimpleGameController();
            controller.InitializeGame();

            controller.Game.LoadGame(CompleteOutsideSection());

            Assert.AreEqual("Hallway", controller.Game.CurrentRoom.Name);
            Assert.AreEqual(controller.RetrieveContentItem("HallwayLightsOff"), controller.Game.CurrentRoom.Description);

            Assert.IsTrue(controller.ProcessCommand("flip light switch").Reply.StartsWith(controller.RetrieveContentItem("FlipLightSwitch"), System.StringComparison.Ordinal));

            Assert.AreEqual(controller.RetrieveContentItem("KitchenDescription"), controller.ProcessCommand("go north").Reply);
            Assert.AreEqual("Kitchen", controller.Game.CurrentRoom.Name);

            Assert.AreEqual(controller.RetrieveContentItem("HallwayDescription"), controller.ProcessCommand("go south").Reply);
            Assert.AreEqual("Hallway", controller.Game.CurrentRoom.Name);
        }
    }
}
