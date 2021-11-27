using Oasis_Reader.Data;
using Oasis_Reader.Models;
using Oasis_Reader.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;

   
namespace Oasis_Reader
{
    /// <summary>
    /// Página del menú principal
    /// </summary>
    public partial class MainPage : ContentPage
    {
        public string name;



        public MainPage(string nombreusuario)
        {
            InitializeComponent();
#if DEBUG
            
#endif

            name = nombreusuario;
            
            inventario.Clicked += Main_a_inventario;
            archivar.Clicked += Main_a_archivar;
            borrarTodo.Clicked += Main_a_borrarTodo;
            salir.Clicked += Main_a_salir;


        }


        /// <summary>
        /// Ir de página principal a inventario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void Main_a_inventario(object sender, EventArgs args)
        {
            
            this.IsEnabled = false;
            Timer aTimer = new Timer();
            aTimer.Elapsed += (object sender2, ElapsedEventArgs e) => { this.IsEnabled = true; };
            aTimer.Interval = 5000; //ms
            aTimer.Enabled = true;
            Globals.DoBack++;
            //Navigation.PushModalAsync(new PaginaInventario(name));
             
            Navigation.PushModalAsync(new MenuOasis(name));


        }


        /// <summary>
        /// Ir de página principal a la página de envío de archivo de inventario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Main_a_archivar(object sender, EventArgs args)
        {

 

            this.IsEnabled = false;
            Timer aTimer = new Timer();
            aTimer.Elapsed += (object sender2, ElapsedEventArgs e) => { this.IsEnabled = true; };
            aTimer.Interval = 5000; //ms
            aTimer.Enabled = true;

            Globals.DoBack++;
            Navigation.PushModalAsync(new PaginaEnviarArchivo());



            

        }


        /// <summary>
        /// Borrar todo el archivo de inventario registrado hasta ahora.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Main_a_borrarTodo(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Borrar todo", "¿Quieres borrar todo el inventario acumulado?", "Sí", "Cancelar");

            if (response)
            {

                response = await DisplayAlert("Borrar todo", "¿Seguro?", "Sí", "Cancelar");

                if (response)
                {

                    if ( Login.Ta_inventario_producto_copyDatabase.DeleteAllTa_inventario_producto_copy() == true  && Login.Tm_conteoDatabase.DeleteAllTm_conteo() == true )
                    {
                        await DisplayAlert("Inventario borrado","El procedimiento fue realizado correctamente", "Ok");
                    }
                    else
                    {
                        await DisplayAlert("ERROR", "Ha ocurrido un problema con el borrado", "Ok");
                    }
                    
                }
            }


        }


        /// <summary>
        /// Salir de la aplicación
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Main_a_salir(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Salir", "¿Quieres salir de Oasis Reader?", "Sí", "Cancelar");

            if (response)
            {

                
                var closer = DependencyService.Get<ICloseApplication>();
                closer?.CloseApp();
            }

        }


    }
}
