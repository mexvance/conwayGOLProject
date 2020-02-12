using GameOfLifeSolver.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLifeSolver.Services
{
    public interface ISolverService
    {
        IEnumerable<UpdateResponseBoardSquare> Solve(IEnumerable<UpdateResponseBoardSquare> startingBoard, long numGenerations);

        List<UpdateResponseBoardSquare> SolveGeneration(List<UpdateResponseBoardSquare> resultBoard);
    }
}
