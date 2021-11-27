using Oasis_Reader.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Oasis_Reader.Data
{

    /// <summary>
    /// Controlador de la clase de la tabla Tb_pocket
    /// </summary>
    public class Tb_pocketDatabaseController
    {
        static readonly object locker = new object();
        List<Tb_pocket> Tb_pocketLista { get; set; }

        SQLiteConnection database;

        public Tb_pocketDatabaseController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.CreateTable<Tb_pocket>();
        }

        public List<Tb_pocket> ListTb_pocket()
        {
            return database.Query<Tb_pocket>("Select * From [Tb_pocket]");

        }



        public List<Tb_pocket> GetTb_pocket(string Un)
        {
            lock (locker)
            {
                return database.Query<Tb_pocket>("Select * From [Tb_pocket] Where Id  = ? ", Un);
            }
        }




        public int SaveTb_pocket(Tb_pocket pocket)
        {

            lock (locker)
            {


                if (database.Query<Tb_pocket>("Select * From [Tb_pocket] Where Id  = ? ", pocket.Id).Count > 0)
                {

                    return database.Update(pocket);



                }
                else
                {

                    return database.Insert(pocket);


                }
            }
        }

        public int DeleteTb_pocket(int cod)
        {
            lock (locker)
            {
                return database.Delete<Tb_pocket>(cod);
            }
        }

        public bool DeleteAllTb_pocket()
        {
            lock (locker)
            {
                database.Query<Tb_pocket>("Delete From [Tb_pocket]");
                if (database.Table<Tb_pocket>().Count() == 0)
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
