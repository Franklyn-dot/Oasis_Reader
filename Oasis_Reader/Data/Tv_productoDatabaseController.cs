using Oasis_Reader.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Oasis_Reader.Data
{
    /// <summary>
    /// Controlador de la clase de la tabla Tv_producto
    /// </summary>
    public class Tv_productoDatabaseController
    {
        static readonly object locker = new object();
        List<Tv_producto> Tv_productoLista { get; set; }
        



        SQLiteConnection database;

        public Tv_productoDatabaseController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.CreateTable<Tv_producto>();
        }

        public List<Tv_producto> ListTv_producto()
        {
            return database.Query<Tv_producto>("Select * From [Tv_producto]");

        }



        public List<Tv_producto> GetTv_producto(string Un)
        {
            lock (locker)
            {
                return database.Query<Tv_producto>("Select * From [Tv_producto] Where Cod_interno  = ? ", Un);
            }
        }




        //---------------------------------------------------- Queries de búsqueda avanzada ------------------------------------------//

        /// <summary>
        /// Buscar el producto según el patrón dentro de su código interno, descripción y referencia, en ese orden
        /// </summary>
        /// <param name="Un"></param>
        /// <returns></returns>
        public List<Tv_producto> GetTv_producto_Cod_Desc_Ref(string Cod, string Desc, string Ref)
        {
            lock (locker)
            {
                return database.Query<Tv_producto>("Select * From [Tv_producto] Where Cod_interno like ?  and Txt_descripcion_larga like ? and Txt_referencia like ? ", "%" + Cod +"%", "%" + Desc + "%", "%" + Ref + "%");
            }
        }


        /// <summary>
        /// Buscar el producto según el patrón dentro de su código interno y descripción  en ese orden
        /// </summary>
        /// <param name="Un"></param>
        /// <returns></returns>
        public List<Tv_producto> GetTv_producto_Cod_Desc(string Cod, string Desc)
        {
            lock (locker)
            {
                return database.Query<Tv_producto>("Select * From [Tv_producto] Where Cod_interno like ?  and Txt_descripcion_larga like ? ", "%" + Cod + "%", "%" + Desc + "%");
            }
        }


        /// <summary>
        /// Buscar el producto según el patrón dentro de su descripción y referencia, en ese orden
        /// </summary>
        /// <param name="Un"></param>
        /// <returns></returns>
        public List<Tv_producto> GetTv_producto_Desc_Ref(string Desc, string Ref)
        {
            lock (locker)
            {
                return database.Query<Tv_producto>("Select * From [Tv_producto] Where Txt_descripcion_larga like ? and Txt_referencia like ? ", "%" + Desc + "%", "%" + Ref + "%");
            }
        }

        /// <summary>
        /// Buscar el producto según el patrón dentro de su código interno, y referencia, en ese orden
        /// </summary>
        /// <param name="Un"></param>
        /// <returns></returns>
        public List<Tv_producto> GetTv_producto_Cod_Ref(string Cod, string Ref)
        {
            lock (locker)
            {
                return database.Query<Tv_producto>("Select * From [Tv_producto] Where Cod_interno like ?  and Txt_referencia like ? ", "%" + Cod + "%", "%" + Ref + "%");
            }
        }


        /// <summary>
        /// Buscar el producto según el patrón dentro de su código interno
        /// </summary>
        /// <param name="Un"></param>
        /// <returns></returns>
        public List<Tv_producto> GetTv_producto_Cod(string Cod)
        {
            lock (locker)
            {
                return database.Query<Tv_producto>("Select * From [Tv_producto] Where Cod_interno like ?   ", "%" + Cod + "%");
            }
        }

        /// <summary>
        /// Buscar el producto según el patrón dentro de su descripción
        /// </summary>
        /// <param name="Un"></param>
        /// <returns></returns>
        public List<Tv_producto> GetTv_producto_Desc(string Desc)
        {
            lock (locker)
            {
                return database.Query<Tv_producto>("Select * From [Tv_producto] Where Txt_descripcion_larga like ? ", "%" + Desc + "%");
            }
        }


        /// <summary>
        /// Buscar el producto según el patrón dentro de su referencia
        /// </summary>
        /// <param name="Un"></param>
        /// <returns></returns>
        public List<Tv_producto> GetTv_producto_Ref(string Ref)
        {
            lock (locker)
            {
                return database.Query<Tv_producto>("Select * From [Tv_producto] Where Txt_referencia like ? ", "%" + Ref + "%");
            }
        }

        //------------------------------------------------- FIN Queries de búsqueda avanzada ------------------------------------------//



        public int SaveTv_producto(Tv_producto producto)
        {

            lock (locker)
            {


                if (database.Query<Tv_producto>("Select * From [Tv_producto] Where Cod_interno  = ? ", producto.Cod_interno).Count > 0)
                {

                    return database.Update(producto);



                }
                else
                {

                    return database.Insert(producto);


                }
            }
        }

        public int DeleteTv_producto(int cod)
        {
            lock (locker)
            {
                return database.Delete<Tv_producto>(cod);
            }
        }

        public bool DeleteAllTv_producto()
        {
            lock (locker)
            {
                database.Query<Tv_producto>("Delete From [Tv_producto]");
                if (database.Table<Tv_producto>().Count() == 0)
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
