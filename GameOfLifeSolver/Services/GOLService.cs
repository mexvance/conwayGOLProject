using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeSolver.Services
{
    public class GOLService
    {
        private readonly GOLAPIService _apiService;

        public GOLService(GOLAPIService apiService)
        {
            _apiService = apiService;
        }
        public async Task<string> GetToken(string name)
        {
            var result = await _apiService.GetToken(name);
            return result.Token;
        }

        public async Task<(string State, Board board, int? Generations)> PostUpdate(string token, int generationsComputed)
        {
            var result = await _apiService.PostUpdateAsync(token, generationsComputed);
            return (result.gamestate, new Board(result.seedBoard.Select(c => new Cell(c.X, c.Y)).ToList()), result.generationsToCompute);
        }

        public async Task<Board> SolveBoard(Board board)
        {
            var newBoard = new Board();
            foreach(var cell in board.Cells)
            {
                board.SetNewCell(cell,ref newBoard);
                solveNeighborCells(cell, board, ref newBoard);
            }
            return newBoard;
        }

        private void solveNeighborCells(Cell cell, Board oldBoard, ref Board newBoard)
        {
            //var neighbors = new List<Cell> {
            //    new Cell(cell.X -1, cell.Y - 1),
            //    new Cell(cell.X -1, cell.Y),
            //    new Cell(cell.X -1, cell.Y + 1),
            //    new Cell(cell.X, cell.Y - 1),
            //    new Cell(cell.X, cell.Y + 1),
            //    new Cell(cell.X +1, cell.Y -1),
            //    new Cell(cell.X +1, cell.Y),
            //    new Cell(cell.X +1, cell.Y + 1),
            //};
            foreach(var c in cell.neighbors)
            {
                oldBoard.SetNewCell(c, ref newBoard);
            }
        }
    }

    public class Board
    {
        public Board(List<Cell> cells)
        {
            Cells = cells;
        }
        public Board()
        {
            Cells = new List<Cell>();
        }
        public List<Cell> Cells { get; set; }
        public void Add(Cell cell)
        {
            if(Cells.Any(c=> c == cell))
            {
                return;
            }
            Cells.Add(cell);
        }
        public void Remove(Cell cell)
        {
            Cells = Cells.Where(c => c != cell).ToList();
        }
        public int GetCellNeighbors(Cell cell)
        {
            return Cells.Where(c => c.CellIsNeighbor(cell)).ToList().Count;
        }
        public void SetNewCell(Cell cell, ref Board newboard)
        {
            var neighbors = GetCellNeighbors(cell);
            var alive = Cells.Any(c => c == cell);
            if (alive && neighbors >= 2 && neighbors <= 3)
            {
                newboard.Add(cell);
            }
            else if(!alive && neighbors == 3)
            {
                newboard.Add(cell);
            }
            return;
        }
    }

    public class Cell
    {
        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public List<Cell> neighbors { get => new List<Cell> {
                new Cell(X -1, Y - 1),
                new Cell(X -1, Y),
                new Cell(X -1, Y + 1),
                new Cell(X, Y - 1),
                new Cell(X, Y + 1),
                new Cell(X +1, Y -1),
                new Cell(X +1, Y),
                new Cell(X +1, Y + 1),
            };
        }

        public override bool Equals(object obj)
        {
            return obj is Cell cell &&
                   X == cell.X &&
                   Y == cell.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public bool CellIsNeighbor(Cell cell)
        {
            return X <= cell.X + 1 && X >= cell.X - 1 && Y <= cell.Y + 1 && Y >= cell.Y - 1 && this != cell;
        }

        public static bool operator ==(Cell right, Cell left)
        {
            return right.X == left.X && right.Y == left.Y;
        }
        public static bool operator !=(Cell right, Cell left)
        {
            return !(right == left);
        }
    }
}
