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

namespace BioData_Activity.OtherForm
{
    public partial class Char_Reference_Form : Form
    {
        string ID;
        string connstring = Program.connstring;
        public Char_Reference_Form(string Id, string Name)
        {
            InitializeComponent();
            ID = Id;
            load_data();
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
    }
}
