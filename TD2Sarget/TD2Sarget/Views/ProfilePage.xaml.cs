using Storm.Mvvm.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD2Sarget.ModelViews;
using Xamarin.Forms;

namespace TD2Sarget.Views
{
    public partial class ProfilePage : BaseContentPage
    {
        public ProfilePage()
        {
            BindingContext = new ModelViewProfile();
            InitializeComponent();
        }
    }
}
