using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Oasis_Reader.Views
{

    public class MenuOasisMasterMenuItem
    {
        public MenuOasisMasterMenuItem()
        {
            TargetType = typeof(MenuOasisMasterMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
        public ImageSource icon { get; set; }

          
    }
}