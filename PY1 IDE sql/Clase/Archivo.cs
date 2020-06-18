using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PY1_IDE_sql.Clase
{
    class Archivo
    {
        OpenFileDialog ventanaAbrir;
        SaveFileDialog ventanaGuardar;
        public String ruta=null;
        public String contenido=null;

        public Archivo() {
            ventanaAbrir = new OpenFileDialog();
            ventanaAbrir.Filter= "orvl Archivo (*.orvl)|*.orvl|sqle Archivo (*.sqle)|*.sqle";
        }
        public void abrirArchivo() {
            if (ventanaAbrir.ShowDialog() == DialogResult.OK)
            {
                ruta = ventanaAbrir.FileName;
                this.contenido=leerArchivo(ruta);
            }
        }
        public string leerArchivo(String fichero) {
            return File.ReadAllText(fichero, Encoding.UTF8);
            //MessageBox.Show(contenido);
        }
        public void guardar(String contenido) {
            if (ruta==null) {
                obtenerRuta();
                guardar(contenido);
            } else {
                System.IO.File.WriteAllText(ruta, contenido);
            }
        }
        private void obtenerRuta() {
            ventanaGuardar = new SaveFileDialog();
            ventanaGuardar.Filter = "orvl Archivo (*.orvl)|*.orvl|sqle Archivo (*.sqle)|*.sqle";
            if (ventanaGuardar.ShowDialog() == DialogResult.OK)
            {
                this.ruta = ventanaGuardar.FileName;
                MessageBox.Show(this.ruta);
            }
        }
    }
}
