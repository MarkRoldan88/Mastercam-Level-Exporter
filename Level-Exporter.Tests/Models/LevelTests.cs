using NUnit.Framework;
using Level_Exporter.Models;
using System.Collections.Generic;
using System.Linq;

namespace Level_Exporter.Tests.Models
{
    [TestFixture]
    public class LevelTests
    {
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

        [Test]
        public void Name_IfValueIsEmptyString_SetsNameAsLevel()
        {
            string name = "";
            string expectedName = "Level";

            Level level = new Level { Name = name };

            Assert.AreEqual(expectedName, level.Name);
        }

        [Test]
        [TestCaseSource(nameof(invalidNames))]
        public void Name__IfValueContainsSymbols_SetsNameAsLevel()
        {
            List<Level> names = invalidNames.Select(name => new Level { Name = name }).ToList();

            Assert.That(names, Has.Exactly(names.Count).Matches<Level>(c => c.Name.Equals("Level")),
                $"Should contain {names.Count} names of Level");
        }

        [Test]
        [TestCaseSource(nameof(validNames))]
        public void Name_IfValidValue_SetsNameAsValue()
        {
           var levelObjects = invalidNames.Select(name => new Level { Name = name }).ToList();
           var names = levelObjects.Select(x => x.Name).ToList();
           
            CollectionAssert.AreEqual(validNames, names);
        }
    }
}
