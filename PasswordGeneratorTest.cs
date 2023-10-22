using CipherShield;
using Xunit.Sdk;

namespace CypherShield_TestProject
{
    [TestClass]
    public class PasswordGeneratorTest
    {
        [TestMethod]
        public void TestGenerateRandomPassword_LengthOnly()
        {
            PasswordGenerator passwordGenerator = new PasswordGenerator();
            string password = passwordGenerator.GenerateRandomPassword(10, true, true, true, true, "", '\0');
            Assert.AreEqual(10, password.Length, "Generated password length should match the specified length.");
        }

        [TestMethod]
        public void TestGenerateRandomPassword_WithUppercase()
        {
            PasswordGenerator passwordGenerator = new PasswordGenerator();
            string password = passwordGenerator.GenerateRandomPassword(10, true, false, false, false, "", '\0');
            Assert.IsTrue(ContainsCharactersFromSet(password, "ABCDEFGHIJKLMNOPQRSTUVWXYZ"), "Generated password should include uppercase characters.");
        }

        [TestMethod]
        public void TestGenerateRandomPassword_WithLowercase()
        {
            PasswordGenerator passwordGenerator = new PasswordGenerator();
            string password = passwordGenerator.GenerateRandomPassword(10, false, true, false, false, "", '\0');
            Assert.IsTrue(ContainsCharactersFromSet(password, "abcdefghijklmnopqrstuvwxyz"), "Generated password should include lowercase characters.");
        }

        [TestMethod]
        public void TestGenerateRandomPassword_WithNumbers()
        {
            PasswordGenerator passwordGenerator = new PasswordGenerator();
            string password = passwordGenerator.GenerateRandomPassword(10, false, false, true, false, "", '\0');
            Assert.IsTrue(ContainsCharactersFromSet(password, "0123456789"), "Generated password should include numbers.");
        }

        [TestMethod]
        public void TestGenerateRandomPassword_WithSymbols()
        {
            PasswordGenerator passwordGenerator = new PasswordGenerator();
            string password = passwordGenerator.GenerateRandomPassword(10, false, false, false, true, "", '\0');
            Assert.IsTrue(ContainsCharactersFromSet(password, "!@#$%^&*()_+"), "Generated password should include symbols.");
        }

        [TestMethod]
        public void TestGenerateRandomPassword_ExcludeSymbols()
        {
            PasswordGenerator passwordGenerator = new PasswordGenerator();
            string password = passwordGenerator.GenerateRandomPassword(10, true, true, true, true, "#", '\0');
            Assert.IsFalse(password.Contains("#"), "Generated password should not contain excluded symbol '#'.");
        }

        [TestMethod]
        public void TestGenerateRandomPassword_ExcludeAllSymbols()
        {
            // Test when all symbols are excluded, should not contain symbols.
            PasswordGenerator passwordGenerator = new PasswordGenerator();
            string password = passwordGenerator.GenerateRandomPassword(12, true, true, true, true, "!@#$%^&*()_+", '\0');
            Assert.IsTrue(!ContainsCharactersFromSet(password, "!@#$%^&*()_+"), "Generated password should not contain any excluded symbols.");
        }

        [TestMethod]
        public void TestGenerateRandomPassword_ValidInputs()
        {
            // Test with valid inputs and expected output.
            PasswordGenerator passwordGenerator = new PasswordGenerator();
            string password = passwordGenerator.GenerateRandomPassword(12, true, true, true, true, "@", '*');
            Assert.AreEqual(12, password.Length, "Generated password length should match the specified length.");
            Assert.IsFalse(password.Contains("@"), "Generated password should not contain the excluded symbol '@'.");
            Assert.IsTrue(password.Contains("*"), "Generated password should contain the required symbol '*'.");
        }

        [TestMethod]
        public void TestGenerateRandomPassword_WithRequiredSymbol()
        {
            PasswordGenerator passwordGenerator = new PasswordGenerator();
            char requiredSymbol = '!';
            string password = passwordGenerator.GenerateRandomPassword(10, true, true, true, true, "", requiredSymbol);
            Assert.IsTrue(password.Contains(requiredSymbol.ToString()), $"Generated password should contain the required symbol '{requiredSymbol}'.");
        }

        [TestMethod]
        public void TestGenerateRandomPassword_RequiredSymbolNotInAllowedSymbols()
        {
            PasswordGenerator passwordGenerator = new PasswordGenerator();
            char requiredSymbol = '=';
            string password = passwordGenerator.GenerateRandomPassword(10, true, true, true, false, "", requiredSymbol);
            Assert.AreEqual("", password, "If required symbol is not in the list of allowed symbols, the function should return an empty string.");
        }

        [TestMethod]
        public void TestGenerateRandomPassword_LengthZero()
        {
            // Test when the requested password length is zero, should return an empty string.
            PasswordGenerator passwordGenerator = new PasswordGenerator();
            string password = passwordGenerator.GenerateRandomPassword(0, true, true, true, true, "", '\0');
            Assert.AreEqual("", password, "If the requested password length is zero, the function should return an empty string.");
        }

        [TestMethod]
        public void TestGenerateRandomPassword_AllFalseInputs()
        {
            PasswordGenerator passwordGenerator = new PasswordGenerator();
            string password = passwordGenerator.GenerateRandomPassword(10, false, false, false, false, "", '\0');
            Assert.AreEqual("", password, "If all character types are set to false, the function should return an empty string.");
        }

        private bool ContainsCharactersFromSet(string input, string characterSet)
        {
            foreach (char c in input)
            {
                if (characterSet.Contains(c.ToString()))
                {
                    return true;
                }
            }
            return false;
        }

    }
}