namespace Dungeoneer.MapGenerator.Helpers
{
    
    internal interface IRngHelper
    {
        int Next(float minimum, float maximum);
        int Next(int minimum, int maximum);
    }
}