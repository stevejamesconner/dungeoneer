namespace Dungeoneer.MapGenerator.Data
{
    public class Position : IPosition
    {
        public uint X { get; }
        public uint Y { get; }

        public Position(uint x, uint y)
        {
            X = x;
            Y = y;
        }
        
        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(Position))
            {
                return ((Position)obj).X == X && ((Position)obj).Y == Y;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return (int)(X * 13 + Y);
        }
    }
}