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

        public async Task<(string State, List<Cell> Cells, int? Generations)> PostUpdate(string token, int generationsComputed)
        {
            var result = await _apiService.PostUpdateAsync(token, generationsComputed);
            return (result.gamestate, result.seedBoard.Select(c => new Cell(c.X, c.Y)).ToList(), result.generationsToCompute);
        }

        public async Task<List<Cell>> SolveBoard(List<Cell> board)
        {
            var newBoard = new List<Cell>();
            foreach(var cell in board)
            {

            }
            return newBoard;
        }
    }

    public class Board
    {
        public Board(List<Cell> cells)
        {
            Cells = cells;
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
    }

    public class Cell
    {
        public Cell()
        {
            Visited = false;
        }
        public Cell(int x, int y)
        {
            X = x;
            Y = y;
            Visited = false;
        }
        public Cell(int x, int y, bool visited)
        {
            X = x;
            Y = y;
            Visited = visited;
        }
        public int X { get; set; }
        public int Y { get; set; }
        public bool Visited { get; set; } = false;
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
