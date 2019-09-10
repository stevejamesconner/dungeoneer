using System.Collections.Generic;
using Dungeoneer.MapGenerator.Data;

namespace Dungeoneer.MapGenerator.Helpers
{
    internal static class MapElementHelper
    {
        internal static IEnumerable<IPosition> GetAdjacentCells(IPosition currentCell, MapElement[,] dungeonElements)
        {
            var cells = new List<IPosition>();

            if ((int)currentCell.X - 1 >= 0)
            {
                cells.Add(new Position(currentCell.X - 1, currentCell.Y));
            }

            if (currentCell.X + 1 < dungeonElements.GetLength(0))
            {
                cells.Add(new Position(currentCell.X + 1, currentCell.Y));
            }

            if ((int)currentCell.Y - 1 >= 0)
            {
                cells.Add(new Position(currentCell.X, currentCell.Y - 1));
            }
            
            if (currentCell.Y + 1 < dungeonElements.GetLength(1))
            {
                cells.Add(new Position(currentCell.X, currentCell.Y + 1));
            }

            return cells;
        }
    }
}