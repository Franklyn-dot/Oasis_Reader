using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oasis_Reader.Models
{

    /// <summary>
    /// Tabla Td_conteo
    /// </summary>
    class Td_conteo
    {

        [PrimaryKey]
        public int Conteo { get; set; }
        [PrimaryKey]
        public int Parte { get; set; }
        public decimal Cantidad { get; set; } //numeric(10,3)
        [MaxLength(18)]
        public string Cod_barra{ get; set; } //varchar(18)
        [MaxLength(10)]
        public string Cod_interno { get; set; } //char(10)
        public string Fecha_conteo { get; set; }
        [PrimaryKey]
        public int Orden { get; set; }
        public int Precio { get; set; }
        [PrimaryKey, MaxLength(40)]
        public string Id_dispositivo { get; set; } //varchar(40)

        public Td_conteo() { }

        public Td_conteo(int Conteo, int Parte, decimal Cantidad, string Cod_barra, string Cod_interno, 
            string Fecha_conteo, int Orden, int Precio, string Id_dispositivo)
        {
            this.Conteo = Conteo;
            this.Parte = Parte;
            this.Cantidad = Cantidad;
            this.Cod_barra = Cod_barra;
            this.Cod_interno = Cod_interno;
            this.Fecha_conteo = Fecha_conteo;
            this.Orden = Orden;
            this.Precio = Precio;
            this.Id_dispositivo = Id_dispositivo;
        }

    }
}
