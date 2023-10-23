using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CipherShield;

namespace CypherShield_TestProject
{
    [TestClass]
    public class LengthDetectTest
    {
        [TestMethod]
        public void CheckStrength_LongPassword_Returns100()
        {
            // Arrange
            LengthDetect lengthDetect = new LengthDetect();
            string longPassword = "ThisIsAVeryLongPassword";

            // Act
            int strength = lengthDetect.CheckStrength(longPassword);

            // Assert
            Assert.AreEqual(100, strength);
        }

        [TestMethod]
        public void CheckStrength_ModerateLengthPassword_Returns70()
        {
            // Arrange
            LengthDetect lengthDetect = new LengthDetect();
            string moderatePassword = "MedPwd12";

            // Act
            int strength = lengthDetect.CheckStrength(moderatePassword);

            // Assert
            Assert.AreEqual(70, strength);
        }

        [TestMethod]
        public void CheckStrength_ShortPassword_Returns40()
        {
            // Arrange
            LengthDetect lengthDetect = new LengthDetect();
            string shortPassword = "Pwd123";

            // Act
            int strength = lengthDetect.CheckStrength(shortPassword);

            // Assert
            Assert.AreEqual(40, strength);
        }

        [TestMethod]
        public void CheckStrength_EmptyPassword_Returns40()
        {
            // Arrange
            LengthDetect lengthDetect = new LengthDetect();
            string emptyPassword = string.Empty;

            // Act
            int strength = lengthDetect.CheckStrength(emptyPassword);

            // Assert
            Assert.AreEqual(40, strength);
        }

        /*
        [TestMethod]
        public void CheckStrength_NullPassword_Returns40()
        {
            // Arrange
            LengthDetect lengthDetect = new LengthDetect();
            string nullPassword = null;

            // Act
            int strength = lengthDetect.CheckStrength(nullPassword);

            // Assert
            Assert.AreEqual(40, strength);
        }
        */
    }
}
