using GameOfLifeSolver.Services;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameOfLifeSolver
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var serverAPI = RestService.For<IServerAPI>("http://localhost");
            var apiService = new GOLAPIService(serverAPI);
            var solverService = new GOLService(apiService);
            var random = new Random();
            var time = DateTime.Now.Second;
            var token = await solverService.GetToken("Bob"+random.Next().ToString());
            Console.WriteLine(token);
            List<Cell> board = null;
            int count = 0;
            var state = await solverService.PostUpdate(token, count);
            //game loop
            while (true)
            {
                var newTime = DateTime.Now.Second;
                if (newTime - time >= 1)
                {
                    time = newTime;
                    state = await solverService.PostUpdate(token, count);
                    if(board is null || board.Count == 0)
                    {
                        board = state.Cells;
                    }
                    Console.WriteLine(state.State);
                }
                if(state.State == "Started")
                {
                    board = await solverService.SolveBoard(board);
                    count++;
                }
            }
        }
    }
}
