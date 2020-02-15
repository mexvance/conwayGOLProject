using GameOfLifeSolver.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLifeSolver.Services
{
    public class SolverService
    {
        public static List<Cell> SolveGeneration(List<Cell> board)
        {
            var list = new List<Cell>();
            foreach(var cell in board)
            {
                var neighbors = FindNeighborCount(cell, board);
                list = SetLivingCells(neighbors, cell, board, ref list);
            }

            return list;
        }

        private static List<Cell> SetLivingCells(int neighbors, Cell cell, List<Cell> board, ref List<Cell>list)
        { 
            if (neighbors == 2 || neighbors == 3)
            {
                list.Add(cell);
            }

            foreach (var neighbor in cell.Neighbors)
            {
                var neighborIsCurrentlyDead = !board.Any(c => c == neighbor);
                if (neighborIsCurrentlyDead && FindNeighborCount(neighbor, board) == 3)
                    list.Add(neighbor);
            }
            return list.Distinct().ToList();
        }

        public static int FindNeighborCount(Cell cell, IEnumerable<Cell> board)
        {
            return board.Where(c => c.CellIsNeighbor(cell)).Count();
        }

    }
}
