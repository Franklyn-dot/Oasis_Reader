using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Oasis_Reader.Models
{
    class tc_codmsc
        //modelo de sucursales
        //agregada por Ing. Franklyn Tinoco
    {
        [PrimaryKey]
        public string cod_msc { get; set; }
        public string codempresa { get; set; }
        public int num_sucursal { get; set; }

        public string nom_sucursal { get; set; }

        public int tipo_suc4 { get; set; }

        public int activo { get; set; }

        public string cod_alt_msc { get; set; }

        public string cod_sec_arc { get; set; }

        public string fec_creacion { get; set; }

        public int sync { get; set; }

        public int pais154 { get; set; }

        public int grupo_sucursal180 { get; set; }

        public string region { get; set; }

    }
}
