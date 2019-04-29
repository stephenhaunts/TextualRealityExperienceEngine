using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.SimpleGame.Library;

namespace Tests.Unit.SimpleGame
{
    [TestClass]
    public class OutsideTests
    {
        [TestMethod]
        public void GetKeyUnlockDoor()
        {
            SimpleGameController controller = new SimpleGameController();
            controller.InitializeGame();

            Assert.AreEqual("Outside", controller.Game.CurrentRoom.Name);
            Assert.AreEqual(controller.RetrieveContentItem("DoorMat"), controller.ProcessCommand("look at doormat").Reply);

            Assert.AreEqual(controller.RetrieveContentItem("MovePlantPot"), controller.ProcessCommand("look at plant pot").Reply);
            Assert.AreEqual(controller.RetrieveContentItem("GrabKey"), controller.ProcessCommand("pick up the key").Reply);

            Assert.IsTrue(controller.Game.Player.Inventory.Exists("Key"));

            Assert.AreEqual(controller.RetrieveContentItem("TurnKey"), controller.ProcessCommand("unlock the door").Reply);
            Assert.AreEqual(controller.RetrieveContentItem("HallwayLightsOff"), controller.ProcessCommand("go north").Reply);
            Assert.AreEqual("Hallway", controller.Game.CurrentRoom.Name);
        }

        [TestMethod]
        public void TryToPickUpKeyTwice()
        {
            SimpleGameController controller = new SimpleGameController();
            controller.InitializeGame();

            Assert.AreEqual("Outside", controller.Game.CurrentRoom.Name);
            Assert.AreEqual(controller.RetrieveContentItem("DoorMat"), controller.ProcessCommand("look at doormat").Reply);

            Assert.AreEqual(controller.RetrieveContentItem("MovePlantPot"), controller.ProcessCommand("look at plant pot").Reply);
            Assert.AreEqual(controller.RetrieveContentItem("GrabKey"), controller.ProcessCommand("pick up the key").Reply);

            Assert.IsTrue(controller.Game.Player.Inventory.Exists("Key"));

            Assert.AreEqual(controller.RetrieveContentItem("AlreadyHaveKey"), controller.ProcessCommand("pick up the key").Reply);
            Assert.AreEqual(controller.RetrieveContentItem("TurnKey"), controller.ProcessCommand("unlock the door").Reply);
            Assert.AreEqual(controller.RetrieveContentItem("HallwayLightsOff"), controller.ProcessCommand("go north").Reply);
            Assert.AreEqual("Hallway", controller.Game.CurrentRoom.Name);
        }

        [TestMethod]
        public void TryToPickUpKeyBeforeLookingAtThePlantPot()
        {
            SimpleGameController controller = new SimpleGameController();
            controller.InitializeGame();

            Assert.AreEqual("Outside", controller.Game.CurrentRoom.Name);
            Assert.AreEqual(controller.RetrieveContentItem("DoorMat"), controller.ProcessCommand("look at doormat").Reply);

            Assert.AreEqual("You can not pick up a key.", controller.ProcessCommand("pick up the key").Reply);

            Assert.AreEqual(controller.RetrieveContentItem("MovePlantPot"), controller.ProcessCommand("look at plant pot").Reply);
            Assert.AreEqual(controller.RetrieveContentItem("GrabKey"), controller.ProcessCommand("pick up the key").Reply);

            Assert.IsTrue(controller.Game.Player.Inventory.Exists("Key"));

            Assert.AreEqual(controller.RetrieveContentItem("TurnKey"), controller.ProcessCommand("unlock the door").Reply);
            Assert.AreEqual(controller.RetrieveContentItem("HallwayLightsOff"), controller.ProcessCommand("go north").Reply);
            Assert.AreEqual("Hallway", controller.Game.CurrentRoom.Name);
        }

        [TestMethod]
        public void TryToUseAKeyBeforeLookingAtThePlantPot()
        {
            SimpleGameController controller = new SimpleGameController();
            controller.InitializeGame();

            Assert.AreEqual("Outside", controller.Game.CurrentRoom.Name);
            Assert.AreEqual(controller.RetrieveContentItem("DoorMat"), controller.ProcessCommand("look at doormat").Reply);

            Assert.AreEqual(controller.RetrieveContentItem("DoNotHaveKey"), controller.ProcessCommand("use key").Reply);

            Assert.AreEqual(controller.RetrieveContentItem("MovePlantPot"), controller.ProcessCommand("look at plant pot").Reply);
            Assert.AreEqual(controller.RetrieveContentItem("GrabKey"), controller.ProcessCommand("pick up the key").Reply);

            Assert.IsTrue(controller.Game.Player.Inventory.Exists("Key"));

            Assert.AreEqual(controller.RetrieveContentItem("TurnKey"), controller.ProcessCommand("unlock the door").Reply);
            Assert.AreEqual(controller.RetrieveContentItem("HallwayLightsOff"), controller.ProcessCommand("go north").Reply);
            Assert.AreEqual("Hallway", controller.Game.CurrentRoom.Name);
        }

        [TestMethod]
        public void TryToUnlockDoorBeforeLookingAtThePlantPot()
        {
            SimpleGameController controller = new SimpleGameController();
            controller.InitializeGame();

            Assert.AreEqual("Outside", controller.Game.CurrentRoom.Name);
            Assert.AreEqual(controller.RetrieveContentItem("DoorMat"), controller.ProcessCommand("look at doormat").Reply);

            Assert.AreEqual(controller.RetrieveContentItem("DoorLocked"), controller.ProcessCommand("open door").Reply);
            Assert.AreEqual(controller.RetrieveContentItem("DoorLocked"), controller.ProcessCommand("unlock door").Reply);

            Assert.AreEqual(controller.RetrieveContentItem("MovePlantPot"), controller.ProcessCommand("look at plant pot").Reply);
            Assert.AreEqual(controller.RetrieveContentItem("GrabKey"), controller.ProcessCommand("pick up the key").Reply);

            Assert.IsTrue(controller.Game.Player.Inventory.Exists("Key"));

            Assert.AreEqual(controller.RetrieveContentItem("TurnKey"), controller.ProcessCommand("unlock the door").Reply);
            Assert.AreEqual(controller.RetrieveContentItem("HallwayLightsOff"), controller.ProcessCommand("go north").Reply);
            Assert.AreEqual("Hallway", controller.Game.CurrentRoom.Name);
        }

        [TestMethod]
        public void DropKey()
        {
            SimpleGameController controller = new SimpleGameController();
            controller.InitializeGame();

            Assert.AreEqual("Outside", controller.Game.CurrentRoom.Name);
            Assert.AreEqual(controller.RetrieveContentItem("DoorMat"), controller.ProcessCommand("look at doormat").Reply);

            Assert.AreEqual(controller.RetrieveContentItem("DoorLocked"), controller.ProcessCommand("open door").Reply);
            Assert.AreEqual(controller.RetrieveContentItem("DoorLocked"), controller.ProcessCommand("unlock door").Reply);

            Assert.AreEqual(controller.RetrieveContentItem("MovePlantPot"), controller.ProcessCommand("look at plant pot").Reply);
            Assert.AreEqual(controller.RetrieveContentItem("GrabKey"), controller.ProcessCommand("pick up the key").Reply);

            Assert.IsTrue(controller.Game.Player.Inventory.Exists("Key"));

            Assert.AreEqual("You drop the key.", controller.ProcessCommand("drop the key").Reply);

            Assert.AreEqual(controller.RetrieveContentItem("DoorLocked"), controller.ProcessCommand("unlock the door").Reply);
        }

        [TestMethod]
        public void DropTheKeyThenPickItUpAgain()
        {
            SimpleGameController controller = new SimpleGameController();
            controller.InitializeGame();

            Assert.AreEqual("Outside", controller.Game.CurrentRoom.Name);
            Assert.AreEqual(controller.RetrieveContentItem("DoorMat"), controller.ProcessCommand("look at doormat").Reply);

            Assert.AreEqual(controller.RetrieveContentItem("DoorLocked"), controller.ProcessCommand("open door").Reply);
            Assert.AreEqual(controller.RetrieveContentItem("DoorLocked"), controller.ProcessCommand("unlock door").Reply);

            Assert.AreEqual(controller.RetrieveContentItem("MovePlantPot"), controller.ProcessCommand("look at plant pot").Reply);
            Assert.AreEqual(controller.RetrieveContentItem("GrabKey"), controller.ProcessCommand("pick up the key").Reply);

            Assert.IsTrue(controller.Game.Player.Inventory.Exists("Key"));

            Assert.AreEqual("You drop the key.", controller.ProcessCommand("drop the key").Reply);

            Assert.AreEqual(controller.RetrieveContentItem("GrabKey"), controller.ProcessCommand("pick up the key").Reply);

            Assert.AreEqual(controller.RetrieveContentItem("TurnKey"), controller.ProcessCommand("unlock the door").Reply);
            Assert.AreEqual(controller.RetrieveContentItem("HallwayLightsOff"), controller.ProcessCommand("go north").Reply);
            Assert.AreEqual("Hallway", controller.Game.CurrentRoom.Name);
        }
    }
}
