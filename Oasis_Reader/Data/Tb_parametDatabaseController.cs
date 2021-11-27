using Oasis_Reader.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Oasis_Reader.Data
{

    /// <summary>
    /// Controlador de la clase de la tabla Tb_paramet
    /// </summary>
    class Tb_parametDatabaseController
    {
        static readonly object locker = new object();
        List<Tb_paramet> Tb_parametLista { get; set; }

               
        SQLiteConnection database;



        public Tb_parametDatabaseController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.Execute("CREATE TABLE IF NOT EXISTS Tb_paramet (  Num_registro INTEGER, Cod_param TEXT,Datos TEXT,Flag_estado TEXT, " +
            "PRIMARY KEY(Num_registro) ");
        }

        public List<Tb_paramet> ListTb_paramet()
        {
            return database.Query<Tb_paramet>("Select * From [Tb_paramet]");

        }


        public List<Tb_paramet> LisTb_UltimaFila()
        {
            //lee la ultima Fila Grabada de Parametros
            //Rutina Realizada por: Ing. Franklny Tinoco 27-08-2019
            return database.Query<Tb_paramet>("Select * From [Tb_paramet] order by id desc");
        }
        public List<Tb_paramet> GetTb_paramet(string Un)
        {
            lock (locker)
            {
                return database.Query<Tb_paramet>("Select * From [Tb_paramet] Where Id  = ? ", Un);
            }
        }




        public int SaveTb_paramet(Tb_paramet paramet)
        {

            lock (locker)
            {


                if (database.Query<Tb_paramet>("Select * From [Tb_paramet] Where Cod_param  = ? ", paramet.Cod_param).Count > 0 && 
                    database.Query<Tb_paramet>("Select * From [Tb_paramet] Where Num_registro  = ? ", paramet.Num_registro).Count > 0)
                {

                    return database.Update(paramet);



                }
                else
                {

                    return database.Insert(paramet);


                }
            }
        }

        public int DeleteTb_paramet(int cod)
        {
            lock (locker)
            {
                return database.Delete<Tb_paramet>(cod);
            }
        }

        public bool DeleteAllTb_paramet()
        {
            lock (locker)
            {
                database.Query<Tb_paramet>("Delete From [Tb_paramet]");
                if (database.Table<Tb_paramet>().Count() == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
    }
}
