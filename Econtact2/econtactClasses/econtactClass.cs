using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Econtact2.econtactClasses
{
    class EcontactClass
    {
        //Getter Setter Properties
        //Act as a Data Carrier in our application
        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNumber { get; set; }
        public string ContactAddress { get; set; }
        public string Gender { get; set; }

        //Datbase connection string
        static string myconnstring = ConfigurationManager.ConnectionStrings["comnstrng"].ConnectionString;

        //Selecting data from Datbase
        public DataTable Select()
        {
            //Step 1: Datbase Connection
            SqlConnection conn = new SqlConnection(myconnstring);
            DataTable dt = new DataTable();
            try
            {
                //Step 2: Writting Sql Query
                string sql = "SELECT * FROM tbl_contact";
                //Creating cmd using sql and comm
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Creating SQL Dataadapter using cmd
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        //Inserting Data into Database
        public bool Insert(EcontactClass c)
        {
            //Creating a default return type and its value to false
            bool isSuccess = false;

            //Step 1: Connect Database
            SqlConnection conn = new SqlConnection(myconnstring);
            try
            {
                //Ste 2: Create a SQL Query to insert Data
                string sql = "INSERT INTO dbo.tbl_contact (FirstName, LastName, ContactNumber, ContactAddress, Gender) VALUES (@FirstName, @LastName, @ContactNumber, @ContactAddress, @Gender)";
                //Creating SQL using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Create parameters to add data
                /*cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNumber", c.ContactNumber);
                cmd.Parameters.AddWithValue("@ContactAdress", c.ContactAdress);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);*/
                
                //Create parameters to add data
                //This is best practice compare to the previous
                cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = c.FirstName;
                cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = c.LastName;
                cmd.Parameters.Add("@ContactNumber", SqlDbType.VarChar).Value = c.ContactNumber;
                cmd.Parameters.Add("@ContactAddress", SqlDbType.VarChar).Value = c.ContactAddress;
                cmd.Parameters.Add("@Gender", SqlDbType.VarChar).Value = c.Gender;
                

                //Connection Open Here
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                //If the query runs successfully then  the rows value will be greater than zero else its value willbe 0
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

        //Method to update date in Datbase from our application

        public bool Update(EcontactClass c)
        {
            //Create a default return type and set it to false;
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstring);
            try
            {
                //SQL to update data in our database
                string sql = "UPDATE dbo.tbl_contact SET FirstName = @FirstName, LastName = @LastName, ContactNumber = @ContactNumber, ContactAddress = @ContactAddress, Gender = @Gender WHERE ContactID = @ContactID";

                //Creating SQL cOMMAND
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Create Parameters to add value
                /*cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNumber", c.ContactNumber);
                cmd.Parameters.AddWithValue("@ContactAddress", c.ContactAddress);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);
                cmd.Parameters.AddWithValue("@ContactID", c.ContactID);*/

                //Create parameters to add data
                //This is best practice compare to the previous
                cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = c.FirstName;
                cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = c.LastName;
                cmd.Parameters.Add("@ContactNumber", SqlDbType.VarChar).Value = c.ContactNumber;
                cmd.Parameters.Add("@ContactAddress", SqlDbType.VarChar).Value = c.ContactAddress;
                cmd.Parameters.Add("@Gender", SqlDbType.VarChar).Value = c.Gender;
                cmd.Parameters.Add("@ContactID", SqlDbType.Int).Value = c.ContactID;
                
                //Open database connection
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                //If the query runs successfully then  the rows value will be greater than zero else its value willbe 0
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
        //Method to delete data from Datbase
        public bool Delete(EcontactClass c)
        {
            //Create default return value and set its value to false
            bool isSuccess = false;
            //Create sql Connection
            SqlConnection conn = new SqlConnection(myconnstring);
            try
            {
                //Sql to delete data
                string sql = "DELETE FROM tbl_contact WHERE ContactID = @ContactID";
                //Creating sql Command
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ContactID", c.ContactID);
                //Open Connection
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                //If the query runs successfully then  the rows value will be greater than zero else its value willbe 0
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
