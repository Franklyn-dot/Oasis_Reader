using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Oasis_Reader.Models
{
    /// <summary>
    /// Tabla Td_departamento
    /// </summary>
    public class Td_departamento
    {
        [PrimaryKey, MaxLength(4)]
        public string Cod_departamento { get; set; } //char(4)
        [MaxLength(40)]
        public string Txt_descrip_dep { get; set; }  //varchar(40)

        public Td_departamento() { }

        public Td_departamento(string Cod_departamento, string Txt_descrip_dep)
        {
            this.Cod_departamento = Cod_departamento;
            this.Txt_descrip_dep = Txt_descrip_dep;
        }


    }
}
