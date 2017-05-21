using Denifia.Stardew.BuyRecipes.Core.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Denifia.Stardew.BuyRecipes.Tests.CoreTests.Domain
{
    [TestClass]
    public class GameDateTimeTests
    {
        [TestMethod]
        [DataRow("spring", 1)]
        [DataRow("summer", 2)]
        [DataRow("fall", 3)]
        [DataRow("winter", 4)]
        public void SeasonIsParsedCorrectly(string season, int expectedSeasonAsInt)
        {
            var gameDateTime = GameDateTime.Deserialise(1, 1, season, 1);
            Assert.AreEqual(gameDateTime.Season, expectedSeasonAsInt, $"\"{season}\" did not deserialise as {expectedSeasonAsInt}.");
        }

        [TestMethod]
        [DataRow("blah", 0)]
        [DataRow("", 0)]
        [DataRow(null, 0)]
        public void UnexpectedSeasonIsParsedCorrectly(string season, int expectedSeasonAsInt)
        {
            SeasonIsParsedCorrectly(season, expectedSeasonAsInt);
        }

        [TestMethod]
        public void StartOfWeekDoesntChangeYear()
        {
            var gameDateTime = new GameDateTime(2, 2, 2, 2);
            var startOfWeek = gameDateTime.StartOfWeek;
            Assert.AreEqual(gameDateTime.Year, startOfWeek.Year, $"Start of week changed the year.");
        }

        [TestMethod]
        public void StartOfWeekDoesntChangeSeason()
        {
            var gameDateTime = new GameDateTime(2, 2, 2, 2);
            var startOfWeek = gameDateTime.StartOfWeek;
            Assert.AreEqual(gameDateTime.Season, startOfWeek.Season, $"Start of week changed the year.");
        }

        [TestMethod]
        [DataRow(1, 1)]
        [DataRow(2, 1)]
        [DataRow(3, 1)]
        [DataRow(4, 1)]
        [DataRow(5, 1)]
        [DataRow(6, 1)]
        [DataRow(7, 1)]
        [DataRow(8, 8)]
        [DataRow(9, 8)]
        [DataRow(10, 8)]
        [DataRow(11, 8)]
        [DataRow(12, 8)]
        [DataRow(13, 8)]
        [DataRow(14, 8)]
        [DataRow(15, 15)]
        [DataRow(21, 15)]
        [DataRow(22, 22)]
        [DataRow(28, 22)]
        public void StartOfWeekResolvesCorrectly(int dayOfWeek, int expectedStartDayOfWeek)
        {
            var gameDateTime = new GameDateTime(1, dayOfWeek, 1, 1);
            var startOfWeek = gameDateTime.StartOfWeek;
            Assert.AreEqual(expectedStartDayOfWeek, startOfWeek.DayOfMonth, $"StartOfWeek didn't return correctly if dayOfWeek was {dayOfWeek}.");
        }
    }
}
