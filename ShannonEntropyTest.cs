using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CipherShield;

namespace CypherShield_TestProject
{
    [TestClass]
    public class ShannonEntropyTest
    {
        /*
        [TestMethod]
        public void CheckStrength_WhenStrongPassword_Returns100()
        {
            // Arrange
            ShannonEntropy shannonEntropy = new ShannonEntropy();
            string strongPassword = "P@ssw0rd!";

            // Act
            int strength = shannonEntropy.CheckStrength(strongPassword);

            // Assert
            Assert.AreEqual(100, strength);
        }
        */

        [TestMethod]
        public void CheckStrength_WhenWeakPassword_ReturnsLessThan100()
        {
            // Arrange
            ShannonEntropy shannonEntropy = new ShannonEntropy();
            string weakPassword = "password123";

            // Act
            int strength = shannonEntropy.CheckStrength(weakPassword);

            // Assert
            Assert.IsTrue(strength < 100);
        }

        [TestMethod]
        public void CheckStrength_WhenEmptyPassword_Returns0()
        {
            // Arrange
            ShannonEntropy shannonEntropy = new ShannonEntropy();
            string emptyPassword = string.Empty;

            // Act
            int strength = shannonEntropy.CheckStrength(emptyPassword);

            // Assert
            Assert.AreEqual(0, strength);
        }

        /*
        [TestMethod]
        public void CheckStrength_WhenNullPassword_Returns0()
        {
            // Arrange
            ShannonEntropy shannonEntropy = new ShannonEntropy();
            string nullPassword = null;

            // Act
            int strength = shannonEntropy.CheckStrength(nullPassword);

            // Assert
            Assert.AreEqual(0, strength);
        }
        */
    }
}
