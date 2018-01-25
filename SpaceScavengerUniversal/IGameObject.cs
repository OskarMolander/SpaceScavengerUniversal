using Microsoft.Xna.Framework;

namespace Space_Scavenger
{
    public interface IGameObject
    {
        bool IsDead { get; set; }
        Vector2 Position { get; set; }
        float Radius { get; set; }
        Vector2 Speed { get; set; }
        float Rotation { get; set; }
        int Health { get; set; }
    }
}
