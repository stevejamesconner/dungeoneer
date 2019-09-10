using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dungeoneer.MapGenerator.Corridors;
using Dungeoneer.MapGenerator.Data;
using Dungeoneer.MapGenerator.Helpers;
using Dungeoneer.MapGenerator.QuadTree;
using Dungeoneer.MapGenerator.Rooms;

namespace Dungeoneer.MapGenerator
{
    internal class MapGenerator : IMapGenerator
    {
        private readonly IMapGeneratorConfiguration _mapGeneratorConfiguration;
        private IRngHelper _rngHelper;
        public MapGenerator(IMapGeneratorConfiguration mapGeneratorConfiguration)
        {
            _mapGeneratorConfiguration = mapGeneratorConfiguration;
            SeedMapGenerator(mapGeneratorConfiguration.MapSeed);
        }

        private void SeedMapGenerator(int seed)
        {
            _rngHelper = new RngHelper(seed);
        }

        public MapElement[,] CreateMap()
        {
            var mapElements = new MapElement[_mapGeneratorConfiguration.MapWidth, _mapGeneratorConfiguration.MapHeight];
            var mapQuads = CreateMapQuads();
            var mapRooms = CreateMapRooms(mapQuads);

            var roomArray = mapRooms as IRoom[] ?? mapRooms.ToArray();
            
            foreach (var room in roomArray)
            {
                for (var x = 0; x < room.RoomQuad.Width; x++)
                {
                    for (var y = 0; y < room.RoomQuad.Height; y++)
                    {
                        mapElements[x + room.RoomQuad.Position.X, y + room.RoomQuad.Position.Y] = MapElement.Room;
                    }
                }
            }
            
            CreateCorridors(mapElements, roomArray);

            Visualise(mapElements);
            
            return mapElements;
        }

        private IQuadTree CreateMapQuads()
        {
            var quad = new Quad(new Position(0,0), _mapGeneratorConfiguration.MapWidth, _mapGeneratorConfiguration.MapHeight);
            var quadTree = new QuadTree.QuadTree(
                _rngHelper, 
                quad, 
                _mapGeneratorConfiguration.QuadTreeSplitDepth, 
                _mapGeneratorConfiguration.QuadTreeSplitMaximumOffset);

            return quadTree;
        }

        private IEnumerable<IRoom> CreateMapRooms(IQuadTree quadTree)
        {
            var roomGenerator = new RoomGenerator();
            var rooms = roomGenerator.Generate(quadTree, _rngHelper, _mapGeneratorConfiguration.MinimumRoomArea);

            return rooms;
        }

        private void CreateCorridors(MapElement[,] dungeonElemnts, IEnumerable<IRoom> rooms)
        {
            var corridorGenerator = new CorridorGenerator();
            var doorGenerator = new DoorGenerator();
            var roomArray = rooms as IRoom[] ?? rooms.ToArray();
            
            corridorGenerator.CreateCorridors(dungeonElemnts, roomArray, _rngHelper);
            doorGenerator.PlaceDoors(dungeonElemnts, roomArray, _rngHelper);
            corridorGenerator.RemoveDeadEnds(dungeonElemnts);
        }

        private void Visualise(MapElement[,] dungeonMap)
        {
            var output = new List<StringBuilder>();
            
            for (var x = 0; x < dungeonMap.GetLength(0); x++)
            {
                var mapLine = new StringBuilder();
                
                for (var y = 0; y < dungeonMap.GetLength(1); y++)
                {
                    var mapString = string.Empty;
                    
                    switch (dungeonMap[x,y])
                    {
                        case MapElement.Room:
                            mapString = "R";
                            break;
                        case MapElement.Corridor:
                            mapString = "C";
                            break;
                        case MapElement.Door:
                            mapString = "D";
                            break;
                        default:
                            mapString = " ";
                            break;
                    }

                    mapLine.Append(mapString);
                }
                
                output.Add(mapLine);
            }

            foreach (var line in output)
            {
                Console.WriteLine(line.ToString());
            }
        }
    }
}