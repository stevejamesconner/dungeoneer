using Dungeoneer.MapGenerator.Data;

namespace Dungeoneer.MapGenerator.Rooms
{
    internal interface IRoom
    {
        IQuad RoomQuad { get; }
    }
}