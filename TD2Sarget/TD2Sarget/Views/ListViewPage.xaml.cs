using Storm.Mvvm.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD2Sarget.ModelViews;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TD2Sarget.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListViewPage : BaseContentPage
    {
        public ListViewPage()
        {
            BindingContext = new ModelViewList();
            InitializeComponent();
        }
    }
}