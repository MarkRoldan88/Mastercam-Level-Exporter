namespace Level_Exporter.Tests.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using Level_Exporter.Models;
    using Level_Exporter.ViewModels;
    using Moq;
    using NUnit.Framework;
    using NSubstitute;

    [TestFixture]
    public class LevelInfoViewModelTests
    {
        [Test]
        public void SelectAllCommand_ShouldSetLevelIsSelectedProperty_ToMatchIsSelectAll()
        {
            // Arrange
            Level levelA = new Level { IsSelected = false };
            Level levelB = new Level { IsSelected = false };

            LevelInfoViewModel.LevelInfoHelper = Substitute.For<ILevelInfo>();
            Mock<ILevelInfo> levelInfoHelperMock = new Mock<ILevelInfo>();
            LevelInfoViewModel.LevelInfoHelper = levelInfoHelperMock.Object;

            levelInfoHelperMock.SetupGet(x => x.Levels).Returns(new ObservableCollection<Level>
            {
                levelA, levelB
            });

            LevelInfoViewModel levelInfoViewModel = new LevelInfoViewModel()
            {
                IsSelectAll = true
            };

            // Act

            levelInfoViewModel.SelectAll.Execute(null);

            // Assert
            Assert.That(levelInfoViewModel.Levels.ToList(),
                Has.All.Matches<Level>(x => x.IsSelected == levelInfoViewModel.IsSelectAll),
                "All levels.IsSelected property must match isSelectAll");
        }

    }
}
