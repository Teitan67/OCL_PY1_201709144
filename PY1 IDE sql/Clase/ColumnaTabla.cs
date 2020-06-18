using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PY1_IDE_sql.Clase
{
    class ColumnaTabla
    {
        public String titulo;
        public String tipoDato;
        public ArrayList datos;
        public ColumnaTabla(String nombre, String tipoDato) 
        {
            this.titulo = nombre;
            this.tipoDato = tipoDato;
            this.datos = new ArrayList();
        }
    }
}
