using System;
using NUnit.Framework;
using Moq;
using Level_Exporter.Models;

namespace Level_Exporter.Tests.Models
{
    [TestFixture]
    public class LevelTests
    {
        [Test]
        public void NameProperty_ValueIsEmptyString_SetsNameAsLevel()
        {
            string name = "";
            string expectedName = "Level";

            Level level = new Level { Name = name };

            Assert.AreEqual(expectedName, level.Name);
        }

    }
}
