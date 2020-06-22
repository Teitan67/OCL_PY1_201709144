using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PY1_IDE_sql.Clase
{
   
    class TablaColumna
    {
        public String tabla, columna,alias="";
        public TablaColumna(String tabla, String columna,String alias)
        {
            this.tabla = tabla;
            this.columna = columna;
            this.alias = alias;
        }
    }
}
