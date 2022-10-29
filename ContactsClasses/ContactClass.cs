using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.ContactsClasses
{
    class ContactClass
    {
        //getters and setters
        public int ContactID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ContactNumber { get; set; }

        public string Address { get; set; }

        public string Gender { get; set; }

        static string myconnstring = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

        //select data from db

        public  DataTable Select()
        {
            SqlConnection conn = new SqlConnection(myconnstring);
            DataTable dt = new DataTable();
            try
            {
                //sql query
                string sql = "SELECT * from tbl_contact";

                //create command using sql and conn
                SqlCommand sqlCommand = new SqlCommand(sql,conn);
                
                //create sql adapter using sql command
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                conn.Open();
                adapter.Fill(dt);

            }catch(Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        //Inserting data
        public bool Insert(ContactClass c)
        {
            //Creating default return value
            bool isSuccess = false;

            //connect to db
            SqlConnection conn = new SqlConnection(myconnstring);
            try
            {
                string sql = "INSERT INTO tbl_contact (FirstName,LastName,ContactNumber,Address,Gender) VALUES(@FirstName,@LastName,@ContactNumber,@Address,@Gender)";

                //ceating sql command using sql and conn'
                SqlCommand sqlCommand = new SqlCommand(sql, conn);

                //create parameter to add data to db
                sqlCommand.Parameters.AddWithValue("@FirstName", c.FirstName);
                sqlCommand.Parameters.AddWithValue("@LastName", c.LastName);
                sqlCommand.Parameters.AddWithValue("@ContactNumber", c.ContactNumber);
                sqlCommand.Parameters.AddWithValue("@Address", c.Address);
                sqlCommand.Parameters.AddWithValue("@Gender", c.Gender);

                //open connection
                conn.Open();
                int rows = sqlCommand.ExecuteNonQuery();
                //if query is a success number of rows will be > than 0

                if(rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }



            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        //update data
        public bool Update(ContactClass c)
        {
            bool isSuccess = false;
            //sql connection
            SqlConnection conn = new SqlConnection(myconnstring);
            try
            {
                string sql = "UPDATE tbl_contact SET FirstName=@FirstName,LastName=@LastName,ContactNumber=@ContactNumber,Address=@Address,Gender=@Gender Where ContactID=@ContactID";

                //sql command
                SqlCommand sqlCommand = new SqlCommand(sql, conn);

                sqlCommand.Parameters.AddWithValue("@FirstName", c.FirstName);
                sqlCommand.Parameters.AddWithValue("@LastName", c.LastName);
                sqlCommand.Parameters.AddWithValue("@ContactNumber", c.ContactNumber);
                sqlCommand.Parameters.AddWithValue("@Address", c.Address);
                sqlCommand.Parameters.AddWithValue("@Gender", c.Gender);
                sqlCommand.Parameters.AddWithValue("@ContactID", c.ContactID);

                //open connection
                conn.Open();
                int rows = sqlCommand.ExecuteNonQuery();

                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }




            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        //delete data
        public bool Delete(ContactClass c)
        {
            bool isSuccess = false;

            //sql connection
            SqlConnection conn = new SqlConnection(myconnstring);
            try
            {
                string sql = "DELETE from tbl_contact WHERE ContactID = @ContactID";
                SqlCommand sqlCommand = new SqlCommand(sql, conn);

                //parameters
                sqlCommand.Parameters.AddWithValue("@ContactID", c.ContactID);

                //open connection
                conn.Open();
                int rows = sqlCommand.ExecuteNonQuery();

                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }



            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
    }
}
