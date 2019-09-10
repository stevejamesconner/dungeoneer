using Dungeoneer.MapGenerator.Data;

namespace Dungeoneer.MapGenerator.QuadTree
{
    internal interface IQuadTree
    {
        IQuadTree TopLeftQuadTree { get; }
        IQuadTree TopRightQuadTree { get; }
        IQuadTree BottomLeftQuadTree { get; }
        IQuadTree BottomRightQuadTree { get; }
        
        uint QuadArea { get; }
        Quad Quad { get; }
        bool HasChildren { get; }
    }
}