using Oasis_Reader.Data;
using Oasis_Reader.Models;

using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading;
using System.Threading.Tasks;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Oasis_Reader
{
    /// <summary>
    /// Clase principal de la aplicación
    /// </summary>
    public partial class App : Application
    {
        //Solo debug, comentar cuando se haga el release
        public Filler x;

        


  


        public App()
        {
            

            InitializeComponent();
#if DEBUG
          
#endif

            //Solo debug, comentar cuando se haga el release
            x = new Filler();
            x.Fill();


            //Solo release, Colocar como comentario en modo debug
            //DependencyService.Get<ISQLite>().SetDB();//



            MainPage = new NavigationPage(new Login());

           

        }




        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }




    }
}
