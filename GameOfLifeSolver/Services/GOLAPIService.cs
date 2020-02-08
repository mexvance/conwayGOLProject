using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
    }

    public interface IServerAPI
    {
        [Post("/register")]
        Task<RegisterResult> RegisterAsync([Body(BodySerializationMethod.Serialized)]RegisterRequest name);
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
