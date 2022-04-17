namespace Level_Exporter.Tests.Models
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Level_Exporter.Models;
    using NUnit.Framework;

    [TestFixture]
    public class CadFormatTests
    { 
        [Test]
        public void FileExtension_SetterShouldPrependValue_WithPeriod()
        {
            const string expectedValue = ".stl";

            CadFormat cadFormat = new CadFormat(CadTypes.Stl);

            Assert.AreEqual(expectedValue,cadFormat.FileExtension);
        }

        [Test]
        public void FileExtension_ShouldNotContain_UppercaseLetters()
        {
            var invalidFormat = new Regex(@"([^a-z])");


        }

        [Test]
        public void Description_StringShouldBe_SpecificFormat()
        {
            //Example: "AutoCAD Drawing File (*.stl)"
            var validFormat = new Regex(@"((^[A-Z\s]{1,})+\w+\s+\({1}\*{1}.{1}([a-z]{1,})\){1}$)", RegexOptions.IgnoreCase);
            var invalidFormatExamples = new List<string>
            {
                "Test (*.stl)sss",
                "12 (*.stl)",
                "12 (*.stl)sss",
                "12 (.stl)",
                "ss (sss*sss.stl)",
            };

            string actualDescription = new CadFormat(CadTypes.Emcam).Description;

            Assert.Multiple(() =>
            {
                Assert.IsTrue(validFormat.IsMatch(actualDescription));

                Assert.That(invalidFormatExamples,
                    Has.Exactly(invalidFormatExamples.Count).Matches<string>(invalidFormat => !validFormat.IsMatch(invalidFormat)),
                    "Should not contain any valid regex match");
            });

        }
    }
}
