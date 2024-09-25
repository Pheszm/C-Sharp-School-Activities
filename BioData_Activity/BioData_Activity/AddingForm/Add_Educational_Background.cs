using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BioData_Activity.AddingForm
{
    public partial class Add_Educational_Background : Form
    {
        string connstring = Program.connstring;
        string ID;
        public Add_Educational_Background(string Id, string Name)
        {
            InitializeComponent();
            ID = Id;
            if (Program.Updating == true)
            {
                load_data();

            }
        }

        void load_data()
        {
            using (MySqlConnection connection = new MySqlConnection(connstring))
            {
                connection.Open();

                string query = "SELECT Elementary, E_Year, HighSchool, H_Year, College, C_Year, Course, Skill " +
                               "FROM Educational_Back_tbl " +
                               "WHERE P_Id = @P_id";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@P_id", ID);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Elebox.Text = reader["Elementary"].ToString();
                        EYears.Text = reader["E_Year"].ToString();
                        Hschoolbox.Text = reader["HighSchool"].ToString();
                        HYears.Text = reader["H_Year"].ToString();
                        ColBox.Text = reader["College"].ToString();
                        CYears.Text = reader["C_Year"].ToString();
                        CourseBox.Text = reader["Course"].ToString();
                        SkillBox.Text = reader["Skill"].ToString();
                    }
                }
            }
        }

        void Update_Function()
        {
            if (ID != "" &&
                SkillBox.Text != "")
            {
                using (MySqlConnection connection = new MySqlConnection(connstring))
                {
                    connection.Open();
                    string query = "UPDATE Educational_Back_tbl SET " +
                                   "Elementary = @Elementary, " +
                                   "E_Year = @E_Year, " +
                                   "HighSchool = @HighSchool, " +
                                   "H_Year = @H_Year, " +
                                   "College = @College, " +
                                   "C_Year = @C_Year, " +
                                   "Course = @Course, " +
                                   "Skill = @Skill " +
                                   "WHERE P_Id = @P_Id";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@P_Id", ID);
                    command.Parameters.AddWithValue("@Elementary", Elebox.Text);
                    command.Parameters.AddWithValue("@E_Year", EYears.Text);
                    command.Parameters.AddWithValue("@HighSchool", Hschoolbox.Text);
                    command.Parameters.AddWithValue("@H_Year", HYears.Text);
                    command.Parameters.AddWithValue("@College", ColBox.Text);
                    command.Parameters.AddWithValue("@C_Year", CYears.Text);
                    command.Parameters.AddWithValue("@Course", CourseBox.Text);
                    command.Parameters.AddWithValue("@Skill", SkillBox.Text);
                    command.ExecuteNonQuery();

                    MessageBox.Show("Successfully Updated");
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Please Fill-up all important details");
            }
        }




        private void Save_btn_Click(object sender, EventArgs e)
        {
            if(Program.Updating == true)
            {
                Update_Function();
            }
            else { 
                Adding_Function();
            }

        }

        void Adding_Function()
        {
            if (ID != "" &&
                SkillBox.Text != "")
            {
                using (MySqlConnection connection = new MySqlConnection(connstring))
                {
                    connection.Open();
                    string query = "INSERT INTO Educational_Back_tbl (P_Id, Elementary, E_Year, HighSchool, H_Year, College, C_Year, Course, Skill) " +
                                   "VALUES (@P_Id, @Elementary, @E_Year, @HighSchool, @H_Year, @College, @C_Year, @Course, @Skill)";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@P_Id", ID);
                    command.Parameters.AddWithValue("@Elementary", Elebox.Text);
                    command.Parameters.AddWithValue("@E_Year", EYears.Text);
                    command.Parameters.AddWithValue("@HighSchool", Hschoolbox.Text);
                    command.Parameters.AddWithValue("@H_Year", HYears.Text);
                    command.Parameters.AddWithValue("@College", ColBox.Text);
                    command.Parameters.AddWithValue("@C_Year", CYears.Text);
                    command.Parameters.AddWithValue("@Course", CourseBox.Text);
                    command.Parameters.AddWithValue("@Skill", SkillBox.Text);                   
                    command.ExecuteNonQuery();

                    MessageBox.Show("Successfully Added");
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Please Fill-up all important details");
            }
        }
    }
}
