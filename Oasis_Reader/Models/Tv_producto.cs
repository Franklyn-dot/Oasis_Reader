using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oasis_Reader.Models
{
    /// <summary>
    /// Tabla Tv_producto
    /// </summary>
    public class Tv_producto
    {
        [PrimaryKey, MaxLength(10)]
        public string Cod_interno { get; set; } //char(10)
        [MaxLength(18)]
        public string Txt_descripcion_larga { get; set; } //varchar(18)
        [MaxLength(30)]
        public string Txt_referencia { get; set; } //varchar(30)
        [MaxLength(2)]
        public string Sec_unidad_medida { get; set; } //varchar(2)
        [MaxLength(14)]
        public decimal Total { get; set; } //numeric(10,3)
        [MaxLength(13)]
        public decimal Precio { get; set; } //numeric(10,2)
        [MaxLength(14)]
        public decimal Unid_empaque { get; set; } //numeric(10,3)
        [MaxLength(4)]
        public string Cod_departamento { get; set; } //char(4)

        public Tv_producto() { }

        public Tv_producto(string Cod_interno, string Txt_descripcion_larga, string Txt_referencia, string Sec_unidad_medida, decimal Total, decimal Precio, decimal Unid_empaque, string Cod_departamento)
        {
            this.Cod_interno = Cod_interno;
            this.Txt_descripcion_larga = Txt_descripcion_larga;
            this.Txt_referencia = Txt_referencia;
            this.Sec_unidad_medida = Sec_unidad_medida;
            this.Total = Total;
            this.Precio = Precio;
            this.Unid_empaque = Unid_empaque;
            this.Cod_departamento = Cod_departamento;

        }
    }
}
