using System.Collections.Generic;
using Dungeoneer.MapGenerator.Data;
using Dungeoneer.MapGenerator.Helpers;
using Dungeoneer.MapGenerator.QuadTree;

namespace Dungeoneer.MapGenerator.Rooms
{
    internal class RoomGenerator : IRoomGenerator
    {
        private readonly IList<IRoom> _rooms = new List<IRoom>();

        public IEnumerable<IRoom> Generate(IQuadTree quadTree, IRngHelper rngHelper, uint minimumRoomArea)
        {
            CarveRooms(quadTree, rngHelper, minimumRoomArea);

            return _rooms;
        }

        private void CarveRooms(IQuadTree quadTree, IRngHelper rngHelper, uint minimumRoomArea)
        {
            if (quadTree.HasChildren)
            {
                CarveRooms(quadTree.TopLeftQuadTree, rngHelper, minimumRoomArea);
                CarveRooms(quadTree.TopRightQuadTree, rngHelper, minimumRoomArea);
                CarveRooms(quadTree.BottomLeftQuadTree, rngHelper, minimumRoomArea);
                CarveRooms(quadTree.BottomRightQuadTree, rngHelper, minimumRoomArea);
            }
            else
            {
                CarveRoom(quadTree.Quad, rngHelper, minimumRoomArea);
            }
        }

        private void CarveRoom(IQuad quad, IRngHelper rngHelper, uint minimumRoomArea)
        {
            const uint hardMinimum = 3;

            var maxRoomWidth = quad.Width - 2;
            var maxRoomHeight = quad.Height - 2;
            var roomWidth = (uint) rngHelper.Next(hardMinimum, maxRoomWidth);
            var roomHeight = (uint) rngHelper.Next(hardMinimum, maxRoomHeight);
            var roomArea = roomWidth * roomHeight;

            if (roomArea < minimumRoomArea || maxRoomWidth < hardMinimum || maxRoomHeight < hardMinimum)
            {
                return;
            }

            var roomx = (uint) rngHelper.Next(1, quad.Width - roomWidth);
            var roomy = (uint) rngHelper.Next(1, quad.Height - roomHeight);
            var room = new Room(new Quad(new Position(roomx + quad.Position.X, roomy + quad.Position.Y), roomWidth, roomHeight));
            
            _rooms.Add(room);
        }
    }
}