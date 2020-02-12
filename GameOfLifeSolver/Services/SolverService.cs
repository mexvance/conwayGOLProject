using GameOfLifeSolver.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLifeSolver.Services
{
    public class SolverService
    {
        public static IEnumerable<Cell> Solve(IEnumerable<Cell> startingBoard, long numGenerations)
        {
            var resultBoard = new List<Cell>(startingBoard);

            for (long generation = 0; generation < numGenerations; generation++)
            {
                Console.Write($"{generation} ");
                resultBoard = SolveGeneration(resultBoard);

            }

            return resultBoard;
        }

        public static List<Cell> SolveGeneration(List<Cell> resultBoard)
        {
            return new List<Cell>();
        }
        
        public static int FindNeighborCount(Cell cell, IEnumerable<Cell> board)
        {
            return cell.neighbors.Where(c => c.CellIsNeighbor(cell)).ToList().Count;
        }

    }
}
