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
namespace Oasis_Reader
{
    /// <summary>
    /// Aquí se coloca la información preliminar para la toma del inventario.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaInventario : ContentPage
    {
        public int numeroConteo; // numero de veces que se va a realizar el conteo

        public List<string> Listagg { get; private set; }

        //ubicaciones con el codigo y descripcion concatenado
        //cambio realizado por: Ing. Franklyn Tinoco 29-08-2019
        public List<Td_desc_ubica> UbicacionConca { get; private set; }
       
        public List<Td_desc_ubica> Ubicaciones { get; private set; }
        public List<Td_desc_ubica> Codigos { get; private set; }
        public List<Td_departamento> CodigoDepto { get; private set; }
        public List<Td_departamento> Departamentos { get; private set; }
        public List<Tb_pocket> Dispositivos { get; private set; }
        public Ta_inventario_producto_copy invProdGuardar;
        //trae el ultimo registro
        // cambio realizado por: Ing. Franklyn Tinoco
        public List<Tm_conteo> Conteos { get; private set; }


        public bool inventarioCantidad;

        public string name;
        public int indice;          // indice de picker ubicaciones
        public string codigoubi;    //  codigo de ubicacion
        public string coddepto;     //   codigo de departamento
        public int nroconteo;       //    numero del conteo
        public int nroparte;        //     numero de parte
        public string dispositivo;  //     numero dispositivo  
        public string selecccionaubi { get; private set; }

        public PaginaInventario(string nombreusuario)


        {
            InitializeComponent();

            name = nombreusuario;


            inventarioCantidad = pideCantidad.IsToggled;
            numconteo.IsVisible = !primerConteo.IsToggled;
            numconteolabel.IsVisible = !primerConteo.IsToggled;


            salirInventario.Clicked += Inventario_a_main;
            aceptarInventario.Clicked += Inventario_a_aceptar_inventario;
            primerConteo.Toggled += Mostrar_Conteo;
            pideCantidad.Toggled += Pedir_Cantidad;
            agregarUbicacion.Clicked += Inventario_a_agregar_ubicacion;

            ubics.SelectedIndexChanged += Cambio_ubicacion;
            ubics.Focused += Cambiar_datos;

            Ubicaciones = Login.Td_desc_ubicaDatabase.ListTd_desc_ubica();
            ubics.ItemsSource = Ubicaciones;

            dep.Focused += Refrescar_dptos;
            dep.SelectedIndexChanged += Cambiar_dpto; 
            //llama el ultimo registro del conteo
            Conteos = Login.Tm_conteoDatabase.UltimoRegistro();
            
            Departamentos = Login.Td_departamentoDatabase.ListTd_departamento();
            dep.ItemsSource = Departamentos;
            Ultimo_dato();
        }

                              
        /// <summary>
        /// Retornar del inventario al menú principal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void Inventario_a_main(object sender, EventArgs args)
        {
            this.IsEnabled = false;
            Timer aTimer = new Timer();
            aTimer.Elapsed += (object sender2, ElapsedEventArgs e) => { this.IsEnabled = true; };
            aTimer.Interval = 5000; //ms
            aTimer.Enabled = true;
            Globals.DoBack--;
            Navigation.PopModalAsync();
            
            
        }
        public void Cambiar_dpto(object sender, EventArgs args)
        {
            //Busca el Codigo de ubicacion por la posicion del picker
            //cambio realizado por: Ing Franklyn Tinoco 02-09-2019
            indice = ubics.SelectedIndex;
            CodigoDepto = Login.td_departamentoDatabase.Codigo_departamento();
            //variable global donde se  guarda el codigo de ubicacion
            if (indice>-1)
            {
            coddepto = Convert.ToString(CodigoDepto[indice].Cod_departamento);
            }
            
        }
        public void Ultimo_dato ()
        {
            //rutina traer el ultimo registro de la configuracion del inventario
            //cambio realizado por: Ing. Franklyn Tinoco 02-09-2019
            Conteos = Login.tm_conteoDatabase.UltimoRegistro();

            // pregunta para saber si tiene registros la tabla de 
            // conteos 
            if (Conteos.Count>0) {
                      
            coddepto = Convert.ToString(Conteos[0].Cod_departamento);//graba en codubicacion
            nroconteo = Convert.ToInt32(Conteos[0].Conteo);
            nroparte = Convert.ToInt32(Conteos[0].Parte);
            dispositivo = Convert.ToString(Conteos[0].Id_dispositivo);
            disp.Text = dispositivo;
            numconteo.Text = Convert.ToString(nroconteo);
            Ubicaciones = Login.td_desc_ubicaDatabase.GetTd_desc_ubica(coddepto);
            ubics.ItemsSource = Ubicaciones;
            ubics.SelectedIndex = 0;
            //picker de departamento
             Departamentos=Login.Td_departamentoDatabase.GetTd_departamento(coddepto);
           // Departamentos = Login.Td_departamentoDatabase.ListTd_departamento();
            dep.ItemsSource = Departamentos;
            dep.SelectedIndex = 0;
            }
        }
        public void Cambiar_datos (object sender, EventArgs args)
        {
            // refrescar ubicaciones
            //rutina realizada por: Ing. Franklyn Tinoco
            ubics.ItemsSource = Login.td_desc_ubicaDatabase.ListTd_desc_ubica();
           
        }
        public void Refrescar_dptos(object sender, EventArgs args)
        {
            // refrescar departamentos
            //rutina realizada por: Ing. Franklyn Tinoco
            dep.ItemsSource = Login.td_departamentoDatabase.ListTd_departamento();
        }
        public void Cambio_ubicacion (object sender, EventArgs args)
        {
            //Busca el Codigo de ubicacion por la posicion del picker
            //cambio realizado por: Ing Franklyn Tinoco 02-09-2019
            indice = ubics.SelectedIndex;
            Codigos = Login.td_desc_ubicaDatabase.ListTd_codigo_descripcion();
            //variable global donde se  guarda el codigo de ubicacion
            if (indice > -1)
            {
                codigoubi = Convert.ToString(Codigos[indice].Cod_ubicacion);
            }          

        }
        public void Inventario_a_agregar_ubicacion(object sender, EventArgs args)
        {
            MessagingCenter.Subscribe<PaginaAgregarUbicacion>(this, "Actualizar", (arg) =>
            {
                Ubicaciones = Login.Td_desc_ubicaDatabase.ListTd_desc_ubica();
                ubics.ItemsSource = Ubicaciones;
             
            });

            this.IsEnabled = false;
            Timer aTimer = new Timer();
            aTimer.Elapsed += (object sender2, ElapsedEventArgs e) => { this.IsEnabled = true; };
            aTimer.Interval = 5000; //ms
            aTimer.Enabled = true;
            Globals.DoBack++;
            Navigation.PushModalAsync(new PaginaAgregarUbicacion());
        }






        /// <summary>
        /// Navegar a la página de aceptar inventario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void Inventario_a_aceptar_inventario(object sender, EventArgs args)
        {
            if (ubics.SelectedItem != null && disp.Text != null && disp.Text != String.Empty)
            {
                if (disp.Text.Length > 99)
                {
                    DisplayAlert("N° dispositivo", numconteo.Text + " es muy largo", "Ok");
                }

                //Evalúa si es el primer conteo o no y valida los campos.
                if (numconteo.IsVisible)
                {
                    Regex regex = new Regex("^\\d{1,9}$");
                    if (!string.IsNullOrEmpty(numconteo.Text))
                    {
                        Match match = regex.Match(numconteo.Text);
                        if (match.Success)
                        {

                            

                            numeroConteo = Convert.ToInt32(numconteo.Text);
                            //cambio realizado por Ing. Franklyn Tinoco 03-09-2019
                            //se agrego la variable  codigoubi por ubics.Items[ubics.SelectedIndex] 
                            invProdGuardar = new Ta_inventario_producto_copy(name, 1, codigoubi, disp.Text, 
                                numeroConteo, null, null, 1, 0, null, 0, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), null, null, null);
                            this.IsEnabled = false;
                            Timer aTimer = new Timer();
                            aTimer.Elapsed += (object sender2, ElapsedEventArgs e) => { this.IsEnabled = true; };
                            aTimer.Interval = 5000; //ms
                            aTimer.Enabled = true;
                            Globals.DoBack++;
                            Navigation.PushModalAsync(new PaginaAceptarInventario(inventarioCantidad, numeroConteo, name, invProdGuardar, new Tm_conteo(numeroConteo, disp.Text, pideCantidad.IsToggled.ToString(), codigoubi))); // reemplazo ubics.Items[ubics.SelectedIndex] por coddepto



                        }
                        else
                        {
                            DisplayAlert("N° conteo", numconteo.Text + " es inválido", "Ok");
                        }
                    }
                    else
                    {
                        DisplayAlert("Aceptar", "Datos incompletos", "Ok");
                    }
                }
                else
                {



                    this.IsEnabled = false;
                    Timer aTimer = new Timer();
                    aTimer.Elapsed += (object sender2, ElapsedEventArgs e) => { this.IsEnabled = true; };
                    aTimer.Interval = 5000; //ms
                    aTimer.Enabled = true;
                    invProdGuardar = new Ta_inventario_producto_copy(name, 1, ubics.Items[ubics.SelectedIndex], disp.Text, 1, null, null, 0, 0, null, 0, 
                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), null, null, null);
                    Globals.DoBack++;
                    Navigation.PushModalAsync(new PaginaAceptarInventario(inventarioCantidad, 1, name, invProdGuardar, new Tm_conteo(1, disp.Text, pideCantidad.IsToggled.ToString(), codigoubi)));// reemplazo ubics.Items[ubics.SelectedIndex] por coddepto



                }

            }
            else
            {




                // Esto es lo que va si no acepta los primeros campos vacios
                DisplayAlert("Aceptar", "Datos incompletos", "Ok");
            }




        }

        
        /// <summary>
        /// Muestra el número de conteo si no es el primero por default
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void Mostrar_Conteo(object sender, EventArgs args)
        {
            numconteo.IsVisible = !numconteo.IsVisible;
            numconteolabel.IsVisible = !numconteolabel.IsVisible;
        }
        public void Muestra_Ultima_Data(object sender, EventArgs args)
        {
            //Muestra el Ultimo Valor Cargado en la Configuracion del Inventario

        }

        /// <summary>
        /// Cambia el valor de muestra de cantidad de la página siguiente a ésta.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void Pedir_Cantidad(object sender, EventArgs args)
        {
            inventarioCantidad = !inventarioCantidad;
        }
    }
}