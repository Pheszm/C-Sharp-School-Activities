using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BioData_Activity.OtherForm
{
    public partial class Personal_Info_Form : Form
    {
        string connstring = Program.connstring;
        string ID;
        public Personal_Info_Form(string Id)
        {
            InitializeComponent();
            ID = Id;
            LoadTheFile();
            LoadChild();
        }

        void LoadChild()
        {
            using (MySqlConnection connection = new MySqlConnection(connstring))
            {
                connection.Open();

                string query = "SELECT C_Id, Name, DateofBirth " +
                                "FROM Child_tbl " +
                              "WHERE P_id = @P_id";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@P_id", ID);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    { 
                        DataGridChilds.Rows.Add(reader["C_Id"].ToString(), reader["Name"].ToString(), reader["DateofBirth"].ToString());
                    }
                }
            }
        }

        void LoadTheFile()
        {
            using (MySqlConnection connection = new MySqlConnection(connstring))
            {
                connection.Open();

                string query = "SELECT ImgDirectory, Position_Desired, Date, Name, NickName, Present_Address, Permanent_Address, Place_of_Birth, P_Contact_No, Date_of_Birth, Email_Address, Gender, Age, Civil_Status, Religion, Citizenship, Language_Spoken, Height, Weight, Name_of_Spouse, POccupation, ChildrensName_DateOfBirth, FathersName, FOccupation, MothersName, MOccupation, PContactEmergency, PAddress, Relationship, PContactNo " +
                               "FROM Personal_Info " +
                               "WHERE P_id = @P_id";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@P_id", ID);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        pictureBox1.Image = new Bitmap(reader["ImgDirectory"].ToString());
                        Position_Desired.Text = reader["Position_Desired"].ToString();
                        FDate.Text = reader["Date"].ToString();
                        FName.Text = reader["Name"].ToString();
                        NickName.Text = reader["NickName"].ToString();
                        Present_Address.Text = reader["Present_Address"].ToString();
                        Permanent_Address.Text = reader["Permanent_Address"].ToString();
                        Place_of_Birth.Text = reader["Place_of_Birth"].ToString();
                        P_Contact_No.Text = reader["P_Contact_No"].ToString();
                        Date_of_Birth.Text = reader["Date_of_Birth"].ToString();
                        Email_Address.Text = reader["Email_Address"].ToString();
                        Gender.Text = reader["Gender"].ToString();
                        Age.Text = reader["Age"].ToString();
                        Civil_Status.Text = reader["Civil_Status"].ToString();
                        Religion.Text = reader["Religion"].ToString();
                        Citizenship.Text = reader["Citizenship"].ToString();
                        Language_Spoken.Text = reader["Language_Spoken"].ToString();
                        Height.Text = reader["Height"].ToString();
                        Weight.Text = reader["Weight"].ToString();
                        Name_of_Spouse.Text = reader["Name_of_Spouse"].ToString();
                        POccupation.Text = reader["POccupation"].ToString();
                        FathersName.Text = reader["FathersName"].ToString();
                        FOccupation.Text = reader["FOccupation"].ToString();
                        MothersName.Text = reader["MothersName"].ToString();
                        MOccupation.Text = reader["MOccupation"].ToString();
                        PContactEmergency.Text = reader["PContactEmergency"].ToString();
                        PAddress.Text = reader["PAddress"].ToString();
                        Relationship.Text = reader["Relationship"].ToString();
                        PContactNo.Text = reader["PContactNo"].ToString();
                    }
                }

            }
        }



    }

}
