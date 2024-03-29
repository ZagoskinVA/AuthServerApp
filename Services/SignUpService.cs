﻿using AuthServerApp.EventBus;
using AuthServerApp.EventBus.Abstract;
using AuthServerApp.Interfaces;
using AuthServerApp.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace AuthServerApp.Services
{
    public class SignUpService : ISignUp
    {
        private readonly IUserRepository _userRepository;
        private readonly IRabbitMqProducer<EmailConfirmationEvent> _rabbitMqProducer;

        public List<string> Errors { get; }

        public SignUpService(IUserRepository userRepository, IRabbitMqProducer<EmailConfirmationEvent> rabbitMqProducer)
        {
            if(userRepository == null)
                throw new ArgumentNullException(nameof(userRepository));
            _userRepository = userRepository;
            _rabbitMqProducer = rabbitMqProducer;
            Errors = new List<string>();
        }

        private Task<bool> ConfirmEmail(string userId, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SignUp(SignUpModel model)
        {
            var user = await _userRepository.GetUserByEmailAsync(model.Email);
            if (user == null) 
            {
                user = new User { Email = model.Email, UserName = model.Email, NickName = model.Name };
                user.EmailConfirmed = true;
                var result = await _userRepository.AddUserAsync(user, model.Password);
                if (result.Succeeded) 
                {
                    var message = await _userRepository.GetEmailConfarmationTokenAsync(user);
                    var emailMessage = new EmailMessageModel { Email = model.Email, Subject = "Подтверждение email", Message = $"Ссылка на подтвеждение <a>{message}</a>" };
                    _rabbitMqProducer.Publish(emailMessage);
                    return true;
                }
                Errors.Add("Ошибка при создании пользователя");
            }
            Errors.Add("Пользователь с таким email уже существует");
            return false;
        }
    }
}
