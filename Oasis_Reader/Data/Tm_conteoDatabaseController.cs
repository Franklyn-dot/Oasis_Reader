using Oasis_Reader.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Oasis_Reader.Data
{
    /// <summary>
    /// Controlador de la clase de la tabla Tm_conteo
    /// </summary>
    public class Tm_conteoDatabaseController
    {
        static readonly object locker = new object();
        List<Tm_conteo> Tm_conteoLista { get; set; }




        SQLiteConnection database;

        public Tm_conteoDatabaseController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.Execute("CREATE TABLE IF NOT EXISTS Tm_conteo (   Conteo INTEGER, Parte INTEGER, Id_dispositivo TEXT, Pide_cantidad TEXT, Cod_departamento TEXT," +
                        "PRIMARY KEY(Conteo, Parte, Id_dispositivo) CHECK(length(Id_dispositivo) <= 40  AND length(Cod_departamento) <= 4))");
        }


        public List<Tm_conteo> ListTm_conteo()
        {
            return database.Query<Tm_conteo>("Select * From [Tm_conteo]");

        }

        public List <Tm_conteo> Numero_registros()
        {
            //nro de registros de tabla conteo
            //cambio realizado por: Ing Franklyn Tinoco 03-09-2019
            return database.Query<Tm_conteo>("Select count(Conteo) as ref from [Tm_conteo]");
        }


        public List<Tm_conteo> GetTm_conteo(string Un)
        {
            lock (locker)
            {
                return database.Query<Tm_conteo>("Select * From [Tm_conteo] Where Parte  = ? ", Un);
            }
        }

        /// <summary>
        /// Chequea Tm_conteo por sus claves primarias
        /// </summary>
        /// <param name="dep"></param>
        /// <param name="disp"></param>
        /// <param name="Un"></param>
        /// <returns></returns>
        public List<Tm_conteo> GetTm_conteoPrimKeys(int dep, string disp, int Un)
        {
            lock (locker)
            {
                return database.Query<Tm_conteo>("Select * From [Tm_conteo] Where Conteo  = ? and Id_dispositivo = ? and Cod_departamento = ? ", Un, disp, dep);
            }
        }

        public List<Tm_conteo> UltimoRegistro()
            {
            // linq que busca el ultimo registro de la tabla conteo
            // realizado por: Ing Franklyn Tinoco 28-08-2019

            return database.Query<Tm_conteo>("Select * From [Tm_conteo]   order by conteo desc limit 1");
            }


        public int SaveTm_conteo(Tm_conteo conteo)
        {

            lock (locker)
            {


                if (database.Query<Tm_conteo>("Select * From [Tm_conteo] Where Conteo  = ?  and Id_dispositivo = ? and Cod_departamento = ?", conteo.Conteo, conteo.Id_dispositivo, conteo.Cod_departamento).Count > 0)
                {

                    return database.Update(conteo);



                }
                else
                {

                    return database.Insert(conteo);


                }
            }
        }

        public void DeleteTm_conteo(int cod)
        {
            lock (locker)
            {
                database.Query<Tm_conteo>("Delete From [Tm_conteo] Where Parte  = ? ", cod);
            }
        }

        public bool DeleteAllTm_conteo()
        {
            lock (locker)
            {
                database.Query<Tm_conteo>("Delete From [Tm_conteo]");
                if (database.Table<Tm_conteo>().Count() == 0)
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

