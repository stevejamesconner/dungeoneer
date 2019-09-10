using System.Collections.Generic;
using Dungeoneer.MapGenerator.Helpers;
using Dungeoneer.MapGenerator.Rooms;

namespace Dungeoneer.MapGenerator.Corridors
{
    internal interface ICorridorGenerator
    {
        void CreateCorridors(MapElement[,] mapElements, IEnumerable<IRoom> rooms, IRngHelper rngHelper);
        void RemoveDeadEnds(MapElement[,] mapElements);
    }
}