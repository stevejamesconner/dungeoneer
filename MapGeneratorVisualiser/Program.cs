using Dungeoneer.MapGenerator;

namespace MapGeneratorVisualiser
{
    class Program
    {
        static void Main(string[] args)
        {
            var mapConfiguration = new MapGeneratorConfiguration(244040, 50, 50, 2, 8, 20);
            var mapGenerator = MapGeneratorFactory.Create(mapConfiguration);

            var map = mapGenerator.CreateMap();
        }
    }
}