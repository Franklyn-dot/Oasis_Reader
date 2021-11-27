using Oasis_Reader.Data;
using Oasis_Reader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Oasis_Reader.Views
{
    /// <summary>
    /// Información adicional del producto escaneado
    /// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PaginaInfoAdicional : ContentPage
	{
        private string descripcionActual;
        private string precioActual;
        private string tituloActual;




        public PaginaInfoAdicional(Tv_producto muestraProducto)
		{



            descripcionActual = muestraProducto.Txt_referencia;
            precioActual = muestraProducto.Precio.ToString();
            tituloActual = muestraProducto.Txt_descripcion_larga;
            
            InitializeComponent ();

         


            atrasDetalles.Clicked += Adicional_a_aceptar_inventario;

            TituloLabel.Text = muestraProducto.Txt_descripcion_larga;
            PrecioLabel.Text = muestraProducto.Precio.ToString();
            DescripcionLabel.Text = muestraProducto.Txt_referencia;
        }


        /// <summary>
        /// Salir de la información adicional a la página anterior.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void Adicional_a_aceptar_inventario(object sender, EventArgs args)
        {

            this.IsEnabled = false;
            Timer aTimer = new Timer();
            aTimer.Elapsed += (object sender2, ElapsedEventArgs e) => { this.IsEnabled = true; };
            aTimer.Interval = 5000; //ms
            aTimer.Enabled = true;
            Globals.DoBack--;
            Navigation.PopModalAsync();

            

        }
    }
}