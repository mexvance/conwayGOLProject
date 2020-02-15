using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using GameOfLifeSolver.Enums;
using GameOfLifeSolver.Models;

namespace GameOfLifeSolver.Services
{
    public class GOLAPIService
    {
        private readonly IServerAPI _serverAPI;

        public GOLAPIService(IServerAPI serverAPI)
        {
            _serverAPI = serverAPI;
        }
        public async Task<RegisterResult> GetToken(string name)
        {
            var request = new RegisterRequest { Name = name };
            return await _serverAPI.RegisterAsync(request);
        }

        public async Task<UpdateResponse> PostUpdateAsync(string token, int generations)
        {
            var request = new UpdateRequest { token = token, generationsComputed = generations };
            return await _serverAPI.UpdateAsync(request);
        }

        public async Task<UpdateResponse> PostCompletedAsync(string token, int generationsComputed, List<Cell> board)
        {
            var request = new CompletedRequest { token = token, generationsComputed = generationsComputed, ResultBoard = board };
            return await _serverAPI.CompletedAsync(request);
        }
    }

    public class CompletedRequest
    {
        public string token { get; set; }
        public int generationsComputed { get; set; }
        public List<Cell> ResultBoard { get; set; }
    }

    public class UpdateRequest
    {
        public string token {get; set;}

        public int generationsComputed { get; set; }
    }

    public class UpdateResponse
    {
        public GameState GameState { get; set; }
        public int? generationsToCompute { get; set; }
        public List<Cell> seedBoard { get; set; }
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class RegisterResult
    {
        public string Token { get; set; }
        public int GenerationsComputed { get; set; }
    }

    public class RegisterRequest
    {
        public string Name { get; set; }
    }
}
