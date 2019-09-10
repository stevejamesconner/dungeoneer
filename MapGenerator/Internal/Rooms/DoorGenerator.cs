using System.Collections.Generic;
using Dungeoneer.MapGenerator.Data;
using Dungeoneer.MapGenerator.Helpers;

namespace Dungeoneer.MapGenerator.Rooms
{
    internal class DoorGenerator : IDoorGenerator
    {
        public void PlaceDoors(MapElement[,] mapElements, IEnumerable<IRoom> rooms, IRngHelper rngHelper)
        {
            foreach (var room in rooms)
            {
                var potentialDoorPositions = new List<IPosition>();
                var maxNumberOfDoorsToPlace = 0;

                if (room.RoomQuad.Area <= 16)
                {
                    maxNumberOfDoorsToPlace = 1;
                }
                else if (room.RoomQuad.Area > 16 && room.RoomQuad.Area <= 64)
                {
                    maxNumberOfDoorsToPlace = 2;
                }
                else
                {
                    maxNumberOfDoorsToPlace = 3;
                }

                // Get each of the edge cells
                for (uint x = 0; x < room.RoomQuad.Width; x++)
                {
                    for (uint y = 0; y < room.RoomQuad.Height; y++)
                    {
                        if (x > 0 && y != 0 && x > 0 && y != room.RoomQuad.Height - 1 && y > 0 && x != 0 && y > 0 && x != room.RoomQuad.Width - 1)
                        {
                            continue;
                        }

                        if (x == 0 && y == 0 || x == room.RoomQuad.Width - 1 && y == 0 ||
                            x == 0 && y == room.RoomQuad.Height - 1 || x == room.RoomQuad.Width - 1 && y == room.RoomQuad.Height - 1)
                        {
                            continue;
                        }

                        var adjacentCells = MapElementHelper.GetAdjacentCells(new Position(x + room.RoomQuad.Position.X, y + room.RoomQuad.Position.Y), mapElements);
                        
                        foreach (var cell in adjacentCells)
                        {
                            if (mapElements[cell.X, cell.Y] == MapElement.Corridor)
                            {
                                potentialDoorPositions.Add(new Position(x + room.RoomQuad.Position.X, y + room.RoomQuad.Position.Y));
                            }
                        }
                    }
                }

                // Just keep a maximum of one door on each edge...
                var finalDoorList = new List<IPosition>();

                if (potentialDoorPositions.FindAll(i => i.X == room.RoomQuad.Position.X).Count > 0)
                {
                    finalDoorList.Add(potentialDoorPositions.FindAll(i => i.X == room.RoomQuad.Position.X)
                        [rngHelper.Next(0, potentialDoorPositions.FindAll(i => i.X == room.RoomQuad.Position.X).Count)]);
                }

                if (potentialDoorPositions.FindAll(i => i.X == room.RoomQuad.Position.X + room.RoomQuad.Width - 1).Count > 0)
                {
                    finalDoorList.Add(potentialDoorPositions.FindAll(i => i.X == room.RoomQuad.Position.X + room.RoomQuad.Width - 1)
                        [rngHelper.Next(0, potentialDoorPositions.FindAll(i => i.X == room.RoomQuad.Position.X + room.RoomQuad.Width - 1).Count)]);
                }

                if (potentialDoorPositions.FindAll(i => i.Y == room.RoomQuad.Position.Y).Count > 0)
                {
                    finalDoorList.Add(potentialDoorPositions.FindAll(i => i.Y == room.RoomQuad.Position.Y)
                        [rngHelper.Next(0, potentialDoorPositions.FindAll(i => i.Y == room.RoomQuad.Position.Y).Count)]);
                }

                if (potentialDoorPositions.FindAll(i => i.Y == room.RoomQuad.Position.Y + room.RoomQuad.Height - 1).Count > 0)
                {
                    finalDoorList.Add(potentialDoorPositions.FindAll(i => i.Y == room.RoomQuad.Position.Y + room.RoomQuad.Height - 1)
                        [rngHelper.Next(0, potentialDoorPositions.FindAll(i => i.Y == room.RoomQuad.Position.Y + room.RoomQuad.Height - 1).Count)]);
                }

                if (finalDoorList.Count < maxNumberOfDoorsToPlace)
                {
                    maxNumberOfDoorsToPlace = finalDoorList.Count;
                }

                for (var i = 0; i < maxNumberOfDoorsToPlace; i++)
                {
                    var doorPosition = rngHelper.Next(0, finalDoorList.Count);
                    var doorCell = finalDoorList[doorPosition];
                    mapElements[doorCell.X, doorCell.Y] = MapElement.Door;
                    finalDoorList.RemoveAt(doorPosition);
                }
            }
        }
    }
}