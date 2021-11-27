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
using ZXing.Net.Mobile.Forms;


namespace Oasis_Reader.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Menu : TabbedPage

    {
        public string name;
        public Menu(string nombreusuario)
        {
           // InitializeComponent();

            name = nombreusuario;
            
        }      
        public void corre()
        {

        }

    }
   
}