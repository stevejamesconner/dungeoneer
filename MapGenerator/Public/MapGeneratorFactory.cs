namespace Dungeoneer.MapGenerator
{
    public static class MapGeneratorFactory
    {
        public static IMapGenerator Create(IMapGeneratorConfiguration mapGeneratorConfiguration)
        {
            return new MapGenerator(mapGeneratorConfiguration); 
        }
    }
}