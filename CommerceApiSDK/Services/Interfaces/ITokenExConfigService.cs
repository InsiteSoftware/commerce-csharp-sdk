﻿using System;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Results;

namespace CommerceApiSDK.Services.Interfaces
{
    public interface ITokenExConfigService
    {
        Task<TokenExDto> GetTokenexconfigAsync();
    }
}