namespace Dungeoneer.MapGenerator
{
    public interface IMapGeneratorConfiguration
    {
        int MapSeed { get; }
        uint MapWidth { get; }
        uint MapHeight { get; }
        
        uint QuadTreeSplitDepth { get; }
        uint QuadTreeSplitMaximumOffset { get; }
        
        uint MinimumRoomArea { get; }
    }
}