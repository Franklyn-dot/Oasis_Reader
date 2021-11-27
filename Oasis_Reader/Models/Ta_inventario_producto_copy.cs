using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oasis_Reader.Models
{
    /// <summary>
    /// Tabla de los productos almacenados en el inventario
    /// </summary>
    public class Ta_inventario_producto_copy
    {
        [MaxLength(12)]
        public string Usuario { get; set; } //char(12)
        public int Almacen { get; set; }
        [MaxLength(10)]
        public string Ubicacion { get; set; } //char(10)
        [MaxLength(12)]
        public string Id_dispositivo { get; set; } //char(12)
        public string Unimed { get; set; } //char(2)
        [MaxLength(20)]
        public string Cod_barra { get; set; } //char(20)
        public int Tipo_conteo { get; set; }
        [MaxLength(2)]
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public string Cod_interno { get; set; }
        public int Orden { get; set; }
        public string Fecha_conteo { get; set; }
        public string Fecha_toma { get; set; }
        [MaxLength(1)]
        public string Status { get; set; } //char(1)
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(20)]
        public string Barra { get; set; } //char(20)
        

        public Ta_inventario_producto_copy() { }

        public Ta_inventario_producto_copy( string Usuario, int Almacen, string Ubicacion, string Id_dispositivo, int Tipo_conteo, string Unimed,
            string Cod_barra, decimal Cantidad, decimal Precio, string Cod_interno, int Orden, string Fecha_conteo, string Fecha_toma, string Status,
             string Barra)
        {
            this.Usuario = Usuario;
            this.Almacen = Almacen;
            this.Ubicacion = Ubicacion;
            this.Id_dispositivo = Id_dispositivo;
            this.Tipo_conteo = Tipo_conteo;
            this.Unimed = Unimed;
            this.Cod_barra = Cod_barra;
            this.Cantidad = Cantidad;
            this.Precio = Precio;
            this.Cod_interno = Cod_interno;
            this.Orden = Orden;
            this.Fecha_conteo = Fecha_conteo;
            this.Fecha_toma = Fecha_toma;
            this.Status = Status;
            this.Barra = Barra;
            

        }
    }
}
