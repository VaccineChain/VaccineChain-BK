﻿using vaccine_chain_bk.DTO.User;

namespace vaccine_chain_bk.Services.Users
{
    public interface IUserService
    {
        string Register(RegisterDto registerDto);
        Task<AuthResponse> Login(LoginDto loginDto);
    }
}