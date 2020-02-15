using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLifeSolver.Models
{
    public class CompletedRequest
    {
        public string token { get; set; }
        public int generationsComputed { get; set; }
        public List<Cell> ResultBoard { get; set; }
    }
}
