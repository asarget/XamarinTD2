using System;
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
    public class ModelViewProfile : ViewModelBase
    {
        private string _mail;
        private string _nom;
        private string _prenom;
        private ApiClient _apiClient;
        private Lazy<IDialogService> _dialogService;
        private Lazy<INavigationService> _navigationService;

        public ModelViewProfile()
        {
            _apiClient = new ApiClient();
            _dialogService = new Lazy<IDialogService>(() => DependencyService.Resolve<IDialogService>());
            _navigationService = new Lazy<INavigationService>(() => DependencyService.Resolve<INavigationService>());
            ModifyProfileCommand = new Command(ModifyProfileActionAsync);
        }

        public string Mail
        {
            get => _mail;
            set => SetProperty(ref _mail, value);
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

        public ICommand ModifyProfileCommand
        {
            get;
        }

        private async void ModifyProfileActionAsync()
        {

            if (String.IsNullOrEmpty(Prenom) || String.IsNullOrEmpty(Nom))
            {
                await _dialogService.Value.DisplayAlertAsync("Erreur", $"Champ(s) non renseigné(s)", "OK");
            }
            else
            {
                var updateProfileRequest = new UpdateProfileRequest
                {
                    FirstName = Prenom,
                    LastName = Nom
                };

                try
                {
                    var request = await _apiClient.Execute(HttpMethod.Put, "https://td-api.julienmialon.com/me", updateProfileRequest);
                    var response = await _apiClient.ReadFromResponse<Response<UpdateProfileRequest>>(request);

                    if (response.IsSuccess)
                    {
                        await _dialogService.Value.DisplayAlertAsync("Information", "Modification réussie", "OK");
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
                await _navigationService.Value.PopAsync();
            }
        }



        public override async Task OnResume()
        {
            await base.OnResume();
            string token = SecureStorage.GetAsync("accessToken").ToString();
            var request = await _apiClient.Execute(HttpMethod.Get, "https://td-api.julienmialon.com/me",null,token);
            var response = await _apiClient.ReadFromResponse<Response<UserItem>>(request);

            if (response.IsSuccess)
            {
                Mail = response.Data.Email;
                Prenom = response.Data.FirstName;
                Nom = response.Data.LastName;
            }
            else
            {
                string msgErreur = response.ErrorMessage;
                await _dialogService.Value.DisplayAlertAsync("Erreur", msgErreur, "OK");
            }

        }
    }
}