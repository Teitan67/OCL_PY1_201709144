using PY1_IDE_sql.Clase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PY1_IDE_sql.Analizadores
{
    class Scaneer_201709144
    {
        ArrayList tSsimbolos, tbl_errores;
        public ArrayList tablas;
        public int comentarios;
        public int errores;
        public int tokens;
        public String comentario="";
        public Parser_201709144 sintactico;
        public Scaneer_201709144(String contenido, ArrayList bancaDatos) {
            this.tablas = bancaDatos;
            tSsimbolos = new ArrayList();
            tbl_errores = new ArrayList();
            comentarios = 0;
            errores = 0;
            tokens = 0;
            analisis(contenido);
        }
        private String caracter(int asc) {
            char character = (char)asc;
            return character.ToString();
        }
        private void analisis(String texto) {
            
            int cActual, cSiguiente;
            int estado = 0, ID = -666, fila=1,columna=0;
            String lexemaAux="";
            for (int indice=0;indice<texto.Length;indice++) {
                cActual = texto.ElementAt(indice);
                columna++;
                if (cActual == 10) {
                    fila++;
                    columna = 0;
                }
                if (indice<texto.Length-1) {
                    cSiguiente = texto.ElementAt(indice + 1);
                }
                else
                {
                    cSiguiente = -1;
                }
                if (estado==0) {
                    estado = qEstadoIr(cActual);
                }
               // MessageBox.Show(estado.ToString()+" Caracter:"+cSiguiente.ToString());
                switch (estado)
                {
                    case -1:
                        break;
                    case 1:
                        if (cSiguiente==45) {
                            estado = 8;
                        }
                        else
                        {
                            lexemaAux += caracter(cActual);
                            estado = -666;
                        }
                        break;
                    case 2:
                        if (cSiguiente==42)
                        {
                            estado = 9;
                        }
                        else
                        {
                            lexemaAux += caracter(cActual);
                            estado = -666;
                        }
                        break;
                    case 3:
                        lexemaAux += caracter(cActual);
                        if (cSiguiente==61)
                        {
                            if (cActual == 60)
                            {
                                ID = 16;
                            } else if (cActual == 62) {
                                ID = 15;
                            } else if (cActual==33) {
                                ID = 12;
                            }
                            estado = 11;
                        }
                        else
                        {
                            if (cActual == 60) {
                                ID = 14;
                            } else if (cActual==62) {
                                ID =13;
                            }
                            estado = 0;
                        }
                        break;
                    case 4:
                        if (cActual!=34) {
                            lexemaAux += caracter(cActual);
                        }
                        if (cSiguiente==34) 
                        {
                            estado = 11;
                            ID = 2;
                        }
                        else
                        {
                            estado = 4;
                        }
                        break;
                    case 5:
                        lexemaAux += caracter(cActual);
                        if (cSiguiente>=48&&cSiguiente<=57)
                        {
                            estado = 5;
                        }else if (cSiguiente==46) {
                            estado = 13;
                        }
                        else 
                        {
                            ID = 1;
                            estado = 0;
                        }
                        break;
                    case 6:
                        lexemaAux += caracter(cActual);
                        if (cSiguiente >= 65 && cSiguiente <= 90 || cSiguiente >= 97 && cSiguiente <= 122 || cSiguiente == 95 || cSiguiente == 45)
                        {
                            estado = 6;
                        }
                        else
                        {
                            ID = 5;
                            estado = 0;
                            if (lexemaAux.Equals("Eliminar", StringComparison.OrdinalIgnoreCase))
                            {
                                ID =18;
                            }
                            else if (lexemaAux.Equals("Y", StringComparison.OrdinalIgnoreCase))
                            {
                                ID = 19;
                            }
                            else if (lexemaAux.Equals("O", StringComparison.OrdinalIgnoreCase))
                            {
                                ID = 20;
                            }
                            else if (lexemaAux.Equals("Actualizar", StringComparison.OrdinalIgnoreCase))
                            {
                                ID = 21;
                            }
                            else if (lexemaAux.Equals("Establecer", StringComparison.OrdinalIgnoreCase))
                            {
                                ID = 22;
                            }
                            else if (lexemaAux.Equals("Como", StringComparison.OrdinalIgnoreCase))
                            {
                                ID = 23;
                            }
                            else if (lexemaAux.Equals("De", StringComparison.OrdinalIgnoreCase))
                            {
                                ID = 24;
                            }
                            else if (lexemaAux.Equals("Donde", StringComparison.OrdinalIgnoreCase))
                            {
                                ID = 25;
                            }
                            else if (lexemaAux.Equals("Insertar", StringComparison.OrdinalIgnoreCase))
                            {
                                ID = 26;
                            }
                            else if (lexemaAux.Equals("En", StringComparison.OrdinalIgnoreCase))
                            {
                                ID = 27;
                            }
                            else if (lexemaAux.Equals("Valores", StringComparison.OrdinalIgnoreCase))
                            {
                                ID = 28;
                            }
                            else if (lexemaAux.Equals("Seleccionar", StringComparison.OrdinalIgnoreCase))
                            {
                                ID = 29;
                            }
                            else if (lexemaAux.Equals("Crear", StringComparison.OrdinalIgnoreCase))
                            {
                                ID = 30;
                            }
                            else if (lexemaAux.Equals("Tabla", StringComparison.OrdinalIgnoreCase))
                            {
                                ID = 31;
                            }
                            else if (lexemaAux.Equals("Entero", StringComparison.OrdinalIgnoreCase))
                            {
                                ID = 32;
                            }
                            else if (lexemaAux.Equals("Cadena", StringComparison.OrdinalIgnoreCase))
                            {
                                ID = 33;
                            }
                            else if (lexemaAux.Equals("Flotante", StringComparison.OrdinalIgnoreCase))
                            {
                                ID = 34;
                            }
                            else if (lexemaAux.Equals("Fecha", StringComparison.OrdinalIgnoreCase))
                            {
                                ID = 35;
                            }
                        }
                        break;
                    case 7:
                        if (cSiguiente >= 48 && cSiguiente <= 57)
                        {
                            estado = 12;
                        }
                        else 
                        {
                            estado = -666;
                        }
                        break;
                    case 8:
                        if (cActual != 45)
                        {
                            lexemaAux += caracter(cActual);
                        }
                        if (cSiguiente == 10)
                        {
                            estado = -1;
                        }
                        else
                        {
                            estado = 8;
                        }
                        break;
                    case 9:
                        if (cActual!=42)
                        {
                            lexemaAux += caracter(cActual);
                        }
                        if (cSiguiente==42) 
                        {
                            estado = 10;
                        } 
                        else 
                        {
                            estado = 9;
                        }
                        break;
                    case 10:
                        if (cSiguiente==47)
                        {
                            estado = 30;
                        }
                        else 
                        {
                            estado = -666;
                        }
                        break;
                    case 11:
                        if (ID<0&&cActual==61) {
                            ID = 17;                        
                        }else if (cActual==46) {
                            ID =11;
                        }else if (cActual==42)
                        {
                            ID = 10;
                        }else if (cActual==59){
                            ID = 9;
                        }else if (cActual==44){
                            ID =8;
                        }else if (cActual==40){
                            ID = 6;
                        }else if (cActual == 41){
                            ID = 7;
                        }
                        if (cActual!=34&&cActual!=39)
                        {
                            lexemaAux += caracter(cActual);
                        }
                        estado = 0;
                        break;
                    case 12:
                        lexemaAux += caracter(cActual);
                        if (cSiguiente >= 48 && cSiguiente <= 57)
                        {
                            estado = 15;
                        }
                        else
                        {
                            estado = -666;
                        }
                        break;
                    case 13:
                        lexemaAux += caracter(cActual);
                        if (cSiguiente >= 48 && cSiguiente <= 57)
                        {
                            estado = 14;
                        }
                        else 
                        {
                            estado = -666;
                        }
                        break;
                    case 14:
                        lexemaAux += caracter(cActual);
                        if (cSiguiente >= 48 && cSiguiente <= 57)
                        {
                            estado = 14;
                        }
                        else
                        {
                            ID = 4;
                            estado = 0;
                        }
                        break;
                    case 15:
                        lexemaAux += caracter(cActual);
                        if (cSiguiente==47)
                        {
                            estado = 16;
                        }
                        else
                        {
                            estado = -666;
                        }
                        break;
                    case 16:
                        lexemaAux += caracter(cActual);
                        if (cSiguiente >= 48 && cSiguiente <= 57)
                        {
                            estado = 17;
                        }
                        else
                        {
                            estado = -666;
                        }
                        break;
                    case 17:
                        lexemaAux += caracter(cActual);
                        if (cSiguiente >= 48 && cSiguiente <= 57)
                        {
                            estado = 18;
                        }
                        else
                        {
                            estado = -666;
                        }
                        break;
                    case 18:
                        lexemaAux += caracter(cActual);
                        if (cSiguiente==47)
                        {
                            estado = 19;
                        }
                        else
                        {
                            estado = -666;
                        }
                        break;
                    case 19:
                        lexemaAux += caracter(cActual);
                        if (cSiguiente >= 48 && cSiguiente <= 57)
                        {
                            estado = 20;
                        }
                        else
                        {
                            estado = -666;
                        }
                        break;
                    case 20:
                        lexemaAux += caracter(cActual);
                        if (cSiguiente >= 48 && cSiguiente <= 57)
                        {
                            estado = 21;
                        }
                        else
                        {
                            estado = -666;
                        }
                        break;
                    case 21:
                        lexemaAux += caracter(cActual);
                        if (cSiguiente >= 48 && cSiguiente <= 57)
                        {
                            estado = 22;
                        }
                        else
                        {
                            estado = -666;
                        }
                        break;
                    case 22:
                        lexemaAux += caracter(cActual);
                        if (cSiguiente >= 48 && cSiguiente <= 57)
                        {
                            estado = 23;
                        }
                        else
                        {
                            estado = -666;
                        }
                        break;
                    case 23:
                        lexemaAux += caracter(cActual);
                        if (cSiguiente==39)
                        {
                            ID = 3;
                            estado = 11;
                        }
                        else
                        {
                            estado = -666;
                        }
                        break;
                    case 30:
                        estado = -1;
                        break;
                    case -666:
                        lexemaAux += caracter(cActual);
                        break;
                }
                
                if (estado == 0)
                {
                    tSsimbolos.Add(new Token(ID,qTokenEs(ID),lexemaAux,fila,columna));
                    lexemaAux = "";
                    this.tokens++;
                    ID = -666;
                    estado = 0;
                }
                else if (estado == -1)
                {
                    this.comentario += "Comentario:"+lexemaAux+"\n<br>";
                    this.comentarios++;
                    estado = 0;
                    lexemaAux = "";
                    ID = -666;
                } else if (estado==-2) {
                    estado = 0;
                    lexemaAux = "";
                } else if (estado == -666) {
                    estado = 0;
                    tbl_errores.Add(new TokenErrores(lexemaAux,fila,columna)) ;
                    this.errores++;
                    lexemaAux = "";
                }
            }
            sintactico = new Parser_201709144(this.tSsimbolos, this.tablas);
            
        }
        public String qTokenEs(int id) {
            String aux = "";
            switch (id) {
                case 1:
                    aux ="Entero";
                    break;
                case 2:
                    aux = "Cadena";
                    break;
                case 3:
                    aux = "Fecha";
                    break;
                case 4:
                    aux = "Flotante";
                    break;
                case 5:
                    aux = "Identificador";
                    break;
                case 6:
                    aux = "Parentesis abierto";
                    break;
                case 7:
                    aux = "Parentesis cerrado";
                    break;
                case 8:
                    aux = "Coma";
                    break;
                case 9:
                    aux = "Punto y coma";
                    break;
                case 10:
                    aux = "Asterisco";
                    break;
                case 11:
                    aux = "Punto";
                    break;
                case 12:
                    aux = "Diferente de";
                    break;
                case 13:
                    aux = "Mayor que";
                    break;
                case 14:
                    aux = "Menor que";
                    break;
                case 15:
                    aux = "Mayor o igual";
                    break;
                case 16:
                    aux = "Menor o igual";
                    break;
                case 17:
                    aux = "Igual";
                    break;
                case 18:
                    aux = "PR Eliminar";
                    break;
                case 19:
                    aux = "PR Y";
                    break;
                case 20:
                    aux = "PR O";
                    break;
                case 21:
                    aux = "PR Actualizar";
                    break;
                case 22:
                    aux = "PR Establecer";
                    break;
                case 23:
                    aux = "PR As";
                    break;
                case 24:
                    aux = "PR De";
                    break;
                case 25:
                    aux = "PR Donde";
                    break;
                case 26:
                    aux = "PR Insertar";
                    break;
                case 27:
                    aux = "PR En";
                    break;
                case 28:
                    aux = "PR Valores";
                    break;
                case 29:
                    aux = "PR Seleccionar";
                    break;
                case 30:
                    aux = "PR Crear";
                    break;
                case 31:
                    aux = "PR Table";
                    break;
                case 32:
                    aux = "PR Entero";
                    break;
                case 33:
                    aux = "PR Cadena";
                    break;
                case 34:
                    aux = "PR Flotante";
                    break;
                case 35:
                    aux = "PR Flecha";
                    break;
                default:
                    aux = "No definido "+id.ToString();
                    break;
            }
                return aux;
        }
        private int qEstadoIr(int caracter) {
            int estado = -666;
            if (caracter >= 65 && caracter <= 90 || caracter >= 97 && caracter <= 122||caracter==95)
            {
                estado = 6;
            }
            else if (caracter >= 48 && caracter <= 57)
            {
                estado = 5;
            }
            else if (caracter == 45)
            {
                estado = 1;
            }
            else if (caracter == 60)
            {
                estado = 3;
            }
            else if (caracter == 62)
            {
                estado = 3;
            }
            else if (caracter == 61)
            {
                estado = 11;
            }
            else if (caracter == 47)
            {
                estado = 2;
            }
            else if (caracter == 33)
            {
                estado = 3;
            }
            else if (caracter == 44)
            {
                estado = 11;
            }
            else if (caracter == 59)
            {
                estado = 11;
            }
            else if (caracter == 41)
            {
                estado = 11;
            }
            else if (caracter == 40)
            {
                estado = 11;
            }
            else if (caracter==46) 
            {
                estado = 11;
            }
            else if (caracter == 34)
            {
                estado = 4;
            }
            else if (caracter == 39)
            {
                estado = 7;
            }
            else if(caracter==42)
            {
                estado = 11;
            }
            else if (caracter==32||caracter==10) 
            {
                estado = -2;
            }
            else
            {
                estado = -666;
            }
            return estado;
        }

        private void generarHmtl(String contenido)
        {
            Archivo archivo = new Archivo();
            archivo.ruta="C:\\PY1\\ReporteTokens.html";
            String cabeza = archivo.leerArchivo("C:\\PY1\\reportes_Cabeza.txt");
            cabeza += contenido + archivo.leerArchivo("C:\\PY1\\reportes_Fin.txt");
            archivo.guardar(cabeza);
            Process.Start(archivo.ruta);
        }
        public void generarReporte(String html)
        {

            
            if (tSsimbolos.Count <= 0)
            {
                MessageBox.Show("No hay tokens registrados"); 
            }
            else
            {

                html += "<table>\n" +
                           "\t<tr id =\"Cabeza\">\n" +
                           "\t<td> No.</td>\n" +
                           "\t<td> ID </td>\n" +
                           "\t<td> Token</td>\n" +
                           "\t<td> Lexema</td>\n" +
                           "\t<td> Columna</td>\n" +
                           "\t<td> Fila </td>\n" +
                           "\t</tr>\n";
                for (int i = 0; i < tSsimbolos.Count; i++)
                {
                    Token aux = (Token)tSsimbolos[i];

                    //html
                    html += "\t<tr>\n";
                    html += "\t\t<td>" + i + "</td>\n";
                    html += "\t\t<td>" + aux.id + "</td>\n";
                    html += "\t\t<td>" + aux.nombreToken + "</td>\n";
                    html += "\t\t<td>" + aux.lexema + "</td>\n";
                    html += "\t\t<td>" + aux.columna + "</td>\n";
                    html += "\t\t<td>" + aux.fila + "</td>\n";
                    html += "\t</tr>\n";

                }
                //consola.consola(html);
                generarHmtl(html);
            }
            
        }
        public void reporteErrores(String html) {
            
            if (errores > 0||sintactico.errores>0)
            {
                html+= "<table>\n"+
                            "\t<tr id =\"Cabeza\">\n"+
                            "\t<td> No.</td>\n"+
                            "\t<td> ID </td>\n"+
                            "\t<td> Token</td>\n"+
                            "\t<td> Lexema</td>\n"+
                            "\t<td> Columna</td>\n"+
                            "\t<td> Fila </td>\n"+
                            "\t</tr>\n";
                TokenErrores aux;
                for (int i = 0; i < tbl_errores.Count; i++)
                {
                    aux = (TokenErrores)tbl_errores[i];
                    html += "\t<tr>\n";
                    html += "\t\t<td>" + i + "</td>\n";
                    html += "\t\t<td>" + (int)aux.lexema.ElementAt(0) + "</td>\n";
                    html += "\t\t<td>" + "ERROR LEXICO: patron no reconocido" + "</td>\n";
                    html += "\t\t<td>" + aux.lexema + "</td>\n";
                    html += "\t\t<td>" + aux.columna + "</td>\n";
                    html += "\t\t<td>" + aux.fila + "</td>\n";
                    html += "\t</tr>\n";
                }
                generarHmtl(html);
            }
            else
            {
                MessageBox.Show("No hay errores lexicos ni sintacticos registrados");
            }
        }
    }
}
