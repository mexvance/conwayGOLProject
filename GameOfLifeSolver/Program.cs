using GameOfLifeSolver.Enums;
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
            Console.WriteLine("Hello World!");
            UpdateResponse updateResponse = null;
            var serverAPI = RestService.For<IServerAPI>("http://daybellphotography.com");
            var apiService = new GOLAPIService(serverAPI);
            var solverService = new GOLService(apiService);
            var random = new Random();
            var time = DateTime.Now.Second;
            var token = await solverService.GetToken("BarnDoorMike"+random.Next().ToString());
            Console.WriteLine(token);

            do
            {
                if (updateResponse != null)
                {
                    Console.WriteLine("Waiting for game to start... {0}", DateTime.Now);
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                }
                updateResponse = await solverService.PostUpdate(token, 0);
            } while (updateResponse.gamestate == GameState.NotStarted);

            //game loop
            while (updateResponse.gamestate == GameState.InProgress)
            {
                var newTime = DateTime.Now.Second;
                if (newTime - time >= 1)
                {
                    time = newTime;
                    updateResponse = await solverService.PostUpdate(token, 0);
                    if (updateResponse.seedBoard != null)
                        foreach (var item in updateResponse.seedBoard)
                        {
                            Console.WriteLine(item.X + "" +  item.Y);
                        }
                }
            }
        }
    }
}
