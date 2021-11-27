using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Oasis_Reader.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuOasisMaster : ContentPage
    {
        public ListView ListView;

        public MenuOasisMaster()
        {
            InitializeComponent();

            BindingContext = new MenuOasisMasterViewModel();
            ListView = MenuItemsListView;
        }

        class MenuOasisMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MenuOasisMasterMenuItem> MenuItems { get; set; }

            public MenuOasisMasterViewModel()
            {
                
                MenuItems = new ObservableCollection<MenuOasisMasterMenuItem>(new[]
                {
                    new MenuOasisMasterMenuItem { Id = 0, Title = "Inventario" },
                    new MenuOasisMasterMenuItem { Id = 1, Title = "Enviar Datos" },
                    new MenuOasisMasterMenuItem { Id = 2, Title = "Actualizar Datos" },
                    new MenuOasisMasterMenuItem { Id = 3, Title = "Borrar Datos" },
                    new MenuOasisMasterMenuItem { Id = 4, Title = "Salir" }, 
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}