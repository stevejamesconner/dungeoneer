using Dungeoneer.MapGenerator.Data;
using Dungeoneer.MapGenerator.Helpers;
using Dungeoneer.MapGenerator.QuadTree;
using Dungeoneer.MapGenerator.Rooms;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class RoomGeneratorTests
    {
        private const int DungeonSeed = 1;

        private IRngHelper _rngHelper;
        private IQuadTree _quadTree;

        [SetUp]
        public void SetUp()
        {
            _rngHelper = new RngHelper(DungeonSeed);
            _quadTree = new QuadTree(_rngHelper, new Quad(new Position(0, 0), 100, 100), 2, 10);
        }

        [Test]
        public void When()
        {
            var sut = new RoomGenerator();
            var rooms = sut.Generate(_quadTree, _rngHelper, 70);
            
            Assert.IsNotEmpty(rooms);
        }
    }
}