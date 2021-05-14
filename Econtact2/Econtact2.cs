using Econtact2.econtactClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Econtact2
{
    public partial class Econtact2 : Form
    {
        public Econtact2()
        {
            InitializeComponent();
        }

        EcontactClass c = new EcontactClass();

        private void Form1_Load(object sender, EventArgs e)
        {
            //Load data on datagridview
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnAddContact_Click(object sender, EventArgs e)
        {
            //Get the value from the input fields
            c.FirstName = textBoxFirstName.Text;
            c.LastName = textBoxLastName.Text;
            c.ContactNumber = textBoxContactNumber.Text;
            c.ContactAddress = textBoxContactAddress.Text;
            c.Gender = cmbBoxGender.Text;

            //Insert data into database using method
            bool success = c.Insert(c);
            if(success ==true)
            {
                MessageBox.Show("New contact successfully added");
                //Clear cells after contact has been added
                Clear();
            }
            else
            {
                MessageBox.Show("Failed to add contact. Try again.");
            }
            //Load data on datagridview
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //Method to clear fields
        public void Clear()
        {
            textBoxContactID.Text = "";
            textBoxFirstName.Text = "";
            textBoxLastName.Text = "";
            textBoxContactNumber.Text = "";
            textBoxContactAddress.Text = "";
            cmbBoxGender.Text = "";
        }

        private void btnUpdateContact_Click(object sender, EventArgs e)
        {
            //Get the data from textboxes
          
            c.FirstName = textBoxFirstName.Text;
            c.LastName = textBoxLastName.Text;
            c.ContactNumber = textBoxContactNumber.Text;
            c.ContactAddress = textBoxContactAddress.Text;
            c.Gender = cmbBoxGender.Text;
            c.ContactID = int.Parse(textBoxContactID.Text);
            //Update data in data base
            bool success = c.Update(c);
            if(success == true)
            {
                //Upadte Successfully
                MessageBox.Show("Contact has been successfully updated.");
                //Refresh data on datagridview
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
                //Clear fields
                Clear();
            }
            else
            {
                //Failed Update
                MessageBox.Show("Failed to update Contact. Try Again");
            }

        }

        private void dgvContactList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Get data from Dat Gridview and Load it to the text boxes respectively
            //Identify the row which mouse is clicked
            int rowIndex = e.RowIndex;
            textBoxContactID.Text = dgvContactList.Rows[rowIndex].Cells[0].Value.ToString();
            textBoxFirstName.Text = dgvContactList.Rows[rowIndex].Cells[1].Value.ToString();
            textBoxLastName.Text = dgvContactList.Rows[rowIndex].Cells[2].Value.ToString();
            textBoxContactNumber.Text = dgvContactList.Rows[rowIndex].Cells[3].Value.ToString();
            textBoxContactAddress.Text = dgvContactList.Rows[rowIndex].Cells[4].Value.ToString();
            cmbBoxGender.Text = dgvContactList.Rows[rowIndex].Cells[5].Value.ToString();
        }

        private void btnClearFields_Click(object sender, EventArgs e)
        {
            //Clear fields
            Clear();
        }

        private void btnDeleteContact_Click(object sender, EventArgs e)
        {
            //Verify if fields a clear
            if (textBoxContactID.Text == "")
            {
                MessageBox.Show("No record has been selected. Try again");
            }
            else
            {

                //Get data from TextBox
                c.ContactID = Convert.ToInt32(textBoxContactID.Text);


                bool success = c.Delete(c);
                if (success == true)
                {
                    //Successfully Deleted
                    MessageBox.Show("Contact record successfully deleted");
                    //Refresh data on datagridview
                    DataTable dt = c.Select();
                    dgvContactList.DataSource = dt;
                    //Clear fields
                    Clear();
                }
                else
                {
                    //Failed at deleting
                    MessageBox.Show("Failed to delete record. Try again.");
                }
            }

        }
        static string myconnstr = ConfigurationManager.ConnectionStrings["comnstrng"].ConnectionString;
        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            //Get the value from text Boz
            string keyboard = textBoxSearch.Text;

            SqlConnection conn = new SqlConnection(myconnstr);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tbl_contact WHERE FirstName LIKE '%" + keyboard + "%' OR LastName LIKE '%" +keyboard+ "%' OR ContactAddress LIKE '%"+keyboard+"%'", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgvContactList.DataSource = dt;
;        }
    }
}
