using GameOfLifeSolver.Enums;
using GameOfLifeSolver.Models;
using GameOfLifeSolver.Services;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfLifeSolver
{
    static class Program
    {
        static long generationsComputed = 0;
        static long generationsToCompute = 0;
        static string token = null;
        static IEnumerable<Cell> solvedBoard = null;
        static GameStatus status;
        static Timer heartbeatTimer;

        static IServerAPI serverAPI = RestService.For<IServerAPI>("http://daybellphotography.com");
        static GOLAPIService apiService = new GOLAPIService(serverAPI);
        static GOLService solverService = new GOLService(apiService);
        static UpdateResponse updateResponse = new UpdateResponse();
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var random = new Random();
            token = await solverService.GetToken("BarnDoorMike"+random.Next().ToString());
            status = GameStatus.Waiting;


            do
            {
                if (updateResponse != null)
                {
                    Console.WriteLine("Waiting for game to start... {0}", DateTime.Now);
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                }
                updateResponse = await solverService.PostUpdate(token, 0, null);
            } while (updateResponse.GameState == GameState.NotStarted);

            var seedBoard = updateResponse.seedBoard;
            generationsToCompute = updateResponse.generationsToCompute;

            heartbeatTimer = new Timer(new TimerCallback(heartbeat), state: null, dueTime: 500, period: 500);

            status = GameStatus.Processing;
            SolverService.GenerationBatchCompleted += (s, e) => Interlocked.Exchange(ref generationsComputed, e.GenerationsComputed);                                                  //game loop
            solvedBoard = SolverService.Solve(seedBoard, generationsToCompute); //call the solver
            

            status = GameStatus.Complete;
            Console.WriteLine("All Done!");
            updateResponse = await solverService.PostUpdate(token, generationsToCompute, solvedBoard);

        }
        private static async void heartbeat(object state)
        {
            var generations = (status == GameStatus.Complete) ?
                generationsToCompute :
                Interlocked.Read(ref generationsComputed);

            updateResponse = await solverService.PostUpdate(token, generationsComputed, solvedBoard);
            Console.WriteLine($"\t[Reporting heartbeat: Status={status}; Generations={generations}]");

            if (status == GameStatus.Complete)
            {
                if (updateResponse.GameState != GameState.Over)
                    Console.WriteLine("Waiting for server to say game is over.");
                else
                {
                    Console.WriteLine("All done, good game!");
                    heartbeatTimer.Dispose();//stop the timer.
                }
            }
        }
    }
}
