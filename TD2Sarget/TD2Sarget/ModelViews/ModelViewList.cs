using Storm.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TD2Sarget.DTO;
using Xamarin.Forms;

namespace TD2Sarget.ModelViews
{
    class ModelViewList: ViewModelBase
    {

        private ApiClient _apiClient;


        public ObservableCollection<PlaceItemSummary> ImageList { get; }
        public ICommand DetailCommand{ get; }

        public ModelViewList()
        {
            _apiClient = new ApiClient();

            ImageList = new ObservableCollection<PlaceItemSummary>();
            DetailCommand = new Command<PlaceItemSummary>(DetailAction);
        }


        public override async Task OnResume()
        {
            await base.OnResume();

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

        }

        public void DetailAction(PlaceItemSummary pis)
        {

        }
    }
}
