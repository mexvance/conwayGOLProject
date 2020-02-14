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

        public async Task<UpdateResponse> PostUpdateAsync(string token, long? generations, IEnumerable<Cell> solvedBoard)
        {
            var request = new UpdateRequest { token = token, generationsComputed = generations, ResultBoard = solvedBoard };
            return await _serverAPI.UpdateAsync(request);
        }
    }

    public interface IServerAPI
    {
        [Post("/register")]
        Task<RegisterResult> RegisterAsync([Body(BodySerializationMethod.Serialized)]RegisterRequest name);
        [Post("/Update")]
        Task<UpdateResponse> UpdateAsync([Body(BodySerializationMethod.Serialized)]UpdateRequest request);
    }

    public class UpdateRequest
    {
        public string token {get; set;}

        public long? generationsComputed { get; set; }

        public IEnumerable<Cell> ResultBoard { get; set; }
    }

    public class UpdateResponse
    {
        public GameState GameState { get; set; }
        public long generationsToCompute { get; set; }
        public IEnumerable<Cell> seedBoard { get; set; }
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
