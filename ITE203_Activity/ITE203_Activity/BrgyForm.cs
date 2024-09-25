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

namespace ITE203_Activity
{
    public partial class BrgyForm : Form
    {
        string connstring = Program.connstring;


        public BrgyForm()
        {
            InitializeComponent();
            LoadTable();
            cleardata();
        }

        private void Add_btn_Click(object sender, EventArgs e)
        {
            if(BrgyBox.Text != "" && MuniBox.Text != "" && RegionBox.Text != "")
            {
                using (MySqlConnection connection = new MySqlConnection(connstring))
                {
                    connection.Open();
                    string query = "INSERT INTO Brgy_tbl (Brgy, Municipality, Region) VALUES (@Brgy, @Municipality, @Region)";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Brgy", BrgyBox.Text);
                    command.Parameters.AddWithValue("@Municipality", MuniBox.Text);
                    command.Parameters.AddWithValue("@Region", RegionBox.Text);

                    command.ExecuteNonQuery();
                    MessageBox.Show($"{BrgyBox.Text} HAS BEEN ADDED SUCCESSFULLY");
                    cleardata();
                    LoadTable();
                }
            }
        }

        string id;

        void cleardata()
        {
            BrgyBox.Text = "";
            MuniBox.Text = "";
            RegionBox.Text = "";
            id = "";
            Add_btn.Enabled = true;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        void LoadTable()
        {
            using (MySqlConnection connection = new MySqlConnection(connstring))
            {

                connection.Open();
                string query = "SELECT * FROM brgy_tbl";


                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void TableClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                id = selectedRow.Cells["Brgy_Id"].Value.ToString();
                BrgyBox.Text = selectedRow.Cells["Brgy"].Value.ToString();
                MuniBox.Text = selectedRow.Cells["Municipality"].Value.ToString();
                RegionBox.Text = selectedRow.Cells["Region"].Value.ToString();
                Add_btn.Enabled = false;
                dataGridView1.DefaultCellStyle.SelectionBackColor = Color.SkyBlue;
                dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
                if (id == "")
                {
                    cleardata();
                }
            }
        }

        void search()
        {
            using (MySqlConnection connection = new MySqlConnection(connstring))
            {

                connection.Open();
                string query = $"SELECT * FROM Brgy_tbl WHERE Brgy_Id LIKE '%{SearchBox.Text}%' OR Brgy LIKE '%{SearchBox.Text}%' OR Municipality LIKE '%{SearchBox.Text}%' OR Region LIKE '%{SearchBox.Text}%'";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            search();
        }

        private void Update_btn_Click(object sender, EventArgs e)
        {
            if (id != "" && BrgyBox.Text != "" && MuniBox.Text != "" && RegionBox.Text != "")
            {
                using (MySqlConnection connection = new MySqlConnection(connstring))
                {
                    connection.Open();
                    string query = "UPDATE Brgy_tbl SET Brgy = @Brgy, Municipality = @Municipality, Region = @Region WHERE Brgy_Id = @Id";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Brgy", BrgyBox.Text);
                    command.Parameters.AddWithValue("@Municipality", MuniBox.Text);
                    command.Parameters.AddWithValue("@Region", RegionBox.Text);
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                    MessageBox.Show("UPDATED SUCCESSFUL");
                    cleardata();
                    LoadTable();
                }
            }
        }

        private void Remove_btn_Click(object sender, EventArgs e)
        {
            if (id != "")
            {
                try
                {
                    using (MySqlConnection connection = new MySqlConnection(connstring))
                    {
                        connection.Open();
                        string query = "DELETE FROM Brgy_tbl WHERE Brgy_Id = @Id";

                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@Id", id);
                        command.ExecuteNonQuery();
                        MessageBox.Show("REMOVED SUCCESSFUL");
                        cleardata();
                        LoadTable();
                    }
                }
                catch (MySqlException ex)
                {
                    if (ex.Number == 1451 || ex.Number == 1452)
                    {
                        MessageBox.Show("Connot Delete a Used Foreign Key.");
                    }
                    else
                    {
                        MessageBox.Show("MySQL Exception occurred: " + ex.Message);
                    }
                }
            }
        }
    }
}
