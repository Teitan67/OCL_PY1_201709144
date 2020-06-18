using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PY1_IDE_sql.Clase
{
    class Tabla
    {
        public String nombreTabla;
        public ArrayList columnas;
        public Tabla(String nombre)
        {
            this.nombreTabla = nombre;
            columnas = new ArrayList();
        }
    }
}
