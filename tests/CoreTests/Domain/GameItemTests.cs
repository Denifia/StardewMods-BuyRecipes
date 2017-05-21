using Denifia.Stardew.BuyRecipes.Core.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;

namespace Denifia.Stardew.BuyRecipes.Tests.CoreTests.Domain
{
    [TestClass]
    public class GameItemTests
    {
        [TestMethod]
        [DataRow("1", 1)]
        [DataRow("2", 2)]
        [DataRow("3", 3)]
        public void Deserialise_CorrectlyExtractsId(string serialisedGameItem, int expectedId)
        {
            var gameItem = GameItem.Deserialise(serialisedGameItem);
            Assert.AreEqual(expectedId, gameItem.Id, $"GameItem Id wasn't deserialised correctly when data was \"{serialisedGameItem}\".");
        }

        [TestMethod]
        [DataRow("1 test")]
        [DataRow("blah")]
        [DataRow("")]
        public void Deserialise_IfBadInput_ReturnsNull(string badlySerialisedGameItem)
        {
            var gameItem = GameItem.Deserialise(badlySerialisedGameItem);
            Assert.AreEqual(null, gameItem, $"GameItem was NOT null when input was bad (\"{badlySerialisedGameItem}\").");
        }
    }
}
