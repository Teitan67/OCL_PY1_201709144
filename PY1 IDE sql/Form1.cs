using PY1_IDE_sql.Analizadores;
using PY1_IDE_sql.Clase;
using PY1_IDE_sql.Ventanas;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PY1_IDE_sql
{
    public partial class Form1 : Form
    {
        Archivo archivo;
        Scaneer_201709144 analizadorLexico;
        ArrayList bancaDatos;
        int bIndice = 0;
        public Form1()
        {
            InitializeComponent();
            this.bancaDatos = new ArrayList();

        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AcercaDe ventanaInfo = new AcercaDe();
            ventanaInfo.Visible=true;
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            archivo = new Archivo();
            richTextBox1.Text="";
            richTextBox1.Enabled=true;
            activarBoton();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            archivo = new Archivo();
            archivo.abrirArchivo();
            richTextBox1.Text =archivo.contenido;
            richTextBox1.Enabled = true;
            activarBoton();

        }
        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            archivo.guardar(richTextBox1.Text);
        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            archivo.ruta = null;
            archivo.guardar(richTextBox1.Text);
        }
        private void activarBoton()
        {
            guardarComoToolStripMenuItem.Enabled = true;
            guardarToolStripMenuItem.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
       
        }
        public static int[] AllIndexesOf(string str, string substr, bool ignoreCase = false)
        {
            if (string.IsNullOrWhiteSpace(str) ||
                string.IsNullOrWhiteSpace(substr))
            {
            }
            var indexes = new List<int>();
            int index = 0;
            while ((index = str.IndexOf(substr, index, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal)) != -1)
            {
                indexes.Add(index++);
            }

            return indexes.ToArray();
        }
        private void pintar() 
        {
            pintarCafe();
            String []palabra = { "Tabla","Insertar","Eliminar","Crear"," En ","Valores","0","1","2","3","4","5","6","7","8","9",".","=","!","<",">","--","Seleccionar", " como " ," de "," donde "," Y "," o ","Entero","fecha","flotante","cadena"};
            for (int ii=0;ii<palabra.Length;ii++) {
                int[] indice = AllIndexesOf(richTextBox1.Text, palabra[ii], true);
                int[] indice2 = AllIndexesOf(richTextBox1.Text, "\n", true);
                for (int i = 0; i < indice.Length; i++)
                {
                    richTextBox1.Select(indice[i], palabra[ii].Length);
                    if (ii < 2)
                    {
                        richTextBox1.SelectionColor = Color.Purple;
                    } else if (ii >= 6 && ii <= 16)
                    {
                        richTextBox1.SelectionColor = Color.Blue;
                    }
                    else if (ii>=3&&ii<=5)
                    {
                        richTextBox1.SelectionColor = Color.Black;
                    }
                    else if(ii>=17&&ii<=20)
                    {
                        richTextBox1.SelectionColor =Color.Red;
                    }else if (ii==21) {
                        for (int o = 0; o < indice2.Length; o++)
                        {
                            if (indice[i]< indice2[o])
                            {
                                pintarLinea(indice[i], indice2[o]- (indice[i]));
                                break;
                            }
                        }
                    }
                    else
                    {
                        richTextBox1.SelectionColor = Color.Black;
                    }
                }
            }
            pintarCadena();
            

        }
        private void pintarCafe()
        {
            String[] palabra = { "a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z","-","_","á","é","í","ó","ú" };
            for (int ii = 0; ii < palabra.Length; ii++)
            {
                int[] indice = AllIndexesOf(richTextBox1.Text, palabra[ii], true);
                for (int i = 0; i < indice.Length; i++)
                {
                    richTextBox1.Select(indice[i], palabra[ii].Length);
                    richTextBox1.SelectionColor = Color.BurlyWood;
                }
            }

        }
        public void pintarLinea( int index, int length)
        {
            richTextBox1.Select(index, length);                 
            richTextBox1.SelectionColor = Color.Gray;
        }
        private void pintarCadena() {
            String[] palabra = { "/","\"", "\'" };
            for (int ii = 0; ii < palabra.Length; ii++)
            {
                int[] indice = AllIndexesOf(richTextBox1.Text, palabra[ii], true);
                for (int i = 0; i < indice.Length-1; i++)
                {
                    richTextBox1.Select(indice[i], indice[i+1]- indice[i]+1);
                    if (ii == 0)
                    {
                        richTextBox1.SelectionColor = Color.Gray;
                    }
                    else if (ii == 1)
                    {
                        richTextBox1.SelectionColor = Color.Green;
                    }
                    else
                    {
                        richTextBox1.SelectionColor=Color.Orange;
                    }
                   
                    i++;
                }
            }
        }
        private void ejecutarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String cadena = richTextBox1.SelectedText;
            if (cadena.Length>0)
            {
                analizadorLexico = new Scaneer_201709144(cadena, this.bancaDatos);
            }
            else
            {
                analizadorLexico = new Scaneer_201709144(richTextBox1.Text, this.bancaDatos);
            }
            int erroes = analizadorLexico.sintactico.errores+analizadorLexico.errores;
            mostrarTokensToolStripMenuItem.Enabled = true;
            mostrarErroresToolStripMenuItem.Enabled = true;
            MessageBox.Show("Analisis realizado!!\n Tokens:"+analizadorLexico.tokens+" Comentarios:"+analizadorLexico.comentarios+" Errores:"+erroes);
            //richTextBox1.Text= analizadorLexico.sintactico.imprimir();
            bancaDatos = analizadorLexico.tablas;
            // actualizar(String tabla,ArrayList columnaDato, ArrayList condicionales)
        }

        private void mostrarTokensToolStripMenuItem_Click(object sender, EventArgs e)
        {
            analizadorLexico.generarReporte("<p>"+ analizadorLexico .comentario+ "</p>");
        }

        private void mostrarErroresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            analizadorLexico.reporteErrores("<p>"+analizadorLexico.sintactico.ErrorSintactico+"</p>");
        }

        private void cargarTablasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            archivo = new Archivo();
            archivo.abrirArchivo();
            richTextBox1.Text = archivo.contenido;
            richTextBox1.Enabled = true;
            activarBoton();
        }
        private void verTablas() {
            String contenido = "Tablas\n";
            Tabla auxTab = null;
            ColumnaTabla auxColumna = null;
            for (int i=0;i<this.bancaDatos.Count;i++) 
            {
                auxTab = (Tabla)bancaDatos[i];
                contenido +="\t"+auxTab.nombreTabla+"\n";
                for (int ii=0;ii<auxTab.columnas.Count;ii++)
                {
                    auxColumna = (ColumnaTabla)auxTab.columnas[ii];
                    contenido +="\t\t"+auxColumna.titulo+"\n";
                }
            }
            richTextBox2.Text =contenido;
        }

        private void verTablasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.bancaDatos!=null) 
            {
                verTablas();
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            //pintar();
        }

        private void pintarTextoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pintar();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[] indice = AllIndexesOf(richTextBox1.Text, txtBuscar.Text, true);
            if (bIndice>=indice.Length) {
                bIndice = 0;
            }
            if (indice.Length > 0)
            {
                richTextBox1.SelectAll();
                richTextBox1.SelectionBackColor = Color.White;
                richTextBox1.Select(indice[bIndice], txtBuscar.Text.Length);
                richTextBox1.SelectionBackColor = Color.Yellow;
                bIndice++;
            }
            else
            {
                MessageBox.Show("No hay coincidencia");
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            int[] indice = AllIndexesOf(richTextBox1.Text, txtBuscar.Text, true);
            if (bIndice >= indice.Length)
            {
                bIndice = 0;
            }
            if (indice.Length > 0)
            {
                richTextBox1.SelectAll();
                richTextBox1.SelectionBackColor = Color.White;
                richTextBox1.Select(indice[bIndice], txtBuscar.Text.Length);
                richTextBox1.SelectionBackColor = Color.Yellow;
                richTextBox1.Text=richTextBox1.Text.Remove(indice[bIndice], txtBuscar.Text.Length);
                richTextBox1.Text=richTextBox1.Text.Insert(indice[bIndice],txtRemplazo.Text);
            }
            else 
            {
                MessageBox.Show("No hay coincidencia");
            }
        }

    }
}