using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Storm.Mvvm;
using TD2Sarget.DTO;
using Xamarin.Forms;

namespace TD2Sarget
{
    public class ModelViewLogin : ViewModelBase
    {
        private string _mail;
        private string _mdp;
        private ApiClient _apiClient;

        public ModelViewLogin()
        {
            LoginCommand = new Command(LoginActionAsync);
            _apiClient = new ApiClient();
        }

        public string Mail
        {
            get => _mail;
            set => SetProperty(ref _mail, value);
        }

        public string Mdp
        {
            get => _mdp;
            set => SetProperty(ref _mdp, value);
        }

        public ICommand LoginCommand
        {
            get;
        }

        private async void LoginActionAsync()
        {
            var loginRequest = new LoginRequest {
                Email = Mail,
                Password = Mdp
            };
            var request = await _apiClient.Execute(HttpMethod.Post, "https://td-api.julienmialon.com/auth/login", loginRequest);

            var response = _apiClient.ReadFromResponse<Response<LoginResult>>(request);
        }
    }
}