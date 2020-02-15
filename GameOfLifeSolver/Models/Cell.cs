using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GameOfLifeSolver.Models
{
    public class Cell
    {
        private List<Cell> neighbors;
    
        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }
    
        public int X { get; set; }
        public int Y { get; set; }
    
        [JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public List<Cell> Neighbors { get
            {
                if (neighbors is null)
                {
                    neighbors = new List<Cell>
                    {
                        new Cell(X - 1, Y - 1),
                        new Cell(X - 1, Y),
                        new Cell(X - 1, Y + 1),
                        new Cell(X, Y - 1),
                        new Cell(X, Y + 1),
                        new Cell(X + 1, Y - 1),
                        new Cell(X + 1, Y),
                        new Cell(X + 1, Y + 1)
                    };
                }
                return neighbors;
            }
        }


        public override string ToString()
        {
            return $"{{{X},{Y}}}";
        }

        public override bool Equals(object obj)
        {
            return obj is Cell cell &&
                   X == cell.X &&
                   Y == cell.Y;
        }
    
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    
        public bool CellIsNeighbor(Cell cell)
        {
            return X <= cell.X + 1 && X >= cell.X - 1 && Y <= cell.Y + 1 && Y >= cell.Y - 1 && this != cell;
        }
    
        public static bool operator == (Cell right, Cell left)
        {
            return right.X == left.X && right.Y == left.Y;
        }
        public static bool operator != (Cell right, Cell left)
        {
            return !(right == left);
        }
    }
}