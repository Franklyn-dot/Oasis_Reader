using Oasis_Reader.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Oasis_Reader.Data
{
    /// <summary>
    /// Controlador de la clase de la tabla Td_departamento
    /// </summary>
    public class Td_departamentoDatabaseController
    {
        static readonly object locker = new object();
        List<Td_departamento> Td_departamentoLista { get; set; }




        SQLiteConnection database;

        public Td_departamentoDatabaseController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.CreateTable<Td_departamento>();
        }

        public List<Td_departamento> ListTd_departamento()
        {
            return database.Query<Td_departamento>("Select * From [Td_departamento]");

        }

        public List<Td_departamento> Codigo_departamento()
        {
            return database.Query<Td_departamento>("Select Cod_departamento from [Td_departamento]");
        }

        public List<Td_departamento> GetTd_departamento(string Un)
        {
            lock (locker)
            {
                return database.Query<Td_departamento>("Select * From [Td_departamento] Where Cod_departamento  = ? ", Un);
            }
        }




        public int SaveTd_departamento(Td_departamento departamento)
        {

            lock (locker)
            {


                if (database.Query<Td_departamento>("Select * From [Td_departamento] Where Cod_departamento  = ? ", departamento.Cod_departamento).Count > 0)
                {

                    return database.Update(departamento);



                }
                else
                {

                    return database.Insert(departamento);


                }
            }
        }

        public int DeleteTd_departamento(int cod)
        {
            lock (locker)
            {
                return database.Delete<Td_departamento>(cod);
            }
        }

        public bool DeleteAllTd_departamento()
        {
            lock (locker)
            {
                database.Query<Td_departamento>("Delete From [Td_departamento]");
                if (database.Table<Td_departamento>().Count() == 0)
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