using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PY1_IDE_sql.Clase
{
    class Condicion
    {
        public String tabla1="";
        public String columna1 ="";
        public String dato1="";
        public String tabla2 ="";
        public String columna2 ="";
        public String dato2="";
        public String condicion="";
        public Condicion(String columna,String comparador,String dato2)
        {
            this.columna1 = columna;
            this.condicion = comparador;
            this.dato2 = dato2;
        }
    }
}
