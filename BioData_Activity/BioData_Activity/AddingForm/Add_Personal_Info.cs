using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using MySql.Data.MySqlClient;
using System.Diagnostics.Eventing.Reader;
using Org.BouncyCastle.Asn1.Ocsp;
using MySqlX.XDevAPI.Common;

namespace BioData_Activity.AddingForm
{
    public partial class Add_Personal_Info : Form
    {
        string connstring = Program.connstring;
        string fileDirectory = "";
        string ID;
        public Add_Personal_Info(string Id)
        {
            InitializeComponent();
            ID = Id;
            if (Program.Updating == true)
            {
                LoadTheFile();
                LoadChild();
            }

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

                string query = "SELECT ImgDirectory, Position_Desired, Date, Name, NickName, Present_Address, Permanent_Address, Place_of_Birth, P_Contact_No, Date_of_Birth, Email_Address, Gender, Age, Civil_Status, Religion, Citizenship, Language_Spoken, Height, Weight, Name_of_Spouse, POccupation, FathersName, FOccupation, MothersName, MOccupation, PContactEmergency, PAddress, Relationship, PContactNo " +
                               "FROM Personal_Info " +
                               "WHERE P_id = @P_id";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@P_id", ID);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        pictureBox1.Image = new Bitmap(reader["ImgDirectory"].ToString());
                        fileDirectory = reader["ImgDirectory"].ToString();
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
                        DataGridChilds.Text = reader["ChildrensName_DateOfBirth"].ToString();
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

        private void UploadImg_btn_Click(object sender, EventArgs e)
        {
            OpenFileDialog opnfd = new OpenFileDialog();
            opnfd.Filter = "Image Files (*.jpg;*.jpeg;.*.gif;)|*.jpg;*.jpeg;.*.gif";
            if (opnfd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(opnfd.FileName);
                fileDirectory = opnfd.FileName;
            }

        }

        void Update_Function()
        {
            if (fileDirectory != "" &&
                Position_Desired.Text != "" &&
                FDate.Text != "" &&
                FName.Text != "" &&
                NickName.Text != "" &&
                Present_Address.Text != "" &&
                Permanent_Address.Text != "" &&
                Place_of_Birth.Text != "" &&
                P_Contact_No.Text != "" &&
                Date_of_Birth.Text != "" &&
                Email_Address.Text != "" &&
                Gender.Text != "" &&
                Age.Text != "" &&
                Civil_Status.Text != "" &&
                Religion.Text != "" &&
                Citizenship.Text != "" &&
                Language_Spoken.Text != "" &&
                Height.Text != "" &&
                Weight.Text != "" &&
                PContactEmergency.Text != "" &&
                PAddress.Text != "" &&
                Relationship.Text != "" &&
                PContactNo.Text != "")
            {
                using (MySqlConnection connection = new MySqlConnection(connstring))
                {
                    connection.Open();
                    string query = "UPDATE Personal_Info SET " +
                                   "ImgDirectory = @ImgDirectory, " +
                                   "Position_Desired = @Position_Desired, " +
                                   "Date = @Date, " +
                                   "Name = @Name, " +
                                   "NickName = @NickName, " +
                                   "Present_Address = @Present_Address, " +
                                   "Permanent_Address = @Permanent_Address, " +
                                   "Place_of_Birth = @Place_of_Birth, " +
                                   "P_Contact_No = @P_Contact_No, " +
                                   "Date_of_Birth = @Date_of_Birth, " +
                                   "Email_Address = @Email_Address, " +
                                   "Gender = @Gender, " +
                                   "Age = @Age, " +
                                   "Civil_Status = @Civil_Status, " +
                                   "Religion = @Religion, " +
                                   "Citizenship = @Citizenship, " +
                                   "Language_Spoken = @Language_Spoken, " +
                                   "Height = @Height, " +
                                   "Weight = @Weight, " +
                                   "Name_of_Spouse = @Name_of_Spouse, " +
                                   "POccupation = @POccupation, " +
                                   "ChildrensName_DateOfBirth = @ChildrensName_DateOfBirth, " +
                                   "FathersName = @FathersName, " +
                                   "FOccupation = @FOccupation, " +
                                   "MothersName = @MothersName, " +
                                   "MOccupation = @MOccupation, " +
                                   "PContactEmergency = @PContactEmergency, " +
                                   "PAddress = @PAddress, " +
                                   "Relationship = @Relationship, " +
                                   "PContactNo = @PContactNo " +
                                   "WHERE P_Id = @P_Id";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ImgDirectory", fileDirectory);
                    command.Parameters.AddWithValue("@Position_Desired", Position_Desired.Text);
                    command.Parameters.AddWithValue("@Date", FDate.Text);
                    command.Parameters.AddWithValue("@Name", FName.Text);
                    command.Parameters.AddWithValue("@NickName", NickName.Text);
                    command.Parameters.AddWithValue("@Present_Address", Present_Address.Text);
                    command.Parameters.AddWithValue("@Permanent_Address", Permanent_Address.Text);
                    command.Parameters.AddWithValue("@Place_of_Birth", Place_of_Birth.Text);
                    command.Parameters.AddWithValue("@P_Contact_No", P_Contact_No.Text);
                    command.Parameters.AddWithValue("@Date_of_Birth", Date_of_Birth.Text);
                    command.Parameters.AddWithValue("@Email_Address", Email_Address.Text);
                    command.Parameters.AddWithValue("@Gender", Gender.Text);
                    command.Parameters.AddWithValue("@Age", Age.Text);
                    command.Parameters.AddWithValue("@Civil_Status", Civil_Status.Text);
                    command.Parameters.AddWithValue("@Religion", Religion.Text);
                    command.Parameters.AddWithValue("@Citizenship", Citizenship.Text);
                    command.Parameters.AddWithValue("@Language_Spoken", Language_Spoken.Text);
                    command.Parameters.AddWithValue("@Height", Height.Text);
                    command.Parameters.AddWithValue("@Weight", Weight.Text);
                    command.Parameters.AddWithValue("@Name_of_Spouse", Name_of_Spouse.Text);
                    command.Parameters.AddWithValue("@POccupation", POccupation.Text);
                    command.Parameters.AddWithValue("@ChildrensName_DateOfBirth", DataGridChilds.Text);
                    command.Parameters.AddWithValue("@FathersName", FathersName.Text);
                    command.Parameters.AddWithValue("@FOccupation", FOccupation.Text);
                    command.Parameters.AddWithValue("@MothersName", MothersName.Text);
                    command.Parameters.AddWithValue("@MOccupation", MOccupation.Text);
                    command.Parameters.AddWithValue("@PContactEmergency", PContactEmergency.Text);
                    command.Parameters.AddWithValue("@PAddress", PAddress.Text);
                    command.Parameters.AddWithValue("@Relationship", Relationship.Text);
                    command.Parameters.AddWithValue("@PContactNo", PContactNo.Text);
                    command.Parameters.AddWithValue("@P_Id", ID);
                    command.ExecuteNonQuery();

                    MessageBox.Show("Successfully Updated");
                    this.Close();
                }
            }
            else
            {
                textboxChecker();
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
                AddChild_Function();

            }
        }

        string currentID;

        void getCurrentID()
        {
            using (MySqlConnection connection = new MySqlConnection(connstring))
            {
                connection.Open();

                string query = "SELECT P_id " +
                               "FROM Personal_Info " +
                               "WHERE Name = @Name";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", FName.Text);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        currentID = reader["P_id"].ToString();
                    }
                }
            }
        }

        void AddChild_Function()
        {
            getCurrentID();
            if (DataGridChilds.Rows.Count > 0)
            {
                using (MySqlConnection connection = new MySqlConnection(connstring))
                {
                    connection.Open();

                    foreach (DataGridViewRow row in DataGridChilds.Rows)
                    {
                        string cId = row.Cells["C_ID"].Value.ToString();
                        string name = row.Cells["Namez"].Value.ToString();
                        string dob = row.Cells["DofB"].Value.ToString();

                        string query = "INSERT INTO child_tbl (C_Id, P_Id, Name, DateofBirth) " +
                                       "VALUES (@C_Id, @P_Id, @Name, @DateofBirth)";


                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@C_Id", cId);
                        command.Parameters.AddWithValue("@P_Id", currentID);
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@DateofBirth", dob);

                        command.ExecuteNonQuery();
                    }
                }
            }
        }


        void Adding_Function()
        {
            if(allowed == true)
            {
                Age.BackColor = Color.FromArgb(255, 192, 192);
                MessageBox.Show("Must be in a Valid Age.");
                return;
            }

            if (goodsNa == false)
            {
                Email_Address.BackColor = Color.FromArgb(255, 192, 192);
                MessageBox.Show("Must be a Valid Email Address.");
                return;
            }
            if (P_Contact_No.Text.Length <= 12)
            {
                P_Contact_No.BackColor = Color.FromArgb(255, 192, 192);
                MessageBox.Show("Invalid Contact Number.");
                return;
            }
            if (PContactNo.Text.Length <= 12)
            {
                PContactNo.BackColor = Color.FromArgb(255, 192, 192);
                MessageBox.Show("Invalid Contact Number.");
                return;
            }

            if (int.Parse(Age.Text) <= 15)
            {
                Age.BackColor = Color.FromArgb(255, 192, 192);
                MessageBox.Show("Must be in a Legal Age.");
                return;
            }

            if (fileDirectory != "" &&
                Position_Desired.Text != "" &&
                FDate.Text != "" &&
                FName.Text != "" &&
                NickName.Text != "" &&
                Present_Address.Text != "" &&
                Permanent_Address.Text != "" &&
                Place_of_Birth.Text != "" &&
                P_Contact_No.Text != "" &&
                Date_of_Birth.Text != "" &&
                Email_Address.Text != "" &&
                Gender.Text != "" &&
                Age.Text != "" &&
                Civil_Status.Text != "" &&
                Religion.Text != "" &&
                Citizenship.Text != "" &&
                Language_Spoken.Text != "" &&
                Height.Text != "" &&
                Weight.Text != "" &&
                PContactEmergency.Text != "" &&
                PAddress.Text != "" &&
                Relationship.Text != "" &&
                PContactNo.Text != "")
            {
                using (MySqlConnection connection = new MySqlConnection(connstring))
                {
                    connection.Open();
                    string query = "INSERT INTO Personal_Info (ImgDirectory, Position_Desired, Date, Name, NickName, Present_Address, Permanent_Address, Place_of_Birth, P_Contact_No, Date_of_Birth, Email_Address, Gender, Age, Civil_Status, Religion, Citizenship, Language_Spoken, Height, Weight, Name_of_Spouse, POccupation , FathersName, FOccupation, MothersName, MOccupation, PContactEmergency, PAddress, Relationship, PContactNo) " +
                                   "VALUES (@ImgDirectory, @Position_Desired, @Date, @Name, @NickName, @Present_Address, @Permanent_Address, @Place_of_Birth, @P_Contact_No, @Date_of_Birth, @Email_Address, @Gender, @Age, @Civil_Status, @Religion, @Citizenship, @Language_Spoken, @Height, @Weight, @Name_of_Spouse, @POccupation, @FathersName, @FOccupation, @MothersName, @MOccupation, @PContactEmergency, @PAddress, @Relationship, @PContactNo)";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ImgDirectory", fileDirectory);
                    command.Parameters.AddWithValue("@Position_Desired", Position_Desired.Text);
                    command.Parameters.AddWithValue("@Date", FDate.Text);
                    command.Parameters.AddWithValue("@Name", FName.Text);
                    command.Parameters.AddWithValue("@NickName", NickName.Text);
                    command.Parameters.AddWithValue("@Present_Address", Present_Address.Text);
                    command.Parameters.AddWithValue("@Permanent_Address", Permanent_Address.Text);
                    command.Parameters.AddWithValue("@Place_of_Birth", Place_of_Birth.Text);
                    command.Parameters.AddWithValue("@P_Contact_No", P_Contact_No.Text);
                    command.Parameters.AddWithValue("@Date_of_Birth", Date_of_Birth.Text);
                    command.Parameters.AddWithValue("@Email_Address", Email_Address.Text);
                    command.Parameters.AddWithValue("@Gender", Gender.Text);
                    command.Parameters.AddWithValue("@Age", Age.Text);
                    command.Parameters.AddWithValue("@Civil_Status", Civil_Status.Text);
                    command.Parameters.AddWithValue("@Religion", Religion.Text);
                    command.Parameters.AddWithValue("@Citizenship", Citizenship.Text);
                    command.Parameters.AddWithValue("@Language_Spoken", Language_Spoken.Text);
                    command.Parameters.AddWithValue("@Height", Height.Text);
                    command.Parameters.AddWithValue("@Weight", Weight.Text);
                    command.Parameters.AddWithValue("@Name_of_Spouse", Name_of_Spouse.Text);
                    command.Parameters.AddWithValue("@POccupation", POccupation.Text);
                    command.Parameters.AddWithValue("@FathersName", FathersName.Text);
                    command.Parameters.AddWithValue("@FOccupation", FOccupation.Text);
                    command.Parameters.AddWithValue("@MothersName", MothersName.Text);
                    command.Parameters.AddWithValue("@MOccupation", MOccupation.Text);
                    command.Parameters.AddWithValue("@PContactEmergency", PContactEmergency.Text);
                    command.Parameters.AddWithValue("@PAddress", PAddress.Text);
                    command.Parameters.AddWithValue("@Relationship", Relationship.Text);
                    command.Parameters.AddWithValue("@PContactNo", PContactNo.Text);
                    command.ExecuteNonQuery();

                    MessageBox.Show("Successfully Added");
                    this.Close();
                }
            }
            else
            {
                textboxChecker();
                MessageBox.Show("Please Fill-up all important details");
            }
        }

        private void Keypress(object sender, KeyPressEventArgs e)
        {
            int MaxLength = 13;

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '+'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '+') && (sender as TextBox).Text.IndexOf('+') > 0)
            {
                e.Handled = true;
            }
            if ((sender as TextBox).Text.Length >= MaxLength && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        bool allowed = false;
        private void Date_of_Birth_ValueChanged(object sender, EventArgs e)
        {
            DateTime birthDate = Date_of_Birth.Value;
            DateTime currentDate = DateTime.Today;

            int age = currentDate.Year - birthDate.Year;

            if (birthDate.Date > currentDate.AddYears(-age))
            {
                age--;
            }

            Age.Text = age.ToString();
            
            if (age > 15)
            {
                Age.BackColor = Color.FromArgb(192, 192, 255);
                allowed = false;
            }
            else
            {
                
                allowed = true;
                Age.BackColor = Color.FromArgb(255, 192, 192);
            }
        }

        private void Add_Child_Btn_Click(object sender, EventArgs e)
        {
            if (ID == "")
            {
                int maxID = 0;
                foreach (DataGridViewRow row in DataGridChilds.Rows)
                {
                    int id;
                    if (row.Cells[0].Value != null && int.TryParse(row.Cells[0].Value.ToString(), out id))
                    {
                        if (id > maxID)
                        {
                            maxID = id;
                        }
                    }
                }

                int C_ID = maxID + 1;
                if (!string.IsNullOrEmpty(CDofB.Text) && !string.IsNullOrEmpty(Cname.Text))
                {
                    DataGridChilds.Rows.Add(C_ID, Cname.Text, CDofB.Text);
                    CDofB.Text = "";
                    Cname.Text = "";
                }
            }
            else
            {
                int maxID = 0;
                foreach (DataGridViewRow row in DataGridChilds.Rows)
                {
                    int id;
                    if (row.Cells[0].Value != null && int.TryParse(row.Cells[0].Value.ToString(), out id))
                    {
                        if (id > maxID)
                        {
                            maxID = id;
                        }
                    }
                }

                int C_ID = maxID + 1;
                if (!string.IsNullOrEmpty(CDofB.Text) && !string.IsNullOrEmpty(Cname.Text))
                {
                    DataGridChilds.Rows.Add(C_ID, Cname.Text, CDofB.Text);
                }

                getCurrentID();

                using (MySqlConnection connection = new MySqlConnection(connstring))
                {
                    connection.Open();

                    string query = "INSERT INTO child_tbl (C_Id, P_Id, Name, DateofBirth) " +
                                   "VALUES (@C_Id, @P_Id, @Name, @DateofBirth)";


                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@C_Id", C_ID);
                    command.Parameters.AddWithValue("@P_Id", ID);
                    command.Parameters.AddWithValue("@Name", Cname.Text);
                    command.Parameters.AddWithValue("@DateofBirth", CDofB.Text);

                    command.ExecuteNonQuery();
                }
            }
            CDofB.Text = "";
            Cname.Text = "";
        }





        void textboxChecker()
        {
            TextBox[] textBoxes = {
            Position_Desired,
            FName,
            NickName,
            Present_Address,
            Permanent_Address,
            Place_of_Birth,
            P_Contact_No,
            Email_Address,
            Age,
            Religion,
            Citizenship,
            Language_Spoken,
            Height,
            Weight,
            PContactEmergency,
            PAddress,
            Relationship,
            PContactNo
        };

            ComboBox[] ComboBoxes = {
            Gender, Civil_Status,
        };

            foreach (TextBox textBox in textBoxes)
            {
                if (string.IsNullOrEmpty(textBox.Text.Trim()))
                {
                    textBox.BackColor = Color.FromArgb(255, 192, 192);
                }
                else
                {
                    textBox.BackColor = Color.FromArgb(192, 192, 255);
                }
            }

            foreach (ComboBox ComBox in ComboBoxes)
            {
                if (string.IsNullOrEmpty(ComBox.Text.Trim()))
                {
                    ComBox.BackColor = Color.FromArgb(255, 192, 192);
                }
                else
                {
                    ComBox.BackColor = Color.FromArgb(192, 192, 255);
                }
            }

            if (fileDirectory == "")
            {
                pictureBox1.BackColor = Color.FromArgb(255, 192, 192);
                UploadImg_btn.BackColor = Color.FromArgb(255, 192, 192);
            }
            else
            {
                pictureBox1.BackColor = Color.FromArgb(255, 192, 192);
                UploadImg_btn.BackColor = Color.FromArgb(128, 128, 255);
            }
        }


        string Ccc_Id;
        private void CTableCellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = DataGridChilds.Rows[e.RowIndex];

                Ccc_Id = selectedRow.Cells["C_ID"].Value.ToString();
                Cname.Text = selectedRow.Cells["Namez"].Value.ToString();
                CDofB.Text = selectedRow.Cells["DofB"].Value.ToString();
                DataGridChilds.DefaultCellStyle.SelectionBackColor = Color.FromArgb(40, 40, 120);
                DataGridChilds.DefaultCellStyle.SelectionForeColor = Color.White;
                Update_C_Btn.Visible = true;
                RemoveC_btn.Visible = true;
                ClearBtn.Visible = true;
                Add_Child_Btn.Visible = false;
                if (Ccc_Id == "") cleardata();
            }
        }

        void cleardata()
        {
            Ccc_Id = "";
            Cname.Text = "";
            CDofB.Text = "";
            DataGridChilds.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 255, 255);
            DataGridChilds.DefaultCellStyle.SelectionForeColor = Color.Black;
            Update_C_Btn.Visible = false;
            RemoveC_btn.Visible = false;
            ClearBtn.Visible = false;
            Add_Child_Btn.Visible = true;
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            cleardata();
        }

        private void RemoveC_btn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to remove this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes && ID != "")
            {
                using (MySqlConnection connection = new MySqlConnection(connstring))
                {
                    connection.Open();
                    string query = "DELETE FROM Child_tbl WHERE C_Id = @C_Id AND P_Id = @P_Id";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@C_Id", Ccc_Id);
                    command.Parameters.AddWithValue("@P_Id", ID);
                    command.ExecuteNonQuery();

                    foreach (DataGridViewRow row in DataGridChilds.Rows)
                    {
                        if (Ccc_Id == row.Cells["C_ID"].Value.ToString())
                        {
                            DataGridChilds.Rows.Remove(row);
                        }

                    }
                }
            }
            else
            {
                foreach (DataGridViewRow row in DataGridChilds.Rows)
                {
                    if (Ccc_Id == row.Cells["C_ID"].Value.ToString())
                    {
                        DataGridChilds.Rows.Remove(row);
                    }
                }
            }
            MessageBox.Show("Remove Successfully");
            cleardata();
        }

        private void Update_C_Btn_Click(object sender, EventArgs e)
        {
            if (ID != "")
            {
                using (MySqlConnection connection = new MySqlConnection(connstring))
                {
                    connection.Open();
                    string query = "UPDATE Child_tbl SET " +
                                   "Name = @Name, " +
                                   "DateofBirth = @DateofBirth " +
                                   "WHERE P_Id = @P_Id AND C_Id = @C_Id";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Name", Cname.Text);
                    command.Parameters.AddWithValue("@DateofBirth", CDofB.Text);
                    command.Parameters.AddWithValue("@P_Id", ID);
                    command.Parameters.AddWithValue("@C_Id", Ccc_Id);
                    command.ExecuteNonQuery();

                    foreach (DataGridViewRow row in DataGridChilds.Rows)
                    {
                        if (row.Cells["C_ID"].Value != null && Ccc_Id.Equals(row.Cells["C_ID"].Value.ToString()))
                        {
                            row.Cells["Namez"].Value = Cname.Text;
                            row.Cells["DofB"].Value = CDofB.Text;
                        }
                    }
                }
            }
            else
            {
                foreach (DataGridViewRow row in DataGridChilds.Rows)
                {
                    if (row.Cells["C_ID"].Value != null && Ccc_Id.Equals(row.Cells["C_ID"].Value.ToString()))
                    {
                        row.Cells["Namez"].Value = Cname.Text;
                        row.Cells["DofB"].Value = CDofB.Text;
                    }
                }
            }
            MessageBox.Show("Successfully Updated");
            cleardata();
        }
        private void Height_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            int cursorPosition = textBox.SelectionStart;

            string currentText = textBox.Text;
            if (currentText.EndsWith(" cm"))
            {
                currentText = currentText.Substring(0, currentText.Length - 3);
            }

            if (decimal.TryParse(currentText, out decimal height))
            {
                textBox.Text = $"{height} cm";
            }
            else
            {
                textBox.Text = currentText;
            }
            textBox.SelectionStart = cursorPosition;
        }

        private void Weight_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            int cursorPosition = textBox.SelectionStart;

            string currentText = textBox.Text;
            if (currentText.EndsWith(" kg"))
            {
                currentText = currentText.Substring(0, currentText.Length - 3);
            }

            if (decimal.TryParse(currentText, out decimal height))
            {
                textBox.Text = $"{height} kg";
            }
            else
            {
                textBox.Text = currentText;
            }
            textBox.SelectionStart = cursorPosition;
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

        bool goodsNa = false;



        private void Email_Address_TextChanged(object sender, EventArgs e)
        {
            string emailText = Email_Address.Text;

            int atIndex = emailText.IndexOf('@');
            int dotIndex = emailText.IndexOf('.');

            if (atIndex != -1 && dotIndex != -1 && atIndex < dotIndex)
            {
                Email_Address.BackColor = Color.FromArgb(192, 192, 255);
                goodsNa = true;
            }
            else
            {
                Email_Address.BackColor = Color.FromArgb(255, 192, 192);
                goodsNa = false;
            }
        }

        private void P_Contact_No_TextChanged(object sender, EventArgs e)
        {
            if (P_Contact_No.Text.Length >= 13)
            {
                P_Contact_No.BackColor = Color.FromArgb(192, 192, 255);
            }
            else
            {
                P_Contact_No.BackColor = Color.FromArgb(255, 192, 192);
            }

        }

        private void PContactNo_TextChanged(object sender, EventArgs e)
        {
            if (PContactNo.Text.Length >= 13)
            {
                PContactNo.BackColor = Color.FromArgb(192, 192, 255);
            }
            else
            {
                PContactNo.BackColor = Color.FromArgb(255, 192, 192);
            }
        }



    }
}
