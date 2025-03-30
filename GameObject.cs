using Raylib_cs;
using System.Numerics;
public abstract class GameObject
{
    public int _x;
    public int _y;

    protected Color _color;

    public GameObject(int x, int y, Color color)
    {
        _x = x;
        _y = y;
        _color = color;
    }

    public abstract void Draw();

    public virtual void HandleInput()
    {
        // default behavior is to do nothing
    }

    public virtual void ProcessActions()
    {
        // default behavior is to do nothing
    }

    public virtual void CollideWith(GameObject other)
    {
        // default behavior is to do nothing.
    }
}