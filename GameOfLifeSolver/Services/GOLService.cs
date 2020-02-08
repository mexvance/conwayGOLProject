﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeSolver.Services
{
    public class GOLService
    {
        private readonly GOLAPIService _apiService;

        public GOLService(GOLAPIService apiService)
        {
            _apiService = apiService;
        }
        public async Task<string> GetToken(string name)
        {
            var result = await _apiService.GetToken(name);
            return result.Token;
        }
    }
}
