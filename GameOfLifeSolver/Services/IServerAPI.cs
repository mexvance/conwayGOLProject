using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeSolver.Services
{
    public interface IServerAPI
    {
        [Post("/register")]
        Task<RegisterResult> RegisterAsync([Body(BodySerializationMethod.Serialized)]RegisterRequest name);
        [Post("/Update")]
        Task<UpdateResponse> UpdateAsync([Body(BodySerializationMethod.Serialized)]UpdateRequest request);
        [Post("/Update")]
        Task<UpdateResponse> CompletedAsync([Body(BodySerializationMethod.Serialized)]CompletedRequest request);
    }
}
