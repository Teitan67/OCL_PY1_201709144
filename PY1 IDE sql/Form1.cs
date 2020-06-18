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
        public Form1()
        {
            InitializeComponent();
            bancaDatos = new ArrayList();
           
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

        private void button1_Click(object sender, EventArgs e)
        {
            
            
            
        }

        private void ejecutarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            analizadorLexico = new Scaneer_201709144(richTextBox1.Text);
            int erroes = analizadorLexico.sintactico.errores+analizadorLexico.errores;
            mostrarTokensToolStripMenuItem.Enabled = true;
            mostrarErroresToolStripMenuItem.Enabled = true;
            MessageBox.Show("Analisis realizado!!\n Tokens:"+analizadorLexico.tokens+" Comentarios:"+analizadorLexico.comentarios+" Errores:"+erroes);
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
    }
}