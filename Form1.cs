using Contacts.ContactsClasses;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Windows.Forms;

namespace Contacts
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ContactClass contact = new ContactClass();
        string PATH = "";
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
            string error = "";
            
            contact.FirstName = textFirstName.Text;
            contact.LastName = textLastName.Text;
            contact.ContactNumber = textContactNumber.Text;
            contact.Address = textAddress.Text;
            contact.Gender = comboGender.Text;
            contact.Image = PATH;

            if(textFirstName.Text == ""){
                error = "First Name is required";
            }else if (textLastName.Text == "")
            {
                error = "Last Name is required";
            }
            else if (textContactNumber.Text == "")
            {
                error = "Contact Number is required";
            }
            else if (textAddress.Text == "")
            {
                error = "Address is required";
            }
            else if (comboGender.Text == "")
            {
                error = "Gender is required";
            }
           
            else
            {
                error = "";
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

            if (error == "")
            {
                textError.Text = "";
            }
            else
            {
                textError.Text = "You have an error: " + error;
            }

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
            contactImage.Image = null;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string error = "";
            if (textID.Text == "")
            {
                error = "Please select a contact to update";
            }
          
            else if (textFirstName.Text == "")
            {
                error = "First Name is required";
            }
            else if (textLastName.Text == "")
            {
                error = "Last Name is required";
            }
            else if (textContactNumber.Text == "")
            {
                error = "Contact Number is required";
            }
            else if (textAddress.Text == "")
            {
                error = "Address is required";
            }
            else if (comboGender.Text == "")
            {
                error = "Gender is required";
            }

            else
            {
                error = "";
                contact.ContactID = int.Parse(textID.Text);
                contact.FirstName = textFirstName.Text;
                contact.LastName = textLastName.Text;
                contact.ContactNumber = textContactNumber.Text;
                contact.Address = textAddress.Text;
                contact.Gender = comboGender.Text;
                contact.Image = PATH;

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
            if(error == "")
            {
                textError.Text = "";
            }
            else
            {
                textError.Text = "You have an error: "+error;
            }
            

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
            if (dataContacts.Rows[rowIndex].Cells[6].Value.ToString()!="")
            {
                contactImage.Image = Image.FromFile(dataContacts.Rows[rowIndex].Cells[6].Value.ToString());

            }
            else
            {
                contactImage.Image = null;
            }




        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string error = "";
            if (textID.Text == "")
            {
                error = "Please select a contact to update";
            }
            else
            {
                error = "";
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

            if (error == "")
            {
                textError.Text = "";
            }
            else
            {
                textError.Text = "You have an error: " + error;
            }




        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void btnImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "C://Desktop";
            //Your opendialog box title name.
            openFileDialog1.Title = "Select file to be upload.";
            //which type file format you want to upload in database. just add them.
            openFileDialog1.Filter = "Select Valid Document(*.png; *.jpeg; *.jpg;)|*.png; *.jpg; *.jpeg;";
            //FilterIndex property represents the index of the filter currently selected in the file dialog box.
            openFileDialog1.FilterIndex = 1;
            try
            {
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (openFileDialog1.CheckFileExists)
                    {
                        PATH = System.IO.Path.GetFullPath(openFileDialog1.FileName);
                        contactImage.Image = Image.FromFile(PATH);
                    }
                }
                else
                {
                    MessageBox.Show("Please Upload document.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}