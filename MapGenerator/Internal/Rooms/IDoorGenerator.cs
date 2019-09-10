using System.Collections.Generic;
using Dungeoneer.MapGenerator.Helpers;

namespace Dungeoneer.MapGenerator.Rooms
{
    internal interface IDoorGenerator
    {
        void PlaceDoors(MapElement[,] mapElements, IEnumerable<IRoom> rooms, IRngHelper rngHelper);
    }
}