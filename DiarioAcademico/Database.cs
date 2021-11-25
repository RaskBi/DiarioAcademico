using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace DiarioAcademico
{
    public interface Database
    {
        SQLiteAsyncConnection GetConnection();
    }
}
