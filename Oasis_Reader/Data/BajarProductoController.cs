using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Oasis_Reader.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Xamarin.Forms;

namespace Oasis_Reader.Data
{  //rutinas de REST API Consumir el Web Services
   //realizado por: Ing Franklyn Tinoco ultima actualizacion 02-10-2019
    class BajarProductoController

    {
        public int nroregistros = 0;
        SQLiteConnection database;
        public int cantregistros = 0;
        public void Index()
        {
            Grabarparametros(); //va a ws de parametros
            Grabarproductos();  // va a ws de productos
            GrabarDepartamentos(); // va ws de departamentos
            GrabarBarra(); //va ws de codigos de barras
            //modificar el modelo 
            //Grabarusuarios();//va ws de usuarios

        }
        public void Grabarccodmsc()
        {

             //se debe crear este nuevo modelo dentro de la aplicacion
             // de las sucursales

            //lee xml departamentos y lo graba a la tabla
            //lee del webservice
            var wsproductos = new System.Net.WebClient();
            //poner el entry de la ip y el puerto
            var url = "http://173.173.26.249:8082/ws_codmsc";
            //invocar al web service
            var resultado = wsproductos.DownloadString(url);


            //conexion a la bd sql lite
            database = DependencyService.Get<ISQLite>().GetConnection();

            //desserializar en el json


          
        }
        public void Grabarusuarios()
        {
            //lee xml departamentos y lo graba a la tabla
            //lee del webservice
            var wsproductos = new System.Net.WebClient();
            //poner el entry de la ip y el puerto
            var url = "http://173.173.26.249:8082/ws_usuario";
            //invocar al web service
            var resultado = wsproductos.DownloadString(url);


            //conexion a la bd sql lite
            database = DependencyService.Get<ISQLite>().GetConnection();

            //desserializar en el json


            Tm_usuario[] data = JsonConvert.DeserializeObject<Tm_usuario[]>(resultado);

            //manda la json

            int registros = 0;
            registros = data.Count();
            nroregistros = registros;
            for (int i = 0; i < registros; i++)
            {
                //modificar el modelo para actualizar
            //    database.Insert(new Tm_usuario(data[i].Cod_usuario, data[i].Tip_usuario23, data[i].Id_usuario, data[i].Cod_cliente, data[i].Prioridad, data[i].Est_usuario25, data[i].Fec_registro, data[i].Esc_trabajo, data[i].Emp_inicio, data[i].Perfil, data[i].Niv_acc, data[i].Niv_ope, data[i].Menu_asig, data[i].Perfil_pos, data[i].Niv_autoriza, data[i].Niv_informa, data[i].Clave_acc, data[i].Fec_cambio_date, data[i].Clave_ant, data[i].Arranque, data[i].Fun_arranque, data[i].Termin_asign, data[i].Grupo_acc, data[i].Sync));

            }
        }
        public void GrabarBarra()
        {
            //lee xml departamentos y lo graba a la tabla
            //lee del webservice
            var wsproductos = new System.Net.WebClient();
            //poner el entry de la ip y el puerto
            var url = "http://173.173.26.249:8082/ws_barra";
            //invocar al web service
            var resultado = wsproductos.DownloadString(url);


            //conexion a la bd sql lite
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.Query<Tv_barra>("delete from tv_barra");
            //desserializar en el json


            Tv_barra[] data = JsonConvert.DeserializeObject<Tv_barra[]>(resultado);

            //manda la json

            int registros = 0;
            registros = data.Count();
            nroregistros = registros;
            for (int i = 0; i < registros; i++)
            {

                database.Insert(new Tv_barra(data[i].Cod_interno, data[i].Cod_barra));

            }
        }
        public void GrabarDepartamentos()
        {
            //lee xml departamentos y lo graba a la tabla
            //lee del webservice
            var wsproductos = new System.Net.WebClient();
            //poner el entry de la ip y el puerto
            var url = "http://173.173.26.249:8082/ws_departamento";
            //invocar al web service
            var resultado = wsproductos.DownloadString(url);


            //conexion a la bd sql lite
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.Query<Td_departamento>("delete from td_departamento");
            //desserializar en el json


            Td_departamento[] data = JsonConvert.DeserializeObject<Td_departamento[]>(resultado);

            //manda la json

            int registros = 0;
            registros = data.Count();
            nroregistros = registros;
            for (int i = 0; i < registros; i++)
            {

                database.Insert(new Td_departamento(data[i].Cod_departamento, data[i].Txt_descrip_dep));

            }
        }
        public void Grabarproductos()
        {
            //lee xml departamentos y lo graba a la tabla
            //lee del webservice
            var wsproductos = new System.Net.WebClient();
            //poner el entry de la ip y el puerto
            var url = "http://173.173.26.249:8082/ws_producto";
            //invocar al web service
            var resultado = wsproductos.DownloadString(url);


            //conexion a la bd sql lite
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.Query<Tv_producto>("delete from tv_producto");
            //desserializar en el json


            Tv_producto[] data = JsonConvert.DeserializeObject<Tv_producto[]>(resultado);

            //manda la json

            int registros = 0;
            registros = data.Count();
            nroregistros = registros;
            for (int i = 0; i < registros; i++)
            {

                database.Insert(new Tv_producto(data[i].Cod_interno, data[i].Txt_descripcion_larga, data[i].Txt_referencia, data[i].Sec_unidad_medida, data[i].Total, data[i].Precio, data[i].Unid_empaque, data[i].Cod_departamento));

            }
        }
        public void Grabarparametros()
        {

            //lee xml departamentos y lo graba a la tabla
            //lee del webservice
            var wsproductos = new System.Net.WebClient();
            var url = "http://173.173.26.249:8082/ws_parametro";
            //invocar al web service
            var resultado = wsproductos.DownloadString(url);


            //conexion a la bd sql lite
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.Query<Tb_paramet>("delete from tb_paramet");
            //desserializar en el json


            Tb_paramet[] data = JsonConvert.DeserializeObject<Tb_paramet[]>(resultado);

            //manda la json

            int registros = 0;
            registros = data.Count();
            for (int i = 0; i < registros; i++)
            {

                database.Insert(new Tb_paramet(data[i].Cod_param, data[i].Num_registro, data[i].Datos, data[i].Flag_estado));

            }


        }



    }




    }



