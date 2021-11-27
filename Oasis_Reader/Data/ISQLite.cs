using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oasis_Reader.Data
{

    /// <summary>
    /// Interfaz para base de datos de SQLite en Android y UWP
    /// </summary>

    public interface ISQLite
    {
        SQLiteConnection GetConnection();

        void SetDB();
    }
}
