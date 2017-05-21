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
        public void Deserialise()
        {
            var expectedGameItem = new GameItem(1, "Apple");
            var gameItems = new List<GameItem>() { expectedGameItem };

            var gameItem = GameItem.Deserialise("1", gameItems);

            Assert.AreEqual(expectedGameItem.Id, gameItem.Id, $"GameItem Id wasn't deserialised correctly.");
            Assert.AreEqual(expectedGameItem.Name, gameItem.Name, $"GameItem Name wasn't deserialised correctly.");
        }
    }
}
