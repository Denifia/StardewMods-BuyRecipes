using Denifia.Stardew.BuyRecipes.Core.Framework;
using Denifia.Stardew.BuyRecipes.Core.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;

namespace CoreTests.Domain
{
    [TestClass]
    public class GameItemTests
    {
        Mock<IModHelper> modHelperMock = new Mock<IModHelper>();

        [TestInitialize]
        public void Init()
        {
            
        }

        [TestMethod]
        public void Deserialise()
        {
            var expectedGameItem = new GameItem(1, "Apple");
            modHelperMock.SetupGet(x => x.GameObjects).Returns(new List<GameItem>()
            {
                expectedGameItem
            });

            var gameItem = GameItem.Deserialise("1", modHelperMock.Object);
            Assert.AreEqual(expectedGameItem.Id, gameItem.Id, $"GameItem Id wasn't deserialised correctly.");
            Assert.AreEqual(expectedGameItem.Name, gameItem.Name, $"GameItem Name wasn't deserialised correctly.");
        }
    }
}
