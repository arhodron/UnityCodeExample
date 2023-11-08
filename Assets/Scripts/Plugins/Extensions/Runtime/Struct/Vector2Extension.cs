using UnityEngine;

namespace Plugins.Extensions
{
    public static class Vector2Extension
    {
        public static float GetRandom(this Vector2 vector)
        {
            return vector.x == vector.y ? vector.x : Random.Range(vector.x, vector.y);
        }

        public static bool InRange(this Vector2 vector, float value)
        {
            return vector.x <= value && vector.y >= value;
        }

        public static int GetRandom(this Vector2Int vector, bool isExclusive = false)
        {
            return (vector.x >= vector.y) ? vector.x : (Random.Range(vector.x, vector.y) + (isExclusive ? 0 : 1));
        }
    }
}

