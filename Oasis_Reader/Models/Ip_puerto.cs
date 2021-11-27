using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oasis_Reader.Models
{
    public class Ip_puerto
    {
        [PrimaryKey, MaxLength(16)]
        public string Ip { get; set; } //char(16)
        [MaxLength(5)]
        public string Puerto { get; set; }  //char(5)

        public Ip_puerto() { }

        public Ip_puerto(string Ip, string Puerto)
        {
            this.Ip = Ip;
            this.Puerto = Puerto;
        }
    }
}
