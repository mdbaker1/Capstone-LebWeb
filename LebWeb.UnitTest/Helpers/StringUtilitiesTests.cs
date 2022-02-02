using Microsoft.VisualStudio.TestTools.UnitTesting;
using LebWeb.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LebWeb.UnitTests
{
    [TestClass]
    public class StringUtilitiesTests
    {
        // Test to confirm only 1st char capitalized
        [TestMethod]
        public void CustomToUpperTest_1stLetterOnly()
        {
            // Arrange
            string testText = "tEsT";
            string testResultText = "Test";

            // Act
            string result = StringUtilities.CustomToUpper(testText);

            // Assert
            Assert.AreEqual(testResultText, result);
        }

        // Test to confirm 1st & 3rd char are capitalized for specific names and space trimmed at start and end
        [TestMethod]
        public void CustomToUpperTest_1stAnd3rdLetterOnlyTrimWhiteSpace()
        {
            // Arrange
            string testText = " macdonald ";
            string testResultText = "MacDonald";

            // Act
            string result = StringUtilities.CustomToUpper(testText);

            // Assert
            Assert.AreEqual(testResultText, result);
        }

        // Test to confirm 1st char capitalized and first letter after singlequote
        [TestMethod]
        public void CustomToUpperTest_1stAndAfterSingleQuote()
        {
            // Arrange
            string testText = " Bo'dell";
            string testResultText = "Bo'Dell";

            // Act
            string result = StringUtilities.CustomToUpper(testText);

            // Assert
            Assert.AreEqual(testResultText, result);
        }

        // Test to confirm 1st char capitalized and first letter after space
        [TestMethod]
        public void CustomToUpperTest_BeforeAndAfterSpace()
        {
            // Arrange
            string testText = "  van luven";
            string testResultText = "Van Luven";

            // Act
            string result = StringUtilities.CustomToUpper(testText);

            // Assert
            Assert.AreEqual(testResultText, result);
        }

        // Test to confirm 1st char capitalized and first letter after a dash
        [TestMethod]
        public void CustomToUpperTest_BeforeAndAfterDash()
        {
            // Arrange
            string testText = "alam-shoushtari";
            string testResultText = "Alam-Shoushtari";

            // Act
            string result = StringUtilities.CustomToUpper(testText);

            // Assert
            Assert.AreEqual(testResultText, result);
        }

        // Test to confirm value has white space trimmed and all chars are lower case
        [TestMethod]
        public void CustomToUpperTest_All_Lower_NotEqual()
        {
            // Arrange
            string testText = " macdonald ";
            string testResultText = "macdonald";

            // Act
            string result = StringUtilities.CustomToUpper(testText);

            // Assert
            Assert.AreNotEqual(testResultText, result);
        }

        // Test to confirm all characters return lower case with no white space at start and end
        [TestMethod]
        public void CustomToLowerTest_All_Lower_Trimmed()
        {
            // Arrange
            string testText = " MACDONALD ";
            string testResultText = "macdonald";

            // Act
            string result = StringUtilities.CustomToLower(testText);

            // Assert
            Assert.AreEqual(testResultText, result);
        }
    }
}