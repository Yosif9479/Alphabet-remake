namespace Additionals
{
    public static class Random
    {
        public static int Range(int start, int end)
        {
            return UnityEngine.Random.Range(start, end);
        }

        public static float Range(float start, float end)
        {
            return UnityEngine.Random.Range(start, end);
        }

        public static bool Boolean() => UnityEngine.Random.Range(0, 2) == 1;
    }
}
