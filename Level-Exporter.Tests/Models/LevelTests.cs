using NUnit.Framework;
using Level_Exporter.Models;
using System.Collections.Generic;
using System.Linq;

namespace Level_Exporter.Tests.Models
{
    [TestFixture]
    public class LevelTests
    {
        #region TestCaseSource

        private readonly List<string> invalidNames = new List<string>
        {
            "eLee@","$$$!L", "e##LL",
            "l%%ev", "!$LL*", "Lv:",@"\", "\""
        };

        private readonly List<string> validNames = new List<string>
        {
            "3l3La", "dj9Lj", "sm56e",
            "mlkdj", "sv4ll", "sf020",
            "9vj3d", "dedd5", "0je3s", "fvjd6"
        };

        #endregion

        [Test]
        public void Name_IfValueIsEmptyString_SetsNameAsLevel()
        {
            string name = "";
            string expectedName = "Level";

            Level level = new Level { Name = name };

            Assert.AreEqual(expectedName, level.Name,
                "If value provided to name property is an empty string, name setter should set name as 'Level'");
        }

        [Test]
        [TestCaseSource(nameof(invalidNames))]
        public void Name__IfValueContainsSymbols_SetsNameAsLevel()
        {
            List<Level> names = invalidNames.Select(name => new Level { Name = name }).ToList();

            Assert.That(names, Has.All.Matches<Level>(c => c.Name.Equals("Level")),
                $"Should contain {names.Count} names of Level");
        }

        [Test]
        [TestCaseSource(nameof(validNames))]
        public void Name_IfValidValue_SetsNameAsValue()
        {
           var levelObjects = invalidNames.Select(name => new Level { Name = name }).ToList();
           var names = levelObjects.Select(x => x.Name).ToList();
           
            CollectionAssert.AreEqual(validNames, names,
                "Name property setters should set name as value provided if it is a valid value");
        }
    }
}
