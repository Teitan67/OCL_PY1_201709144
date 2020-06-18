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
        public String ErrorSintactico = "";
        public int errores=0;
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
            for (int j = i; j < this.Tokens.Count;j++)
            {
                temporal2 = (Token)Tokens[j];
                if (temporal2.id==9) {
                    regreso = j+1;
                    break;
                }
                cuenta++;
            }
            ErrorSintactico += "Modo Panico activado!!!\n"+
                "\n"+cuenta+ " tokens ignorados<br><br>";
            //MessageBox.Show(ErrorSintactico);
            return regreso;
        }
        private void agregarError(Token aux,int id) {
            ErrorSintactico +="Syntax ERROR: Se esperaba "+qTokenEs(id)+" en vez de "+ aux.lexema+
                "<br> En Fila: " + aux.fila+" Columnas: "+aux.columna+ "<br>";
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
                            //MessageBox.Show("Create aceptado");
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
                            //MessageBox.Show("Eliminar correcto");
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
                        if (tActual == 1|| tActual == 2|| tActual == 3|| tActual == 4)
                        {
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
                        else if (tActual==7) 
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
                            produccion = "D9";
                            i++;
                        }
                        else if (tActual == 9)
                        {
                            produccion = "Y0";
                            i++;
                           // MessageBox.Show("Actualizar correcto");
                        }
                        else
                        {
                            agregarError(temporal2, -5);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "B0":
                        if (tActual==29) 
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
                            produccion = "B6";
                            i++;
                        }
                        else if(tActual==5)
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
                    case "B2":
                        if (tActual == 5)
                        {
                            produccion = "B2.1";
                            i++;
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
                            produccion = "B6";
                        }
                        else if (tActual == 8)
                        {
                            produccion = "B5";
                        }
                        else if (tActual==23) {
                            produccion ="B4";
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
                        else if (tActual==24) 
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
                            produccion = "Y0";
                            i++;
                           // MessageBox.Show("Seleccionar aceptado");
                        }
                        else if (tActual==25) 
                        {
                            produccion ="B8";
                            i++;
                        }
                        else if (tActual==8)
                        {
                            i++;
                            produccion ="B6.1";
                        }
                        else
                        {
                            agregarError(temporal2, 9);
                            i = modoPanico(i);
                            produccion = "Y0";
                        }
                        break;
                    case "B8":
                        if (tActual == 1 || tActual == 2 || tActual == 3 || tActual == 4 )
                        {
                            produccion = "B9";
                            i++;
                        }
                        else if (tActual==5) 
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
                        if (tActual == 1 || tActual == 2 || tActual == 3 || tActual == 4 )
                        {
                            produccion = "B11";
                            i++;
                        }else if (tActual==5) 
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
                        ErrorSintactico += " Se esperaba una palabra de inicio Crear, insertar, actualizar, seleccionar o eliminar en vez de" +temporal2.lexema;
                        produccion ="Y0";
                        break;
                }
            }
        }
    }
}
