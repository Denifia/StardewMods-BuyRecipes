using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Denifia.Stardew.BuyRecipes.Core.Framework.RecipePricing;

namespace Denifia.Stardew.BuyRecipes.Tests.CoreTests.Framework.RecipePricing
{
    [TestClass]
    public class SkillBasedRecipePricingDeserialiseTest
    {
        [TestMethod]
        [DataRow("s Mining 3", 3)]
        [DataRow("s Fishing 7", 7)]
        public void Deserialise_WithCleanData_ReturnsCorrectLevel(string data, int expectedOutput)
        {
            var recipePricing = SkillBasedRecipePricing.Deserialise(data);
            Assert.AreEqual(expectedOutput, recipePricing.SkillLevel);
        }

        [TestMethod]
        [DataRow("s Mining 3")]
        [DataRow("s Fishing 7")]
        public void CanDeserialise_WithCorrectToken_ReturnsTrue(string data)
        {
            var output = SkillBasedRecipePricing.CanDeserialise(data);
            Assert.IsTrue(output);
        }
    }
}
