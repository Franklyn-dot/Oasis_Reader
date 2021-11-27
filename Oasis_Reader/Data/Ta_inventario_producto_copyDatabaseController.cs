using Oasis_Reader.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Oasis_Reader.Data
{
    /// <summary>
    /// Controlador de la clase de la tabla Ta_inventario_producto_copy
    /// </summary>
    public class Ta_inventario_producto_copyDatabaseController
    {
        static readonly object locker = new object();
        List<Ta_inventario_producto_copy> Ta_inventario_producto_copyLista { get; set; }




        SQLiteConnection database;

        public Ta_inventario_producto_copyDatabaseController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.CreateTable<Ta_inventario_producto_copy>();
        }

        public List<Ta_inventario_producto_copy> ListTa_inventario_producto_copy()
        {
            return database.Query<Ta_inventario_producto_copy>("Select * From [Ta_inventario_producto_copy] Where Cantidad > 0");
            //
        }



        public List<Ta_inventario_producto_copy> GetTa_inventario_producto_copy(int Un)
        {
            lock (locker)
            {
                return database.Query<Ta_inventario_producto_copy>("Select * From [Ta_inventario_producto_copy] Where Id  = ? ", Un);
            }
        }


        public List<Ta_inventario_producto_copy> EnviadoTa_inventario_producto_copy(int Un)
        {
            lock (locker)
            {
                return database.Query<Ta_inventario_producto_copy>("Update [Ta_inventario_producto_copy] Set Enviado = 1 Where Id  = ?  to ", Un);
            }
        }




        public int SaveTa_inventario_producto_copy(Ta_inventario_producto_copy producto)
        {

            lock (locker)
            {
  

                if (database.Query<Ta_inventario_producto_copy>("SELECT * FROM [Ta_inventario_producto_copy] WHERE Id  = ? ", producto.Id).Count >= 1)
                {

                    return database.Update(producto);



                }
                else
                {

                    return database.Insert(producto);


                }
            }
        }

        public int DeleteTa_inventario_producto_copy(int cod)
        {
            lock (locker)
            {
                return database.Delete<Ta_inventario_producto_copy>(cod);
            }
        }

        public bool DeleteAllTa_inventario_producto_copy()
        {
            lock (locker)
            {
                database.Query<Ta_inventario_producto_copy>("Delete From [Ta_inventario_producto_copy]");
                if (database.Table<Ta_inventario_producto_copy>().Count() == 0)
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
