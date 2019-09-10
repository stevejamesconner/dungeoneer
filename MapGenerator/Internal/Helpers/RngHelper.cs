using System;

namespace Dungeoneer.MapGenerator.Helpers
{
    public class RngHelper : IRngHelper
    {
        private Random RngSystem { get; }
        
        public RngHelper(int seed)
        {
            RngSystem = new Random(seed);
        }

        public int Next(float minimum, float maximum)
        {
            return Next((int)minimum, (int)maximum);
        }

        public int Next(int minimum, int maximum)
        {
            return RngSystem.Next(minimum, maximum);
        }
    }
}