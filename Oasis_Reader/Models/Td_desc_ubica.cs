using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Oasis_Reader.Models
{
    /// <summary>
    /// Tabla Td_desc_ubica
    /// </summary>
    public class Td_desc_ubica
    { 
        
        [PrimaryKey, MaxLength(10)]
        public string Cod_ubicacion { get; set; }
        [MaxLength(120)]
        public string Descripcion  { get; set; }
        public int Id { get; set; }

        
        public Td_desc_ubica() { }

        public Td_desc_ubica(string Cod_ubicacion, string Descripcion, int Id)
        {
             this.Cod_ubicacion = Cod_ubicacion;
             this.Descripcion = Descripcion;
             this.Id = Id;
        }
    }
}
