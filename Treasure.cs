using Raylib_cs;
using System.Numerics;
using static Raylib_cs.Raylib;

class Treasure : GameObject
{
    private Texture2D[] coin = new Texture2D[8];
    int texCounter = 0;
    int speed = 5;

    public Action<GameObject> OnCollisionWithPlatform;

    public Treasure(int x, int y, Color color, Action<GameObject> onCollisionCallback) : base(x, y, color)
    {
        OnCollisionWithPlatform = onCollisionCallback;

        Console.WriteLine($"Current Directory: {Directory.GetCurrentDirectory()}");
        Image coin_1 = LoadImage("../../../../CSE210Game/Resources/coin_01.png");
        Image coin_2 = LoadImage("../../../../CSE210Game/Resources/coin_02.png");
        Image coin_3 = LoadImage("../../../../CSE210Game/Resources/coin_03.png");
        Image coin_4 = LoadImage("../../../../CSE210Game/Resources/coin_04.png");
        Image coin_5 = LoadImage("../../../../CSE210Game/Resources/coin_05.png");
        Image coin_6 = LoadImage("../../../../CSE210Game/Resources/coin_06.png");
        Image coin_7 = LoadImage("../../../../CSE210Game/Resources/coin_07.png");
        Image coin_8 = LoadImage("../../../../CSE210Game/Resources/coin_08.png");

        coin[0] = LoadTextureFromImage(coin_1);
        coin[1] = LoadTextureFromImage(coin_2);
        coin[2] = LoadTextureFromImage(coin_3);
        coin[3] = LoadTextureFromImage(coin_4);
        coin[4] = LoadTextureFromImage(coin_5);
        coin[5] = LoadTextureFromImage(coin_6);
        coin[6] = LoadTextureFromImage(coin_7);
        coin[7] = LoadTextureFromImage(coin_8);

        UnloadImage(coin_1);
        UnloadImage(coin_2);
        UnloadImage(coin_3);
        UnloadImage(coin_4);
        UnloadImage(coin_5);
        UnloadImage(coin_6);
        UnloadImage(coin_7);
        UnloadImage(coin_8);
    }

    int frameDelay = 10;
    int frameCount = 0;
    public override void Draw()
    {
        frameCount++;

        if (frameCount >= frameDelay)
        {
            texCounter = (texCounter + 1) % coin.Length;
            frameCount = 0;
        }

        Texture2D currentTexture = coin[texCounter];
        int textureWidth = currentTexture.Width;
        int textureHeight = currentTexture.Height;

        Rectangle sourceRect = new Rectangle(0, 0, textureWidth, textureHeight);
        Rectangle destRect = new Rectangle(_x, _y, textureWidth / 2, textureHeight / 2);
        Vector2 origin = new Vector2(textureWidth / 4.0f, textureHeight / 4.0f);
        DrawTexturePro(currentTexture, sourceRect, destRect, origin, 0.0f, Color.White);
    }

    public override void ProcessActions()
    {
        _y += speed;
    }

    public override void CollideWith(GameObject other)
    {
        if (other is Platform platform)
        {
            Rectangle treasureRect = new Rectangle(_x, _y, coin[texCounter].Width, coin[texCounter].Height);
            Rectangle platformRect = new Rectangle(platform._x, platform._y, platform._width, platform._height);

            if (Raylib.CheckCollisionRecs(treasureRect, platformRect))
            {
                OnCollisionWithPlatform?.Invoke(this);
            }
        }
    }

}
