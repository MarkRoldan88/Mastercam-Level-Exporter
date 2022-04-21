namespace Level_Exporter.Tests.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Level_Exporter.Models;
    using Level_Exporter.ViewModels;
    using Moq;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class LevelInfoViewModelTests
    {
        [Test]
        public void SelectAllCommand_ShouldSetLevelIsSelectedProperty_ToMatchIsSelectAll()
        {
            // Arrange
            Level levelA = new Level { IsSelected = false };
            Level levelB = new Level { IsSelected = false };

            Mock<ILevelInfo> levelInfoHelperMock = new Mock<ILevelInfo>();

            LevelInfoViewModel.LevelInfoHelper = levelInfoHelperMock.Object;

            levelInfoHelperMock.SetupGet(x => x.Levels).Returns(new ObservableCollection<Level>
            {
                levelA, levelB
            });

            LevelInfoViewModel levelInfoViewModel = new LevelInfoViewModel { IsSelectAll = true };

            // Act

            levelInfoViewModel.SelectAll.Execute(null);

            // Assert
            Assert.That(levelInfoViewModel.Levels.ToList(),
                Has.All.Matches<Level>(x => x.IsSelected == levelInfoViewModel.IsSelectAll),
                "All levels.IsSelected property must match isSelectAll");
        }

        [Test]
        public void ReadMastercamLevelsCommand_ShouldPopulateLevelsCollection_WithLevels()
        {
            //Arrange 
            var iLevelInfoSub = Substitute.For<ILevelInfo>();

            string expectedNameA = "level5";
            string expectedNameB = "level10";
            int expectedCountA = 5;
            int expectedCountB = 10;

            iLevelInfoSub.GetLevelsWithGeometry().Returns(new Dictionary<int, string>
            {
                { expectedCountA, expectedNameA }, {expectedCountB, expectedNameB }
            });

            iLevelInfoSub.Levels.Returns(new ObservableCollection<Level>());
            iLevelInfoSub.GetLevelEntityCount(5).Returns(5);
            iLevelInfoSub.GetLevelEntityCount(10).Returns(10);

            LevelInfoViewModel.LevelInfoHelper = iLevelInfoSub;
            LevelInfoViewModel levelInfoViewModel = new LevelInfoViewModel();

            //Act
            levelInfoViewModel.ReadMastercamLevels.Execute(null);

            //Assert
            iLevelInfoSub.Received().GetLevelEntityCount(5);
            iLevelInfoSub.Received().GetLevelEntityCount(10);

            Assert.Multiple(() =>
            {
                CollectionAssert.IsNotEmpty(levelInfoViewModel.Levels,
                    "Collection should not be empty if valid information is collected");

                CollectionAssert.AllItemsAreInstancesOfType(levelInfoViewModel.Levels, typeof(Level),
                    $"All items of collections must be of type {typeof(Level)}");

                Assert.That(levelInfoViewModel.Levels,
                    Has.One.Matches<Level>(lvl => lvl.Name.Equals(expectedNameA) && lvl.EntityCount == expectedCountA),
                    "Collection should contain entry with expected property values");

                Assert.That(levelInfoViewModel.Levels,
                    Has.One.Matches<Level>(lvl => lvl.Name.Equals(expectedNameB) && lvl.EntityCount == expectedCountB),
                    "Collection should contain entry with expected property values");
            });

        }

    }
}
