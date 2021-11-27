using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oasis_Reader.Models
{
    /// <summary>
    /// Tabla Tm_usuario
    /// </summary>
    public class Tm_usuario
    {
        [PrimaryKey, MaxLength(12)]
        public string Cod_usuario { get; set; } //char(12)
        [MaxLength(6)]
        public string Cod_msc { get; set; } //char(6)
        public int Tip_usuario23 { get; set; }
        [MaxLength(12)]
        public string Id_usuario { get; set; } //char(12)
        [MaxLength(12)]
        public string Cod_cliente { get; set; } //char(12)
        public int Prioridad { get; set; }
        public int Est_usuario25 { get; set; }
        public DateTime Fec_registro { get; set; }
        public int Esc_trabajo { get; set; }
        [MaxLength(12)]
        public string Emp_inicio { get; set; } //char(12)
        [MaxLength(12)]
        public string Perfil { get; set; } //char(12)
        public Int16 Niv_acc { get; set; }
        public Int16 Niv_ope { get; set; }
        public int Menu_asig { get; set; }
        [MaxLength(12)]
        public string Perfil_pos { get; set; } //char(12)
        public Int16 Niv_autoriza { get; set; }
        public Int16 Niv_informa { get; set; }
        [MaxLength(20)]
        public string Clave_acc { get; set; } //char(20)
        public DateTime Fec_cambio_date { get; set; }
        [MaxLength(10)]
        public string Clave_ant { get; set; } //char(10)
        public int Arranque { get; set; }
        [MaxLength(12)]
        public string Fun_arranque { get; set; } //char(12)
        [MaxLength(4)]
        public int Termin_asign { get; set; } //numeric(4)
        public int Grupo_acc { get; set; }
        public int Sync { get; set; }
        public int Codigo_cargo345 { get; set; }


        public Tm_usuario() { }

        public Tm_usuario(string Cod_usuario, string Cod_msc, int Tip_usuario23, string Id_usuario,
        string Cod_cliente, int Prioridad, int Est_usuario25, DateTime Fec_registro, int Esc_trabajo,
        string Emp_inicio, string Perfil, Int16 Niv_acc, Int16 Niv_ope, int Menu_asig, string Perfil_pos,
        Int16 Niv_autoriza, Int16 Niv_informa, string Clave_acc, DateTime Fec_cambio_date, string Clave_ant,
        int Arranque, string Fun_arranque, int Termin_asign, int Grupo_acc, int Sync, int Codigo_cargo345)
        {

            this.Cod_usuario = Cod_usuario;
            this.Cod_msc = Cod_msc;
            this.Tip_usuario23 = Tip_usuario23;
            this.Id_usuario = Id_usuario;
            this.Cod_cliente = Cod_cliente;
            this.Prioridad = Prioridad;
            this.Est_usuario25 = Est_usuario25;
            this.Fec_registro = Fec_registro;
            this.Esc_trabajo = Esc_trabajo;
            this.Emp_inicio = Emp_inicio;
            this.Perfil = Perfil;
            this.Niv_acc = Niv_acc;
            this.Niv_ope = Niv_ope;
            this.Menu_asig = Menu_asig;
            this.Perfil_pos = Perfil_pos;
            this.Niv_autoriza = Niv_autoriza;
            this.Niv_informa = Niv_informa;
            this.Clave_acc = Clave_acc;
            this.Fec_cambio_date = Fec_cambio_date;
            this.Clave_ant = Clave_ant;
            this.Arranque = Arranque;
            this.Fun_arranque = Fun_arranque;
            this.Termin_asign = Termin_asign;
            this.Grupo_acc = Grupo_acc;
            this.Sync = Sync;
            this.Codigo_cargo345 = Codigo_cargo345;



        }



    }

}
