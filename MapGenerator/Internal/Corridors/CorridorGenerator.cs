using System.Collections.Generic;
using System.Linq;
using Dungeoneer.MapGenerator.Data;
using Dungeoneer.MapGenerator.Helpers;
using Dungeoneer.MapGenerator.Rooms;

namespace Dungeoneer.MapGenerator.Corridors
{
    internal class CorridorGenerator : ICorridorGenerator
    {
        public void CreateCorridors(MapElement[,] mapElements, IEnumerable<IRoom> rooms, IRngHelper rngHelper)
        {
            var cellsToTest = new List<IPosition>();
            var alreadyCarved = new List<IPosition>();

            foreach (var room in rooms)
            {
                for (uint x = 0; x < room.RoomQuad.Width; x++)
                {
                    for (uint y = 0; y < room.RoomQuad.Height; y++)
                    {
                        alreadyCarved.Add(new Position(x + room.RoomQuad.Position.X, y + room.RoomQuad.Position.Y));
                    }
                }
            }

            // Find the first cell we can use to start generating corridors... 
            for (uint i = 0; i < mapElements.GetLength(1); i++)
            {
                if (mapElements[0, i] != MapElement.Empty)
                {
                    continue;
                }
                
                // Mark the cell as a corridor and get it's adjacent cells
                alreadyCarved.Add(new Position(0, i));
                mapElements[0, i] = MapElement.Corridor;
                cellsToTest = MapElementHelper.GetAdjacentCells(new Position(0, i), mapElements).ToList();
                break;
            }

            while (cellsToTest.Count > 0)
            {
                // Choose one of the cells...
                var randomCell = rngHelper.Next(0, cellsToTest.Count - 1);
                if (CanCarveCorridor(cellsToTest[randomCell], mapElements))
                {
                    mapElements[cellsToTest[randomCell].X, cellsToTest[randomCell].Y] = MapElement.Corridor;
                    alreadyCarved.Add(cellsToTest[randomCell]);
                    cellsToTest.AddRange(MapElementHelper.GetAdjacentCells(cellsToTest[randomCell], mapElements));

                    cellsToTest = cellsToTest.Except(alreadyCarved).ToList();
                }
                else
                {
                    alreadyCarved.Add(cellsToTest[randomCell]);
                    cellsToTest.Remove(cellsToTest[randomCell]);
                }
            }            
        }

        public void RemoveDeadEnds(MapElement[,] mapElements)
        {
            var finishedPruning = false;

            while (!finishedPruning)
            {
                finishedPruning = true;

                for (uint x = 0; x < mapElements.GetLength(0); x++)
                {
                    for (uint y = 0; y < mapElements.GetLength(1); y++)
                    {
                        if (mapElements[x, y] == MapElement.Empty ||
                            mapElements[x, y] == MapElement.Room ||
                            mapElements[x, y] == MapElement.Door)
                        {
                            continue;
                        }

                        var adjacentCells = MapElementHelper.GetAdjacentCells(new Position(x, y), mapElements);
                        var occupiedCells = adjacentCells.Count(cell => mapElements[cell.X, cell.Y] == MapElement.Corridor || 
                                                                        mapElements[cell.X, cell.Y] == MapElement.Door);

                        if (occupiedCells != 1)
                        {
                            continue;
                        }
                        
                        mapElements[x, y] = MapElement.Empty;
                        finishedPruning = false;
                    }
                }
            } 
        }
        
        private bool CanCarveCorridor(IPosition cellToCarve, MapElement[,] mapElements)
        {
            var cells = MapElementHelper.GetAdjacentCells(cellToCarve, mapElements);

            var cellEnumeration = cells as IPosition[] ?? cells.ToArray();
            
            if (!cellEnumeration.Any())
            {
                return false;
            }
            
            var adjacentCorridors = 0;

            foreach (var cell in cells)
            {
                if (mapElements[cell.X, cell.Y] == MapElement.Corridor)
                {
                    adjacentCorridors++;
                }
            }

            return adjacentCorridors < 2;
        }
        
        
    }
}