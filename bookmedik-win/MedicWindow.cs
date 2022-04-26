using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bookmedik_win
{
    public partial class MedicWindow : Form
    {
        public static DataGridView data;
        public MedicWindow()
        {
            InitializeComponent();
            data = dataGridView1;
            dataGridView1.Columns.Add("Id", "Id");
            dataGridView1.Columns.Add("Nombre", "Nombre");
            dataGridView1.Columns.Add("Apellido", "Apellido");
            dataGridView1.Columns.Add("Email", "Email");
            dataGridView1.Columns.Add("Telefono", "Telefono");
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[3].Width = 200;
            load();
        }
        public static void load()
        {
            data.Rows.Clear();
            foreach (MedicObj p in MedicObj.getAll())
            {
                data.Rows.Add(p.id, p.name, p.lastname, p.email, p.phone);
            }

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() != "")
            {
                MedicForm.action = 2;
                MedicForm.id = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                MedicForm p = new MedicForm();
                p.ShowDialog();
                load();
            }
        }

        private void MedicWindow_Load(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtID.Text);
            data.Rows.Clear();
            foreach (MedicObj p in MedicObj.getAll())
            {
                if (p.id == id)
                {
                    data.Rows.Add(p.id, p.name, p.lastname, p.email, p.phone);
                    break;
                }
            }
        }
    }
}
