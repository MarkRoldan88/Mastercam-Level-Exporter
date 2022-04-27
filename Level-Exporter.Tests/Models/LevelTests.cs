using NUnit.Framework;
using Level_Exporter.Models;
using System.Collections.Generic;

namespace Level_Exporter.Tests.Models
{
    [TestFixture]
    public class LevelTests
    {
        #region TestCaseSource

        private static readonly List<string> InvalidNames = new List<string>
        {
            "eLee@","$$$!L", "e##LL",
            "l%%ev", "!$LL*", "Lv:",@"\", "\""
        };

        private static readonly List<string> ValidNames = new List<string>
        {
            "3l3La", "dj9Lj", "sm56e",
            "mlkdj", "sv4ll", "sf020",
            "9vj3d", "dedd5", "0je3s", "fvjd6"
        };

        #endregion

        [Test]
        public void Name_IfValueIsEmptyString_SetsNameAsLevel()
        {
            string expectedName = "Level";

            Level level = new Level { Name = "" };

            Assert.AreEqual(expectedName, level.Name,
                "If value provided to name property is an empty string, name setter should set name as 'Level'");
        }

        [Test]
        [TestCaseSource(nameof(InvalidNames))]
        public void Name_IfValueContainsSymbols_SetsNameAsLevel(string invalidName)
        {
            var level = new Level { Name = invalidName };
            const string expectedName = "Level";

            Assert.AreEqual(expectedName, level.Name);
        }

        [Test]
        [TestCaseSource(nameof(ValidNames))]
        public void Name_IfValidValue_SetsNameAsValue(string validName)
        {
            var level = new Level { Name = validName };

            Assert.AreEqual(validName, level.Name, "Name property setters should set name as value provided if it is a valid value");
        }

        [Test]
        public void Name_IfValueIsOnlyWhitespace_SetsNameAsLevel()
        {
            string expectedName = "Level";

            Level level = new Level { Name = "   " };

            Assert.AreEqual(expectedName, level.Name,
                "If value provided to name property is a string only containing white space, name setter should set name as 'Level'");
        }
    }
}
