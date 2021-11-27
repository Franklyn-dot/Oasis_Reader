﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Oasis_Reader.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuOasis : MasterDetailPage
    {
        public string name;
        public MenuOasis(string nombreusuario)
        {
            InitializeComponent();
            name = nombreusuario;
            this.Master = new master();
            this.Detail = new NavigationPage (new detail(name));
        }

    }


    }
