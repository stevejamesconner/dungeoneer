namespace Dungeoneer.MapGenerator
{
    public interface IMapGenerator
    {
        MapElement[,] CreateMap();
    }
}