using Raylib_cs;

public class GameManager
{
    public const int SCREEN_WIDTH = 800;
    public const int SCREEN_HEIGHT = 600;

    private string _title;
    private int points = 0;
    bool gameOver = false;

    public List<GameObject> _gameObjects = new List<GameObject>();
    private List<GameObject> _toRemove = new List<GameObject>();

    public GameManager()
    {
        _title = "CSE 210 Game";
    }

    /// <summary>
    /// The overall loop that controls the game. It calls functions to
    /// handle interactions, update game elements, and draw the screen.
    /// </summary>
    public void Run()
    {
        Raylib.SetTargetFPS(60);
        Raylib.InitWindow(SCREEN_WIDTH, SCREEN_HEIGHT, _title);
        // If using sound, un-comment the lines to init and close the audio device
        // Raylib.InitAudioDevice();

        InitializeGame();

        while (!Raylib.WindowShouldClose())
        {
            Raylib.DrawText(points.ToString(), 25, 25, 50, Color.Green);
            if (gameOver)
            {
                Raylib.DrawText("Game Over", GameManager.SCREEN_WIDTH / 2 - 100, GameManager.SCREEN_HEIGHT / 2 - 20, 40, Color.Red);

                if (Raylib.IsKeyPressed(KeyboardKey.Enter))
                {
                    break;
                }
            }
            else
            {
                HandleInput();
                ProcessActions();

                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.White);

                DrawElements();
            }

            Raylib.EndDrawing();
        }

        // Raylib.CloseAudioDevice();
        Raylib.CloseWindow();
    }

    /// <summary>
    /// Sets up the initial conditions for the game.
    /// </summary>
    private void InitializeGame()
    {
        Platform platform = new Platform((SCREEN_WIDTH / 2) - 50, SCREEN_HEIGHT - 50, 100, 10, Color.Blue);
        _gameObjects.Add(platform);
    }

    /// <summary>
    /// Responds to any input from the user.
    /// </summary>
    private void HandleInput()
    {
        foreach (GameObject gameObject in _gameObjects)
        {
            gameObject.HandleInput();   
        }
    }

    /// <summary>
    /// Processes any actions such as moving objects or handling collisions.
    /// </summary>
    int counter = 100;
    Random rand = new Random();
    private void ProcessActions()
    {
        if (counter >= 100)
        {
            int item = rand.Next(0, 2);
            int xPos = rand.Next(20, SCREEN_WIDTH - 20);
            if (item == 0)
            {
                Treasure treasure = new Treasure(xPos, 50, Color.White, CollectTreasure);
                _gameObjects.Add(treasure);
            }
            else
            {
                Bomb bomb = new Bomb(xPos, 50, 10, Color.Red, HitBomb);
                _gameObjects.Add(bomb);
            }
            counter = 0;
        }
        foreach (GameObject gameObject in _gameObjects)
        {
            gameObject.ProcessActions();

            foreach (var otherObject in _gameObjects)
            {
                if (gameObject != otherObject)
                {
                    gameObject.CollideWith(otherObject);
                }
            }
            if (gameObject._y >= SCREEN_HEIGHT)
            {
                DeleteGameObject(gameObject);
            }
        }
        counter++;
    }

    /// <summary>
    /// Draws all elements on the screen.
    /// </summary>
    private void DrawElements()
    {
        foreach (GameObject gameObject in _gameObjects)
        {
            gameObject.Draw();
        }

        foreach (var obj in _toRemove)
        {
            _gameObjects.Remove(obj);
        }
        _toRemove.Clear();
    }

    public void CollectTreasure(GameObject gameObject)
    {
        points += 10;
        DeleteGameObject(gameObject);
    }

    public void HitBomb(GameObject gameObject)
    {
        gameOver = true;
        DeleteGameObject(gameObject);
    }

    public void DeleteGameObject(GameObject gameObject)
    {
        _toRemove.Add(gameObject);
    }
}