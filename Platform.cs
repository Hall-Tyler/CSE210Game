using Raylib_cs;
using System.Numerics;

class Platform : GameObject
{
    public int _width;
    public int _height;
    private int _speed = 5;

    public Platform(int x, int y, int width, int height, Color color) : base(x, y, color)
    {
        _width = width;
        _height = height;
    }

    public override void Draw()
    {
        Raylib.DrawRectangle(_x, _y, _width, _height, _color);
    }

    public override void HandleInput()
    {
        if (Raylib.IsKeyDown(KeyboardKey.A))
        {
            _x -= _speed;
        }
        if (Raylib.IsKeyDown(KeyboardKey.D))
        {
            _x += _speed;
        }

        if (_x < 0) _x = 0;
        if (_x + _width > GameManager.SCREEN_WIDTH) _x = GameManager.SCREEN_WIDTH - _width;
    }
}
