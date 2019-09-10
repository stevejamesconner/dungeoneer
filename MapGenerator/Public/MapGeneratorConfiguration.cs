namespace Dungeoneer.MapGenerator
{
    public class MapGeneratorConfiguration : IMapGeneratorConfiguration
    {
        public int MapSeed { get; }
        public uint MapWidth { get; }
        public uint MapHeight { get; }
        public uint QuadTreeSplitDepth { get; }
        public uint QuadTreeSplitMaximumOffset { get; }
        public uint MinimumRoomArea { get; }

        public MapGeneratorConfiguration(int mapSeed, uint mapWidth, uint mapHeight, uint quadTreeSplitDepth, uint quadTreeSplitMaximumOffset, uint minimumRoomArea)
        {
            MapSeed = mapSeed;
            MapWidth = mapWidth;
            MapHeight = mapHeight;
            QuadTreeSplitDepth = quadTreeSplitDepth;
            QuadTreeSplitMaximumOffset = quadTreeSplitMaximumOffset;
            MinimumRoomArea = minimumRoomArea;
        }
    }
}