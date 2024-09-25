using BioData_Activity.AddingForm;
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
    public partial class Ed_Background_Form : Form
    {
        string connstring = Program.connstring;
        string ID;
        public Ed_Background_Form(string Id, string Name)
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
    }
}
