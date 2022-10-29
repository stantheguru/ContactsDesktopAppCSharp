using Contacts.ContactsClasses;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;

namespace Contacts
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ContactClass contact = new ContactClass();
        static string myconnstring = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        private void textSearch_TextChanged(object sender, EventArgs e)
        {
            //get textbox value
            string keyword = textSearch.Text;
            SqlConnection conn = new SqlConnection(myconnstring);

            //SQL ADAPATER
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM tbl_contact where FirstName LIKE '%" + keyword + "%' OR LastName LIKE '%" + keyword + "%' OR Address LIKE '%" + keyword + "%' OR ContactNumber  LIKE '%" + keyword +"%'", conn);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            dataContacts.DataSource = dt;


        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //get the value from inut fields
            contact.FirstName = textFirstName.Text;
            contact.LastName = textLastName.Text;
            contact.ContactNumber = textContactNumber.Text;
            contact.Address = textAddress.Text;
            contact.Gender = comboGender.Text;


            //insert data
            bool success = contact.Insert(contact);
            if (success == true)
            {
                MessageBox.Show("New Contact Inserted Succesfully");
                Clear();
            }
            else
            {
                MessageBox.Show("Failed to add contact. Please try again!");

            }

            //Load data on table
            DataTable dt = contact.Select();
            dataContacts.DataSource = dt;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable dt = contact.Select();
            dataContacts.DataSource = dt;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //method to clear fileds 

        public void Clear()
        {
            textContactNumber.Text = "";
            textFirstName.Text = "";
            textLastName.Text = "";
            textAddress.Text = "";
            comboGender.Text = "";
            textID.Text = "";
            textSearch.Text = "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            contact.ContactID = int.Parse(textID.Text);
            contact.FirstName = textFirstName.Text;
            contact.LastName = textLastName.Text;
            contact.ContactNumber = textContactNumber.Text;
            contact.Address = textAddress.Text;
            contact.Gender = comboGender.Text;

            //update
            bool success = contact.Update(contact);

            if (success == true)
            {
                MessageBox.Show("Contact has been updated Succesfully");
                Clear();
            }
            else
            {
                MessageBox.Show("Failed to update contact. Please try again!");

            }
            //Load data on table
            DataTable dt = contact.Select();
            dataContacts.DataSource = dt;

        }

        private void dataContacts_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //get data from from grid viw and lod it to text boxes
            //identify the row
            int rowIndex = e.RowIndex;
            textID.Text = dataContacts.Rows[rowIndex].Cells[0].Value.ToString();
            textFirstName.Text = dataContacts.Rows[rowIndex].Cells[1].Value.ToString();
            textLastName.Text = dataContacts.Rows[rowIndex].Cells[2].Value.ToString();
            textContactNumber.Text = dataContacts.Rows[rowIndex].Cells[3].Value.ToString();
            textAddress.Text = dataContacts.Rows[rowIndex].Cells[4].Value.ToString();
            comboGender.Text = dataContacts.Rows[rowIndex].Cells[5].Value.ToString();




        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            contact.ContactID = int.Parse(textID.Text);

            bool success = contact.Delete(contact);

            if (success == true)
            {
                MessageBox.Show("Contact has been deleted Succesfully");
                Clear();
            }
            else
            {
                MessageBox.Show("Failed to delete contact. Please try again!");

            }
            //Load data on table
            DataTable dt = contact.Select();
            dataContacts.DataSource = dt;


        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}