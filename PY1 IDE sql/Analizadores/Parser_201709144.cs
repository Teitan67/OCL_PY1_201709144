using PY1_IDE_sql.Clase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PY1_IDE_sql.Analizadores
{
    class Parser_201709144
    {
        String ErrorSintactico = "";
        int errores=0;
        ArrayList Tokens;
        public Parser_201709144(ArrayList Tokens) {
            this.Tokens = Tokens;
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
                case 18:
                    produccion = "C0";
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
            for (int j = i; j < this.Tokens.Count;j++)
            {
                temporal2 = (Token)Tokens[j];
                if (temporal2.id==9) {
                    regreso = j+1;
                    break;
                }
                cuenta++;
            }
            ErrorSintactico += "\nModo Panico activado!!!"+
                "\n"+cuenta+" tokens ignorados";
            MessageBox.Show(ErrorSintactico);
            return regreso;
        }
        private void agregarError(Token aux,int id) {
            ErrorSintactico +="Se esperaba "+qTokenEs(id)+" en vez de "+ aux.lexema+
                "\n En Fila: "+aux.fila+" Columnas: "+aux.columna+"\n";
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
                default:
                    aux = "No definido " + id.ToString();
                    break;
            }
            return aux;
        }
        private void analizador(ArrayList token) 
        {
            String produccion = "Y0";
            Token temporal2;
            int tActual;
            for (int i=0; i<token.Count;) 
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
                        if (tActual==30)
                        {
                            produccion = "S1";
                            i++;
                        }
                        else
                        {
                            agregarError(temporal2,30);
                            i = modoPanico(i);
                            produccion = "Y0";

                        }
                        break;
                    case "S1":
                        if (tActual==31)
                        {
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
                        if (tActual ==32|| tActual == 33|| tActual == 34|| tActual == 35)
                        {
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
                        else if(tActual==7)
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
                            MessageBox.Show("Create aceptado");
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
                        if (tActual == 1|| tActual == 2|| tActual == 3|| tActual == 4)
                        {
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
                            produccion = "Y0";
                            i++;
                            MessageBox.Show("Insertar aceptado"); 
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
                        }else if (tActual==9) 
                        {
                            produccion = "Y0";
                            i++;
                            MessageBox.Show("Eliminar correcto");
                        }
                        else
                        {
                            agregarError(temporal2,9);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "C4":
                        if (tActual == 1 || tActual == 2 || tActual == 3 || tActual == 4||tActual==5)
                        {
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
                        if (tActual == 19||tActual==20)
                        {
                            produccion = "C4";
                            i++;
                        }
                        else if (tActual == 9)
                        {
                            produccion = "Y0";
                            i++;
                            MessageBox.Show("Eliminar correcto");
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
                        ErrorSintactico += " Se esperaba una palabra de inicio Crear, insertar, actualizar, seleccionar o eliminar en vez de" +temporal2.lexema;
                        produccion ="Y0";
                        break;
                }
            }
        }
    }
}
