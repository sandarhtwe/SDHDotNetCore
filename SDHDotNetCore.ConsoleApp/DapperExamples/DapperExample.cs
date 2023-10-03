using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Reflection.Metadata;
using SDHDotNetCore.ConsoleApp.Models;

namespace HKSDotNetCore.ConsoleApp.DapperExamples
{
    public class DapperExample
    {
        private readonly SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = "DESKTOP-DDE6MVJ\\TESTINGSDH",
            InitialCatalog = "HKSDotNetCore",
            UserID = "Sa",
            Password = "Sdh@1234"
        };

        public void Run()
        {
            //Create("test 1", "test 2", "test 3");
            Read();
            Edit(1);
           // Edit(100);
            Update(3, "test 1", "test 2", "test 3");
            Delete(11);
        }

        // Read, Edit             => DataTable => Data
        // Create, Update, Delete => Execute (Rows Affected)

        private void Read()
        {
            string query = "select * from tbl_blog"; // Select Query
            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            List<BlogDataModel> lst = db.Query<BlogDataModel>(query).ToList();
            foreach (BlogDataModel item in lst)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
                Console.WriteLine("--------------------------------------");
            }
        }

        private void Create(string title, string author, string content)
        {
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)"; // Insert Query
            BlogDataModel blog = new BlogDataModel
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            var result = db.Execute(query, blog);

            string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            Console.WriteLine(message);
        }

        private void Edit(int id)
        {
            string query = "select * from tbl_blog where BlogId = @BlogId;"; // Select Query

            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            BlogDataModel blog = new BlogDataModel
            {
                BlogId = id
            };
            var item = db.Query<BlogDataModel>(query, blog).FirstOrDefault();
            //BlogDataModel? item = db.Query<BlogDataModel>(query, new { BlogId = id }).FirstOrDefault();
            if (item == null)
            {
                Console.WriteLine("No data found.");
                return;
            }

            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
            Console.WriteLine("--------------------------------------");
        }

        private void Update(int id, string title, string author, string content)
        {
            string query = @"
UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId = @BlogId"; // Update Query
            BlogDataModel blog = new BlogDataModel
            {
                BlogId = id,
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);
            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            Console.WriteLine(message);
        }

        private void Delete(int id)
        {
            string query = @"
DELETE FROM [dbo].[Tbl_Blog]
      WHERE BlogId = @BlogId"; // Delete Query
            BlogDataModel blog = new BlogDataModel
            {
                BlogId = id,
            };

            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);
            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            Console.WriteLine(message);
        }
    }
}
