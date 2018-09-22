using System;
using System.Collections.Generic;
using NUnit.Framework;
using WeddingSeatingCreator.Interfaces;

namespace WeddingSeatingCreator.Test
{
    [TestFixture]
    public class WeddingTableParserTest
    {
        private readonly IParser<WeddingTable> _weddingTableParser = new WeddingTableParser();

        [Test]
        public void WeddingTableParse_WithEmptyString_ReturnsEmptyCollection()
        {
            //Arrange

            //Act
            var result = _weddingTableParser.Parse(string.Empty);

            //Assert
            Assert.IsNotNull(result);
            CollectionAssert.IsEmpty(result);
        }

        [Test]
        public void WeddingTableParse_WithOneTable_ReturnsCorrectWeddingTableList()
        {
            //Arrange
            var config = "tables: A-20";
            var expectedTables = new List<WeddingTable>()
            {
                new WeddingTable()
                {
                    Id = "A",
                    TotalCapacity = 20,
                    Reservations = new List<WeddingReservation>()
                }
            };

            //Act
            var result = _weddingTableParser.Parse(config);

            //Assert
            Assert.IsNotNull(result);
            CollectionAssert.AreEquivalent(expectedTables, result);
        }

        [Test]
        public void WeddingTableParse_WithMultipleTables_ReturnsCorrectWeddingTableList()
        {
            //Arrange
            var config = "tables: A-20 Y-43 X-5";
            var expectedTables = new List<WeddingTable>()
            {
                new WeddingTable()
                {
                    Id = "A",
                    TotalCapacity = 20,
                    Reservations = new List<WeddingReservation>()
                },
                new WeddingTable()
                {
                    Id = "Y",
                    TotalCapacity = 43,
                    Reservations = new List<WeddingReservation>()
                },
                new WeddingTable()
                {
                    Id = "X",
                    TotalCapacity = 5,
                    Reservations = new List<WeddingReservation>()
                }
            };

            //Act
            var result = _weddingTableParser.Parse(config);

            //Assert
            Assert.IsNotNull(result);
            CollectionAssert.AreEquivalent(expectedTables, result);
        }

        [Test]      
        public void WeddingTableParse_WithInvalidInput_ThrowsException()
        {
            //Arrange
            var config = "tables: 20-A";
            IEnumerable<WeddingTable> result = new List<WeddingTable>(); 

            //Act           

            //Assert
            Assert.Throws<ArgumentException>(() => result = _weddingTableParser.Parse(config));
        }
    }
}
