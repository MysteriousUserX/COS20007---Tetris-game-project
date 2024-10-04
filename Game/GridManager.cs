using SplashKitSDK;
using TetrisGame;

public class GridManager
{
    private const int TotalRows = 20;
    private int[,] _grid;
    private Color[,] _colors;

    public int[,] Grid => _grid;
    public Color[,] Colors => _colors;

    public GridManager()
    {
        _grid = new int[TotalRows, 10]; // 20 rows by 10 columns
        _colors = new Color[TotalRows, 10]; // Corresponding color grid
    }

    public void PlaceTetromino(Tetromino tetromino)
    {
        foreach (var block in tetromino.Blocks)
        {
            int x = (int)(tetromino.Position.X + block.X);
            int y = (int)(tetromino.Position.Y + block.Y);
            _grid[y, x] = 1; // Mark the grid cell as occupied
            _colors[y, x] = GetColorForTetromino(tetromino.Type); // Store the color of the tetromino
        }
    }

    public int ClearCompleteLines()
    {
        int linesCleared = 0;

        for (int y = 0; y < TotalRows; y++)
        {
            bool isLineComplete = true;
            for (int x = 0; x < _grid.GetLength(1); x++)
            {
                if (_grid[y, x] == 0)
                {
                    isLineComplete = false;
                    break;
                }
            }
            if (isLineComplete)
            {
                ClearLine(y);
                linesCleared++;
            }
        }

        return linesCleared;
    }

    private void ClearLine(int line)
    {
        for (int y = line; y > 0; y--)
        {
            for (int x = 0; x < _grid.GetLength(1); x++)
            {
                _grid[y, x] = _grid[y - 1, x];
                _colors[y, x] = _colors[y - 1, x]; // Shift colors down
            }
        }
        for (int x = 0; x < _grid.GetLength(1); x++)
        {
            _grid[0, x] = 0;
            _colors[0, x] = Color.Black; // Reset the top line color to black
        }
    }

    private Color GetColorForTetromino(TetrominoType type)
    {
        switch (type)
        {
            case TetrominoType.I: return Color.Cyan;
            case TetrominoType.O: return Color.Yellow;
            case TetrominoType.T: return Color.Purple;
            case TetrominoType.S: return Color.Green;
            case TetrominoType.Z: return Color.Red;
            case TetrominoType.J: return Color.Blue;
            case TetrominoType.L: return Color.Orange;
            default: return Color.White;
        }
    }

    public bool IsGameOver()
    {
        for (int x = 0; x < _grid.GetLength(1); x++)
        {
            if (_grid[0, x] != 0 && _grid[1,x] != 0) // Check the top row
            {
                return true;
            }
        }
        return false;
    }

    public void Reset()
    {
        for (int y = 0; y < _grid.GetLength(0); y++)
        {
            for (int x = 0; x < _grid.GetLength(1); x++)
            {
                _grid[y, x] = 0;
                _colors[y, x] = Color.Black; // Reset colors
            }
        }
    }
}
