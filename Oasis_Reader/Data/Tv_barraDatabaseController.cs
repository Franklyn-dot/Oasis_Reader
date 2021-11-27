using Oasis_Reader.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Oasis_Reader.Data
{
    /// <summary>
    /// Controlador de la clase de la tabla Tv_barra
    /// </summary>
    public class Tv_barraDatabaseController
    {
        static readonly object locker = new object();
        List<Tv_barra> Tv_barraLista { get; set; }




        SQLiteConnection database;

        public Tv_barraDatabaseController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.Execute("CREATE TABLE IF NOT EXISTS Tv_barra (   Cod_interno TEXT, Cod_barra TEXT,   PRIMARY KEY(Cod_interno, Cod_barra)" +
            "CHECK(length(Cod_interno) <= 10  AND length(Cod_barra) <= 20))");

        }

        public List<Tv_barra> ListTv_barra()
        {
            return database.Query<Tv_barra>("Select * From [Tv_barra]");

        }



        public List<Tv_barra> GetTv_barra(string Un)
        {
            lock (locker)
            {
                return database.Query<Tv_barra>("Select * From [Tv_barra] Where Cod_interno  = ? ", Un);
            }
        }


        public List<Tv_barra> GetTv_barraCodBarra(string Un)
        {
            lock (locker)
            {
                return database.Query<Tv_barra>("Select * From [Tv_barra] Where Cod_barra  = ? ", Un);
            }
        }




        public int SaveTv_barra(Tv_barra barra)
        {

            lock (locker)
            {


                if (database.Query<Tv_barra>("Select * From [Tv_barra] Where Cod_interno  = ? ", barra.Cod_interno).Count > 0
                    && database.Query<Tv_barra>("Select * From [Tv_barra] Where Cod_barra  = ? ", barra.Cod_barra).Count > 0)
                {

                    return database.Update(barra);



                }
                else
                {

                    return database.Insert(barra);


                }
            }
        }

        public int DeleteTv_barra(int cod)
        {
            lock (locker)
            {
                return database.Delete<Tv_barra>(cod);
            }
        }

        public bool DeleteAllTv_barra()
        {
            lock (locker)
            {
                database.Query<Tv_barra>("Delete From [Tv_barra]");
                if (database.Table<Tv_barra>().Count() == 0)
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