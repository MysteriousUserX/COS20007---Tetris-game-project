// File: src/Game/TetrominoSpawner.cs
using System;

namespace TetrisGame
{
    public class TetrominoSpawner
    {
        private static Random _random = new Random();

        public static Tetromino GetNextTetromino()
        {
            Array values = Enum.GetValues(typeof(TetrominoType));
            TetrominoType randomType = (TetrominoType)values.GetValue(_random.Next(values.Length));
            return TetrominoFactory.CreateTetromino(randomType);
        }
    }
}
