using SDHDotNetCore.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDHDotNetCore.ConsoleApp.EFCoreExamples
{
    internal class EFCoreExample
    {
        public void Run()
        {
            Create("test 3", "MKK", "test 3");
            Read();
            Edit(1);
            //Edit(100);
            //Update(2, "test 1", "test 2", "test 3");
            Update(7, "Sandar", null, null);
            Delete(10);
        }

        private void Read()
        {
            AppDbContext db = new AppDbContext();
            var lst = db.Blogs.ToList();
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
            BlogDataModel blog = new BlogDataModel
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            AppDbContext db = new AppDbContext();
            //db.Add(blog);
            db.Blogs.Add(blog);
            var result = db.SaveChanges();

            string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            Console.WriteLine(message);
        }

        private void Edit(int id)
        {
            AppDbContext db = new AppDbContext();
            var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);
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
            AppDbContext db = new AppDbContext();
            var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item == null)
            {
                Console.WriteLine("No data found.");
                return;
            }

            if (!string.IsNullOrWhiteSpace(title))
            {
                item.BlogTitle = title;
            }
            if (!string.IsNullOrWhiteSpace(author))
            {
                item.BlogAuthor = author;
            }
            if (!string.IsNullOrWhiteSpace(content))
            {
                item.BlogContent = content;
            }
            //BlogDataModel blog = new BlogDataModel
            //{
            //    BlogId = id,
            //    BlogTitle = title,
            //    BlogAuthor = author,
            //    BlogContent = content
            //};
            //item = blog;
            var result = db.SaveChanges();

            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            Console.WriteLine(message);
        }

        private void Delete(int id)
        {
            AppDbContext db = new AppDbContext();
            var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item == null)
            {
                Console.WriteLine("No data found.");
                return;
            }
            db.Blogs.Remove(item);
            var result = db.SaveChanges();
            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            Console.WriteLine(message);
        }
    }
}
