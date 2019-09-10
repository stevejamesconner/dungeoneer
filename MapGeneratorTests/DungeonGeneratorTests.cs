using Dungeoneer.MapGenerator;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class DungeonGeneratorTests
    {
        private IMapGenerator _sut;
        
        [SetUp]
        public void Setup()
        {
            const int dungeonSeed = 435253;
            
            var dungeonConfiguration = new MapGeneratorConfiguration(dungeonSeed, 50,50,2, 6, 12);
            _sut = MapGeneratorFactory.Create(dungeonConfiguration);
        }

        [Test]
        public void CreateDungeon()
        {
            var dungeonMap = _sut.CreateMap();
            
            Assert.IsNotEmpty(dungeonMap);
        }
    }
}