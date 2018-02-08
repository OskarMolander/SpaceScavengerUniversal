using Microsoft.Xna.Framework;

namespace Space_Scavenger
{
    public interface IGameObject
    {
        int     Health   { get; set; }
        float   Radius   { get; set; }
        float   Rotation { get; set; }
        bool    IsDead   { get; set; }
        Vector2 Position { get; set; }
        Vector2 Speed    { get; set; }
    }
}