using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Storm.Mvvm;
using Xamarin.Forms;

namespace TD2Sarget
{
    public class ModelViewLogin : ViewModelBase
    {
        private string _mail;
        private string _mdp;

        public ModelViewLogin()
        {
            LoginCommand = new Command(LoginAction);
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


        private void LoginAction()
        {
            Console.WriteLine(Mail);
        }
    }
}