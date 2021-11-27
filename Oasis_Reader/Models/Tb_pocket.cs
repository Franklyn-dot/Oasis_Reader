using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oasis_Reader.Models
{    
    /// <summary>
    /// Tabla Tb_pocket
    /// </summary>
    public class Tb_pocket
    {
        [PrimaryKey, MaxLength(100)]
        public string Id { get; set; } //varchar(100)
        [MaxLength(100)]
        public string Password { get; set; } //varchar(100)
        [MaxLength(12)]
        public string Id_dispositivo { get; set; }  //char(12)

        public Tb_pocket() { }

        public Tb_pocket(string Id, string Password, string Id_dispositivo)
        {
            this.Id = Id;
            this.Password = Password;
            this.Id_dispositivo = Id_dispositivo;
        }


    }
}
