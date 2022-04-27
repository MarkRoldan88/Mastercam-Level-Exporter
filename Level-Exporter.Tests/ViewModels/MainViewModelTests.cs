namespace Level_Exporter.Tests.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Level_Exporter.ViewModels;
    using NUnit.Framework;

    [TestFixture]
    public class MainViewModelTests
    {
        //TODO Add tests for
        // 2. File output for on ok command

        public static List<string> ValidDestinationPaths = new List<string>
        {
            "C:\\Projects\\apiLibrary \\", "C:\\Documents\\Newsletters\\Test",
            "D:\\User\\Newsletters\\", "d:\\User", "   ",
            string.Empty, Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
        };

        public static List<string> InvalidDestinationPaths = new List<string>
        {
            "Css:\\Projects\\apiLibrary \\", "C:::\\Documents\\Newsletters\\Test",
            ":\\User\\Newsletters\\", "d:\\>User",
            "d", "d:\\?", "d\\", "C:\\Documents\\Newsletters\\Test.stl", "C:\\Documents\\News**letters\\Test",
            "C:\\Documents\\Newsletters\\Test+++", "C:\\Docume<nts\\Newsletters\\Test",
            "C    :   \\Documents\\Newsletters\\Test"
        };

        [Test]
        [TestCaseSource(nameof(ValidDestinationPaths))]
        public void DestinationDirectorySetter_ShouldSetToValue_IfValueIsValid(string expected)
        {
            // Arrange
            MainViewModel mainViewModel = new MainViewModel { DestinationDirectory = expected };

            // Assert
            Assert.AreEqual(expected, mainViewModel.DestinationDirectory, 
                $"{nameof(mainViewModel.DestinationDirectory)} Should match valid value");
        }

        [Test]
        [TestCaseSource(nameof(InvalidDestinationPaths))]
        public void DestinationDirectorySetter_ShouldSetToDesktop_IfValueIsInvalid(string invalidValue)
        {
            string expectedDesktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            MainViewModel mainViewModel = new MainViewModel { DestinationDirectory = invalidValue };

            Assert.AreEqual(expectedDesktop, mainViewModel.DestinationDirectory,
                $"{nameof(mainViewModel.DestinationDirectory)} should be set to {expectedDesktop} if value provided is invalid");
        }


    }
}
