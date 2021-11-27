using Oasis_Reader.Data;
using Oasis_Reader.Models;
using Oasis_Reader.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace Oasis_Reader
{
  
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PaginaAceptarInventario : ContentPage
	{

        public string descripcionActual;
        public string precioActual;
        public string precioTotal;
        public string name;
        public int cantidadTotal;
        public bool mostrado;
        public Tv_producto muestraProducto;
        public List<Tv_barra> listBarra;
        public List<Tv_producto> listProducto;
        public Ta_inventario_producto_copy invProdGuardar;
        public Ta_inventario_producto_copy auxProdEnviado;
        public Tm_conteo encabezado;

        public List<Tm_conteo> SavParte { get; set; }
        public Tm_conteo Sav { get; set; }

        public string mensajeAvanzado;

        public PaginaAceptarInventario (bool inventarioCantidad, int numeroConteo, string nombreusuario, Ta_inventario_producto_copy invProdenviado, Tm_conteo encabezado_)
		{
			InitializeComponent ();


            mostrado = false;
            name = nombreusuario;
            descripcionActual = "        ";
            precioActual = "        ";
            precioTotal = string.Empty;


            encabezado = encabezado_;
            auxProdEnviado = invProdenviado; 

            salirAceptarInventario.Clicked += Aceptar_inventario_a_inventario;
            aceptarProducto.Clicked += Cargar_producto;
            adicionalInventario.Clicked += Aceptar_inventario_a_informacion_adicional;
            limpiarInventario.Clicked += Limpiar_descripcion;
            introducirCodBarra.Clicked += ScanAsync;
            busquedaAvanzada.Clicked += Aceptar_inventario_a_busqueda_avanzada;
            entryCodigoBarra.Completed += Query_barra;
            entryCantidad.Completed += Query_barra;
            



            DescripcionLabel.Text = descripcionActual;

            entryCantidad.IsVisible = inventarioCantidad;
            entryCantidadLabel.IsVisible = inventarioCantidad;
        }


        /// <summary>
        /// Activa el Scan y recibe la información del producto.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>

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
                    entryCodigoBarra.Text = result.Text;
                    Query_barra(sender, args);
                    
                });
            };

        }



        /// <summary>
        /// Maneja el resultado del scan en caso de que envíe alguno que sea nulo
        /// </summary>
        /// <param name="result"></param>
        void HandleResult(ZXing.Result result)
        {
            if (result != null)
            {
                entryCodigoBarra.Text = result.Text;
            }
            else
            {
                DisplayAlert("", "No hay código de barra", "Ok");
            }   
        }
    

        /// <summary>
        /// Regresar de la página de aceptar inventario hasta inventario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void Aceptar_inventario_a_inventario(object sender, EventArgs args)
        {

            this.IsEnabled = false;
            Timer aTimer = new Timer();
            aTimer.Elapsed += (object sender2, ElapsedEventArgs e) => { this.IsEnabled = true; };
            aTimer.Interval = 5000; //ms
            aTimer.Enabled = true;
            Globals.DoBack--;
            Navigation.PopModalAsync();

        }


        /// <summary>
        /// Navega a la búsqueda avanzada desde la página actual.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private async void Aceptar_inventario_a_busqueda_avanzada(object sender, EventArgs args)
        {



            // Se recive el mensaje Cod_ interno de PaginaBusqueda Avanzada

            MessagingCenter.Subscribe<PaginaBusquedaAvanzada, string>(this, "Cod_interno", (sender3, arg) =>
            {

                mensajeAvanzado = arg;

                listBarra = Login.Tv_barraDatabase.GetTv_barra(mensajeAvanzado);
                
                if (listBarra.Count >= 1 )
                {
                    entryCodigoBarra.Text = listBarra[0].Cod_barra;

                    invProdGuardar = new Ta_inventario_producto_copy(auxProdEnviado.Usuario, 1, auxProdEnviado.Ubicacion, auxProdEnviado.Id_dispositivo, auxProdEnviado.Tipo_conteo,
                    null, null, 1, 0, null, 0, auxProdEnviado.Fecha_conteo, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), null, null);
                    listProducto = Login.Tv_productoDatabase.GetTv_producto(mensajeAvanzado);

                    if (listProducto.Count >= 1)
                    {

                        muestraProducto = listProducto[0];
                    }
                    else
                    {
                        muestraProducto = null;
                    }
                }
                else
                {
                    DisplayAlert("Código interno", mensajeAvanzado  + " no tiene código de barra asignado", "Ok");
                    muestraProducto = null;
                }

                if (muestraProducto != null)
                {
                    mostrado = true;
                    MostrarProducto(muestraProducto);
                }
                else
                {
                    mostrado = false;
                    PrecioTotal.Text = "";
                    PrecioLabel.Text = "";
                }


            });




            this.IsEnabled = false;
            Timer aTimer = new Timer();
            aTimer.Elapsed += (object sender2, ElapsedEventArgs e) => { this.IsEnabled = true; };
            aTimer.Interval = 5000; //ms
            aTimer.Enabled = true;
            Globals.DoBack++;
            ContentPage Avanzada = new PaginaBusquedaAvanzada();
            await Navigation.PushModalAsync(Avanzada);



        }




        /// <summary>
        /// Chequea si a la ubicacion, Dispositivo y Conteo correrpondiente ya le correponde una parte en la tabla Tm_Conteo
        /// </summary>
        /// <param name="ubics"></param>
        /// <param name="disp"></param>
        /// <param name="numeroConteo"></param>
        /// <returns></returns>
        public void ChequearParte( Tm_conteo enc)
        {
            SavParte = Login.Tm_conteoDatabase.GetTm_conteoPrimKeys(enc.Conteo, enc.Id_dispositivo, enc.Conteo);

            if (SavParte.Count == 0)
            {
                

                Console.WriteLine("NUMERO CONTEO = " + enc.Conteo);

                Console.WriteLine("DISPOSITIVO = " + enc.Id_dispositivo);
                Console.WriteLine("PIDE CONTEO = " + enc.Conteo);
                Console.WriteLine("UBICACION = " + enc.Cod_departamento);




                Login.Tm_conteoDatabase.SaveTm_conteo(enc);

                SavParte = Login.Tm_conteoDatabase.GetTm_conteoPrimKeys(enc.Conteo  , enc.Id_dispositivo, enc.Conteo);

                Console.WriteLine("PARTES = " + SavParte.Count);
            }

        }




        /// <summary>
        /// Realizar la carga del producto mostrado en la base de datos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void Cargar_producto(object sender, EventArgs args)
        {

            if (muestraProducto != null && mostrado)
            {
                
                DisplayAlert("Producto", "Se cargará un conteo de este producto", "ok");
                invProdGuardar.Cod_interno = muestraProducto.Cod_interno;
                invProdGuardar.Cod_barra = entryCodigoBarra.Text;
                invProdGuardar.Barra = entryCodigoBarra.Text;

                invProdGuardar.Precio = Convert.ToDecimal(PrecioLabel.Text);
                if (entryCantidad.IsVisible)
                {
                    invProdGuardar.Cantidad = cantidadTotal;
                }
                else
                {
                    invProdGuardar.Cantidad = 1;
                }

                //graba informacion de inventario
                ChequearParte(encabezado);
                Login.Ta_inventario_producto_copyDatabase.SaveTa_inventario_producto_copy(invProdGuardar);
                Limpiar_descripcion(sender, args);


            }
            


        }

        /// <summary>
        /// Quita la descripcion de producto para introducir uno nuevo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void Limpiar_descripcion(object sender, EventArgs args)
        {
            
            muestraProducto = null;
            DescripcionLabel.Text = "        ";
            entryCantidad.Text = string.Empty;
            PrecioLabel.Text = string.Empty;
            PrecioTotal.Text = string.Empty;
            entryCodigoBarra.Text = string.Empty;
            cantidadTotal = 0;
        }


        /// <summary>
        /// Navega a la información adicional desde la página actual.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private async void Aceptar_inventario_a_informacion_adicional(object sender, EventArgs args)
        {
            
            if (muestraProducto != null && mostrado)
            {

                this.IsEnabled = false;
                Timer aTimer = new Timer();
                aTimer.Elapsed += (object sender2, ElapsedEventArgs e) => { this.IsEnabled = true; };
                aTimer.Interval = 5000; //ms
                aTimer.Enabled = true;
                Globals.DoBack++;
                await Navigation.PushModalAsync(new PaginaInfoAdicional(muestraProducto));

                
            }
                
        }

        /// <summary>
        /// Muestra los detalles del producto 
        /// </summary>
        /// <param name="muestra"></param>
        public void MostrarProducto(Tv_producto muestra)
        {
            DescripcionLabel.Text = muestra.Txt_descripcion_larga;
            
            if (entryCantidad.IsVisible)
            {
                Regex regex = new Regex("^\\d{1,9}$");
                if (entryCantidad.Text != "" && entryCantidad.Text != null)
                {
                    Match match = regex.Match(entryCantidad.Text);
                    if (match.Success)
                    {
                        cantidadTotal = Convert.ToInt32(entryCantidad.Text);
                        PrecioTotal.Text = decimal.Round((muestra.Precio * cantidadTotal), 2, MidpointRounding.AwayFromZero).ToString(); 
                        PrecioLabel.Text = decimal.Round((muestra.Precio), 2, MidpointRounding.AwayFromZero).ToString();
                    }
                    else
                    {
                        DisplayAlert("Cantidad", entryCantidad.Text + " es inválido", "Ok");
                        entryCantidad.Text = string.Empty;
                    }
                }
                else
                {
                    cantidadTotal = 1;
                    PrecioTotal.Text = decimal.Round((muestra.Precio), 2, MidpointRounding.AwayFromZero).ToString(); 
                    PrecioLabel.Text = decimal.Round((muestra.Precio), 2, MidpointRounding.AwayFromZero).ToString();
                }
            }
            else
            {
                cantidadTotal = 1;
                PrecioTotal.Text = decimal.Round((muestra.Precio ), 2, MidpointRounding.AwayFromZero).ToString();
                PrecioLabel.Text = decimal.Round((muestra.Precio ), 2, MidpointRounding.AwayFromZero).ToString();
            }
        }


        /// <summary>
        /// Busca un producto asociado al código de barras en la entrada correspondiente.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void Query_barra(object sender, EventArgs args)
        {
            if (!string.IsNullOrEmpty(entryCodigoBarra.Text))
            {

                listBarra = Login.Tv_barraDatabase.GetTv_barraCodBarra(entryCodigoBarra.Text);
                if (listBarra.Count >= 1)
                {
                    invProdGuardar = new Ta_inventario_producto_copy(auxProdEnviado.Usuario, 1, auxProdEnviado.Ubicacion, auxProdEnviado.Id_dispositivo, auxProdEnviado.Tipo_conteo,
                        null, null, 1, 0, null, 0, auxProdEnviado.Fecha_conteo, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), null, null);
                    listProducto = Login.Tv_productoDatabase.GetTv_producto(listBarra[0].Cod_interno);
                    if (listProducto.Count >= 1)
                    {
                        
                        muestraProducto = listProducto[0];
                    }
                    else
                    {
                        DisplayAlert("Código de barras", entryCodigoBarra.Text + " no se encuentra asignado a un producto", "Ok");
                        muestraProducto = null;
                    }

                }
                else
                {
                    DisplayAlert("Código de barras", entryCodigoBarra.Text + " no se encuentra en la base de datos", "Ok");
                    muestraProducto = null;
                }


            }
            if (muestraProducto != null)
            {
                mostrado = true;
                MostrarProducto(muestraProducto);
            }
            else
            {
                mostrado = false;
                PrecioTotal.Text = "";
                PrecioLabel.Text = "0";
            }
            

        }

    }

}