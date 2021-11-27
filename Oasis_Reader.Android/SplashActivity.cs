using Android.App;
using Android.Content.PM;
using Android.OS;

namespace Oasis_Reader.Droid
{
    [Activity(Theme = "@style/Theme.Splash", //Indicamos el tema que usaremos en esta actividad
             MainLauncher = true, //Lo inicializamos como pantalla de inicio
             NoHistory = true)
        ] //No generamos historial para que no nos regrese a esta pantalla

    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            this.StartActivity(typeof(MainActivity));
        }
    }
}