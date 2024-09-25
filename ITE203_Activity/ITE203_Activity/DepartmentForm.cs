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
    public partial class DepartmentForm : Form
    {

        string consstring = Program.connstring;
        public DepartmentForm()
        {
            InitializeComponent();
            cleardata();
        }


        private void Add_btn_Click(object sender, EventArgs e)
        {
            if(DepBox.Text != "")
            {
                using (MySqlConnection connection = new MySqlConnection(consstring))
                {
                    connection.Open();
                    string query = "INSERT INTO department_tbl (Department) VALUES (@Department)";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Department", DepBox.Text);
                    command.ExecuteNonQuery();
                    MessageBox.Show($"{DepBox.Text} HAS ADDED SUCCESSFULLY");
                    cleardata();
                    LoadTable();
                }
            }
        }

        string id = "";
        void cleardata()
        {
            DepBox.Text = "";
            id = "";
            Add_btn.Enabled = true;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        void LoadTable()
        {
            using (MySqlConnection connection = new MySqlConnection(consstring))
            {

                connection.Open();
                string query = "SELECT * FROM department_tbl";


                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }

        }

        private void DepartmentForm_Load(object sender, EventArgs e)
        {
            LoadTable();
        }

        private void TableClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                id = selectedRow.Cells["Dep_Id"].Value.ToString();
                DepBox.Text = selectedRow.Cells["Department"].Value.ToString();
                Add_btn.Enabled = false;
                dataGridView1.DefaultCellStyle.SelectionBackColor = Color.SkyBlue;
                dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
                if (id == "") cleardata();
            }
        }

        void search()
        {
            using (MySqlConnection connection = new MySqlConnection(consstring))
            {

                connection.Open();
                string query = $"SELECT * FROM Department_tbl WHERE Dep_Id LIKE '%{SearchBox.Text}%' OR Department LIKE '%{SearchBox.Text}%'";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }


        private void Update_btn_Click(object sender, EventArgs e)
        {
            if (id != "" && DepBox.Text != "")
            {
                using (MySqlConnection connection = new MySqlConnection(consstring))
                {
                    connection.Open();
                    string query = "UPDATE department_tbl SET department = @department WHERE Dep_Id = @Id";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@department", DepBox.Text);
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                    MessageBox.Show("UPDATED SUCCESSFUL");
                    cleardata();
                    LoadTable();
                }
            }

        }

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            search();
        }

        private void Remove_btn_Click(object sender, EventArgs e)
        {
            if (id != "")
            {
                try
                {
                    using (MySqlConnection connection = new MySqlConnection(consstring))
                    {
                        connection.Open();
                        string query = "DELETE FROM Department_tbl WHERE Dep_Id = @Id";

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
