using Oasis_Reader.Data;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Oasis_Reader.Models
{
    public class Filler
    {

        SQLiteConnection database;
        DateTime TestDate = new DateTime(2008, 5, 1, 8, 30, 52);
        public string DateSQL ;

        public Filler()
        {
            

            database = DependencyService.Get<ISQLite>().GetConnection();
           // database.DropTable<Tv_barra>();
           // database.DropTable<Tv_producto>();
          //  database.DropTable<Tm_usuario>();
            //database.DropTable<Tm_conteo>();
          //  database.DropTable<Td_desc_ubica>();
           // database.DropTable<Tb_paramet>();
          //  database.DropTable<Tb_pocket>();
          //  database.DropTable<Td_conteo>();
          //  database.DropTable<Td_departamento>();
            //database.DropTable<Ta_inventario_producto_copy>();


            database.Execute("CREATE TABLE IF NOT EXISTS Tb_paramet (   Cod_param TEXT, Num_registro INTEGER, Datos TEXT, Flag_estado TEXT,  PRIMARY KEY(Cod_param, Num_registro)" +
            "CHECK(length(Cod_param) <= 6 AND length(Num_registro) <= 6 AND length(Datos) <= 380 AND length(Flag_estado) <= 2))");

            database.CreateTable<Tb_pocket>();

            database.Execute("CREATE TABLE IF NOT EXISTS Td_conteo (   Conteo INTEGER, Parte INTEGER, Cantidad NUMERIC, Cod_barra TEXT, Cod_interno TEXT," +
            "Fecha_conteo DATETIME, Orden INTEGER, Precio INTEGER, Id_dispositivo TEXT, " +
            "PRIMARY KEY(Conteo, Parte, Orden, Id_dispositivo) CHECK(length(Id_dispositivo) <= 40  AND length(Cod_interno) <= 10 AND length(Cod_barra) <= 18))");

            database.CreateTable<Td_departamento>();
            database.CreateTable<Td_desc_ubica>();
            database.Execute("CREATE TABLE IF NOT EXISTS Tm_conteo (   Conteo INTEGER, Parte INTEGER, Id_dispositivo TEXT, Pide_cantidad TEXT, Cod_departamento TEXT," +
            "PRIMARY KEY(Conteo, Parte, Id_dispositivo) CHECK(length(Id_dispositivo) <= 40  AND length(Cod_departamento) <= 4))");
            database.CreateTable<Tm_usuario>();
            database.Execute("CREATE TABLE IF NOT EXISTS Tv_barra (   Cod_interno TEXT, Cod_barra TEXT,   PRIMARY KEY(Cod_interno, Cod_barra)" +
            "CHECK(length(Cod_interno) <= 10  AND length(Cod_barra) <= 20))");
            database.CreateTable<Tv_producto>();
        }

        public void Fill()
        {
            DateSQL = TestDate.ToString("s");
            char[] ch = DateSQL.ToCharArray();
            ch[10] = ' ';
            DateSQL = new string(ch);




            Console.WriteLine("Llenando Base de datos de prueba <----------------------------------------------");

            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            //database.Query<Tv_producto>("Delete From [Tv_producto]");
           // database.Insert(new Tb_paramet("hola", 123, "holaholahola", "06"));
          //  database.Insert(new Tb_pocket("pocket 1", "este es el pocket de prueba", "001"));
          //  database.Insert(new Tb_pocket("2", "este es el pocket de prueba", "002"));
          //  database.Insert(new Td_conteo(1, 1, 0, "1", "1", DateSQL,1,1,"01"));
          //  database.Insert(new Td_departamento("INS001","departamento inventado"));
          //  database.Insert(new Td_desc_ubica("1","ubicacion 1", 8));
          //  database.Insert(new Td_departamento("MAQ350", " otro departamento inventado"));
          //  database.Insert(new Td_desc_ubica("350", "300 mas 50", 8));
          //  database.Insert(new Td_departamento("IND022", "departamento inventado 3"));
          //  database.Insert(new Td_desc_ubica("420", "esto no es una ubicacion o si?", 0));
         //   database.Insert(new Td_departamento("LAR150", " otro departamento inventado mas 4"));
         //   database.Insert(new Td_departamento("INS002", "departamento inventado 5"));
         //   database.Insert(new Td_desc_ubica("1", "ubicacion unica", 8));
         //   database.Insert(new Td_departamento("MAQ450", " otro departamento inventado 6"));
         //   database.Insert(new Td_desc_ubica("460", "300 mas 50", 8));
         //   database.Insert(new Td_departamento("IND000", "departamento inventado 7"));
         //   database.Insert(new Td_desc_ubica("620", "esto no es una ubicacion o si?", 0));
         //   database.Insert(new Td_departamento("LJP300", " otro departamento inventado mas 8"));
         //   database.Insert(new Td_desc_ubica("251", "lorem ipsum", 7));
         //   database.Insert(new Td_desc_ubica("1234", "franklyn", 7));

         //   database.Insert(new Tm_usuario("1", "1", 1, "1", "8", 1, 1, new DateTime(2008, 5, 1, 8, 30, 52), 2,"hola", "hello", 1, 1, 2, "pos", 34, 42, "clavee", new DateTime(2008, 5, 1, 8, 30, 52), "claveant a" , 1, "hola", 1, 1, 1, 1  ));

         //   database.Insert(new Tv_barra("30", "7777777"));
         //   database.Insert(new Tv_barra("31", "88888888"));
         //   database.Insert(new Tv_barra("42", "7591518006415"));
         //   database.Insert(new Tv_producto("30","pan","producto de prueba 3","kg", 5.0m, 5.0m, 5.0m, "842"));
         //   database.Insert(new Tv_producto("31", "carne", "producto de prueba 4", "kg", 7.0m, 5.0m, 3.0m, "842"));
         //   database.Insert(new Tv_producto("42", "Vodka", "Producto Escaneado", "kg", 10.0m, 10.0m, 10.0m, "842"));

        }
        


    }
}
