using System.Collections.Generic;
using Dungeoneer.MapGenerator.Helpers;
using Dungeoneer.MapGenerator.QuadTree;

namespace Dungeoneer.MapGenerator.Rooms
{
    internal interface IRoomGenerator
    {
        IEnumerable<IRoom> Generate(IQuadTree quadTree, IRngHelper rngHelper, uint minimumRoomArea);
    }
}