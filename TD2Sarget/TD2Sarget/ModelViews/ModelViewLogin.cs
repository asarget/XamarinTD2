﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using System.Windows.Input;
using Storm.Mvvm;
using Storm.Mvvm.Services;
using TD2Sarget.DTO;
using TD2Sarget.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TD2Sarget.ModelViews
{
    public class ModelViewLogin : ViewModelBase
    {
        private string _mail;
        private string _mdp;
        private string _nom;
        private string _prenom;
        private bool _isRegisterVisible = false;

        private ApiClient _apiClient;
        private Lazy<INavigationService> _navigationService;
        private Lazy<IDialogService> _dialogService;

        public ModelViewLogin()
        {
            LoginCommand = new Command(LoginActionAsync);
            RegisterCommand = new Command(RegisterActionAsync);
            _navigationService = new Lazy<INavigationService>(() => DependencyService.Resolve<INavigationService>());
            _dialogService = new Lazy<IDialogService>(() => DependencyService.Resolve<IDialogService>());
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

        public string Nom
        {
            get => _nom;
            set => SetProperty(ref _nom, value);
        }

        public string Prenom
        {
            get => _prenom;
            set => SetProperty(ref _prenom, value);
        }

        public bool IsRegisterVisible
        {
            get => _isRegisterVisible;
            set => SetProperty(ref _isRegisterVisible, value);
        }

        public ICommand LoginCommand
        {
            get;
        }

        public ICommand RegisterCommand
        {
            get;
        }

        private async void LoginActionAsync()
        {
            if (IsRegisterVisible)
            {
                IsRegisterVisible = false;
            }
            else
            {
                if (String.IsNullOrEmpty(Mail) || String.IsNullOrEmpty(Mdp))
                {
                    await _dialogService.Value.DisplayAlertAsync("Erreur", $"Champ(s) non renseigné(s)", "OK");
                }
                else
                {
                    var loginRequest = new LoginRequest
                    {
                        Email = Mail,
                        Password = Mdp
                    };

                    try
                    {
                        var request = await _apiClient.Execute(HttpMethod.Post, "https://td-api.julienmialon.com/auth/login", loginRequest);
                        var response = await _apiClient.ReadFromResponse<Response<LoginResult>>(request);

                        if (response.IsSuccess)
                        {
                            var accessToken = response.Data.AccessToken;
                            await SecureStorage.SetAsync("accessToken", accessToken);
                            await _navigationService.Value.PushAsync<ListViewPage>();
                        }
                        else
                        {
                            string msgErreur = response.ErrorMessage;
                            await _dialogService.Value.DisplayAlertAsync("Erreur", msgErreur, "OK");
                        }
                    } catch(Exception e)
                    {
                        await _dialogService.Value.DisplayAlertAsync("Erreur", "Vérifiez votre connection internet", "OK");
                    }
                    
                }
            }
        }

        public async void RegisterActionAsync()
        {
            if(!IsRegisterVisible)
            {
                IsRegisterVisible = true;
            } else
            {
                if (String.IsNullOrEmpty(Mail) || String.IsNullOrEmpty(Mdp) || String.IsNullOrEmpty(Prenom) || String.IsNullOrEmpty(Nom))
                {
                    await _dialogService.Value.DisplayAlertAsync("Erreur", $"Champ(s) non renseigné(s)", "OK");
                }
                else
                {
                    var registerRequest = new RegisterRequest
                    {
                        Email = Mail,
                        Password = Mdp,
                        FirstName = Prenom,
                        LastName = Nom
                    };

                    try
                    {
                        var request = await _apiClient.Execute(HttpMethod.Post, "https://td-api.julienmialon.com/auth/register", registerRequest);
                        var response = await _apiClient.ReadFromResponse<Response<LoginResult>>(request);

                        if (response.IsSuccess)
                        {
                            var accessToken = response.Data.AccessToken;
                            await SecureStorage.SetAsync("accessToken", accessToken);
                            await _navigationService.Value.PushAsync<ListViewPage>();
                        }
                        else
                        {
                            string msgErreur = response.ErrorMessage;
                            await _dialogService.Value.DisplayAlertAsync("Erreur", msgErreur, "OK");
                        }
                    }
                    catch (Exception)
                    {
                        await _dialogService.Value.DisplayAlertAsync("Erreur", "Vérifiez votre connection internet", "OK");
                    }
                    
                }
            }
        }
    }
}