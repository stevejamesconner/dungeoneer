using Dungeoneer.MapGenerator.Data;

namespace Dungeoneer.MapGenerator.Rooms
{
    internal class Room : IRoom
    {
        public IQuad RoomQuad { get; }

        public Room(IQuad roomQuad)
        {
            RoomQuad = roomQuad;
        }
    }
}