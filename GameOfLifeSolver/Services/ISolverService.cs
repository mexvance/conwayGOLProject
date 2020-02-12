using GameOfLifeSolver.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLifeSolver.Services
{
    public interface ISolverService
    {
        IEnumerable<Cell> Solve(IEnumerable<Cell> startingBoard, long numGenerations);

        List<Cell> SolveGeneration(List<Cell> resultBoard);
    }
}
