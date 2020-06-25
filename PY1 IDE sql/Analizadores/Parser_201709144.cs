using PY1_IDE_sql.Clase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PY1_IDE_sql.Analizadores
{
    class Parser_201709144
    {
        public String ErrorSintactico = "";
        public int errores = 0;
        ArrayList Tokens;
        public ArrayList tablas;
        ArrayList datosAux;
        ArrayList auxColumnas = new ArrayList();
        ArrayList alias = new ArrayList();
        Tabla tabAux;
        Boolean asterisco = false;
        int indexTabla = -1;
        String impresionColumna = "digraph {tbl [shape=plaintext label=< <table color='white' ><tr>";
        public Parser_201709144(ArrayList Tokens, ArrayList tablas) {

            this.Tokens = Tokens;
            this.tablas = tablas;
            analizador(this.Tokens);

        }
        private String qProduccionEs(int token)
        {
            String produccion = "";
            switch (token)
            {
                case 30:
                    produccion = "S0";
                    break;
                case 26:
                    produccion = "A0";
                    break;
                case 29:
                    produccion = "B0";
                    break;
                case 18:
                    produccion = "C0";
                    break;
                case 21:
                    produccion = "D0";
                    break;
                default:
                    produccion = "Error";
                    break;
            }
            return produccion;
        }
        private int modoPanico(int i)
        {
            Token temporal2;
            int cuenta = 0;
            errores++;
            int regreso = this.Tokens.Count;
            for (int j = i; j < this.Tokens.Count; j++)
            {
                temporal2 = (Token)Tokens[j];
                if (temporal2.id == 9) {
                    regreso = j + 1;
                    break;
                }
                cuenta++;
            }
            ErrorSintactico += "Modo Panico activado!!!\n" +
                "\n" + cuenta + " tokens ignorados<br><br>";
            //MessageBox.Show(ErrorSintactico);
            return regreso;
        }
        private void agregarError(Token aux, int id) {
            ErrorSintactico += "Syntax ERROR: Se esperaba " + qTokenEs(id) + " en vez de " + aux.lexema +
                "<br> En Fila: " + aux.fila + " Columnas: " + aux.columna + "<br>";
        }
        public String qTokenEs(int id)
        {
            String aux = "";
            switch (id)
            {
                case 1:
                    aux = "Entero";
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
                case -2:
                    aux = "Tipo de dato";
                    break;
                case -3:
                    aux = ", o (";
                    break;
                case -4:
                    aux = "Comparador";
                    break;
                case -5:
                    aux = "Y o O";
                    break;
                default:
                    aux = "No definido " + id.ToString();
                    break;
            }
            return aux;
        }
        private int buscarTabla(String tabla)
        {
            int aux = -1;
            Tabla auxTabla;
            for (int i = 0; i < tablas.Count; i++) {
                auxTabla = (Tabla)tablas[i];
                if (auxTabla.nombreTabla.Equals(tabla, StringComparison.OrdinalIgnoreCase))
                {
                    aux = i;
                    break;
                }
            }
            return aux;
        }
        public void imprimirTabla(int i)
        {
            String impresion = "digraph {tbl [ shape=plaintext label=< <table border='0' cellborder='0'  cellspacing='0'>";
            Tabla aux = (Tabla)tablas[i];
            impresion += " \n<tr><td>" + aux.nombreTabla + "</td></tr>\n";
            impresion += "<tr><td cellpadding='1'><table  cellspacing='0'><tr>";
            for (int indice = 0; indice < aux.columnas.Count; indice++)
            {
                ColumnaTabla aux2 = (ColumnaTabla)aux.columnas[indice];
                impresion += "<td>" + aux2.titulo + "</td>";
            }
            impresion += "</tr>";
            ColumnaTabla auxa2 = (ColumnaTabla)aux.columnas[0];
            for (int a = 0; auxa2.datos.Count > a; a++)
            {
                impresion += "<tr>";
                for (int indice = 0; indice < aux.columnas.Count; indice++)
                {
                    ColumnaTabla auxa2s = (ColumnaTabla)aux.columnas[indice];
                    impresion += "<td>" + auxa2s.datos[a] + "</td>";
                }
                impresion += "</tr>";
            }
            impresion += " </table> </td></tr></table>>];}";
        }
        private Tabla obtenerTabla(String nombre)
        {
            Tabla auxiliar = null;
            for (int i = 0; i < tablas.Count; i++)
            {
                auxiliar = (Tabla)tablas[i];
                if (auxiliar.nombreTabla.Equals(nombre, StringComparison.OrdinalIgnoreCase))
                {
                    i = tablas.Count;
                    break;
                }
                else
                {
                    auxiliar = null;
                }
            }
            return auxiliar;
        }
        private ColumnaTabla obtenerColumna(Tabla tabla, String nombre) {
            ColumnaTabla aux = null;
            if (tabla.columnas!=null) {
                for (int i = 0; i < tabla.columnas.Count; i++)
                {
                    aux = (ColumnaTabla)tabla.columnas[i];
                    if (aux.titulo.Equals(nombre, StringComparison.OrdinalIgnoreCase))
                    {
                        i = tabla.columnas.Count;
                        break;
                    }
                    else
                    {
                        aux = null;
                    }
                }
            }
            else
            {
                MessageBox.Show("No se econtro columnas en tabla "+tabla);
            }
           
            return aux;
        }
        public String imprimir()
        {
            this.impresionColumna += "</tr></table> >]; }";
            return this.impresionColumna;
        }
        public void generarColumna(String tabla, String columna, String alias)
        {
            String texto = " <td><table cellspacing='0' >";
            Tabla auxTabla = obtenerTabla(tabla);
            if (auxTabla != null)
            {
                ColumnaTabla auxColumna = obtenerColumna(auxTabla, columna);
                if (auxColumna != null)
                {
                    if (alias.Equals("")) {
                        texto += "<tr><td>" + columna + "</td></tr>";
                    }
                    else
                    {
                        texto += "<tr><td>" + alias + "</td></tr>";
                    }
                    for (int i = 0; i < auxColumna.datos.Count; i++)
                    {
                        if(!auxColumna.datos[i].Equals(" "))
                        {
                            texto += "<tr><td>" + auxColumna.datos[i] + "</td></tr>";
                        }  
                    }
                    texto += "</table> </td>";
                    this.impresionColumna += texto;
                }
                else
                {
                    MessageBox.Show("No existe la columna " + columna + " en la tabla " + tabla);
                }
            }
            else
            {
                MessageBox.Show("La tabla " + tabla + " no existe");
            }

        }
        private void imprimirTodo(ArrayList tablas)
        {
            Tabla auxiliar = null;
            ColumnaTabla auxColumna = null;
            for (int i = 0; i < tablas.Count; i++)
            {
                auxiliar = obtenerTabla(tablas[i].ToString());

                for (int j = 0; j < auxiliar.columnas.Count; j++)
                {
                    auxColumna = (ColumnaTabla)auxiliar.columnas[j];
                    generarColumna(auxiliar.nombreTabla, auxColumna.titulo, "");
                }
            }
        }
        private void eliminarTodo(String nombre)
        {
            Tabla aux = obtenerTabla(nombre);
            for (int i = 0; i < aux.columnas.Count; i++) {
                ColumnaTabla aux2 = (ColumnaTabla)aux.columnas[i];
                aux2.datos = new ArrayList();
            }
        }
        private void imprimirColumnas(ArrayList columnas, ArrayList alias)
        {
            TablaColumna aux = null;
            for (int i = 0; i < columnas.Count; i++) {
                aux = (TablaColumna)columnas[i];
                generarColumna(aux.tabla, aux.columna, alias[i].ToString());
            }
        }
        private void analizador(ArrayList token)
        {
            ArrayList auxColumnas = new ArrayList();
            ArrayList auxTablas = new ArrayList();
            ArrayList columnaDato = new ArrayList();
            ArrayList condiciones = new ArrayList();
            ArrayList YOs = new ArrayList();
            //cabeza
            String produccion = "Y0";
            String nomTabla = "", nomColumna="", nuevoDato="",condicion="",dato;
            Token temporal2;
            Token temporal;
            int tActual;
            impresionColumna = "digraph {tbl [shape=plaintext label=< <table margin='0' color='white' ><tr>";
            for (int i = 0; i < token.Count;)
            {
                temporal2 = (Token)token[i];
                tActual = temporal2.id;
                if (produccion.Equals("Y0"))
                {
                    produccion = qProduccionEs(tActual);
                }
                switch (produccion)
                {
                    case "S0":
                        if (tActual == 30)
                        {
                            produccion = "S1";
                            i++;

                        }
                        else
                        {
                            agregarError(temporal2, 30);
                            i = modoPanico(i);
                            produccion = "Y0";

                        }
                        break;
                    case "S1":
                        if (tActual == 31)
                        {
                            tabAux = new Tabla();
                            produccion = "S2";
                            i++;
                        }
                        else
                        {
                            agregarError(temporal2, 31);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "S2":
                        if (tActual == 5)
                        {
                            tabAux.nombreTabla = temporal2.lexema;
                            produccion = "S3";
                            i++;
                        }
                        else
                        {
                            agregarError(temporal2, 5);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "S3":
                        if (tActual == 6)
                        {
                            produccion = "S4";
                            i++;
                        }
                        else
                        {
                            agregarError(temporal2, 6);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "S4":
                        if (tActual == 5)
                        {

                            produccion = "S5";
                            i++;
                        }
                        else
                        {
                            agregarError(temporal2, 5);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "S5":
                        if (tActual == 32 || tActual == 33 || tActual == 34 || tActual == 35)
                        {
                            temporal = (Token)token[i - 1];
                            tabAux.columnas.Add(new ColumnaTabla(temporal.lexema, temporal2.lexema));
                            //MessageBox.Show("Columnas:"+tabAux.columnas.Count);
                            produccion = "S6";
                            i++;
                        }
                        else
                        {
                            agregarError(temporal2, -2);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "S6":
                        if (tActual == 8)
                        {
                            produccion = "S4";
                            i++;
                        }
                        else if (tActual == 7)
                        {
                            produccion = "S7";
                            i++;
                        }
                        else
                        {
                            agregarError(temporal2, -3);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "S7":
                        if (tActual == 9)
                        {
                            produccion = "Y0";
                            this.tablas.Add(tabAux);

                            // MessageBox.Show("Tabla creada: "+tablas.Count);
                            i++;
                        }
                        else
                        {
                            agregarError(temporal2, 9);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "A0":
                        if (tActual == 26)
                        {
                            produccion = "A1";
                            datosAux = new ArrayList();
                            i++;
                        }
                        else
                        {
                            agregarError(temporal2, 26);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "A1":
                        if (tActual == 27)
                        {
                            produccion = "A2";
                            i++;
                        }
                        else
                        {
                            agregarError(temporal2, 27);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "A2":
                        if (tActual == 5)
                        {
                            produccion = "A3";
                            indexTabla = buscarTabla(temporal2.lexema);
                            // MessageBox.Show("Tabla encontrada:"+indexTabla);
                            i++;
                        }
                        else
                        {
                            agregarError(temporal2, 5);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "A3":
                        if (tActual == 28)
                        {
                            produccion = "A4";
                            i++;
                        }
                        else
                        {
                            agregarError(temporal2, 28);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "A4":
                        if (tActual == 6)
                        {
                            produccion = "A5";
                            i++;
                        }
                        else
                        {
                            agregarError(temporal2, 6);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "A5":
                        if (tActual == 1 || tActual == 2 || tActual == 3 || tActual == 4)
                        {
                            datosAux.Add(temporal2.lexema);
                            produccion = "A6";
                            i++;
                        }
                        else
                        {
                            agregarError(temporal2, -2);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "A6":
                        if (tActual == 8)
                        {
                            produccion = "A5";
                            i++;
                        }
                        else if (tActual == 7)
                        {
                            produccion = "A7";
                            i++;
                        }
                        else
                        {
                            agregarError(temporal2, -3);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "A7":
                        if (tActual == 9)
                        {
                            //MessageBox.Show("Indice" + indexTabla);
                            if (indexTabla >= 0)
                            {
                                Tabla temp = (Tabla)tablas[indexTabla];
                                // MessageBox.Show("Datos auxiliares" + datosAux.Count);
                                // MessageBox.Show("Datos reales" + temp.columnas.Count);
                                for (int s = 0; s < temp.columnas.Count; s++)
                                {
                                    ColumnaTabla columna = (ColumnaTabla)temp.columnas[s];
                                    columna.datos.Add(datosAux[s]);
                                    temp.columnas[s] = columna;
                                }
                                tablas[indexTabla] = temp;
                                ColumnaTabla columssnas = (ColumnaTabla)temp.columnas[0];
                                //MessageBox.Show("No de columnas" + temp.columnas.Count);
                                //MessageBox.Show("No de datos" + columssnas.datos.Count);
                                // MessageBox.Show("Insertar aceptado");
                            }
                            else
                            {
                                MessageBox.Show("No existe la tabla para insertar " + indexTabla);
                            }
                            produccion = "Y0";
                            i++;
                            // MessageBox.Show("Insertar aceptado"); 
                        }
                        else
                        {
                            agregarError(temporal2, 9);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "C0":
                        if (tActual == 18)
                        {
                            produccion = "C1";
                            i++;
                        }
                        else
                        {
                            agregarError(temporal2, 18);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "C1":
                        if (tActual == 24)
                        {
                            produccion = "C2";
                            i++;
                        }
                        else
                        {
                            agregarError(temporal2, 24);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "C2":
                        if (tActual == 5)
                        {
                            produccion = "C3";
                            nomTabla = temporal2.lexema;
                            i++;
                        }
                        else
                        {
                            agregarError(temporal2, 5);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "C3":
                        if (tActual == 25)
                        {
                            produccion = "C4";
                            i++;
                        } else if (tActual == 9)
                        {
                            produccion = "Y0";
                            i++;
                            eliminarTodo(nomTabla);
                            //MessageBox.Show("Eliminar correcto");
                        }
                        else
                        {
                            agregarError(temporal2, 9);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "C4":
                        if (tActual == 1 || tActual == 2 || tActual == 3 || tActual == 4 || tActual == 5)
                        {
                            nomColumna = temporal2.lexema;
                            produccion = "C5";
                            i++;
                        }
                        else
                        {
                            agregarError(temporal2, -2);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "C5":
                        if (tActual == 17 || tActual == 13 || tActual == 14 || tActual == 15 || tActual == 16 || tActual == 12)
                        {
                            condicion = temporal2.lexema;
                            produccion = "C6";
                            i++;
                        }
                        else
                        {
                            agregarError(temporal2, -4);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "C6":
                        if (tActual == 1 || tActual == 2 || tActual == 3 || tActual == 4 || tActual == 5)
                        {
                            dato = temporal2.lexema;
                            condiciones.Add(new Condicion(nomColumna,condicion,dato));
                            produccion = "C7";
                            i++;
                        }
                        else
                        {
                            agregarError(temporal2, -2);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "C7":
                        if (tActual == 19 || tActual == 20)
                        {
                            YOs.Add(temporal2.lexema);
                            produccion = "C4";
                            i++;
                        }
                        else if (tActual == 9)
                        {
                            // condicion(String tabla,String columna,String comparador,String dato)
                            //eliminar(String tabla, ArrayList condicionales, ArrayList Y_O)
                            eliminar(nomTabla,condiciones,YOs);
                            produccion = "Y0";
                            i++;
                            // MessageBox.Show("Eliminar correcto");
                        }
                        else
                        {
                            agregarError(temporal2, -5);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "D0":
                        if (tActual == 21)
                        {
                            produccion = "D1";
                            i++;
                        }
                        else
                        {
                            agregarError(temporal2, 21);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "D1":
                        if (tActual == 5)
                        {
                            nomTabla =temporal2.lexema;
                            produccion = "D2";
                            i++;
                        }
                        else
                        {
                            agregarError(temporal2, 5);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "D2":
                        if (tActual == 22)
                        {
                            produccion = "D3";
                            i++;
                        }
                        else
                        {
                            agregarError(temporal2, 22);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "D3":
                        if (tActual == 6)
                        {
                            produccion = "D4";
                            i++;
                        }
                        else
                        {
                            agregarError(temporal2, 6);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "D4":
                        if (tActual == 5)
                        {
                            nomColumna = temporal2.lexema;
                            produccion = "D5";
                            i++;
                        }
                        else
                        {
                            agregarError(temporal2, 5);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "D5":
                        if (tActual == 17)
                        {
                            produccion = "D6";
                            i++;
                        }
                        else
                        {
                            agregarError(temporal2, 17);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "D6":
                        if (tActual == 1 || tActual == 2 || tActual == 3 || tActual == 4)
                        {
                            columnaDato.Add(new ColumnaDato(nomColumna,temporal2.lexema));
                            produccion = "D7";
                            i++;
                        }
                        else
                        {
                            agregarError(temporal2, -2);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "D7":
                        if (tActual == 8)
                        {
                            produccion = "D4";
                            i++;
                        }
                        else if (tActual == 7)
                        {
                            produccion = "D8";
                            i++;
                        }
                        else
                        {
                            agregarError(temporal2, -3);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "D8":
                        if (tActual == 25)
                        {
                            
                            produccion = "D9";
                            i++;
                        }
                        else
                        {
                            agregarError(temporal2, 25);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "D9":
                        if (tActual == 1 || tActual == 2 || tActual == 3 || tActual == 4 || tActual == 5)
                        {
                            nomColumna = temporal2.lexema;
                            produccion = "D11";
                            i++;
                        }
                        else
                        {
                            agregarError(temporal2, -2);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "D11":
                        if (tActual == 17 || tActual == 13 || tActual == 14 || tActual == 15 || tActual == 16 || tActual == 12)
                        {
                            condicion = temporal2.lexema;
                            produccion = "D12";
                            i++;
                        }
                        else
                        {
                            agregarError(temporal2, -4);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "D12":
                        if (tActual == 1 || tActual == 2 || tActual == 3 || tActual == 4 || tActual == 5)
                        {
                            dato = temporal2.lexema;
                            condiciones.Add(new Condicion(nomColumna,condicion,dato));


                            produccion = "D13";
                            i++;
                        }
                        else
                        {
                            agregarError(temporal2, -2);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "D13":
                        if (tActual == 19 || tActual == 20)
                        {
                            YOs.Add(temporal2.lexema);
                            produccion = "D9";
                            i++;
                        }
                        else if (tActual == 9)
                        {
                            produccion = "Y0";
                            i++;
                            actualizar(nomTabla,columnaDato,condiciones,YOs);
                            // MessageBox.Show("Actualizar correcto");
                        }
                        else
                        {
                            agregarError(temporal2, -5);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    //Rubi
                    case "B0":
                        if (tActual == 29)
                        {
                            produccion = "B1";
                            i++;
                        }
                        else
                        {
                            agregarError(temporal2, 29);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "B1":
                        if (tActual == 10)
                        {
                            asterisco = true;
                            produccion = "B6";
                            i++;
                        }
                        else if (tActual == 5)
                        {
                            produccion = "B2";
                        }
                        else
                        {
                            agregarError(temporal2, 5);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    //rubi
                    case "B2":
                        if (tActual == 5)
                        {
                            produccion = "B2.1";
                            i++;
                            nomTabla = temporal2.lexema;
                        }
                        else
                        {
                            agregarError(temporal2, 5);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "B2.1":
                        if (tActual == 11)
                        {
                            produccion = "B2.2";
                            i++;
                        }
                        else
                        {
                            agregarError(temporal2, 11);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "B2.2":
                        if (tActual == 5)
                        {
                            nomColumna = temporal2.lexema;
                            auxColumnas.Add(new TablaColumna(nomTabla, nomColumna, ""));
                            produccion = "B3";
                            i++;
                        }
                        else
                        {
                            agregarError(temporal2, 5);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "B3":
                        if (tActual == 24)
                        {
                            alias.Add("");
                            produccion = "B6";
                        }
                        else if (tActual == 8)
                        {
                            alias.Add("");
                            produccion = "B5";
                        }
                        else if (tActual == 23) {
                            produccion = "B4";
                        }
                        else
                        {
                            agregarError(temporal2, 24);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "B4":
                        if (tActual == 23)
                        {
                            produccion = "B4.1";
                            i++;
                        }
                        else
                        {
                            agregarError(temporal2, 23);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "B4.1":
                        if (tActual == 5)
                        {
                            alias.Add(temporal2.lexema);
                            produccion = "B5";
                            i++;
                        }
                        else
                        {
                            agregarError(temporal2, 5);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "B5":
                        if (tActual == 8)
                        {
                            produccion = "B2";
                            i++;
                        }
                        else if (tActual == 24)
                        {
                            produccion = "B6";
                        }
                        else
                        {
                            agregarError(temporal2, 24);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "B6":
                        if (tActual == 24)
                        {

                            produccion = "B6.1";
                            i++;
                        }
                        else
                        {
                            agregarError(temporal2, 24);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "B6.1":
                        if (tActual == 5)
                        {

                            auxTablas.Add(temporal2.lexema);
                            produccion = "B7";
                            i++;
                        }
                        else
                        {
                            agregarError(temporal2, 5);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "B7":
                        if (tActual == 9)
                        {
                            if (asterisco)
                            {
                                imprimirTodo(auxTablas);
                                asterisco = false;
                            }
                            else
                            {
                                imprimirColumnas(auxColumnas, alias);
                            }
                            generarImagen();
                            auxColumnas = new ArrayList();
                            alias = new ArrayList();
                            produccion = "Y0";
                            i++;
                            // MessageBox.Show("Seleccionar aceptado");
                        }
                        else if (tActual == 25)
                        {
                            produccion = "B8";
                            i++;
                        }
                        else if (tActual == 8)
                        {
                            i++;
                            produccion = "B6.1";
                        }
                        else
                        {
                            agregarError(temporal2, 9);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "B8":
                        if (tActual == 1 || tActual == 2 || tActual == 3 || tActual == 4)
                        {
                            produccion = "B9";
                            i++;
                        }
                        else if (tActual == 5)
                        {
                            produccion = "B8.1";
                            i++;
                        }
                        else
                        {
                            agregarError(temporal2, -2);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "B8.1":
                        if (tActual == 11)
                        {
                            produccion = "B8.2";
                            i++;
                        }
                        else
                        {
                            agregarError(temporal2, 11);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "B8.2":
                        if (tActual == 5)
                        {
                            produccion = "B9";
                            i++;
                        }
                        else
                        {
                            agregarError(temporal2, 5);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    //
                    case "B9":
                        if (tActual == 17 || tActual == 13 || tActual == 14 || tActual == 15 || tActual == 16 || tActual == 12)
                        {
                            produccion = "B10";
                            i++;
                        }
                        else
                        {
                            agregarError(temporal2, -4);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "B10":
                        if (tActual == 1 || tActual == 2 || tActual == 3 || tActual == 4)
                        {
                            produccion = "B11";
                            i++;
                        } else if (tActual == 5)
                        {
                            produccion = "B10.1";
                            i++;
                        }
                        else
                        {
                            agregarError(temporal2, -2);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "B10.1":
                        if (tActual == 11)
                        {
                            produccion = "B10.2";
                            i++;
                        }
                        else
                        {
                            agregarError(temporal2, 11);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "B10.2":
                        if (tActual == 5)
                        {
                            produccion = "B11";
                            i++;
                        }
                        else
                        {
                            agregarError(temporal2, 5);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "B11":
                        if (tActual == 19 || tActual == 20)
                        {
                            produccion = "B8";
                            i++;
                        }
                        else if (tActual == 9)
                        {
                            produccion = "Y0";
                            i++;
                            // MessageBox.Show("SELECCIONAR correcto");
                        }
                        else
                        {
                            agregarError(temporal2, -5);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    default:
                        i = modoPanico(i);
                        ErrorSintactico += " Se esperaba una palabra de inicio Crear, insertar, actualizar, seleccionar o eliminar en vez de" + temporal2.lexema;
                        produccion = "Y0";
                        break;
                }
            }
        }
        private void generarImagen() {
            this.impresionColumna += "</tr></table> >]; }";
            Archivo temporar = new Archivo();
            temporar.ruta = "C:\\PY1\\Consulta.txt";
            temporar.guardar(this.impresionColumna);
            Process.Start("C:\\PY1\\generador.bat");
        }
        private ArrayList condicion(String tabla,String columna,String comparador,String dato)
        {
            ArrayList aux = new ArrayList();
            Tabla auxTab = obtenerTabla(tabla);
            ColumnaTabla auxCol = obtenerColumna(auxTab,columna);
            for (int i=0;i<auxCol.datos.Count;i++)
            {
                if (auxCol.datos[i].ToString().Equals(dato))
                {
                    aux.Add(i);
                }
            }
            return aux;
        }
        public void actualizar(String tabla,ArrayList columnaDato, ArrayList condicionales,ArrayList Y_O ) {
            Tabla auxTab = obtenerTabla(tabla);
            ArrayList indices = new ArrayList();
            ColumnaDato auxColDato= null;
            for (int indexC=0;indexC<condicionales.Count;indexC++) {
                if (indexC==0)
                {
                    Condicion conAux = (Condicion)condicionales[indexC];
                    indices = condicion(tabla, conAux.columna1, conAux.condicion, conAux.dato2);
                   // MessageBox.Show("indice: "+ indexC + " columna1 "+conAux.columna1+ " comparador "+conAux.condicion+" dato2 "+ conAux.dato2);
                   
                }
                else
                {
                    if (Y_O[indexC-1].ToString().Equals("Y"))
                    {
                        Condicion conAux = (Condicion)condicionales[indexC];
                        indices = and(indices, condicion(tabla, conAux.columna1, conAux.condicion, conAux.dato2));
                    }
                    else
                    {
                        Condicion conAux = (Condicion)condicionales[indexC];
                        indices = or(indices, condicion(tabla, conAux.columna1, conAux.condicion, conAux.dato2));
                       // MessageBox.Show("columna2 " + conAux.columna1 + " comparador " + conAux.condicion + " dato2 " + conAux.dato2);
                    }
                }
               
            }
            for (int i=0;i<indices.Count;i++)
            {
                int index = (int)indices[i];
                for (int ii=0;ii<columnaDato.Count;ii++) {
                    auxColDato = (ColumnaDato)columnaDato[ii];
                    
                    ColumnaTabla auxCol = obtenerColumna(auxTab,auxColDato.columna);
                   
                    auxCol.datos[index] =auxColDato.dato;
                    
                }
                // MessageBox.Show("Cuenta:auxCol.datos[index]);
            }
        }

        public  ArrayList and(ArrayList comparacion1, ArrayList comparacion2)
        {
            ArrayList regreso=new ArrayList();
            for (int i = 0; i < comparacion1.Count; i++)
            {
                for (int ii = 0; ii < comparacion2.Count; ii++) {
                    if (comparacion1[i].Equals(comparacion2[ii])) {
                        regreso.Add(comparacion2[ii]);
                    }
                }
            }
            return regreso;
        }
        public ArrayList or(ArrayList comparacion1, ArrayList comparacion2)
        {
            ArrayList regreso = new ArrayList();
            for (int i=0;i<comparacion1.Count;i++) 
            {
                regreso.Add(comparacion1[i]);
            }
            for (int i = 0; i < comparacion2.Count; i++)
            {
                regreso.Add(comparacion2[i]);
            }
            return regreso;
        }

        public void eliminar(String tabla, ArrayList condicionales, ArrayList Y_O)
        {
            Tabla auxTab = obtenerTabla(tabla);
            ArrayList indices = new ArrayList();
            ColumnaTabla auxDatos = null;
            for (int indexC = 0; indexC < condicionales.Count; indexC++)
            {
                if (indexC == 0)
                {
                    Condicion conAux = (Condicion)condicionales[indexC];
                    indices = condicion(tabla, conAux.columna1, conAux.condicion, conAux.dato2);
                    // MessageBox.Show("indice: "+ indexC + " columna1 "+conAux.columna1+ " comparador "+conAux.condicion+" dato2 "+ conAux.dato2);

                }
                else
                {
                    if (Y_O[indexC - 1].ToString().Equals("Y"))
                    {
                        Condicion conAux = (Condicion)condicionales[indexC];
                        indices = and(indices, condicion(tabla, conAux.columna1, conAux.condicion, conAux.dato2));
                    }
                    else
                    {
                        Condicion conAux = (Condicion)condicionales[indexC];
                        indices = or(indices, condicion(tabla, conAux.columna1, conAux.condicion, conAux.dato2));
                        // MessageBox.Show("columna2 " + conAux.columna1 + " comparador " + conAux.condicion + " dato2 " + conAux.dato2);
                    }
                }

            }
            for (int i = indices.Count-1; i >= 0; i--)
            {
                int index = (int)indices[i];
                for (int ii = 0; ii < auxTab.columnas.Count; ii++)
                {
                    auxDatos = (ColumnaTabla)auxTab.columnas[ii];
                    auxDatos.datos[index]=" ";

                }
               

                // MessageBox.Show("Cuenta:auxCol.datos[index]);
            }
            for (int ii = 0; ii < auxTab.columnas.Count; ii++)
            {
                auxDatos = (ColumnaTabla)auxTab.columnas[ii];
                for (int i=0;i<auxDatos.datos.Count;i++)
                {
                    auxDatos.datos.Remove(" ");
                }
                

            }
        }
    }
}
