// File: src/Game/TetrominoFactory.cs
namespace TetrisGame
{
    public static class TetrominoFactory
    {
        public static Tetromino CreateTetromino(TetrominoType type)
        {
            IRotationStrategy rotationStrategy = new DefaultRotationStrategy();
            return new Tetromino(type, rotationStrategy);
        }
    }
}
