using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SpaceScavengerUniversal
{
    public class ControllerValidator
    {
        public static bool IsGamePadConnected() => GamePad.GetCapabilities(PlayerIndex.One).IsConnected;
    }
}