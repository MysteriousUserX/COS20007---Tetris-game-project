using SplashKitSDK;
using TetrisGame;

public class DrawManager
{
    private const double ScoreX = 350; // Adjusted X position for score and preview
    private const double ScoreY = 50;
    private const double PreviewX = 350; // Adjusted X position for the preview area
    private const double PreviewY = 150; // Y position for the preview area (right under the score)
    private const double PreviewSize = 120; // Size of the preview area
    private const int BlockSize = 30;

    public void Draw(Tetromino currentTetromino, Tetromino nextTetromino, int[,] grid, Color[,] colors, int score, int highScore, int level, int linesCleared)
    {
        // Draw the grid with stored colors and grid lines
        DrawGrid(grid, colors);

        // Draw the current Tetromino with color based on the tetromino type
        DrawTetromino(currentTetromino, GetColorForTetromino(currentTetromino.Type));

        // Draw the next Tetromino with color based on its type
        DrawNextTetromino(nextTetromino, GetColorForTetromino(nextTetromino.Type));

        // Display score and high score
        SplashKit.LoadFont("Jetbrain", "JetBrainsMonoNL-ExtraBold.ttf");
        SplashKit.DrawText("Score: " + score.ToString(), Color.White, "Jetbrain", 20, ScoreX, ScoreY);
        SplashKit.DrawText("Lines Cleared: " + linesCleared.ToString(), Color.White, "Jetbrain", 20, ScoreX, ScoreY + 30);
        SplashKit.DrawText("Next: ", Color.White, "Jetbrain", 20, ScoreX, ScoreY + 60);
        SplashKit.DrawText("Level: " + level.ToString(), Color.White, "Jetbrain", 20, ScoreX, ScoreY + 250);
        SplashKit.DrawText("High Score: " + highScore.ToString(), Color.White, "Jetbrain", 20, ScoreX, ScoreY + 280);
    }

    private void DrawGrid(int[,] grid, Color[,] colors)
    {
        for (int y = 0; y < grid.GetLength(0); y++)
        {
            for (int x = 0; x < grid.GetLength(1); x++)
            {
                double blockX = x * BlockSize;
                double blockY = y * BlockSize;
                // Draw grid lines
                SplashKit.DrawRectangle(Color.DarkGray, blockX, blockY, BlockSize, BlockSize);
                if (grid[y, x] != 0)
                {
                    // Use DrawBlockTexture to draw the block with the border
                    DrawBlockTexture(blockX, blockY, BlockSize, BlockSize, colors[y, x]);
                }
            }
        }
    }

    private void DrawTetromino(Tetromino tetromino, Color color)
    {
        foreach (var block in tetromino.Blocks)
        {
            double x = (tetromino.Position.X + block.X) * BlockSize;
            double y = (tetromino.Position.Y + block.Y) * BlockSize;
            DrawBlockTexture(x, y, BlockSize, BlockSize, color);
        }
    }

    private void DrawNextTetromino(Tetromino tetromino, Color color)
    {
        double centerX = PreviewX + (PreviewSize / 2);
        double centerY = PreviewY + (PreviewSize / 2);

        // Calculate the offset to center the tetromino in the preview box
        double offsetX = 0;
        double offsetY = 0;

        switch (tetromino.Type)
        {
            case TetrominoType.I:
                offsetX = 60; // Offset for I shape
                offsetY = 15;
                break;
            case TetrominoType.O:
                offsetX = 30; // Offset for O shape
                offsetY = 30;
                break;
            case TetrominoType.T:
            case TetrominoType.S:
            case TetrominoType.Z:
            case TetrominoType.J:
            case TetrominoType.L:
                offsetX = 15; // Offset for other shapes
                offsetY = 30;
                break;
        }

        foreach (var block in tetromino.Blocks)
        {
            double x = centerX + (block.X * BlockSize) - offsetX;
            double y = centerY + (block.Y * BlockSize) - offsetY;
            DrawBlockTexture(x, y, BlockSize, BlockSize, color);
        }

        // Draw a border around the preview area
        SplashKit.DrawRectangle(Color.White, PreviewX, PreviewY, PreviewSize, PreviewSize);
    }

    private void DrawBlockTexture(double x, double y, double width, double height, Color color)
    {
        // Create lighter and darker variations of the color
        Color darkColor = SplashKit.HSBColor(SplashKit.HueOf(color), SplashKit.SaturationOf(color), SplashKit.BrightnessOf(color) * 0.9);
        Color lightColor = SplashKit.HSBColor(SplashKit.HueOf(color), SplashKit.SaturationOf(color), SplashKit.BrightnessOf(color) * 1.1);

        // Create a border color that is slightly darker than the main color
        Color borderColor = SplashKit.HSBColor(SplashKit.HueOf(color), SplashKit.SaturationOf(color), SplashKit.BrightnessOf(color) * 0.8);

        // Fill the block with the main color
        SplashKit.FillRectangle(color, x, y, width, height);


        // Draw a simple 3D effect for the block texture
        // Top highlight
        SplashKit.FillTriangle(lightColor, x, y, x + width, y, x + width / 2, y + height / 2);

        // Bottom shadow
        SplashKit.FillTriangle(darkColor, x, y + height, x + width, y + height, x + width / 2, y + height / 2);

        // Border for depth
        SplashKit.DrawRectangle(Color.Black, x, y, width, height);
    }


    private Color GetColorForTetromino(TetrominoType type)
    {
        // Return different colors based on the tetromino type
        switch (type)
        {
            case TetrominoType.I:
                return Color.Cyan;
            case TetrominoType.O:
                return Color.Yellow;
            case TetrominoType.T:
                return Color.Purple;
            case TetrominoType.S:
                return Color.Green;
            case TetrominoType.Z:
                return Color.Red;
            case TetrominoType.J:
                return Color.Blue;
            case TetrominoType.L:
                return Color.Orange;
            default:
                return Color.White;
        }
    }

    public void DrawStartMenu()
    {
        // Draw the start menu screen
        SplashKit.ClearScreen(Color.Black);
        SplashKit.LoadFont("Jetbrain", "JetBrainsMonoNL-ExtraBold.ttf");
        SplashKit.DrawText("TETRIS", Color.Cyan, "Jetbrain", 50, 90, 200);
        SplashKit.DrawText("PRESS 'SPACE' TO START", Color.White, "Jetbrain", 30, 90, 300);
    }

    public void DrawGameOver(int score, int highScore)
    {
        // Draw the game over screen
        SplashKit.ClearScreen(Color.Black);
        SplashKit.LoadFont("Jetbrain", "JetBrainsMonoNL-ExtraBold.ttf");
        SplashKit.DrawText("GAME OVER", Color.Red, "Jetbrain", 50, 125, 200);
        SplashKit.DrawText("SCORE: " + score.ToString(), Color.White, "Jetbrain", 30, 125, 300);
        SplashKit.DrawText("HIGH SCORE: " + highScore.ToString(), Color.White, "Jetbrain", 30, 125, 350);
        SplashKit.DrawText("PRESS 'R' TO RESTART", Color.White, "Jetbrain", 20, 125, 400);
    }
}
