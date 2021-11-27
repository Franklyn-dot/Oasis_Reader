using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oasis_Reader.Models
{
    /// <summary>
    /// Tabla Tm_conteo
    /// </summary>
    public class Tm_conteo
    {

        [PrimaryKey]
        public int Conteo { get; set; }
        [PrimaryKey, AutoIncrement]
        public int Parte { get; set; }
        [PrimaryKey, MaxLength(40)]
        public string Id_dispositivo { get; set; } //char(40)
        public string Pide_cantidad { get; set; } //varchar
        [MaxLength(4)]
        public string Cod_departamento { get; set; } //char(4)


        public Tm_conteo() { }

        public Tm_conteo(int Conteo, string Id_dispositivo, string Pide_cantidad, string Cod_departamento)
        {
            this.Conteo = Conteo;
            
            this.Id_dispositivo = Id_dispositivo;
            this.Pide_cantidad = Pide_cantidad;
            this.Cod_departamento = Cod_departamento;

        }
    }
}
