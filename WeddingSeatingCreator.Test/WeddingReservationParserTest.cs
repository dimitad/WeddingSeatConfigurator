using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace WeddingSeatingCreator.Test
{
    [TestFixture]
    public class WeddingReservationParserTest
    { 
        [Test]
        public void WeddingReservationParse_WithEmptyString_ReturnsEmptyCollection()
        {
            //Arrange
            var weddingReservationParser = new WeddingReservationParser(new List<string>() { "A", "B", "C"});

            //Act
            var result = weddingReservationParser.Parse(string.Empty);

            //Assert
            Assert.IsNotNull(result);
            CollectionAssert.IsEmpty(result);
        }

        [Test]
        public void WeddingReservationParse_WithOneReservation_ReturnsCorrectReservationList()
        {
            //Arrange
            var config =
                @"tables: A-10 Y-12 X-10
Smith, party of 10";

            var availableTables = new List<string>() { "A", "Y", "X" };
            var weddingReservationParser = new WeddingReservationParser(availableTables);

            var expectedReservations = new List<WeddingReservation>()
            {
                new WeddingReservation()
                {
                    Title = "Smith, party of 10",
                    Name = "Smith",
                    PartySize = 10,
                    AvailableTables = availableTables,
                    DislikedNames = new List<string>()
                }
            };

            //Act
            var result = weddingReservationParser.Parse(config);

            //Assert
            Assert.IsNotNull(result);
            CollectionAssert.AreEquivalent(expectedReservations, result);
        }

        [Test]
        public void WeddingReservationParse_WithDislikes_ReturnsCorrectWeddingTableList()
        {
            //Arrange
            var config =
                @"tables: A-10 Y-12 X-10
Smith, party of 10
Jack, party of 2
James, party of 12 dislikes Smith, Jack";

            var availableTables = new List<string>() { "A", "Y", "X" };
            var weddingReservationParser = new WeddingReservationParser(availableTables);

            var expectedReservations = new List<WeddingReservation>()
            {
                new WeddingReservation()
                {
                    Title = "Smith, party of 10",
                    Name = "Smith",
                    PartySize = 10,
                    AvailableTables = availableTables,
                    DislikedNames = new List<string>() { "James" }
                },
                new WeddingReservation()
                {
                    Title = "Jack, party of 2",
                    Name = "Jack",
                    PartySize = 2,
                    AvailableTables = availableTables,
                    DislikedNames = new List<string>() { "James" }
                },
                new WeddingReservation()
                {
                    Title = "James, party of 12",
                    Name = "James",
                    PartySize = 12,
                    AvailableTables = availableTables,
                    DislikedNames = new List<string>() { "Smith", "Jack" }
                }
            };

            //Act
            var result = weddingReservationParser.Parse(config);

            //Assert
            Assert.IsNotNull(result);
            CollectionAssert.AreEquivalent(expectedReservations, result);
        }

        [Test]
        public void WeddingReservationParse_WithInvalidInput_ThrowsException()
        {
            //Arrange
            var config =
                @"tables: A-10 Y-12 X-10
Party of 10, Smith";

            var availableTables = new List<string>() { "A", "Y", "X" };
            var weddingReservationParser = new WeddingReservationParser(availableTables);

            IEnumerable<WeddingReservation> result = new List<WeddingReservation>();

            //Act           

            //Assert
            Assert.Throws<ArgumentException>(() => result = weddingReservationParser.Parse(config));
        }
    }
}
