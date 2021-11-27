using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Oasis_Reader.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Menu2 : TabbedPage
    {
        private string name;

        public Menu2 ()
        {
            //InitializeComponent();
        }

        public Menu2(string name)
        {
            this.name = name;
        }
    }
}