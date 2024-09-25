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
    public partial class Other_Details : Form
    {
        string connstring = Program.connstring;
        string ID;
        public Other_Details(string Id, string Name)
        {
            InitializeComponent();
            ID = Id;
            LoadTable();
        }
        void LoadTable()
        {
            using (MySqlConnection connection = new MySqlConnection(connstring))
            {
                connection.Open();

                string query = "SELECT Res, SSS, NBI, Passport, IssuedAt, IssuedOn, TIN, Phil, Place, ExpiryDate, PrintedName " +
                               "FROM Other_Details_tbl " +
                               "WHERE P_Id = @P_id";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@P_id", ID);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Res.Text = reader["Res"].ToString();
                        SSS.Text = reader["SSS"].ToString();
                        NBI.Text = reader["NBI"].ToString();
                        Passport.Text = reader["Passport"].ToString();
                        IssuedAt.Text = reader["IssuedAt"].ToString();
                        IssuedOn.Text = reader["IssuedOn"].ToString();
                        TIN.Text = reader["TIN"].ToString();
                        Phil.Text = reader["Phil"].ToString();
                        Place.Text = reader["Place"].ToString();
                        ExpiryDate.Text = reader["ExpiryDate"].ToString();
                        PrintedName.Text = reader["PrintedName"].ToString();
                    }
                }
            }
        }
    }
}
