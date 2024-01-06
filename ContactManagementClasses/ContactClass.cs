using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContactManagement.ContactManagementClasses
{
    class ContactClass
    {
        //Getter and Setter 
        public int ContactID{ get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set; }

        public String ContactNo{ get; set; }

        public String Address { get; set; }

        public String Gender{ get; set; }

        //Connceting with database

        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;  
        
        //*) Selecting Data from Database

        public DataTable Select ()
        {
            //Step 1 : Data Connection
            SqlConnection conn=new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                //Step 2 : Writing SQL Query 
                string sql = "SELECT * FROM Table_Contact";

                //Creating cmd using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Creating SQL DataAdapter using cmd
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch(Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        //*) Inserting Data into Database
        public bool Insert (ContactClass c)
        {
            //Creating a default return type and setting its value to false
            bool isSuccess = false;

            //Step 1 : Connect Database
            SqlConnection conn = new SqlConnection(myconnstrng);
            try 
            {
                //Step 2 : Create a SQL Query to insert Data
                String sql = "INSERT INTO Table_Contact (FirstName, LastName, ContactNo, Address, Gender) VALUES (@FirstName, @LastName, @ContactNo, @Address, @Gender)";

                //Creating SQL Command using sql and  conn
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Creating Parameters to add conn 
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);


                //Connection Open Here
                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                // If the query runs successfully then the value of row will be greater than zero else its value will be 0
                if(rows > 0) 
                { 
                    isSuccess = true;
                }
                else 
                {
                    isSuccess=false;
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"Error Inserting contact: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        //Methode to update data in database from our application
        public bool Update (ContactClass c) 
        {
            //Creating a default return type and setting its value to false
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //SQL to update data in our Database
                string sql = "UPDATE Table_Contact SET FirstName=@FirstName, LastName=@LastName, ContactNo=@ContactNo, Address=@Address, Gender=@Gender WHERE ContactID=@ContactID";

                //Creating SQL Command using sql and  conn
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Creating Parameters to add value
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);
                cmd.Parameters.AddWithValue("@ContactID", c.ContactID);

                //Open database connection
                conn.Open ();

                int rows = cmd.ExecuteNonQuery();

                // If the query runs successfully then the value of row will be greater than zero else its value will be 0
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

        //Method to delete data from database

        public bool Delete (ContactClass c) 
        {
            //Creating a default return type and setting its value to false
            bool isSuccess = false;

            //Create SQL Connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //SQL To Delete Data
                string sql = "DELETE FROM Table_Contact WHERE ContactID=@ContactID";

                //Creating SQL Command using sql and  conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ContactID", c.ContactID);

                //Open connection
                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                // If the query runs successfully then the value of row will be greater than zero else its value will be 0

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
