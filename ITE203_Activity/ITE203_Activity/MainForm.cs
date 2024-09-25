using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace ITE203_Activity
{
    public partial class MainForm : Form
    {
        string connstring = Program.connstring;

        public MainForm()
        {
            InitializeComponent();
            loadDepBox();
            loadBrgyBox();
            LoadDataGridView();
            cleardata();
        }

        string B_id, D_id;
        private void Add_btn_Click(object sender, EventArgs e)
        {
            if (Fname.Text == "" || LName.Text == "" || DepartBox.Text == "" || BarangayBox.Text == "") return;
            using (MySqlConnection connection = new MySqlConnection(connstring))
            {
                connection.Open();
                string query = "SELECT Brgy_Id FROM Brgy_tbl WHERE Brgy = @Brgy";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Brgy", BarangayBox.Text);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        B_id = reader.GetInt32("Brgy_Id").ToString();
                    }
                }
            } 

            using (MySqlConnection connection = new MySqlConnection(connstring))
            {
                connection.Open();
                string query = "SELECT Dep_Id FROM Department_tbl WHERE Department = @Department";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Department", DepartBox.Text);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        D_id = reader.GetInt32("Dep_Id").ToString();
                    }
                }
            } 


            using (MySqlConnection connection = new MySqlConnection(connstring))
            {
                connection.Open();
                string query = "INSERT INTO Profile (LastName, FirstName, Middle_Initial, BirthDate, Dep_Id, Brgy_Id) " +
                               "VALUES (@LastName, @FirstName, @Middle_Initial, @BirthDate, @Dep_Id, @Brgy_Id)";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@LastName", LName.Text);
                command.Parameters.AddWithValue("@FirstName", Fname.Text);
                command.Parameters.AddWithValue("@Middle_Initial", MInitial.Text);
                command.Parameters.AddWithValue("@BirthDate", Bdate.Text);
                command.Parameters.AddWithValue("@Dep_Id", Int32.Parse(D_id));
                command.Parameters.AddWithValue("@Brgy_Id", Int32.Parse(B_id));

                command.ExecuteNonQuery();
            } 

            MessageBox.Show("SUCCESSFULLY ADDED");
            cleardata();
            LoadDataGridView();
        }



        void search()
        {
            using (MySqlConnection connection = new MySqlConnection(connstring))
            {
                connection.Open();

                string query = $"SELECT p.Id, p.LastName, p.FirstName, p.Middle_Initial, p.BirthDate, " +
                               $"       d.Department, b.Brgy " +
                               $"FROM Profile p " +
                               $"INNER JOIN Department_tbl d ON p.Dep_Id = d.Dep_Id " +
                               $"INNER JOIN Brgy_tbl b ON p.Brgy_Id = b.Brgy_Id " +
                               $"WHERE p.Id LIKE '%{SearchBox.Text}%' " +
                               $"   OR p.LastName LIKE '%{SearchBox.Text}%' " +
                               $"   OR p.FirstName LIKE '%{SearchBox.Text}%' " +
                               $"   OR p.Middle_Initial LIKE '%{SearchBox.Text}%' " +
                               $"   OR p.BirthDate LIKE '%{SearchBox.Text}%' " +
                               $"   OR d.Department LIKE '%{SearchBox.Text}%' " +
                               $"   OR b.Brgy LIKE '%{SearchBox.Text}%'";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }



        void loadDepBox()
        {
            DepartBox.Items.Clear();
            using (MySqlConnection connection = new MySqlConnection(connstring))
            {
                connection.Open();
                string query = "SELECT Department FROM department_tbl";
                MySqlCommand command = new MySqlCommand(query, connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string departmentName = reader.GetString("Department");
                        DepartBox.Items.Add(departmentName);
                    }
                }
            }
        }

        void loadBrgyBox()
        {
            BarangayBox.Items.Clear();
            using (MySqlConnection connection = new MySqlConnection(connstring))
            {
                connection.Open();
                string query = "SELECT Brgy FROM Brgy_tbl";
                MySqlCommand command = new MySqlCommand(query, connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string BrgyNames = reader.GetString("Brgy");
                        BarangayBox.Items.Add(BrgyNames);
                    }
                }
            }
        }

        void LoadDataGridView()
        {
            using (MySqlConnection connection = new MySqlConnection(connstring))
            {
                connection.Open();
                string query = @"SELECT p.Id, p.LastName, p.FirstName, p.Middle_Initial, p.BirthDate, 
                                d.Department, b.Brgy
                         FROM Profile p
                         INNER JOIN Department_tbl d ON p.Dep_Id = d.Dep_Id
                         INNER JOIN Brgy_tbl b ON p.Brgy_Id = b.Brgy_Id";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
        }


        void cleardata()
        {
            string StartTimee = DateTime.Now.ToString("dd/MM/yy");
            Bdate.Text = StartTimee;
            LName.Text = "";
            Fname.Text = "";
            Bdate.Text = "";
            MInitial.Text = "N/A";
            DepartBox.SelectedIndex = -1;
            BarangayBox.SelectedIndex = -1;
            Add_btn.Enabled = true;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        private void BrgyBtn_Click(object sender, EventArgs e)
        {
            BrgyForm brgyForm = new BrgyForm();
            brgyForm.ShowDialog();
            loadDepBox();
            loadBrgyBox();
        }

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            search();
        }


        string id;
        private void TableClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                id = selectedRow.Cells["Id"].Value.ToString();
                LName.Text = selectedRow.Cells["LastName"].Value.ToString();
                Fname.Text = selectedRow.Cells["FirstName"].Value.ToString();
                MInitial.Text = selectedRow.Cells["Middle_Initial"].Value.ToString();
                DepartBox.Text = selectedRow.Cells["Department"].Value.ToString();
                BarangayBox.Text = selectedRow.Cells["Brgy"].Value.ToString();
                Add_btn.Enabled = false;
                if (id != "")
                {
                    Bdate.Text = DateTime.ParseExact(selectedRow.Cells["BirthDate"].Value.ToString(), "dd/MM/yy", CultureInfo.InvariantCulture).ToString();
                    dataGridView1.DefaultCellStyle.SelectionBackColor = Color.SkyBlue;
                    dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
                }
                else
                {
                    cleardata();
                }
            }
            if(id == "")
            {
                Add_btn.Enabled = true;
                DepartBox.SelectedIndex = -1;
                BarangayBox.SelectedIndex = -1;
            }
        }

        private void Remove_btn_Click(object sender, EventArgs e)
        {
            if (id != "")
            {
                using (MySqlConnection connection = new MySqlConnection(connstring))
                {
                    connection.Open();
                    string query = "DELETE FROM Profile WHERE Id = @Id";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                    MessageBox.Show("REMOVED SUCCESSFUL");
                    cleardata();
                    LoadDataGridView();
                }
            }
        }

        private void Update_btn_Click(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(connstring))
            {
                connection.Open();
                string query = "SELECT Brgy_Id FROM Brgy_tbl WHERE Brgy = @Brgy";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Brgy", BarangayBox.Text);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        B_id = reader.GetInt32("Brgy_Id").ToString();
                    }
                }
            }

            using (MySqlConnection connection = new MySqlConnection(connstring))
            {
                connection.Open();
                string query = "SELECT Dep_Id FROM Department_tbl WHERE Department = @Department";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Department", DepartBox.Text);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        D_id = reader.GetInt32("Dep_Id").ToString();
                    }
                }
            }

            if (!string.IsNullOrEmpty(id))
            {
                using (MySqlConnection connection = new MySqlConnection(connstring))
                {
                    connection.Open();
                    string query = "UPDATE Profile SET LastName = @LastName, FirstName = @FirstName, Middle_Initial = @Middle_Initial, BirthDate = @BirthDate, Dep_Id = @Dep_Id, Brgy_Id = @Brgy_Id WHERE Id = @Id";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@LastName", LName.Text);
                    command.Parameters.AddWithValue("@FirstName", Fname.Text);
                    command.Parameters.AddWithValue("@Middle_Initial", MInitial.Text);
                    command.Parameters.AddWithValue("@BirthDate", Bdate.Text);
                    command.Parameters.AddWithValue("@Dep_Id", Int32.Parse(D_id)); 
                    command.Parameters.AddWithValue("@Brgy_Id", Int32.Parse(B_id)); 
                    command.Parameters.AddWithValue("@Id", id);

                    command.ExecuteNonQuery();

                    MessageBox.Show("Update successful.");
                    cleardata(); 
                    LoadDataGridView(); 
                }
            }
        }


        private void DepartmentBtn_Click(object sender, EventArgs e)
        {
            DepartmentForm departmentForm = new DepartmentForm();
            departmentForm.ShowDialog();
            loadDepBox();
            loadBrgyBox();
        }
    }
}
