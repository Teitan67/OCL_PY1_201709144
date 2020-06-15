using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PY1_IDE_sql.Clase
{
    class Token
    {
        public Token(int id, String nombreToken, String lexema,int fila,int columna) {
            this.id = id;
            this.nombreToken = nombreToken;
            this.lexema = lexema;
            this.fila = fila;
            this.columna = columna;
        }
        public int id;
        public String nombreToken;
        public String lexema;
        public int fila;
        public int columna;
    }
}
