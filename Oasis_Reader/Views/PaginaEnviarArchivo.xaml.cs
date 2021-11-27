using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Oasis_Reader.Models;
using System.Collections.ObjectModel;
using Oasis_Reader.Data;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Http;

namespace Oasis_Reader
{
    /// <summary>
    /// Página que envía el archivo a un servicio dentro de la misma red local.
    /// También muestra una lista de los registros de productos en la base de datos hasta ahora.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaEnviarArchivo : ContentPage
    {
        

        public List<string> Listagg { get; private set; }

        public List<Tv_producto> Productos { get; private set; }
        public List<Tm_conteo> Encabezados { get; private set; }
        public List<Ta_inventario_producto_copy> Inventario { get; private set; }


        public Ip_puerto Ipp { get; private set; }
        public List<Ip_puerto> Ipp_list { get; private set; }

        public string LastIp;
        public string LastPort;
       
        public PaginaEnviarArchivo()
        {

 

            




            InitializeComponent();



            LastIp = "0.0.0.0";
            LastPort = "3200";

            Console.WriteLine(LastIp);
            Console.WriteLine(hostApi.Text);

            hostApi.Text = LastIp;
            puertoApi.Text = LastPort;

            Ipp_list = Login.Ip_puertoDatabase.ListIp_puerto();

            if (Ipp_list.Count > 0)
            {
                hostApi.Text = Ipp_list[Ipp_list.Count - 1].Ip;
                puertoApi.Text = Ipp_list[Ipp_list.Count - 1].Puerto;

            }



           // enviarDatos.Clicked += Enviar_DatosAsync;

            enviarDatos.Clicked += Prueba;
            salirEnviarArchivo.Clicked += Salir_A_Menu;


            hostApi.Completed += Guarda_Host;
            puertoApi.Completed += Guarda_Puerto;


            Listagg = new List<string>() { };

            Encabezados = Login.Tm_conteoDatabase.ListTm_conteo();
            

            Inventario = Login.Ta_inventario_producto_copyDatabase.ListTa_inventario_producto_copy();
            stackProductos.ItemsSource = Inventario;


    
           
        }
        static HttpClient _client = new HttpClient();
        HttpResponseMessage respuesta = null;
        public async void Prueba(object sender, EventArgs args)
        {
            //creada por Ing. Franklyn Tinoco 02-02-2019
            //modificada: 08-08-2019
          //  var result = await _client.GetAsync("http://173.173.26.249:8082/ws_inventario?id=80");
            // Write status code.
            String IP_URL = "http://173.173.26.249:8082/ws_inventario?id=50";
            var uri = new Uri(string.Format(IP_URL, string.Empty));
            var content = new StringContent("Datos al 31-10-2019", Encoding.UTF8, "application/xml");
           // Console.WriteLine("STATUS CODE: " + result.StatusCode);
            respuesta = await _client.PostAsync(uri, content);
            //Console.WriteLine(respuesta);
            await DisplayAlert("Enviado", "Carga Completa", "Ok");
        }

        public void Guarda_Host(object sender, EventArgs args)
        {
            Login.Ip_puertoDatabase.SaveIp_puerto(new Ip_puerto(hostApi.Text, puertoApi.Text));
        }


        public void Guarda_Puerto(object sender, EventArgs args)
        {
            Login.Ip_puertoDatabase.SaveIp_puerto(new Ip_puerto(hostApi.Text, puertoApi.Text));
        }

        /// <summary>
        /// cambiar el estátus de la barra de progreso
        /// </summary>
        /// <param name="d"></param>
        public async void Cambio_Progreso(double d)
        {
           await progresoEnvio.ProgressTo(d,100, Easing.Linear);
        }


        /// <summary>
        /// Notificación de carga completa
        /// </summary>
        public void Display_Carga_Completa()
        {
            DisplayAlert("Enviado", "Carga Completa", "Ok");
            
        }


        /// <summary>
        /// Muestra un error de carga
        /// </summary>
        /// <param name="mensaje"></param>
        public void Display_Error_Carga(string mensaje)
        {
            string muestra = mensaje.Substring(1,19);
            Console.WriteLine(muestra);
            if (muestra == "ailed to connect to")
            {
                if (mensaje.Length > 50)
                {
                    DisplayAlert("No enviado", "Tiempo de conexión expirado" , "Ok");
                }
                else
                {
                    DisplayAlert("No enviado", "Fallo de conexión con el servidor" + mensaje.Substring(20, mensaje.Length - 20), "Ok");
                }
                
            }
            else 
            {
                DisplayAlert("No enviado", "Mensaje de error: " + mensaje, "Ok");
            }
            
        }


        public async void Enviar_DatosAsync(object sender, EventArgs args)
        {
            await Enviar_Datos_Async();
            Guarda_Host(sender, args);
            Guarda_Puerto(sender, args);
        }



        


        /// <summary>
        /// Procedimiento principal de envío de datos
        /// </summary>
        /// <returns></returns>
        private async Task Enviar_Datos_Async()
        {
           
            if (Inventario.Count > 0)
            {
                RestService RS = new RestService();

                // Chequeos de verificación de formatos
                bool matched_ip = IPAddress.TryParse(hostApi.Text, out IPAddress ip);
                Regex port_ = new Regex(@"^([1-9]|[1-8][0-9]|9[0-9]|[1-8][0-9]{2}|9[0-8][0-9]|99[0-9]|[1-8][0-9]{3}|9[0-8][0-9]{2}|99[0-8][0-9]|999[0-9]|[1-5][0-9]{4}|6[0-4][0-9]{3}|65[0-4][0-9]{2}|655[0-2][0-9]|6553[0-5])$");
               
                Match match_port = port_.Match(puertoApi.Text);

                // Si los datos son válidos se envía el inventario 
                if (matched_ip)
                {
                    if (match_port.Success)
                    {

                        // Cambios en la pantalla
                        enviarDatos.IsEnabled = false;
                        salirEnviarArchivo.IsEnabled = false;
                        tablaProductos.IsVisible = false;
                        
                        progresoEnvio.IsVisible = true;

                        // Se envian los encabezados
                        await RS.SendEncabezadoList(Encabezados, "http://" + hostApi.Text + ":" + puertoApi.Text + "/api/Conteo");

                        // Se envian los productos
                        await RS.SendProductoList(Inventario, "http://" + hostApi.Text + ":" + puertoApi.Text + "/api/inventario", this);

                        // Retorno de los cambios y progreso de la barra
                        tablaProductos.IsVisible = true;
                        
                        progresoEnvio.IsVisible = false;
                        await progresoEnvio.ProgressTo(0.0f, 100, Easing.Linear);
                        enviarDatos.IsEnabled = true;
                        salirEnviarArchivo.IsEnabled = true;

                        // Actualizar inventario.
                        Inventario = Login.Ta_inventario_producto_copyDatabase.ListTa_inventario_producto_copy();

                    }
                    else
                    {
                        await DisplayAlert("Puerto \"", puertoApi.Text + "\" no es un puerto válido", "Ok");
                    }
                        

                }
                else
                {
                    await DisplayAlert("Dirección IP \"", hostApi.Text + "\" no es una dirección ip válida", "Ok");
                }
            }



        }


        /// <summary>
        /// Salir al menú principal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void Salir_A_Menu(object sender, EventArgs args)
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