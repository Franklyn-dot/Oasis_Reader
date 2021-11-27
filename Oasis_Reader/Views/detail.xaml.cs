using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oasis_Reader.Data;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Oasis_Reader.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class detail : ContentPage
    {
        public string name;
        public detail(string nombreusuario)
        {
            InitializeComponent();
            name = nombreusuario;
            botoniventario.Clicked += Detalles;
            botonenviardatos.Clicked += Enviar;
            botoneliminar.Clicked += Main_a_borrarTodo;
            botonactualizar.Clicked += Actualizar;
            botonsalir.Clicked += Salir;

        }
        public async void Salir (object sender, EventArgs args)
        {
            //Salir
            var response = await DisplayAlert("Salir", "¿Quieres salir de Oasis Reader?", "Sí", "Cancelar");

            if (response)
            {


                var closer = DependencyService.Get<ICloseApplication>();
                closer?.CloseApp();
               
            }
        }
        public void Detalles(object sender,EventArgs args)
        {
            //inventario
           Navigation.PushModalAsync(new PaginaInventario(name));


        }
        public void Enviar(object sender,EventArgs args)
        {
            //enviar datos
            Navigation.PushModalAsync(new PaginaEnviarArchivo());
        }
        public void Borrar(object sender, EventArgs args)
        {
            //enviar datos
            Navigation.PushModalAsync(new borrar());
        }
        public void Actualizar(object sender, EventArgs args)
        {
            //actuliazar datos
            Navigation.PushModalAsync(new PaginaActualizarDatos());
        }
        private async void Main_a_borrarTodo(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Borrar todo", "¿Quieres borrar todo el inventario acumulado?", "Sí", "Cancelar");

            if (response)
            {

                response = await DisplayAlert("Borrar todo", "¿Seguro?", "Sí", "Cancelar");

                if (response)
                {

                    if (Login.Ta_inventario_producto_copyDatabase.DeleteAllTa_inventario_producto_copy() == true && Login.Tm_conteoDatabase.DeleteAllTm_conteo() == true)
                    {
                                             
                       
                        await DisplayAlert("Inventario borrado", "El procedimiento fue realizado correctamente", "Ok");
                    }
                    else
                    {
                        await DisplayAlert("ERROR", "Ha ocurrido un problema con el borrado", "Ok");
                    }

                }
            }


        }
    }
}