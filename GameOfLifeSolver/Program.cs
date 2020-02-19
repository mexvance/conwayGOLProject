using GameOfLifeSolver.Enums;
using GameOfLifeSolver.Models;
using GameOfLifeSolver.Services;
using Refit;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfLifeSolver
{
    class Program
    {
        static async Task Main(string[] args)
        {
            int generationsComputed = 0;
            Console.WriteLine("Hello World!");
            UpdateResponse updateResponse = new UpdateResponse();
            var serverAPI = RestService.For<IServerAPI>("http://daybellphotography.com");
            var apiService = new GOLAPIService(serverAPI);
            var solverService = new GOLService(apiService);
            var random = new Random();
            var time = DateTime.UtcNow.TimeOfDay;
            var token = await solverService.GetToken("BarnDoorMike"+random.Next().ToString());
            Console.WriteLine("Received Token: " + token);

            do
            {
                if (updateResponse != null)
                {
                    Console.WriteLine("Waiting for game to start... {0}", DateTime.Now);
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                }
                updateResponse = await solverService.PostUpdate(token, 0);
            } while (updateResponse.GameState == GameState.NotStarted);

            var board = updateResponse.seedBoard;
            //game loop
            while (updateResponse.GameState == GameState.InProgress && generationsComputed < updateResponse.generationsToCompute)
            {
                var newTime = DateTime.UtcNow.TimeOfDay;
                TimeSpan diff = newTime.Subtract(time).Duration();
                if (diff.TotalSeconds >= 1)
                {
                    time = newTime;
                    _ = solverService.PostUpdate(token, generationsComputed);
                }
                board = SolverService.SolveGeneration(board);
                generationsComputed++;
            }

            _ = await solverService.PostCompleted(token, generationsComputed, board);
            Console.WriteLine("Game solved");
        }
    }
}
