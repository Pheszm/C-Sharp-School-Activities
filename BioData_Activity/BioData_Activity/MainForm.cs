using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySqlX.XDevAPI.Relational;


namespace BioData_Activity
{
    public partial class MainForm : Form
    {
        string connstring = Program.connstring;
        string Id;
        int ImgSizes = 130;
        public MainForm()
        {
            InitializeComponent();
            LoadTable();
            cleardata();
        }

        void LoadTable()
        {
            using (MySqlConnection connection = new MySqlConnection(connstring))
            {
                connection.Open();
                string query = "SELECT P_Id as ID, Name as 'Full Name', ImgDirectory as 'Image Directory' FROM Personal_Info";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dataGridView1.DataSource = dt;

                dt.Columns.Add("Image", typeof(byte[]));

                dataGridView1.RowTemplate.Height = ImgSizes;
                dataGridView1.AllowUserToAddRows = false;

                DataGridViewImageColumn imageColumn = dataGridView1.Columns["Image"] as DataGridViewImageColumn;
                imageColumn.ImageLayout = DataGridViewImageCellLayout.Stretch;

                foreach (DataRow row in dt.Rows)
                {
                    string imgDirectory = row["Image Directory"].ToString();

                    byte[] imageData;
                    using (FileStream fileStream = new FileStream(imgDirectory, FileMode.Open, FileAccess.Read))
                    {
                        using (BinaryReader reader = new BinaryReader(fileStream))
                        {
                            imageData = reader.ReadBytes((int)fileStream.Length);
                        }
                    }
                    row["Image"] = imageData;
                }
                dt.Columns.Remove("Image Directory");
            }
        }


        private void View_btn_Click(object sender, EventArgs e)
        {
            OtherForm.View_Form form = new OtherForm.View_Form(Id);
            this.Hide();
            form.ShowDialog();
            LoadTable();
            cleardata();
            this.Show();
        }

        private void Add_btn_Click(object sender, EventArgs e)
        {
            string idd = "";
            AddingForm.Add_Personal_Info form = new AddingForm.Add_Personal_Info(idd);
            this.Hide();
            form.ShowDialog();
            LoadTable();
            this.Show();
        }

        private void CellClick_Event(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                Id = selectedRow.Cells["ID"].Value.ToString();
                dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(40, 40, 120);
                dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;
                View_btn.Visible = true;
                Clear_select_btn.Visible = true;
                Del_btn.Visible = true;
                SelectTxt.Visible = false;
                Add_btn.Visible = false;
                if (Id == "") cleardata();
            }
        }

        void cleardata()
        {
            Id = "";
            View_btn.Visible = false;
            Clear_select_btn.Visible = false;
            Del_btn.Visible = false;
            Add_btn.Visible = true;
            SelectTxt.Visible = true;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        private void Clear_select_btn_Click(object sender, EventArgs e)
        {
            cleardata();
            LoadTable();
        }

        private void Del_btn_Click(object sender, EventArgs e)
        {
            Remove_Function();
        }

        void Remove_Function()
        {
            DialogResult result = MessageBox.Show("Are you sure you want to remove this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                using (MySqlConnection connection = new MySqlConnection(connstring))
                {
                    connection.Open();
                    string query = "DELETE FROM Personal_Info WHERE P_Id = @P_Id";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@P_Id", Id);
                    command.ExecuteNonQuery();


                    query = "DELETE FROM Educational_Back_tbl WHERE P_Id = @P_Id";

                    command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@P_Id", Id);
                    command.ExecuteNonQuery();


                    query = "DELETE FROM Employment_Rec_tbl WHERE P_Id = @P_Id";

                    command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@P_Id", Id);
                    command.ExecuteNonQuery();

                    query = "DELETE FROM Char_Reference_tbl WHERE P_Id = @P_Id";

                    command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@P_Id", Id);
                    command.ExecuteNonQuery();

                    query = "DELETE FROM Other_Details_tbl WHERE P_Id = @P_Id";

                    command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@P_Id", Id);
                    command.ExecuteNonQuery();


                    MessageBox.Show("Record successfully removed.");
                    LoadTable();
                    cleardata();
                }
            }
        }

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(connstring))
            {

                connection.Open();
                string query = $"SELECT P_Id as ID, Name as 'Full Name', ImgDirectory as 'Image Directory' FROM Personal_Info WHERE P_Id LIKE '%{SearchBox.Text}%' OR Name LIKE '%{SearchBox.Text}%'";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;

                dt.Columns.Add("Image", typeof(byte[]));

                dataGridView1.RowTemplate.Height = ImgSizes;
                dataGridView1.AllowUserToAddRows = false;

                DataGridViewImageColumn imageColumn = dataGridView1.Columns["Image"] as DataGridViewImageColumn;

                imageColumn.ImageLayout = DataGridViewImageCellLayout.Stretch;

                foreach (DataRow row in dt.Rows)
                {
                    string imgDirectory = row["Image Directory"].ToString();

                    byte[] imageData;
                    using (FileStream fileStream = new FileStream(imgDirectory, FileMode.Open, FileAccess.Read))
                    {
                        using (BinaryReader reader = new BinaryReader(fileStream))
                        {
                            imageData = reader.ReadBytes((int)fileStream.Length);
                        }
                    }
                    row["Image"] = imageData;
                }
                dt.Columns.Remove("Image Directory");
            }
        }
    }
}

