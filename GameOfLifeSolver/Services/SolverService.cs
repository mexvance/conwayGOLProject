using GameOfLifeSolver.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLifeSolver.Services
{
    public class SolverService
    {
        public static IEnumerable<UpdateResponseBoardSquare> Solve(IEnumerable<UpdateResponseBoardSquare> startingBoard, long numGenerations)
        {
            var resultBoard = new List<UpdateResponseBoardSquare>(startingBoard);

            for (long generation = 0; generation < numGenerations; generation++)
            {
                Console.Write($"{generation} ");
                resultBoard = SolveGeneration(resultBoard);

            }

            return resultBoard;
        }

        public static List<UpdateResponseBoardSquare> SolveGeneration(List<UpdateResponseBoardSquare> resultBoard)
        {
            return new List<UpdateResponseBoardSquare>();
        }
    }
}
