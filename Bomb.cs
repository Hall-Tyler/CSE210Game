using Raylib_cs;
using System.Numerics;
using static Raylib_cs.Raylib;

class Bomb : GameObject
{
    int _radius;
    int speed = 5;

    public Action<GameObject> OnCollisionWithPlatform;

    public Bomb(int x, int y, int radius, Color color, Action<GameObject> onCollisionCallback) : base(x, y, color)
    {
        _radius = radius;
        OnCollisionWithPlatform = onCollisionCallback;
    }

    public override void Draw()
    {
        Raylib.DrawCircle(_x, _y, _radius, _color);
    }

    public override void ProcessActions()
    {
        _y += speed;
    }

    public override void CollideWith(GameObject other)
    {
        if (other is Platform platform)
        {
            Rectangle platformRect = new Rectangle(platform._x, platform._y, platform._width, platform._height);

            if (CheckCollisionCircleRec(new Vector2(_x, _y), (float)_radius, platformRect))
            {
                OnCollisionWithPlatform?.Invoke(this);
            }
        }
    }

}
