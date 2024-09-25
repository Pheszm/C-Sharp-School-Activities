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
    public partial class Add_Employment_Rec_Form : Form
    {
        string ID;
        string connstring = Program.connstring;
        public Add_Employment_Rec_Form(string Id, string Name)
        {
            InitializeComponent();
            ID = Id;
            if (Program.Updating == true)
            {
              //  LoadTable();
            }
        }
      





        private void Add_Btn_Click(object sender, EventArgs e)
        {
            int maxID = 0;
            foreach (DataGridViewRow row in DataGridView.Rows)
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

            int EM_ID = maxID + 1;
            if (!string.IsNullOrEmpty(InputFrom.Text) && !string.IsNullOrEmpty(InputNoC.Text))
            {
                DataGridView.Rows.Add(EM_ID, InputNoC.Text, InputAddress.Text, InputPosition.Text, InputFrom.Text, InputTo.Text);
            }


            using (MySqlConnection connection = new MySqlConnection(connstring))
            {
                connection.Open();

                string query = "INSERT INTO Employment_Rec_tbl (EM_Id, P_Id, NameOfCompany, Address, Position, FFrom, TTo) " +
                               "VALUES (@EM_Id, @P_Id, @NameOfCompany, @Address, @Position, @FFrom, @TTo)";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@EM_Id", C_ID);
                command.Parameters.AddWithValue("@P_Id", ID);
                command.Parameters.AddWithValue("@NameOfCompany", InputNoC.Text);
                command.Parameters.AddWithValue("@Address", InputAddress.Text);
                command.Parameters.AddWithValue("@Position", InputPosition.Text);
                command.Parameters.AddWithValue("@FFrom", InputFrom.Text);
                command.Parameters.AddWithValue("@TTo", InputTo.Text);  

                command.ExecuteNonQuery();
            }
            InputNoC.Text = "";
            InputAddress.Text = "";
            InputPosition.Text = "";
        }
    }
}
