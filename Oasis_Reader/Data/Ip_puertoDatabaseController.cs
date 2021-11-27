using Oasis_Reader.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Oasis_Reader.Data
{
    
    public class Ip_puertoDatabaseController
    {
        static readonly object locker = new object();
        List<Ip_puerto> Ip_puertoLista { get; set; }




        SQLiteConnection database;

        public Ip_puertoDatabaseController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.CreateTable<Ip_puerto>();

        }

        public List<Ip_puerto> ListIp_puerto()
        {
            return database.Query<Ip_puerto>("Select * From [Ip_puerto]");

        }






  




        public int SaveIp_puerto(Ip_puerto ipp)
        {

            lock (locker)
            {


                if (database.Query<Ip_puerto>("Select * From [Ip_puerto] Where Ip  = ? ", ipp.Ip).Count > 0
                    || database.Query<Ip_puerto>("Select * From [Ip_puerto] Where Puerto  = ? ", ipp.Puerto).Count > 0)
                {

                    DeleteAllIp_puerto();

                    return database.Insert(ipp);



                }
                else
                {

                    return database.Insert(ipp);


                }
            }
        }

        public int DeleteIp_puerto(int ip)
        {
            lock (locker)
            {
                return database.Delete<Ip_puerto>(ip);
            }
        }

        public bool DeleteAllIp_puerto()
        {
            lock (locker)
            {
                database.Query<Ip_puerto>("Delete From [Ip_puerto]");
                if (database.Table<Ip_puerto>().Count() == 0)
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
