using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Oasis_Reader.Models;
using System.Net.Mail;
using System.Net;
using Oasis_Reader.Data;
using System.Timers;
using Oasis_Reader;
using Oasis_Reader.Views;

namespace Oasis_Reader
{
    /// <summary>
    /// Página con el input de usuario y contraseña.
    /// Luego de validar almacena el nombre de usuario como referencia para los proximos registros en tablas
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        
        // Declaración de objetos y controladores esenciales
        static RestService restService;

        public static Ip_puertoDatabaseController ip_puertoDatabase;

        public static Tm_usuarioDatabaseController tm_usuarioDatabase;
        public static Tm_conteoDatabaseController tm_conteoDatabase;
        public static Tv_productoDatabaseController tv_productoDatabase;
        public static Td_desc_ubicaDatabaseController td_desc_ubicaDatabase;
        public static Td_departamentoDatabaseController td_departamentoDatabase;
        public static Tb_pocketDatabaseController tb_pocketDatabase;
        public static Tv_barraDatabaseController tv_barraDatabase;
        public static Ta_inventario_producto_copyDatabaseController ta_inventario_producto_copyDatabase;
       



        public Login()
        {
            InitializeComponent();


           // Content = new Frame
           // {

           // };

            //entrada sistema
            
            botonimagen1.Clicked += entrada_sistema;

            //salida sistema

            botonimagen2.Clicked += Salir_app;

            //acerca de 

            botonimagen3.Clicked += Acerca_de;
        }

        /// <summary>
        /// Validación de usuario y contraseña para permitir login.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        async public void entrada_sistema(object sender, EventArgs args)
        {
            // entrada al sistema modificado por: Ing Franklyn Tinoco 25-7-2019


            List<Tm_usuario> VarList = Login.Tm_usuarioDatabase.GetTm_usuario(Entry_Usuario.Text);
            //VarList[0].Clave_acc;







            //Luego de las pruebas se activa de nuevo con el usuario final

            if (!string.IsNullOrEmpty(Entry_Usuario.Text) && !string.IsNullOrEmpty(Entry_Password.Text))
            {



                if (VarList.Count >= 1)
                {
                   // await DisplayAlert("Login", "Paso el usuario", "Ok");
                  //  await DisplayAlert("Login", "Login exitoso", "Ok");
                    Globals.DoBack++;
                   
                    await Navigation.PushModalAsync(new MenuOasis(Entry_Usuario.Text));
                   // await Navigation.PushModalAsync(new MainPage(Entry_Usuario.Text));
                    //MODIFICADO PARA ENTRAR ING FRANKLYN TINOCO
                    //if ((VarList[0].Clave_acc) == Entry_Password.Text)
                    //{

                    //  await DisplayAlert("Login", "Login exitoso", "Ok");
                    //   Globals.DoBack++;
                    //   await Navigation.PushModalAsync(new MainPage(Entry_Usuario.Text));

                    // }
                    // else
                    // {


                    //   await DisplayAlert("Datos incorrectos", "Introduza su información nuevamente", "Ok");
                    // }
                }
                else
                {

                    await DisplayAlert("Datos incorrectos", "Introduza su información nuevamente", "Ok");
                }

                //Activar solo en pruebas de login
                //Globals.DoBack++;
                //await Navigation.PushModalAsync(new MainPage("aaa"));

            }
            else
            {

                await DisplayAlert("Login", "Por favor rellene todos los campos", "Ok");


            }

            //-------------------------------------- 
        }

        void OnImageButtonClicked(object sender, EventArgs e)
        {
            //entrada al sistema

        }

        // Acerca de

            private  void Acerca_de(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Salir de la aplicación
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Salir_app(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Salir", "¿Quieres salir de Oasis Reader?", "Sí", "Cancelar");

            if (response)
            {


                var closer = DependencyService.Get<ICloseApplication>();
                closer?.CloseApp();
                
            }

        }



        //---------------------------Funciones para controladores de la base de datos y servicios al estilo MVC ------------------//





        public static Tv_productoDatabaseController Tv_productoDatabase
        {
            get
            {
                if (tv_productoDatabase == null)
                {
                    tv_productoDatabase = new Tv_productoDatabaseController();
                }
                return tv_productoDatabase;
            }
        }

        public static Tv_barraDatabaseController Tv_barraDatabase
        {
            get
            {
                if (tv_barraDatabase == null)
                {
                    tv_barraDatabase = new Tv_barraDatabaseController();
                }
                return tv_barraDatabase;
            }
        }

        public static Tm_usuarioDatabaseController Tm_usuarioDatabase
        {
            get
            {
                if (tm_usuarioDatabase == null)
                {
                    tm_usuarioDatabase = new Tm_usuarioDatabaseController();
                }
                return tm_usuarioDatabase;
            }
        }

        public static Tm_conteoDatabaseController Tm_conteoDatabase
        {
            get
            {
                if (tm_conteoDatabase == null)
                {
                    tm_conteoDatabase = new Tm_conteoDatabaseController();
                }
                return tm_conteoDatabase;
            }
        }

        public static Td_desc_ubicaDatabaseController Td_desc_ubicaDatabase
        {
            get
            {
                if (td_desc_ubicaDatabase == null)
                {
                    td_desc_ubicaDatabase = new Td_desc_ubicaDatabaseController();
                }
                return td_desc_ubicaDatabase;
            }
        }


        public static Tb_pocketDatabaseController Tb_pocketDatabase
        {
            get
            {
                if (tb_pocketDatabase == null)
                {
                    tb_pocketDatabase = new Tb_pocketDatabaseController();
                }
                return tb_pocketDatabase;
            }
        }

        public static Td_departamentoDatabaseController Td_departamentoDatabase
        {
            get
            {
                if (td_departamentoDatabase == null)
                {
                    td_departamentoDatabase = new Td_departamentoDatabaseController();
                }
                return td_departamentoDatabase;
            }
        }



        public static Ta_inventario_producto_copyDatabaseController Ta_inventario_producto_copyDatabase
        {
            get
            {
                if (ta_inventario_producto_copyDatabase == null)
                {
                    ta_inventario_producto_copyDatabase = new Ta_inventario_producto_copyDatabaseController();
                }
                return ta_inventario_producto_copyDatabase;
            }
        }




        public static Ip_puertoDatabaseController Ip_puertoDatabase
        {
            get
            {
                if (ip_puertoDatabase == null)
                {
                    ip_puertoDatabase = new Ip_puertoDatabaseController();
                }
                return ip_puertoDatabase;
            }
        }



        public static RestService RestService
        {
            get
            {
                if (restService == null)
                {
                    restService = new RestService();
                }
                return restService;
            }
        }

        //----------------------------------------------------- FIN ----------------------------------------------------------//

    }
}
