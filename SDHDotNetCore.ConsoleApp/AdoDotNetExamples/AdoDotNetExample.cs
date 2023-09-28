using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SDHDotNetCore.ConsoleApp.AdoDotNetExamples
{
    internal class AdoDotNetExample
    {
       readonly SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = "DESKTOP-DDE6MVJ\\TESTINGSDH",
            InitialCatalog = "HKSDotNetCore",
            UserID = "Sa",
            Password = "Sdh@1234"
        };
        public void Run()
        {
            Create("Myo Myo", "0999999999", 27, "Java", "Mandalay");
            Read();
        }
        private void Read()
        {
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            // open the connection
            connection.Open();

            string query = "select * from tbl_blog"; //Read data from table
            SqlCommand command = new SqlCommand(query, connection); //Write query
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command); //Create new query and add query
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            string queryforStudent = "select * from tbl_student"; //Read data from table
            SqlCommand command1 = new SqlCommand(queryforStudent, connection); //Write query
            SqlDataAdapter adapter = new SqlDataAdapter(command1); //Create new query and add query
            DataTable dt_Student = new DataTable();
            adapter.Fill(dt_Student);

            connection.Close();
            //Console.WriteLine("Connection closed.");
            Console.WriteLine();

            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine(dr["BlogID"]);
                Console.WriteLine(dr["BlogTitle"]);
                Console.WriteLine(dr["BlogAuthor"]);
                Console.WriteLine(dr["BlogContent"]);
                Console.WriteLine("------------------------------");
            }

            Console.WriteLine("*****************************************************************");
            Console.WriteLine();
            Console.WriteLine("These are lists of Student....");
            Console.WriteLine();

            foreach (DataRow dr in dt_Student.Rows)
            {
                Console.WriteLine(dr["StudentId"]);
                Console.WriteLine(dr["Name"]);
                Console.WriteLine(dr["Ph_no"]);
                Console.WriteLine(dr["Age"]);
                Console.WriteLine(dr["Subject"]);
                Console.WriteLine(dr["Address"]);
                Console.WriteLine("------------------------------");
            }
        }

        private void Create(string name,string ph_no,int age,string subject, string address)
        {
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            // open the connection
            connection.Open();

            string queryforStudent = @"INSERT INTO [dbo].[Tbl_Student]
           ([Name]
           ,[Ph_no]
           ,[Age]
           ,[Subject]
           ,[Address])
     VALUES
           (@Name
           ,@Ph_no
           ,@Age
           ,@Subject
           ,@Address)"; //Insert data into table
            SqlCommand command1 = new SqlCommand(queryforStudent, connection); //Write query
            command1.Parameters.AddWithValue("@Name", name);
            command1.Parameters.AddWithValue("@Ph_no", ph_no);
            command1.Parameters.AddWithValue("@Age", age);
            command1.Parameters.AddWithValue("@Subject", subject);
            command1.Parameters.AddWithValue("@Address", address);
            
            int result = command1.ExecuteNonQuery();
            string message = result > 0 ? "Save Successful" : "Save Failed";
            Console.WriteLine(message);

            connection.Close();

        }
    }
}
