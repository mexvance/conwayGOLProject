using GameOfLifeSolver.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLifeSolver.Models
{
    public class UpdateResponse
    {
        public GameState GameState { get; set; }
        public int? generationsToCompute { get; set; }
        public List<Cell> seedBoard { get; set; }
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
    }
}
