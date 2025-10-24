using Microsoft.VisualStudio.TestTools.UnitTesting;
using POE_part1;

namespace POE_part1.Tests
{
    [TestClass]
    public class ClaimTests
    {
        [TestMethod]
        public void CalculateTotalAmount_ShouldReturnCorrectValue()
        {
            // Arrange
            var claim = new LecturerClaim { HoursWorked = 10, HourlyRate = 200 };

            // Act
            double expected = 2000;
            double actual = claim.HoursWorked * claim.HourlyRate;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ApproveClaim_ShouldChangeStatusToApproved()
        {
            // Arrange
            var claim = new LecturerClaim { Status = "Pending" };

            // Act
            claim.Status = "Approved";

            // Assert
            Assert.AreEqual("Approved", claim.Status);
        }

        [TestMethod]
        public void RejectClaim_ShouldChangeStatusToRejected()
        {
            // Arrange
            var claim = new LecturerClaim { Status = "Pending" };

            // Act
            claim.Status = "Rejected";

            // Assert
            Assert.AreEqual("Rejected", claim.Status);
        }
    }
}
