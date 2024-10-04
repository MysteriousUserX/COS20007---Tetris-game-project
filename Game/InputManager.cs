// File: src/Game/InputManager.cs
using SplashKitSDK;
using TetrisGame;

public class InputManager
{
    private SplashKitSDK.Timer _leftTimer;
    private SplashKitSDK.Timer _rightTimer;
    private SplashKitSDK.Timer _downTimer;
    private const int MoveInterval = 150; // Time interval in milliseconds for left/right movement
    private const int DownMoveInterval = 100; // Time interval in milliseconds for down movement
    public bool DebugLevelUpRequested { get; set; }

    public bool ExitRequested { get; private set; }

    public InputManager()
    {
        _leftTimer = new SplashKitSDK.Timer("LeftTimer");
        _leftTimer.Start();
        _rightTimer = new SplashKitSDK.Timer("RightTimer");
        _rightTimer.Start();
        _downTimer = new SplashKitSDK.Timer("DownTimer");
        _downTimer.Start();
        ExitRequested = false;
    }

    public void HandleInput(Tetromino currentTetromino, int[,] grid)
    {
        if (SplashKit.KeyTyped(KeyCode.EscapeKey))
        {
            ExitRequested = true;
        }
        DebugLevelUpRequested = SplashKit.KeyTyped(KeyCode.UKey);
        if (SplashKit.KeyDown(KeyCode.LeftKey) && _leftTimer.Ticks > MoveInterval)
        {
            currentTetromino.MoveLeft(grid);
            _leftTimer.Reset();
        }
        if (SplashKit.KeyDown(KeyCode.RightKey) && _rightTimer.Ticks > MoveInterval)
        {
            currentTetromino.MoveRight(grid);
            _rightTimer.Reset();
        }
        if (SplashKit.KeyDown(KeyCode.DownKey) && _downTimer.Ticks > DownMoveInterval)
        {
            currentTetromino.MoveDown(grid);
            _downTimer.Reset();
        }
        if (SplashKit.KeyTyped(KeyCode.UpKey))
        {
            currentTetromino.Rotate(grid);
            SplashKit.PlaySoundEffect("rotate"); // Play rotate sound
        }
    }
}
