﻿using AuthServerApp.Interfaces;
using AuthServerApp.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthServerApp.Services
{
    public class SignInService : ISignIn
    {
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtTokenGenerator _tokenGenerator;
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _tokenRepository;

        public List<string> Errors { get; }

        public SignInService(SignInManager<User> signInManager, IJwtTokenGenerator tokenGenerator, IUserRepository userRepository, IRefreshTokenRepository tokenRepository)
        {
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
            _signInManager = signInManager;
            _tokenRepository = tokenRepository;
            Errors = new List<string>();
        }

        public async Task<RefreshToken?> SignInAsync(SignInModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, true);
            if (result.Succeeded)
            {
                var user = await _userRepository.GetUserByEmailAsync(model.Email);
                var token = _tokenGenerator.GenerateRefreshToken(user);
                _tokenRepository.AddRefreshTokenToUser(token, user.Id);
                return token;
            }
            Errors.Add("Неправильный логин или пароль");
            return null;
        }
    }
}
