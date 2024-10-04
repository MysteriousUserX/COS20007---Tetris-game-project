using SplashKitSDK;

public class GameManager
{
    private static GameManager? _instance;
    private ScoreManager _scoreManager;
    private InputManager _inputManager;
    private TetrominoManager _tetrominoManager;
    private GridManager _gridManager;
    private DrawManager _drawManager;
    private SoundManager _soundManager;
    private SplashKitSDK.Timer _fallTimer;

    private int _level;
    private int _linesCleared;
    private double _gameSpeed;
    private GameState _gameState;

    private GameManager()
    {
        _scoreManager = new ScoreManager();
        _inputManager = new InputManager();
        _tetrominoManager = new TetrominoManager();
        _gridManager = new GridManager();
        _drawManager = new DrawManager();
        _soundManager = new SoundManager();
        _fallTimer = new SplashKitSDK.Timer("FallTimer");
        _fallTimer.Start();

        _level = 1;
        _linesCleared = 0;
        _gameSpeed = 500; // Initial game speed in milliseconds
        _gameState = GameState.StartMenu;
    }

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameManager();
            }
            return _instance;
        }
    }

    public void RunGame()
    {
        Window gameWindow = new Window("Tetris", 600, 600);

        while (!gameWindow.CloseRequested && !_inputManager.ExitRequested)
        {
            SplashKit.ProcessEvents();
            SplashKit.ClearScreen(Color.Black);

            switch (_gameState)
            {
                case GameState.StartMenu:
                    _drawManager.DrawStartMenu();
                    if (SplashKit.KeyTyped(KeyCode.SpaceKey))
                    {
                        StartNewGame();
                    }
                    break;

                case GameState.Running:
                    _inputManager.HandleInput(_tetrominoManager.CurrentTetromino, _gridManager.Grid);

                    if (_inputManager.DebugLevelUpRequested)
                    {
                        _linesCleared += 10; // Simulate lines cleared for debug
                        UpdateLevelAndSpeed(true); // Force level up
                        _inputManager.DebugLevelUpRequested = false; // Reset the flag
                    }

                    if (_fallTimer.Ticks > _gameSpeed)
                    {
                        _fallTimer.Reset();

                        if (_tetrominoManager.CurrentTetromino.CanMoveDown(_gridManager.Grid))
                        {
                            _tetrominoManager.CurrentTetromino.MoveDown(_gridManager.Grid);
                        }
                        else
                        {
                            _soundManager.PlayDrop(); // Play drop sound effect
                            _gridManager.PlaceTetromino(_tetrominoManager.CurrentTetromino);
                            int linesCleared = _gridManager.ClearCompleteLines();

                            if (linesCleared > 0)
                            {
                                if (linesCleared >= 4)
                                {
                                    _soundManager.PlayTetrisClear(); // Play Tetris clear sound
                                }
                                else
                                {
                                    switch (linesCleared)
                                    {
                                        case 1:
                                            _soundManager.PlaySingleClear();
                                            break;
                                        case 2:
                                            _soundManager.PlayDoubleClear();
                                            break;
                                        case 3:
                                            _soundManager.PlayTripleClear();
                                            break;
                                    }
                                }
                            }

                            _scoreManager.UpdateScore(linesCleared);
                            _linesCleared += linesCleared;
                            UpdateLevelAndSpeed(false); // Normal level up check

                            if (!_tetrominoManager.SpawnNextTetromino(_gridManager.Grid))
                            {
                                _soundManager.StopBGM(); // Stop background music
                                _soundManager.PlayGameOver();
                                _gameState = GameState.GameOver;
                            }
                        }
                    }

                    _drawManager.Draw(_tetrominoManager.CurrentTetromino, _tetrominoManager.NextTetromino, _gridManager.Grid, _gridManager.Colors, _scoreManager.Score, _scoreManager.HighScore, _level, _linesCleared);
                    break;

                case GameState.GameOver:
                    _drawManager.DrawGameOver(_scoreManager.Score, _scoreManager.HighScore);
                    if (SplashKit.KeyTyped(KeyCode.RKey))
                    {
                        StartNewGame();
                    }
                    break;
            }

            SplashKit.RefreshScreen(60); // 60 FPS
        }

        _scoreManager.SaveHighScore();
    }

    private void UpdateLevelAndSpeed(bool forceLevelUp)
    {
        if ((_linesCleared >= _level * 10 && _level < 10) || (forceLevelUp && _level < 10))
        {
            _level++;
            _gameSpeed *= 0.8; // Increase falling speed by reducing delay
        }
    }

    private void StartNewGame()
    {
        _level = 1;
        _linesCleared = 0;
        _gameSpeed = 500; // Reset game speed
        _scoreManager.Reset();
        _gridManager.Reset();
        _tetrominoManager.Reset();
        _gameState = GameState.Running;
        _fallTimer.Reset(); // Ensure the fall timer is reset at the start of a new game
        _soundManager.PlayBGM(); // Play background music
    }
}
