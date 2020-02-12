using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GameOfLifeSolver.Models
    {
        public class Cell
        {
            public Cell() { }

            public Cell(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int X { get; set; }
            public int Y { get; set; }

            public override string ToString()
            {
                return $"{{{X},{Y}}}";
            }
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public Cell LowerMiddle => new Cell(X, Y - 1);
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public Cell LowerRight => new Cell(X + 1, Y - 1);
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public Cell LowerLeft => new Cell(X - 1, Y - 1);
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public Cell Right => new Cell(X + 1, Y);
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public Cell Left => new Cell(X - 1, Y);
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public Cell UpperRight => new Cell(X + 1, Y + 1);
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public Cell UpperMiddle => new Cell(X, Y + 1);
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public Cell UpperLeft => new Cell(X - 1, Y + 1);
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public IEnumerable<Cell> Neighbors => new[] { LowerMiddle, LowerRight, LowerLeft, Right, Left, UpperRight, UpperMiddle, UpperLeft };
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