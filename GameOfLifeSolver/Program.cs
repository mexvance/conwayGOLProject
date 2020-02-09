using GameOfLifeSolver.Services;
using Refit;
using System;
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
            
            //game loop
            while (true)
            {
                var newTime = DateTime.Now.Second;
                if (newTime - time >= 1)
                {
                    time = newTime;
                    Console.WriteLine(await solverService.PostUpdate(token, 0));
                }
            }
        }
    }
}
