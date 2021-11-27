using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Oasis_Reader.Data;
using Oasis_Reader.Droid.Data;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_Android))]

namespace Oasis_Reader.Droid.Data
{
    /// <summary>
    /// Gestión de SQLite en Android
    /// </summary>
    class SQLite_Android : ISQLite
    {
        public SQLite_Android() { }

        /// <summary>
        /// Crea una conexión con SQLite
        /// </summary>
        /// <returns></returns>
        public SQLite.SQLiteConnection GetConnection()
        {
            var sqliteFileName = "Capturainventario.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);



            var path = Path.Combine(documentsPath, sqliteFileName);



            





            var conn = new SQLite.SQLiteConnection(path);

            return conn;
        }


        /// <summary>
        /// Copia la base de datos ubicada en el la carpeta local del dispositivo con el nombre Capturainventario.db3";
        /// </summary>
        public void SetDB()
        {
            var sqliteFileName = "Capturainventario.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);



            //Directory with your DB3 file



            
            var dirx = Android.OS.Environment.ExternalStorageDirectory.ToString();
   
            // DB File 
            var dbfile = Path.Combine(dirx, "Capturainventario.db3");
            

            var path = Path.Combine(documentsPath, sqliteFileName);

            if (File.Exists(dbfile))
            {

                
                
                File.Delete(path);
               
                File.Move(dbfile, path);
                

               
            }

            //Console.WriteLine(dbfile);

        }

    }

}