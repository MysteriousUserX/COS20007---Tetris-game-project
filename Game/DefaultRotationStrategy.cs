// File: src/Game/DefaultRotationStrategy.cs
using System.Collections.Generic;

namespace TetrisGame
{
    public class DefaultRotationStrategy : IRotationStrategy
    {
        public void Rotate(Tetromino tetromino)
        {
            // Temporary list to store rotated positions
            var tempBlocks = new List<Point2D>(tetromino.Blocks.Count);

            // Rotate blocks 90 degrees
            foreach (var block in tetromino.Blocks)
            {
                tempBlocks.Add(new Point2D { X = -block.Y, Y = block.X });
            }

            // Apply rotated positions to tetromino blocks
            for (int i = 0; i < tetromino.Blocks.Count; i++)
            {
                tetromino.Blocks[i] = tempBlocks[i];
            }

            // Update rotation state
            tetromino.RotationState = (tetromino.RotationState + 1) % 4;
        }
    }
}
