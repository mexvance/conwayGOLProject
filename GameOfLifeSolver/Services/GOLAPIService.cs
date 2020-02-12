using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using GameOfLifeSolver.Enums;

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
            var request = new RegisterRequest { name = name };
            return await _serverAPI.RegisterAsync(request);
        }

        public async Task<UpdateResponse> PostUpdateAsync(string token, int generations)
        {
            var request = new UpdateRequest { token = token, generationsComputed = generations };
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

        public int generationsComputed { get; set; }
    }

    public class UpdateResponse
    {
        public GameState gamestate { get; set; }
        public int? generationsToCompute { get; set; }
        public List<UpdateResponseBoardSquare> seedBoard { get; set; }
        public bool isError { get; set; }
        public string errorMessage { get; set; }
    }

    public class UpdateResponseBoardSquare
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class RegisterResult
    {
        public string Token { get; set; }
        public int GenerationsComputed { get; set; }
    }

    public class RegisterRequest
    {
        public string name { get; set; }
    }
}
