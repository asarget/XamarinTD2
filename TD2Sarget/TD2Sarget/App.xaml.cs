using Storm.Mvvm;
using System;
using TD2Sarget.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TD2Sarget
{
    public partial class App : MvvmApplication
    {
        public App() : base(() => new MainPage())
        {
            InitializeComponent();
        }
    }
}
