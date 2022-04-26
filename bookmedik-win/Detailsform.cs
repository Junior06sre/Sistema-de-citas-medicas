using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bookmedik_win
{
    public partial class Detailsform : Form
    {
        public Detailsform()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtId.Text);
            string sql = "SELECT nombre, imagen FROM foto WHERE id = '" + id + "'";
            Connection conexionBD = new Connection();
            conexionBD.con.Open();

            try
            {
                MySqlCommand comando = conexionBD.con.CreateCommand();
                comando.CommandText = "SELECT nombre, imagen FROM fotos WHERE id = '" + id + "'";
                MySqlDataReader reader = comando.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    MemoryStream ms = new MemoryStream((byte[])reader["imagen"]);
                    Bitmap mb = new Bitmap(ms);
                    pbImagen.Image = mb;
                    txtNombre.Text = reader["nombre"].ToString();
                }
                else
                {
                    MessageBox.Show("No se encontraron registros");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar imagen: " + ex.Message);
            }
        }

        private void Detailsform_Load(object sender, EventArgs e)
        {

        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofdSeleccionar = new OpenFileDialog();
            ofdSeleccionar.Filter = "Imagenes|*.jpg; *.png";
            ofdSeleccionar.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            ofdSeleccionar.Title = "Seleccionar imagen";

            if (ofdSeleccionar.ShowDialog() == DialogResult.OK)
            {
                pbImagen.Image = Image.FromFile(ofdSeleccionar.FileName);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            MemoryStream ms = new MemoryStream();
            pbImagen.Image.Save(ms, ImageFormat.Jpeg);
            byte[] aBytes = ms.ToArray();

            Connection conexionBD = new Connection();
            conexionBD.con.Open();

            try
            {
                MySqlCommand comando = conexionBD.con.CreateCommand();
                comando.CommandText = "INSERT INTO fotos (nombre, imagen) VALUES ('" + txtNombre.Text + "', @imagen)";
                comando.Parameters.AddWithValue("imagen", aBytes);
                comando.ExecuteNonQuery();
                MessageBox.Show("Imagen guardada");
                pbImagen.Image = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar imagen: " + ex.Message);
            }

            txtNombre.Text = null;
        }
    }
}
