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
    public partial class View_Form : Form
    {
        string connstring = Program.connstring;
        string ID, Nameee;

        public View_Form(string Id)
        {
            InitializeComponent();
            ID = Id;
            Checker();

        }

        void Checker()
        {
            Personal_Info_Check();
            EdBackground_Check();
            Employment_Check();
            Char_Reference_Check();
            Other_Details_Check();
        }

        void Other_Details_Check()
        {
            using (MySqlConnection connection = new MySqlConnection(connstring))
            {
                connection.Open();
                string query = "SELECT * FROM Other_Details_tbl WHERE P_Id = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@id", ID);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (ID == reader.GetString("P_Id").ToString())
                            {
                                OtherDet.BackColor = Color.FromArgb(40, 40, 120);
                            }
                        }
                        else
                        {
                            OtherDet.BackColor = Color.FromArgb(20, 20, 60);
                        }
                    }
                }
            }
        }

        void Char_Reference_Check()
        {
            using (MySqlConnection connection = new MySqlConnection(connstring))
            {
                connection.Open();
                string query = "SELECT * FROM Char_Reference_tbl WHERE P_Id = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@id", ID);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (ID == reader.GetString("P_Id").ToString())
                            {
                                CharBtn.BackColor = Color.FromArgb(40, 40, 120);
                            }
                        }
                        else
                        {
                            CharBtn.BackColor = Color.FromArgb(20, 20, 60);
                        }
                    }
                }
            }
        }
        void EdBackground_Check()
        {
            using (MySqlConnection connection = new MySqlConnection(connstring))
            {
                connection.Open();
                string query = "SELECT * FROM Educational_Back_tbl WHERE P_Id = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@id", ID);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (ID == reader.GetString("P_Id").ToString())
                            {
                                EdBack_btn.BackColor = Color.FromArgb(40, 40, 120);
                            }
                        }
                        else
                        {
                            EdBack_btn.BackColor = Color.FromArgb(20, 20, 60);
                        }
                    }
                }
            }
        }

        void Employment_Check()
        {
            using (MySqlConnection connection = new MySqlConnection(connstring))
            {
                connection.Open();
                string query = "SELECT * FROM Employment_Rec_tbl WHERE P_Id = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@id", ID);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (ID == reader.GetString("P_Id").ToString())
                            {
                                EmployBtn.BackColor = Color.FromArgb(40, 40, 120);
                            }
                        }
                        else
                        {
                            EmployBtn.BackColor = Color.FromArgb(20, 20, 60);
                        }
                    }
                }
            }
        }

        void Personal_Info_Check()
        {
            using (MySqlConnection connection = new MySqlConnection(connstring))
            {
                connection.Open();
                string query = "SELECT Name FROM Personal_Info WHERE P_Id = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@id", ID);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string name = reader.GetString("Name");

                            Nametxt.Text = name + "'s Info:";
                            Nameee = name;
                            PIbtn.Enabled = true;
                            PIbtn.BackColor = Color.FromArgb(40, 40, 120);
                        }
                        else
                        {
                            PIbtn.BackColor = Color.FromArgb(20, 20, 60); 
                        }
                    }
                }
            }

        }

        private void EdBack_btn_Click(object sender, EventArgs e)
        {
            if (EdBack_btn.BackColor == Color.FromArgb(40, 40, 120))
            {
                OtherForm.Ed_Background_Form form = new OtherForm.Ed_Background_Form(ID, Nameee);
                this.Hide();
                form.ShowDialog();
                EdBackground_Check();
                this.Show();
            }
            else
            {
                AddingForm.Add_Educational_Background panel = new AddingForm.Add_Educational_Background(ID, Nameee);
                this.Hide();
                panel.ShowDialog();
                EdBackground_Check();
                this.Show();
            }
            update_off();
            Checker();
        }

        private void EmployBtn_Click(object sender, EventArgs e)
        {
            if (EmployBtn.BackColor == Color.FromArgb(40, 40, 120))
            {
                OtherForm.Employment_Rec_Form form = new OtherForm.Employment_Rec_Form(ID, Nameee);
                this.Hide();
                form.ShowDialog();
                Employment_Check();
                this.Show();
            }
            else
            {
                AddingForm.Add_Employment_Rec_Form panel = new AddingForm.Add_Employment_Rec_Form(ID, Nameee);
                this.Hide();
                panel.ShowDialog();
                Employment_Check();
                this.Show();
            }
            update_off();
            Checker();
        }

        private void CharBtn_Click(object sender, EventArgs e)
        {
            if (CharBtn.BackColor == Color.FromArgb(40, 40, 120))
            {
                OtherForm.Char_Reference_Form form = new OtherForm.Char_Reference_Form(ID, Nameee);
                this.Hide();
                form.ShowDialog();
                Employment_Check();
                this.Show();
            }
            else
            {
                AddingForm.Add_Char_Reference_Form panel = new AddingForm.Add_Char_Reference_Form(ID, Nameee);
                this.Hide();
                panel.ShowDialog();
                Employment_Check();
                this.Show();
            }
            update_off();
            Checker();
        }

        void update_off()
        {
            Program.Updating = false;
            update_btn.BackColor = Color.FromArgb(40, 40, 120);
            update_btn.ForeColor = Color.FromArgb(255, 255, 255);
            update_btn.Text = "Update";
            Toplabel.Text = "Viewing";
            PIbtn.Enabled = true;
            OtherDet.Enabled = true;
            CharBtn.Enabled = true;
            EmployBtn.Enabled = true;
            EdBack_btn.Enabled = true;
            PIbtn.ForeColor = Color.FromArgb(255, 255, 255);
            OtherDet.ForeColor = Color.FromArgb(255, 255, 255);
            CharBtn.ForeColor = Color.FromArgb(255, 255, 255);
            EmployBtn.ForeColor = Color.FromArgb(255, 255, 255);
            EdBack_btn.ForeColor = Color.FromArgb(255, 255, 255);
            Checker();
        }

        private void update_btn_Click(object sender, EventArgs e)
        {
            if (update_btn.Text == "Update")
            {
                Program.Updating = true;
                update_btn.BackColor = Color.FromArgb(192, 0, 0);
                update_btn.ForeColor = Color.FromArgb(255, 255, 255);
                update_btn.Text = "Cancel";
                Toplabel.Text = "Updating";

                if (PIbtn.BackColor == Color.FromArgb(40, 40, 120))
                {
                    PIbtn.BackColor = Color.FromArgb(255, 255, 255);
                    PIbtn.ForeColor = Color.FromArgb(20, 20, 60);

                }
                else
                {
                    PIbtn.Enabled = false;
                }

                if (EdBack_btn.BackColor == Color.FromArgb(40, 40, 120))
                {
                    EdBack_btn.BackColor = Color.FromArgb(255, 255, 255);
                    EdBack_btn.ForeColor = Color.FromArgb(20, 20, 60);
                }
                else
                {
                    EdBack_btn.Enabled = false;
                }

                if (EmployBtn.BackColor == Color.FromArgb(40, 40, 120))
                {
                    EmployBtn.BackColor = Color.FromArgb(255, 255, 255);
                    EmployBtn.ForeColor = Color.FromArgb(20, 20, 60);
                }
                else
                {
                    EmployBtn.Enabled = false;
                }

                if (CharBtn.BackColor == Color.FromArgb(40, 40, 120))
                {
                    CharBtn.BackColor = Color.FromArgb(255, 255, 255);
                    CharBtn.ForeColor = Color.FromArgb(20, 20, 60);
                }
                else
                {
                    CharBtn.Enabled = false;
                }

                if (OtherDet.BackColor == Color.FromArgb(40, 40, 120))
                {
                    OtherDet.BackColor = Color.FromArgb(255, 255, 255);
                    OtherDet.ForeColor = Color.FromArgb(20, 20, 60);
                }
                else
                {
                    OtherDet.Enabled = false;
                }
            }
            else
            {
                update_off();
            }

        }

        private void OtherDet_Click(object sender, EventArgs e)
        {
            if (OtherDet.BackColor == Color.FromArgb(40, 40, 120))
            {
                OtherForm.Other_Details form = new OtherForm.Other_Details(ID, Nameee);
                this.Hide();
                form.ShowDialog();
                Employment_Check();
                this.Show();
            }
            else
            {
                AddingForm.Add_Other_Details panel = new AddingForm.Add_Other_Details(ID, Nameee);
                this.Hide();
                panel.ShowDialog();
                Employment_Check();
                this.Show();
            }
            update_off();
            Checker();
        }

        private void ReturnBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Output_btn_Click(object sender, EventArgs e)
        {
            Output_Form form = new Output_Form(ID);
            form.ShowDialog();
        }

        private void PIbtn_Click(object sender, EventArgs e)
        {
            if (PIbtn.BackColor == Color.FromArgb(40, 40, 120))
            {
                OtherForm.Personal_Info_Form form = new OtherForm.Personal_Info_Form(ID);
                this.Hide();
                form.ShowDialog();
                this.Show();
                update_off();
                Checker();
            }
            else
            {
                AddingForm.Add_Personal_Info form = new AddingForm.Add_Personal_Info(ID);
                this.Hide();
                form.ShowDialog();
                this.Show();
                update_off();
                Checker();
            }
        }
    }
}
