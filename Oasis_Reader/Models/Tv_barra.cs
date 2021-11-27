using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oasis_Reader.Models
{
    /// <summary>
    /// Tabla Tv_barra
    /// </summary>
    public class Tv_barra
    {
        [PrimaryKey, MaxLength(10)]
        public string Cod_interno { get; set; } //char(10)
        [PrimaryKey, MaxLength(20)]
        public string Cod_barra { get; set; }  //char(20)

        public Tv_barra() { }

        public Tv_barra(string Cod_interno, string Cod_barra)
        {
            this.Cod_interno = Cod_interno;
            this.Cod_barra = Cod_barra;
        }
    }
}
