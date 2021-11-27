using Oasis_Reader.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Oasis_Reader.Data
{
    /// <summary>
    /// Controlador de la clase de la tabla Td_desc_ubica
    /// </summary>
    public class Td_desc_ubicaDatabaseController
    {
        static readonly object locker = new object();
        List<Td_desc_ubica> Td_desc_ubicaLista { get; set; }




        SQLiteConnection database;

        public Td_desc_ubicaDatabaseController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.CreateTable<Td_desc_ubica>();
        }

        public List<Td_desc_ubica> ListTd_desc_ubica()
        {
            return database.Query<Td_desc_ubica>("Select * From [Td_desc_ubica]");

        }
        
        public List<Td_desc_ubica> ListTd_codigo_descripcion ()
        {
            return database.Query<Td_desc_ubica>("Select Cod_ubicacion  from [Td_desc_ubica]");
        }


        public List<Td_desc_ubica> GetTd_desc_ubica(string Un)
        {
            lock (locker)
            {
                return database.Query<Td_desc_ubica>("Select * From [Td_desc_ubica] Where Cod_ubicacion  = ? ", Un);
            }
        }




        public int SaveTd_desc_ubica(Td_desc_ubica ubicacion)
        {

            lock (locker)
            {


                if (database.Query<Td_desc_ubica>("Select * From [Td_desc_ubica] Where Cod_ubicacion  = ? ", ubicacion.Cod_ubicacion).Count > 0)
                {

                    return database.Update(ubicacion);



                }
                else
                {

                    return database.Insert(ubicacion);


                }
            }
        }

        public int DeleteTd_desc_ubica(int cod)
        {
            lock (locker)
            {
                return database.Delete<Td_desc_ubica>(cod);
            }
        }

        public bool DeleteAllTd_desc_ubica()
        {
            lock (locker)
            {
                database.Query<Td_desc_ubica>("Delete From [Td_desc_ubica]");
                if (database.Table<Td_desc_ubica>().Count() == 0)
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

