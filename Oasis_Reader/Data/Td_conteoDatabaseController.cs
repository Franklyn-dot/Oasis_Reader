using Oasis_Reader.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Oasis_Reader.Data
{
    /// <summary>
    /// Controlador de la clase de la tabla Td_conteo
    /// </summary>
    class Td_conteoDatabaseController
    {


        static readonly object locker = new object();
        List<Td_conteo> Td_conteoLista { get; set; }




        SQLiteConnection database;

        public Td_conteoDatabaseController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.Execute("CREATE TABLE Td_conteo (   Conteo INTEGER, Parte INTEGER, Cantidad NUMERIC, Cod_barra TEXT, Cod_interno TEXT," +
            "Fecha_conteo DATETIME, Orden INTEGER, Precio INTEGER, Id_dispositivo TEXT, " +
            "PRIMARY KEY(Conteo, Parte, Orden, Id_dispositivo) CHECK(length(Id_dispositivo) <= 40  AND length(Cod_interno) <= 10 AND length(Cod_barra) <= 18))");
        }

        public List<Td_conteo> ListTd_conteo()
        {
            return database.Query<Td_conteo>("Select * From [Td_conteo]");
            
        }
        public List<Td_conteo> Numero_registros()
        {
            //nro de registros
            return database.Query<Td_conteo>("Select count(Conteo) as reg from [Td_conteo]");
        }


        public List<Td_conteo> GetTd_conteo(string Un)
        {
            lock (locker)
            {
                return database.Query<Td_conteo>("Select * From [Td_conteo] Where Conteo  = ? ", Un);
            }
        }




        public int SaveTd_conteo(Td_conteo conteo)
        {

            lock (locker)
            {


                if (database.Query<Td_conteo>("Select * From [Td_conteo] Where Conteo  = ? ", conteo.Conteo).Count > 0)
                {

                    return database.Update(conteo);



                }
                else
                {

                    return database.Insert(conteo);


                }
            }
        }

        public int DeleteTd_conteo(int cod)
        {
            lock (locker)
            {
                return database.Delete<Td_conteo>(cod);
            }
        }

        public bool DeleteAllTd_conteo()
        {
            lock (locker)
            {
                database.Query<Td_conteo>("Delete From [Td_conteo]");
                if (database.Table<Td_conteo>().Count() == 0)
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

