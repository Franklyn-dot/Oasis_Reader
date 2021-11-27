using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oasis_Reader.Models
{
    /// <summary>
    /// Tabla Tb_paramet
    /// </summary>
    class Tb_paramet
    {


        [PrimaryKey, MaxLength(6)]
        public int Num_registro { get; set; } //numeric(6)
        [PrimaryKey, MaxLength(6)]
        public string Cod_param { get; set; } //char(6)
       
        public string Datos { get; set; } //varchar(380)
        [MaxLength(2)]
        public string Flag_estado { get; set; } //char(2)

        public Tb_paramet() { }

        public Tb_paramet(string Cod_param, int Num_registro, string Datos, string Flag_estado)
        {
            this.Num_registro = Num_registro;
            this.Cod_param = Cod_param;
            this.Datos = Datos;
            this.Flag_estado = Flag_estado;
        }



    }
}
