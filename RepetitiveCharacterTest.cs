using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CipherShield;

namespace CypherShield_TestProject
{
    [TestClass]
    public class RepetitiveCharacterTest
    {
        [TestMethod]
        public void CheckStrength_NoRepetitiveChars_Returns100()
        {
            // Arrange
            RepetitiveCharacterDetect repetitiveCharacterDetect = new RepetitiveCharacterDetect();
            string password = "StrongPassword";

            // Act
            int strength = repetitiveCharacterDetect.CheckStrength(password);

            // Assert
            Assert.AreEqual(100, strength);
        }

        /*
        [TestMethod]
        public void CheckStrength_FewRepetitiveChars_Returns70()
        {
            // Arrange
            RepetitiveCharacterDetect repetitiveCharacterDetect = new RepetitiveCharacterDetect();
            string password = "ModeratePass11233";

            // Act
            int strength = repetitiveCharacterDetect.CheckStrength(password);

            // Assert
            Assert.AreEqual(70, strength);
        }
        */

        [TestMethod]
        public void CheckStrength_ManyRepetitiveChars_Returns40()
        {
            // Arrange
            RepetitiveCharacterDetect repetitiveCharacterDetect = new RepetitiveCharacterDetect();
            string password = "WeakPass$$$";

            // Act
            int strength = repetitiveCharacterDetect.CheckStrength(password);

            // Assert
            Assert.AreEqual(40, strength);
        }

        [TestMethod]
        public void CheckStrength_EmptyPassword_Returns100()
        {
            // Arrange
            RepetitiveCharacterDetect repetitiveCharacterDetect = new RepetitiveCharacterDetect();
            string emptyPassword = string.Empty;

            // Act
            int strength = repetitiveCharacterDetect.CheckStrength(emptyPassword);

            // Assert
            Assert.AreEqual(100, strength);
        }

        /*
        [TestMethod]
        public void CheckStrength_NullPassword_Returns100()
        {
            // Arrange
            RepetitiveCharacterDetect repetitiveCharacterDetect = new RepetitiveCharacterDetect();
            string nullPassword = null;

            // Act
            int strength = repetitiveCharacterDetect.CheckStrength(nullPassword);

            // Assert
            Assert.AreEqual(100, strength);
        }
        */
    }
}
