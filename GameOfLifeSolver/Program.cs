using GameOfLifeSolver.Services;
using Refit;
using System;

namespace GameOfLifeSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var serverAPI = RestService.For<IServerAPI>("http://localhost");
            var apiService = new GOLAPIService(serverAPI);
            var solverService = new GOLService(apiService);

            var token = solverService.GetToken("Bob3");
            Console.WriteLine(token.Result);
        }
    }
}
