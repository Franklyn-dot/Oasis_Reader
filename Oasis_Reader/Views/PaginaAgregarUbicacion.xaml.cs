using Oasis_Reader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace Oasis_Reader.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PaginaAgregarUbicacion : ContentPage
	{
		public PaginaAgregarUbicacion ()
		{




			InitializeComponent ();

            idEntry.Text = "";
            descripcionEntry.Text = "";
            entryCodigoBarra.Text = "";

            introducirCodBarra.Clicked += ScanAsync;
            aceptarUbicacion.Clicked += Cargar_ubicacion;
            salirAgregarUbicacion.Clicked += AgregarUbicacion_a_Inventario;
            limpiarUbicacion.Clicked += Limpiar_ubicacion;
        }


        public async void ScanAsync(object sender, EventArgs args)
        {
            var scanPage = new ZXingScannerPage();
            // Navegar hasta nuestra página de scan
            this.IsEnabled = false;
            Timer aTimer = new Timer();
            aTimer.Elapsed += (object sender2, ElapsedEventArgs e) => { this.IsEnabled = true; };
            aTimer.Interval = 5000; //ms
            aTimer.Enabled = true;

            await Navigation.PushModalAsync(scanPage);


            scanPage.OnScanResult += (result) =>
            {
                // Dejar de escanear
                scanPage.IsScanning = false;

                // Soltar la página y mostrar los resultados
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopModalAsync();

                    entryCodigoBarra.Text = result.Text.Substring(0,10);

                });
            };

        }

        public void Cargar_ubicacion(object sender, EventArgs args)
        {
            if (idEntry.Text != "" &&
            descripcionEntry.Text != "" &&
            entryCodigoBarra.Text != "" &&
            idEntry.Text != null &&
            descripcionEntry.Text != null &&
            entryCodigoBarra.Text != null )
            {
                if (entryCodigoBarra.Text.Length > 10)
                {
                    DisplayAlert("Aceptar", "El codigo debe tener menos de 11 caracteres", "Ok");
                }
                else
                {
                    if (idEntry.Text.Length > 9)
                    {
                        DisplayAlert("Aceptar", "El Id debe tener menos de 10 caracteres", "Ok");
                    }
                    else
                    {
                        DisplayAlert("Aceptar", "Ubicación cargada", "Ok");
                        Td_desc_ubica Nueva = new Td_desc_ubica(entryCodigoBarra.Text, descripcionEntry.Text, Convert.ToInt32(idEntry.Text));
                        Login.Td_desc_ubicaDatabase.SaveTd_desc_ubica(Nueva);
                    }

                    

                }
                
            }
            else
            {
                DisplayAlert("Aceptar", "Datos incompletos", "Ok");
            }


        }


        public void Limpiar_ubicacion(object sender, EventArgs args)
        {
            idEntry.Text = "";
            descripcionEntry.Text = "";
            entryCodigoBarra.Text = "";
        }



        public void AgregarUbicacion_a_Inventario(object sender, EventArgs args)
        {

            MessagingCenter.Send<PaginaAgregarUbicacion>(this, "Actualizar");
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