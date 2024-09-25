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
    public partial class Employment_Rec_Form : Form
    {
        string connstring = Program.connstring;
        string ID;

        public Employment_Rec_Form(string Id, string Name)
        {
            InitializeComponent();
            ID = Id;
            //    LoadTable();
        }


        private void Add_Btn_Click(object sender, EventArgs e)
        {

        }

    }
}

