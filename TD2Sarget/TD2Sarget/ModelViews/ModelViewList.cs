using Storm.Mvvm;
using Storm.Mvvm.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TD2Sarget.DTO;
using TD2Sarget.Views;
using Xamarin.Forms;

namespace TD2Sarget.ModelViews
{
    class ModelViewList: ViewModelBase
    {
        private ApiClient _apiClient;
        private Lazy<INavigationService> _navigationService;
        private Lazy<IDialogService> _dialogService;

        public ObservableCollection<PlaceItemSummary> ImageList { get; }
        public ICommand DetailCommand{ get; }

        public ModelViewList()
        {
            _apiClient = new ApiClient();
            _navigationService = new Lazy<INavigationService>(() => DependencyService.Resolve<INavigationService>());
            _dialogService = new Lazy<IDialogService>(() => DependencyService.Resolve<IDialogService>());
            ProfileCommand = new Command(ProfileActionAsync);

            ImageList = new ObservableCollection<PlaceItemSummary>();
            DetailCommand = new Command<PlaceItemSummary>(DetailAction);
        }

        public ICommand ProfileCommand
        {
            get;
        }


        public override async Task OnResume()
        {
            await base.OnResume();

            try
            {
                var request = await _apiClient.Execute(HttpMethod.Get, "https://td-api.julienmialon.com/places");

                var response = await _apiClient.ReadFromResponse<Response<List<PlaceItemSummary>>>(request);

                if (response.IsSuccess)
                {

                    var imageById = ImageList.ToDictionary(x => x.Id, x => x);

                    foreach (var img in response.Data)
                    {
                        if (!imageById.ContainsKey(img.Id))
                        {
                            img.DetailCommand = DetailCommand;
                            ImageList.Add(img);
                        }
                    }
                }
            } catch(Exception)
            {
                await _dialogService.Value.DisplayAlertAsync("Erreur", "Vérifiez votre connection internet", "OK");
            }

        }

        public void DetailAction(PlaceItemSummary pis)
        {
            throw new NotImplementedException();
        }

        private async void ProfileActionAsync()
        {
            await _navigationService.Value.PushAsync<ProfilePage>();
        }
    }
}
