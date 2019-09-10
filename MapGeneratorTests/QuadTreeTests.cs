using Dungeoneer.MapGenerator.Data;
using Dungeoneer.MapGenerator.Helpers;
using Dungeoneer.MapGenerator.QuadTree;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class QuadTreeTests
    {
        private const int DungeonSeed = 1;

        private IRngHelper _rngHelper;

        [SetUp]
        public void SetUp()
        {
            _rngHelper = new RngHelper(DungeonSeed);
        }

        [Test]
        public void When_QuadTreeConstructed_Given_SingleSplitAndWidthHeightDimensionsOf100_Then_QuadTreeCreated()
        {
            const uint expectedArea = 10000;
            
            var systemUnderTest = new QuadTree(
                _rngHelper, 
                new Quad(new Position(0, 0), 100, 100),
                1,
                10);
            
            Assert.IsNotNull(systemUnderTest.TopLeftQuadTree);
            Assert.IsNotNull(systemUnderTest.TopRightQuadTree);
            Assert.IsNotNull(systemUnderTest.BottomLeftQuadTree);
            Assert.IsNotNull(systemUnderTest.BottomRightQuadTree);
            
            Assert.That(expectedArea == 
                        systemUnderTest.TopLeftQuadTree.QuadArea +
                        systemUnderTest.TopRightQuadTree.QuadArea + 
                        systemUnderTest.BottomLeftQuadTree.QuadArea +
                        systemUnderTest.BottomRightQuadTree.QuadArea);
        }

        [Test]
        public void When_QuadTreeConstructed_Given_TwoSplitsAndWidthHeightDimensionsOf100_Then_QuadTreeCreated()
        {
            const uint expectedArea = 2500;
            
            var systemUnderTest = new QuadTree(
                _rngHelper, 
                new Quad(new Position(0, 0), 50, 50),
                2,
                5);
            
            Assert.IsNotNull(systemUnderTest.TopLeftQuadTree);
            Assert.IsNotNull(systemUnderTest.TopRightQuadTree);
            Assert.IsNotNull(systemUnderTest.BottomLeftQuadTree);
            Assert.IsNotNull(systemUnderTest.BottomRightQuadTree);
            
            Assert.IsNotNull(systemUnderTest.TopLeftQuadTree.TopLeftQuadTree);
            Assert.IsNotNull(systemUnderTest.TopLeftQuadTree.TopRightQuadTree);
            Assert.IsNotNull(systemUnderTest.TopLeftQuadTree.BottomLeftQuadTree);
            Assert.IsNotNull(systemUnderTest.TopLeftQuadTree.BottomRightQuadTree);
            
            Assert.IsNotNull(systemUnderTest.TopRightQuadTree.TopLeftQuadTree);
            Assert.IsNotNull(systemUnderTest.TopRightQuadTree.TopRightQuadTree);
            Assert.IsNotNull(systemUnderTest.TopRightQuadTree.BottomLeftQuadTree);
            Assert.IsNotNull(systemUnderTest.TopRightQuadTree.BottomRightQuadTree);
            
            Assert.IsNotNull(systemUnderTest.BottomLeftQuadTree.TopLeftQuadTree);
            Assert.IsNotNull(systemUnderTest.BottomLeftQuadTree.TopRightQuadTree);
            Assert.IsNotNull(systemUnderTest.BottomLeftQuadTree.BottomLeftQuadTree);
            Assert.IsNotNull(systemUnderTest.BottomLeftQuadTree.BottomRightQuadTree);
            
            Assert.IsNotNull(systemUnderTest.BottomRightQuadTree.TopLeftQuadTree);
            Assert.IsNotNull(systemUnderTest.BottomRightQuadTree.TopRightQuadTree);
            Assert.IsNotNull(systemUnderTest.BottomRightQuadTree.BottomLeftQuadTree);
            Assert.IsNotNull(systemUnderTest.BottomRightQuadTree.BottomRightQuadTree);
            
            Assert.That(expectedArea == 
                        systemUnderTest.TopLeftQuadTree.QuadArea +
                        systemUnderTest.TopRightQuadTree.QuadArea + 
                        systemUnderTest.BottomLeftQuadTree.QuadArea +
                        systemUnderTest.BottomRightQuadTree.QuadArea);
            
            Assert.That(systemUnderTest.TopLeftQuadTree.QuadArea == 
                        systemUnderTest.TopLeftQuadTree.TopLeftQuadTree.QuadArea +
                        systemUnderTest.TopLeftQuadTree.TopRightQuadTree.QuadArea +
                        systemUnderTest.TopLeftQuadTree.BottomLeftQuadTree.QuadArea +
                        systemUnderTest.TopLeftQuadTree.BottomRightQuadTree.QuadArea);
            
            Assert.That(systemUnderTest.TopRightQuadTree.QuadArea ==
                        systemUnderTest.TopRightQuadTree.TopLeftQuadTree.QuadArea +
                        systemUnderTest.TopRightQuadTree.TopRightQuadTree.QuadArea +
                        systemUnderTest.TopRightQuadTree.BottomLeftQuadTree.QuadArea +
                        systemUnderTest.TopRightQuadTree.BottomRightQuadTree.QuadArea);
            
            Assert.That(systemUnderTest.BottomLeftQuadTree.QuadArea ==
                        systemUnderTest.BottomLeftQuadTree.TopLeftQuadTree.QuadArea +
                        systemUnderTest.BottomLeftQuadTree.TopRightQuadTree.QuadArea +
                        systemUnderTest.BottomLeftQuadTree.BottomLeftQuadTree.QuadArea +
                        systemUnderTest.BottomLeftQuadTree.BottomRightQuadTree.QuadArea);
            
            Assert.That(systemUnderTest.BottomRightQuadTree.QuadArea ==
                        systemUnderTest.BottomRightQuadTree.TopLeftQuadTree.QuadArea +
                        systemUnderTest.BottomRightQuadTree.TopRightQuadTree.QuadArea +
                        systemUnderTest.BottomRightQuadTree.BottomLeftQuadTree.QuadArea +
                        systemUnderTest.BottomRightQuadTree.BottomRightQuadTree.QuadArea);
        }
    }
}