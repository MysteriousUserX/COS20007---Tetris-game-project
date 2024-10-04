// File: src/Game/Tetromino.cs
using System.Collections.Generic;

namespace TetrisGame
{
    public enum TetrominoType
    {
        I, O, T, S, Z, J, L
    }


    public class Point2D
    {
        public int X { get; set; }
        public int Y { get; set; }
    }


    public class Tetromino
    {
        private IRotationStrategy _rotationStrategy;

        public TetrominoType Type { get; private set; }
        public List<Point2D> Blocks { get; private set; }
        public int RotationState { get; set; }  // Make this property public with a set accessor
        public Point2D Position { get; set; }

        public Tetromino(TetrominoType type, IRotationStrategy rotationStrategy)
        {
            Type = type;
            Blocks = new List<Point2D>();
            RotationState = 0;
            Position = new Point2D { X = 5, Y = 0 }; // Start at the top center of the grid
            _rotationStrategy = rotationStrategy;
            InitializeBlocks();
        }

        private void InitializeBlocks()
        {
            switch (Type)
            {
                case TetrominoType.I:
                    Blocks.Add(new Point2D { X = 0, Y = 0 });
                    Blocks.Add(new Point2D { X = 1, Y = 0 });
                    Blocks.Add(new Point2D { X = 2, Y = 0 });
                    Blocks.Add(new Point2D { X = 3, Y = 0 });
                    break;
                case TetrominoType.O:
                    Blocks.Add(new Point2D { X = 0, Y = 0 });
                    Blocks.Add(new Point2D { X = 1, Y = 0 });
                    Blocks.Add(new Point2D { X = 0, Y = 1 });
                    Blocks.Add(new Point2D { X = 1, Y = 1 });
                    break;
                case TetrominoType.T:
                    Blocks.Add(new Point2D { X = 0, Y = 0 });
                    Blocks.Add(new Point2D { X = -1, Y = 0 });
                    Blocks.Add(new Point2D { X = 1, Y = 0 });
                    Blocks.Add(new Point2D { X = 0, Y = 1 });
                    break;
                case TetrominoType.S:
                    Blocks.Add(new Point2D { X = 0, Y = 0 });
                    Blocks.Add(new Point2D { X = -1, Y = 0 });
                    Blocks.Add(new Point2D { X = 0, Y = 1 });
                    Blocks.Add(new Point2D { X = 1, Y = 1 });
                    break;
                case TetrominoType.Z:
                    Blocks.Add(new Point2D { X = 0, Y = 0 });
                    Blocks.Add(new Point2D { X = 1, Y = 0 });
                    Blocks.Add(new Point2D { X = 0, Y = 1 });
                    Blocks.Add(new Point2D { X = -1, Y = 1 });
                    break;
                case TetrominoType.J:
                    Blocks.Add(new Point2D { X = 0, Y = 0 });
                    Blocks.Add(new Point2D { X = -1, Y = 0 });
                    Blocks.Add(new Point2D { X = 1, Y = 0 });
                    Blocks.Add(new Point2D { X = 1, Y = 1 });
                    break;
                case TetrominoType.L:
                    Blocks.Add(new Point2D { X = 0, Y = 0 });
                    Blocks.Add(new Point2D { X = -1, Y = 0 });
                    Blocks.Add(new Point2D { X = 1, Y = 0 });
                    Blocks.Add(new Point2D { X = -1, Y = 1 });
                    break;
            }
        }

        public void Rotate(int[,] grid)
        {
            if (CanRotate(grid))
            {
                _rotationStrategy.Rotate(this);
            }
        }

        public bool CanMoveLeft(int[,] grid)
        {
            foreach (var block in Blocks)
            {
                int newX = Position.X + block.X - 1;
                int newY = Position.Y + block.Y;
                if (newX < 0 || grid[newY, newX] != 0) return false;
            }
            return true;
        }

        public bool CanMoveRight(int[,] grid)
        {
            foreach (var block in Blocks)
            {
                int newX = Position.X + block.X + 1;
                int newY = Position.Y + block.Y;
                if (newX >= 10 || grid[newY, newX] != 0) return false;
            }
            return true;
        }

        public bool CanMoveDown(int[,] grid)
        {
            foreach (var block in Blocks)
            {
                int newX = Position.X + block.X;
                int newY = Position.Y + block.Y + 1;
                if (newY >= 20 || grid[newY, newX] != 0) return false;
            }
            return true;
        }

        public bool CanRotate(int[,] grid)
        {
            var tempBlocks = new List<Point2D>(Blocks.Count);
            foreach (var block in Blocks)
            {
                tempBlocks.Add(new Point2D { X = -block.Y, Y = block.X });
            }

            foreach (var block in tempBlocks)
            {
                int newX = Position.X + block.X;
                int newY = Position.Y + block.Y;
                if (newX < 0 || newX >= 10 || newY < 0 || newY >= 20 || grid[newY, newX] != 0) return false;
            }
            return true;
        }

        public void MoveLeft(int[,] grid)
        {
            if (CanMoveLeft(grid))
            {
                Position.X -= 1;
            }
        }

        public void MoveRight(int[,] grid)
        {
            if (CanMoveRight(grid))
            {
                Position.X += 1;
            }
        }

        public void MoveDown(int[,] grid)
        {
            if (CanMoveDown(grid))
            {
                Position.Y += 1;
            }
        }
        public bool CanBePlaced(int[,] grid)
        {
            foreach (var block in Blocks)
            {
                int x = (int)(Position.X + block.X);
                int y = (int)(Position.Y + block.Y);

                if (x < 0 || x >= grid.GetLength(1) || y < 0 || y >= grid.GetLength(0) || grid[y, x] != 0)
                {
                    return false;
                }
            }
            return true;
        }

        public void Rotate()
        {
            _rotationStrategy.Rotate(this);
        }
    }
}
