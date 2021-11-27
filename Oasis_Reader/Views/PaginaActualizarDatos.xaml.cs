using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Oasis_Reader.Data;
using Oasis_Reader.Models;
using Xamarin.Forms;
using Newtonsoft.Json;

using Xamarin.Forms.Xaml;

namespace Oasis_Reader.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaActualizarDatos : ContentPage
    {
        public PaginaActualizarDatos()
        {
            InitializeComponent();
            enviarDatos.Clicked += recibir;
            salirEnviarArchivo.Clicked += salir;
        }
        public void ActivarBarra()
        {
            //activador de tiempo
           
        }

        public void recibir(object sender, EventArgs args)
        {
            //llama al programa que trae el json de productos 
            //desde el web service

           BajarProductoController BP = new BajarProductoController();
            progresoEnvio.IsVisible = true;
            progresoEnvio.ProgressTo(0.0f, 100, Easing.Linear);
            BP.Index();
            DisplayAlert("CANTIDAD PRODUCTOS: ", Convert.ToString(BP.nroregistros),"Ok");
            progresoEnvio.IsVisible = false;
        }
        public void salir(object sender, EventArgs args)
        {
            this.IsEnabled = false;
            Timer aTimer = new Timer();
            aTimer.Elapsed += (object sender2, ElapsedEventArgs e) => { this.IsEnabled = true; };
            aTimer.Interval = 5000; //ms
            aTimer.Enabled = true;
            Globals.DoBack--;
            Navigation.PopModalAsync();
            //salir de la pantallas
        }
    }
}