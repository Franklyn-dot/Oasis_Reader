using Oasis_Reader.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Oasis_Reader.Data
{
    /// <summary>
    /// Controlador de la clase de la tabla Tm_usuario
    /// </summary>
    public class Tm_usuarioDatabaseController
    {
        static readonly object locker = new object();
        List<Tm_usuario> Tm_usuarioLista { get; set; }




        SQLiteConnection database;

        public Tm_usuarioDatabaseController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.CreateTable<Tm_usuario>();
        }

        public List<Tm_usuario> ListTm_usuario()
        {
            return database.Query<Tm_usuario>("Select * From [Tm_usuario]");

        }



        public List<Tm_usuario> GetTm_usuario(string cod)
        {
            lock (locker)



            {
                Console.WriteLine("BUSCANDO USUARIO");
                Console.WriteLine(cod);
                 
                return database.Query<Tm_usuario>("Select * From [Tm_usuario] Where cod_usuario  = ?",cod);
            }
        }


        public List<Tm_usuario> GetTm_usuarioUsername(string Un)
        {
            lock (locker)
            {
                return database.Query<Tm_usuario>("Select * From [Tm_usuario] Where id_usuario  = ? ", Un);
                
            }
        }




        public int SaveTm_usuario(Tm_usuario usuario)
        {

            lock (locker)
            {


                if (database.Query<Tm_usuario>("Select * From [Tm_usuario] Where Cod_usuario  = ? ", usuario.Cod_usuario).Count > 0)
                {

                    return database.Update(usuario);



                }
                else
                {

                    return database.Insert(usuario);


                }
            }
        }

        public int DeleteTm_usuario(int cod)
        {
            lock (locker)
            {
                return database.Delete<Tm_usuario>(cod);
            }
        }

        public bool DeleteAllTm_usuario()
        {
            lock (locker)
            {
                database.Query<Tm_usuario>("Delete From [Tm_usuario]");
                if (database.Table<Tm_usuario>().Count() == 0)
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
