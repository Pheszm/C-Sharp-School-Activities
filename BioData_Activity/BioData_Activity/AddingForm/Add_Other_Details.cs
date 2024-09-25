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
    public partial class Add_Other_Details : Form
    {
        string connstring = Program.connstring;
        string ID;

        public Add_Other_Details(string Id, string Name)
        {
            InitializeComponent();
            ID = Id;
            if (Program.Updating == true)
            {
                LoadTable();
            }
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
        void Adding_Function()
        {
            if (ID != "" &&
                Res.Text != "" &&

                IssuedAt.Text != "" &&
                IssuedOn.Text != "" &&
              
                Place.Text != "" &&
                ExpiryDate.Text != "" &&
                PrintedName.Text != "")
            {
                using (MySqlConnection connection = new MySqlConnection(connstring))
                {
                    connection.Open();
                    string query = "INSERT INTO Other_Details_tbl (P_Id, Res, SSS, NBI, Passport, IssuedAt, IssuedOn, TIN, Phil, Place, ExpiryDate, PrintedName) " +
                                   "VALUES (@P_Id, @Res, @SSS, @NBI, @Passport, @IssuedAt, @IssuedOn, @TIN, @Phil, @Place, @ExpiryDate, @PrintedName)";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@P_Id", ID);
                    command.Parameters.AddWithValue("@Res", Res.Text);
                    command.Parameters.AddWithValue("@SSS", SSS.Text);
                    command.Parameters.AddWithValue("@NBI", NBI.Text);
                    command.Parameters.AddWithValue("@Passport", Passport.Text);
                    command.Parameters.AddWithValue("@IssuedAt", IssuedAt.Text);
                    command.Parameters.AddWithValue("@IssuedOn", IssuedOn.Text);
                    command.Parameters.AddWithValue("@TIN", TIN.Text);
                    command.Parameters.AddWithValue("@Phil", Phil.Text);
                    command.Parameters.AddWithValue("@Place", Place.Text);
                    command.Parameters.AddWithValue("@ExpiryDate", ExpiryDate.Text);
                    command.Parameters.AddWithValue("@PrintedName", PrintedName.Text);
                    command.ExecuteNonQuery();

                    MessageBox.Show("Successfully Added");
                    this.Close();
                }
            }
            else
            {
                HighlightTheMissing();
                MessageBox.Show("Please Fill-up all important details");
            }
        }

        void Update_Function()
        {
            if (ID != "" &&
                Res.Text != "" &&
                SSS.Text != "" &&
                NBI.Text != "" &&
                Passport.Text != "" &&
                IssuedAt.Text != "" &&
                IssuedOn.Text != "" &&
                TIN.Text != "" &&
                Phil.Text != "" &&
                Place.Text != "" &&
                ExpiryDate.Text != "" &&
                PrintedName.Text != "")
            {
                using (MySqlConnection connection = new MySqlConnection(connstring))
                {
                    connection.Open();
                    string query = "UPDATE Other_Details_tbl SET " +
                                   "Res = @Res, " +
                                   "SSS = @SSS, " +
                                   "NBI = @NBI, " +
                                   "Passport = @Passport, " +
                                   "IssuedAt = @IssuedAt, " +
                                   "IssuedOn = @IssuedOn, " +
                                   "TIN = @TIN, " +
                                   "Phil = @Phil, " +
                                   "Place = @Place, " +
                                   "ExpiryDate = @ExpiryDate, " +
                                   "PrintedName = @PrintedName " +
                                   "WHERE P_Id = @P_Id";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@P_Id", ID);
                    command.Parameters.AddWithValue("@Res", Res.Text);
                    command.Parameters.AddWithValue("@SSS", SSS.Text);
                    command.Parameters.AddWithValue("@NBI", NBI.Text);
                    command.Parameters.AddWithValue("@Passport", Passport.Text);
                    command.Parameters.AddWithValue("@IssuedAt", IssuedAt.Text);
                    command.Parameters.AddWithValue("@IssuedOn", IssuedOn.Text);
                    command.Parameters.AddWithValue("@TIN", TIN.Text);
                    command.Parameters.AddWithValue("@Phil", Phil.Text);
                    command.Parameters.AddWithValue("@Place", Place.Text);
                    command.Parameters.AddWithValue("@ExpiryDate", ExpiryDate.Text);
                    command.Parameters.AddWithValue("@PrintedName", PrintedName.Text);
                    command.ExecuteNonQuery();

                    MessageBox.Show("Successfully Updated");
                    this.Close();
                }
            }
            else
            {
                HighlightTheMissing();
                MessageBox.Show("Please Fill-up all important details");
            }
        }


        private void Save_btn_Click(object sender, EventArgs e)
        {
            if (Program.Updating == true)
            {
                Update_Function();
            }
            else
            {
                Adding_Function();
            }

        }



        private void DecimalOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (char.IsControl(e.KeyChar))
            {
                return;
            }
            if (char.IsDigit(e.KeyChar))
            {
                return;
            }
            if (e.KeyChar == '.')
            {
                if (textBox.Text.Contains("."))
                {
                    e.Handled = true;
                }
                return;
            }
            e.Handled = true;
        }


        private void HighlightTheMissing()
        {
            TextBox[] textBox = { Res, Place, PrintedName };
            foreach(TextBox text in textBox)
            {
                if (string.IsNullOrEmpty(text.Text.Trim()))
                {
                    text.BackColor = Color.FromArgb(255, 192, 192);
                }
                else
                {
                    text.BackColor = Color.FromArgb(192, 192, 255);
                }
            }
        }
    }
}
