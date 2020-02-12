using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GameOfLifeSolver.Models
    {
        public class UpdateResponseBoardSquare
        {
            public UpdateResponseBoardSquare() { }

            public UpdateResponseBoardSquare(int x, int y)
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
        }
    }