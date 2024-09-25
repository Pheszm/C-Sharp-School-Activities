using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Security.Policy;

using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace BioData_Activity
{
    public partial class Output_Form : Form
    {
        string connstring = Program.connstring;
        string ID;
        public Output_Form(string iD)
        {
            InitializeComponent();
            ID = iD;
            LoadTable();
        }

        void LoadTable()
        {
            Per_LoadTheFile();
            Ed_LoadTable();
            Em_LoadTable();
            Char_LoadTable();
            Other_LoadTable();
            LoadChild();
            HEHEHE();
        }

        void HEHEHE()
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
                        NN1.Text = reader["n1"].ToString();
                        OO1.Text = reader["a1"].ToString();
                        AA3.Text = reader["p1"].ToString();
                        CC1.Text = reader["f1"].ToString();
                        NN2.Text = reader["n2"].ToString();
                        OO2.Text = reader["a2"].ToString();
                        AA2.Text = reader["p2"].ToString();
                        CC2.Text = reader["f2"].ToString();
                    }
                }
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

        void Per_LoadTheFile()
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

        void Ed_LoadTable()
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


        void Em_LoadTable()
        {
            using (MySqlConnection connection = new MySqlConnection(connstring))
            {
                connection.Open();
                string query = "SELECT n1, a1, p1, f1, n2, a2, p2, f2, n3, a3, p3, f3, n4, a4, p4, f4, n5, a5, p5, f5 " +
                               "FROM Employment_Rec_tbl WHERE P_Id = @P_Id";

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
                        N3.Text = reader["n3"].ToString();
                        A3.Text = reader["a3"].ToString();
                        P3.Text = reader["p3"].ToString();
                        F3.Text = reader["f3"].ToString();
                        N4.Text = reader["n4"].ToString();
                        A4.Text = reader["a4"].ToString();
                        P4.Text = reader["p4"].ToString();
                        F4.Text = reader["f4"].ToString();
                        N5.Text = reader["n5"].ToString();
                        A5.Text = reader["a5"].ToString();
                        P5.Text = reader["p5"].ToString();
                        F5.Text = reader["f5"].ToString();
                    }
                }

            }
        }


        void Char_LoadTable()
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
                        NN1.Text = reader["n1"].ToString();
                        OO1.Text = reader["a1"].ToString();
                        AA3.Text = reader["p1"].ToString();
                        CC1.Text = reader["f1"].ToString();
                        NN2.Text = reader["n2"].ToString();
                        OO2.Text = reader["a2"].ToString();
                        AA2.Text = reader["p2"].ToString();
                        CC2.Text = reader["f2"].ToString();
                    }
                }
            }
        }

        void Other_LoadTable()
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

        private void Print_Btn_Click(object sender, EventArgs e)
        {
            Print_Btn.Visible = false;
            CaptureScrollableArea();
            Print_Btn.Visible = true;
        }

        private void CaptureScrollableArea()
        {

            Size panelSize1 = TPanel1.DisplayRectangle.Size;
            Size panelSize2 = TPanel2.DisplayRectangle.Size;
            Size panelSize3 = TPanel3.DisplayRectangle.Size;
            Size panelSize4 = TPanel4.DisplayRectangle.Size;
            Size panelSize5 = TPanel5.DisplayRectangle.Size;


            int totalWidth = Math.Max(panelSize1.Width, Math.Max(panelSize2.Width, Math.Max(panelSize3.Width, Math.Max(panelSize4.Width, panelSize5.Width))));
            int totalHeight = panelSize1.Height + panelSize2.Height + panelSize3.Height + panelSize4.Height + panelSize5.Height;


            XSize pageSize1 = new XSize(totalWidth, panelSize1.Height);
            XSize pageSize2 = new XSize(totalWidth, panelSize2.Height + panelSize3.Height + panelSize4.Height);
            XSize pageSize3 = pageSize1; 


            string defaultFileName = "Output_Form.pdf";
            string saveFilePath = GetSaveFilePath(defaultFileName);

            if (saveFilePath == null)
            {

                return;
            }


            using (PdfDocument pdfDocument = new PdfDocument())
            {
                pdfDocument.Info.Title = "The Bio-Data Output";


                PdfPage page1 = pdfDocument.AddPage();
                page1.Width = pageSize1.Width;
                page1.Height = pageSize1.Height;


                using (XGraphics gfx1 = XGraphics.FromPdfPage(page1))
                {

                    double y = 0;


                    y = AddPanelToPdf(gfx1, TPanel1, panelSize1.Width, panelSize1.Height, totalWidth, ref y);
                }


                PdfPage page2 = pdfDocument.AddPage();
                page2.Width = pageSize2.Width;
                page2.Height = pageSize2.Height;


                using (XGraphics gfx2 = XGraphics.FromPdfPage(page2))
                {

                    double y = 0;


                    y = AddPanelToPdf(gfx2, TPanel2, panelSize2.Width, panelSize2.Height, totalWidth, ref y);

                    y += 10; 
                    y = AddPanelToPdf(gfx2, TPanel3, panelSize3.Width, panelSize3.Height, totalWidth, ref y);

                    y += 10; 
                    y = AddPanelToPdf(gfx2, TPanel4, panelSize4.Width, panelSize4.Height, totalWidth, ref y);
                }

                PdfPage page3 = pdfDocument.AddPage();
                page3.Width = pageSize3.Width;
                page3.Height = pageSize3.Height;

                using (XGraphics gfx3 = XGraphics.FromPdfPage(page3))
                {
                    double y = 0;

                    y = AddPanelToPdf(gfx3, TPanel5, panelSize5.Width, panelSize5.Height, totalWidth, ref y);
                }

                pdfDocument.Save(saveFilePath);
            }

            Process.Start(saveFilePath);
        }

        private double AddPanelToPdf(XGraphics gfx, Panel panel, int panelWidth, int panelHeight, int totalWidth, ref double y)
        {

            double scaleX = (double)totalWidth / panelWidth;
            double scaleY = scaleX;

            Bitmap panelBitmap = new Bitmap(panelWidth, panelHeight);
            panel.DrawToBitmap(panelBitmap, new Rectangle(0, 0, panelWidth, panelHeight));


            using (MemoryStream ms = new MemoryStream())
            {
                panelBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                XImage image = XImage.FromStream(ms);
                gfx.DrawImage(image, 0, y, totalWidth, panelHeight * scaleY);
            }

            y += panelHeight * scaleY;


            panelBitmap.Dispose();


            return y;
        }

        private string GetSaveFilePath(string defaultFileName)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Title = "Bio-Data Save as PDF";
            saveDialog.FileName = defaultFileName;
            saveDialog.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
            saveDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); 

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                return saveDialog.FileName;
            }
            else
            {
                return null; 
            }
        }


    }
}

