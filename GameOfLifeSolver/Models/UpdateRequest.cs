using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLifeSolver.Models
{
    public class UpdateRequest
    {
        public string token { get; set; }
        public int generationsComputed { get; set; }
    }
}
