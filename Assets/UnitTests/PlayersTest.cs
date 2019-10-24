using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class PlayersTest
    {

        [SetUp]
        public void SetUp()
        {
            // Confirm that players list is empty
            Assert.AreEqual(Players.PlayerCount(), 0, $"A player was found when there should be none.");
        }

        [TearDown]
        public void TearDown()
        {
            // Remove all players before the next test
            Players.RemoveAllPlayers();
        }

        [Test]
        public void AllPlayersIsDefaultAListOfNulls()
        {
            Assert.AreEqual(Players.PlayerCount(), 0, $"A player was found when there should be none.");
            var allPlayers = Players.GetAllPlayers();
            Assert.AreEqual(Players.maxPlayers, allPlayers.Count, "All Players List was not the right length");

            foreach (var player in allPlayers)
            {
                Assert.IsNull(player, "A non null player was found");
            }
        }

        [Test]
        public void CanAddAndRemovePlayer()
        {
            var playerName = "Test1";
            Assert.IsFalse(Players.IsPlayer(playerName), $"{playerName} is already a player");
            Assert.IsTrue(Players.AddPlayer(playerName), $"Did not successfully add {playerName}");
            Assert.IsTrue(Players.IsPlayer(playerName), $"Could not find {playerName}");
            Assert.IsTrue(Players.RemovePlayer(playerName), $"Could not remove {playerName}");
            Assert.IsFalse(Players.IsPlayer(playerName), $"{playerName} was not removed");
        }

        [Test]
        public void PlayersAreAddedInOrder()
        {
            var player1Name = "Test1";
            var player2Name = "Test2";
            var player3Name = "Test3";

            Assert.IsTrue(Players.AddPlayer(player1Name), $"Did not successfully add {player1Name}");
            Assert.IsTrue(Players.AddPlayer(player2Name), $"Did not successfully add {player2Name}");
            Assert.IsTrue(Players.AddPlayer(player3Name), $"Did not successfully add {player3Name}");

            Assert.AreEqual(Players.PlayerCount(), 3, $"Expected a number of players but did not find them");

            var players = Players.GetPlayers();
            Assert.AreEqual(3, players.Count);

            Assert.AreEqual(player1Name, players[0].Controller);
            Assert.AreEqual(player2Name, players[1].Controller);
            Assert.AreEqual(player3Name, players[2].Controller);
        }

        [Test]
        public void RemovingAPlayerLetsYouAddANewOneInTheirPlace()
        {
            var player1Name = "Test1";
            var player2Name = "Test2";
            var player3Name = "Test3";
            var player4Name = "Test3";

            Assert.IsTrue(Players.AddPlayer(player1Name), $"Did not successfully add {player1Name}");
            Assert.IsTrue(Players.AddPlayer(player2Name), $"Did not successfully add {player2Name}");
            Assert.IsTrue(Players.AddPlayer(player3Name), $"Did not successfully add {player3Name}");
            Assert.IsTrue(Players.RemovePlayer(player2Name), $"Did not successfully remove {player2Name}");

            Assert.AreEqual(Players.PlayerCount(), 2, $"Expected a number of players but did not find them");

            Assert.IsTrue(Players.AddPlayer(player4Name), $"Did not successfully add {player4Name}");

            var players = Players.GetPlayers();
            Assert.AreEqual(3, players.Count);

            Assert.AreEqual(player1Name, players[0].Controller);
            Assert.AreEqual(player4Name, players[1].Controller);
            Assert.AreEqual(player3Name, players[2].Controller);
        }
    }
}
