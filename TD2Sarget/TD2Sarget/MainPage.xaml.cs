﻿using Storm.Mvvm.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TD2Sarget
{
    public partial class MainPage : BaseContentPage
    {
        public MainPage()
        {
            BindingContext = new ModelViewLogin();
            InitializeComponent();
        }
    }
}
