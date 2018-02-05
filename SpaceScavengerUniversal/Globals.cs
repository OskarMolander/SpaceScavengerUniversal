namespace Space_Scavenger
{
    public static class Globals
    {
        public static int ScreenWidth = 1920;
        public static int ScreenHeight = 1080;

        public static string CenterString(this string stringToCenter, int totalLength)
        {
            return stringToCenter.PadLeft(((totalLength - stringToCenter.Length) / 2)
                                + stringToCenter.Length)
                       .PadRight(totalLength);
        }
    }
}
