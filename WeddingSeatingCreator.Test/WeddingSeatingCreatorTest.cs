using System;
using NUnit.Framework;

namespace WeddingSeatingCreator.Test
{
    [TestFixture]
    public class WeddingSeatingCreatorTest
    {
        [Test]
        public void AssignWeddingTables_WithMultipleTables_ReturnsCorrectAssignment()
        {
            //Arrange
            var config =  @"tables: A-10 Y-12 X-10
Smith, party of 10
Jack, party of 2
James, party of 12 dislikes Smith, Jack";

            var weddingSeatingCreator = new WeddingSeatingCreator(config);

            var expectedOutput = @"Table A: Jack, party of 2
Table Y: James, party of 12
Table X: Smith, party of 10
";

            //Act
            weddingSeatingCreator.AssignWeddingTables();
            var result = weddingSeatingCreator.GetTableAssignments();

            //Assert
            Assert.AreEqual(expectedOutput, result);
        }

        [Test]
        public void AssignWeddingTables_WhenNotPossibleToSeatAll_ThrowsException()
        {
            //Arrange
            var config = @"tables: A-10 Y-12 X-10
Smith, party of 10
Jack, party of 12
James, party of 2 dislikes Smith, Jack
Patrick, party of 3 dislikes James";

            var weddingSeatingCreator = new WeddingSeatingCreator(config);

            //Act

            //Assert
            Assert.Throws<Exception>(() => weddingSeatingCreator.AssignWeddingTables());            
        }
    }
}
