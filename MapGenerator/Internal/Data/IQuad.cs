namespace Dungeoneer.MapGenerator.Data
{
    internal interface IQuad
    {
        IPosition Position { get; }
        uint Width { get; }
        uint Height { get; }
        uint Area { get; }
    }
}