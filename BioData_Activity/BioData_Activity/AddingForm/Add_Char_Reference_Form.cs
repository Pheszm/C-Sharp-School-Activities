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

namespace BioData_Activity.AddingForm
{
    public partial class Add_Char_Reference_Form : Form
    {
        string ID;
        string connstring = Program.connstring;
        public Add_Char_Reference_Form(string Id, string Name)
        {
            InitializeComponent();
            ID = Id;
            if(Program.Updating == true)
            {
              load_data();
            }
        }

        void load_data()
        {
            using (MySqlConnection connection = new MySqlConnection(connstring))
            {
                connection.Open();
                string query = "SELECT n1, a1, p1, f1, n2, a2, p2, f2 FROM Char_Reference_tbl WHERE P_Id = @P_Id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@P_Id", ID);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        N1.Text = reader["n1"].ToString();
                        A1.Text = reader["a1"].ToString();
                        P1.Text = reader["p1"].ToString();
                        F1.Text = reader["f1"].ToString();
                        N2.Text = reader["n2"].ToString();
                        A2.Text = reader["a2"].ToString();
                        P2.Text = reader["p2"].ToString();
                        F2.Text = reader["f2"].ToString();
                    }
                }
            }
        }

        private void Save_btn_Click(object sender, EventArgs e)
        {
            if(Program.Updating == true)
            {
                Update_Function();
            }
            else
            {
                Adding_Function();
            }
        }


        void Update_Function()
        {
            if (N1.Text == "" && A1.Text == "" && P1.Text == "" && F1.Text == "" &&
                N2.Text == "" && A2.Text == "" && P2.Text == "" && F2.Text == "")
            {
                using (MySqlConnection connection = new MySqlConnection(connstring))
                {
                    connection.Open();
                    string updateQuery = "DELETE FROM Char_Reference_tbl WHERE P_Id = @P_Id";

                    MySqlCommand GS = new MySqlCommand(updateQuery, connection);
                    GS.Parameters.AddWithValue("@P_Id", ID);
                    GS.ExecuteNonQuery();
                    connection.Close();
                }
            }

            else if (ID != "" && N1.Text != "" && A1.Text != "" && P1.Text != "" && F1.Text != "")
            {
                using (MySqlConnection connection = new MySqlConnection(connstring))
                {
                    connection.Open();

                    string query = "UPDATE Char_Reference_tbl SET " +
                                   "n1 = @n1, a1 = @a1, p1 = @p1, f1 = @f1, " +
                                   "n2 = @n2, a2 = @a2, p2 = @p2, f2 = @f2 " +
                                   "WHERE P_Id = @P_Id";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@P_Id", ID);
                    command.Parameters.AddWithValue("@n1", N1.Text);
                    command.Parameters.AddWithValue("@a1", A1.Text);
                    command.Parameters.AddWithValue("@p1", P1.Text);
                    command.Parameters.AddWithValue("@f1", F1.Text);
                    command.Parameters.AddWithValue("@n2", N2.Text); 
                    command.Parameters.AddWithValue("@a2", A2.Text);
                    command.Parameters.AddWithValue("@p2", P2.Text);
                    command.Parameters.AddWithValue("@f2", F2.Text);

                    command.ExecuteNonQuery();
                }
            }
            else
            {
                MessageBox.Show("Please Fill-up Important Details");
                return;
            }
            MessageBox.Show("Successfully Updated");
            this.Close();
        }


        void Adding_Function()
        {
            if(ID != "" && N1.Text != "" && A1.Text != "" && P1.Text != "" && F1.Text != "")
            {
                using (MySqlConnection connection = new MySqlConnection(connstring))
                {
                    connection.Open();
                    string query = "INSERT INTO Char_Reference_tbl (P_Id, n1, a1, p1, f1, n2, a2, p2, f2) " +
                                   "VALUES (@P_Id, @n1, @a1, @p1, @f1, @n2, @a2, @p2, @f2)";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@P_Id", ID);
                    command.Parameters.AddWithValue("@n1", N1.Text);
                    command.Parameters.AddWithValue("@a1", A1.Text);
                    command.Parameters.AddWithValue("@p1", P1.Text);
                    command.Parameters.AddWithValue("@f1", F1.Text);
                    command.Parameters.AddWithValue("@n2", N2.Text);
                    command.Parameters.AddWithValue("@a2", A2.Text);
                    command.Parameters.AddWithValue("@p2", P2.Text);
                    command.Parameters.AddWithValue("@f2", F2.Text);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Successfully Added");
                    this.Close();

                }
            }
            else
            {
                MessageBox.Show("Please Fill-up Important Details");
            }
        }
    }
}
