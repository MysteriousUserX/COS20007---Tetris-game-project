// File: src/Game/TetrominoManager.cs
using TetrisGame;

public class TetrominoManager
{
    private Tetromino _currentTetromino;
    private Tetromino _nextTetromino;
    public static readonly int FallInterval = 500; // Time interval in milliseconds for automatic falling

    public Tetromino CurrentTetromino => _currentTetromino;
    public Tetromino NextTetromino => _nextTetromino;

    public bool SpawnNextTetromino(int[,] grid)
    {
        _currentTetromino = _nextTetromino;
        _nextTetromino = TetrominoSpawner.GetNextTetromino();
        _currentTetromino.Position = new Point2D { X = 5, Y = 0 }; // Reset position for new tetromino

        // Check if the new tetromino can be placed without overlap
        return _currentTetromino.CanBePlaced(grid);
    }


    public void SpawnNextTetromino()
    {
        _currentTetromino = _nextTetromino;
        _nextTetromino = TetrominoSpawner.GetNextTetromino();
    }


    public void Reset()
    {
        SpawnNextTetromino();
        SpawnNextTetromino(); // Re-initialize both current and next tetrominoes
    }
}
