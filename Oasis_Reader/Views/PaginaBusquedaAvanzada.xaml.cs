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
    /// Búsqueda por múltiples parámetros para conseguir el priducto correspondiente y su información.
    /// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PaginaBusquedaAvanzada : ContentPage
	{
        public List<Tv_producto> Productos { get; private set; }
        private string codigoSeleccionado;
        private Tv_producto productoSeleccionado;



        public PaginaBusquedaAvanzada()
		{
			InitializeComponent ();


            salirBusquedaAvanzada.Clicked += Busqueda_Avanzada_a_aceptar_inventario;
            buscarBusquedaAvanzada.Clicked += Query_Busqueda_Avanzada;
            limpiarBusquedaAvanzada.Clicked += Limpiar_Busqueda_Avanzada;
            aceptarBusquedaAvanzada.Clicked += Aceptar_Busqueda_Avanzada;
            infoProductos.ItemSelected += Producto_ItemSelected;

            Productos = Login.Tv_productoDatabase.ListTv_producto();

        }


        /// <summary>
        /// Salir de la búsqueda avanzada y retornar a inventario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void Busqueda_Avanzada_a_aceptar_inventario(object sender, EventArgs args)
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
        /// Ejecuta la busqueda avanzada dependiendo de que valores se han rellenado o no 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void Query_Busqueda_Avanzada(object sender, EventArgs args)
        {
            if (!string.IsNullOrEmpty(entryCodigo.Text))
            {
                if (!string.IsNullOrEmpty(entryDescripcion.Text))
                {
                    if(!string.IsNullOrEmpty(entryReferencia.Text))
                    {
                        Productos = Login.Tv_productoDatabase.GetTv_producto_Cod_Desc_Ref(entryCodigo.Text, entryDescripcion.Text, entryReferencia.Text);
                        
                    }
                    else
                    {
                        Productos = Login.Tv_productoDatabase.GetTv_producto_Cod_Desc(entryCodigo.Text, entryDescripcion.Text);
                       
                    }

                }
                else
                {
                    if (!string.IsNullOrEmpty(entryReferencia.Text))
                    {
                        Productos = Login.Tv_productoDatabase.GetTv_producto_Cod_Ref(entryCodigo.Text, entryReferencia.Text);
                        
                    }
                    else
                    {
                        Productos = Login.Tv_productoDatabase.GetTv_producto_Cod(entryCodigo.Text);
                        
                    }

                }

            

            }
            else
            {
                if (!string.IsNullOrEmpty(entryDescripcion.Text))
                {
                    if (!string.IsNullOrEmpty(entryReferencia.Text))
                    {
                        Productos = Login.Tv_productoDatabase.GetTv_producto_Desc_Ref(entryDescripcion.Text, entryReferencia.Text);
                        
                    }
                    else
                    {
                        Productos = Login.Tv_productoDatabase.GetTv_producto_Desc(entryDescripcion.Text);
                        
                    }

                }
                else
                {
                    if (!string.IsNullOrEmpty(entryReferencia.Text))
                    {
                        Productos = Login.Tv_productoDatabase.GetTv_producto_Ref(entryReferencia.Text);
                        
                    }
                    else
                    {
                        Productos.Clear();
                        DisplayAlert("Búsqueda","Por favor rellene al menos uno de los campos","Ok");
                    }

                }
            }
            infoProductos.ItemsSource = Productos;
            infoProductos2.ItemsSource = Productos;
            infoProductos3.ItemsSource = Productos;
            infoProductos4.ItemsSource = Productos;
            infoProductos5.ItemsSource = Productos;
            infoProductos6.ItemsSource = Productos;
        }
        //-------- Fin Query  --------//


        /// <summary>
        /// Borrar los entries de la busqueda
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void Limpiar_Busqueda_Avanzada(object sender, EventArgs args)
        {
            entryReferencia.Text = "";
            entryDescripcion.Text = "";
            entryCodigo.Text = "";
        }



        private void Producto_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            productoSeleccionado = e.SelectedItem as Tv_producto;

            codigoSeleccionado = productoSeleccionado.Cod_interno;
        }




        public void Aceptar_Busqueda_Avanzada(object sender, EventArgs args)
        {

            if (productoSeleccionado != null)
            {
                // Se envia el mensaje Cod_ interno a PaginaAceptarInventario
                MessagingCenter.Send<PaginaBusquedaAvanzada, string>(this, "Cod_interno", productoSeleccionado.Cod_interno);

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




}