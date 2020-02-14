using GameOfLifeSolver.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLifeSolver.Services
{
    public class SolverService
    {


        public static event EventHandler<SolverEventArgs> GenerationBatchCompleted;
        public static IEnumerable<Cell> Solve(IEnumerable<Cell> startingBoard, long numGenerations)
        {
            var resultBoard = new List<Cell>(startingBoard);

            for (long generation = 0; generation < numGenerations; generation++)
            {
                var solvedBoard = SolveGeneration(resultBoard);
                resultBoard = solvedBoard;
                if (generation % 5 == 0)
                    GenerationBatchCompleted?.Invoke(null, new SolverEventArgs(generation));
            }

            return resultBoard;
        }

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
            var distinctList = list.Distinct();
            return distinctList.ToList();
        }

        public static int FindNeighborCount(Cell cell, IEnumerable<Cell> board)
        {
            var neighbors = 0;
            if (board.Any(c => c == cell.UpperLeft))
                neighbors++;
            if (board.Any(c => c == cell.UpperMiddle))
                neighbors++;
            if (board.Any(c => c == cell.UpperRight))
                neighbors++;
            if (board.Any(c => c == cell.Left))
                neighbors++;
            if (board.Any(c => c == cell.Right))
                neighbors++;
            if (board.Any(c => c == cell.LowerLeft))
                neighbors++;
            if (board.Any(c => c == cell.LowerMiddle))
                neighbors++;
            if (board.Any(c => c == cell.LowerRight))
                neighbors++;
            return neighbors;
        }
        public class SolverEventArgs : EventArgs
        {
            public SolverEventArgs(long generationsComputed)
            {
                GenerationsComputed = generationsComputed;
            }

            public long GenerationsComputed { get; set; }
        }

    }
}
