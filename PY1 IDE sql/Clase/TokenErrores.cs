using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PY1_IDE_sql.Clase
{
    class TokenErrores
    {
        public TokenErrores(String lexema,int fila, int columna) 
        {
            this.columna = columna;
            this.fila = fila;
            this.lexema = lexema;
        }
        public int fila;
        public int columna;
        public String lexema;
    }
}
