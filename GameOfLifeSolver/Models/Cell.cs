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
            public List<Cell> neighbors
            {
                get => new List<Cell> {
                    new Cell(X -1, Y - 1),
                    new Cell(X -1, Y),
                    new Cell(X -1, Y + 1),
                    new Cell(X, Y - 1),
                    new Cell(X, Y + 1),
                    new Cell(X +1, Y -1),
                    new Cell(X +1, Y),
                    new Cell(X +1, Y + 1),
                };
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