using Newtonsoft.Json;
using SDHDotNetCore.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SDHDotNetCore.ConsoleApp.HttpClientExamples
{
    public class HttpClientExample
    {
        public async Task Run()
        {
            //await Read();
            //await Edit(4);
            //await Delete(4);
            // await Create("test 2", "test 2", "test 3");
            //await Update(1, "test4", "test5", "test6");
            await Patch(1, "Sandar Htwe", null, null);
            await Patch(1, null, "Sandar Htwe 1", null);
            await Patch(1, null, null, "Sandar Htwe 2");
            await Edit(1);
        }

        public async Task Read()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://localhost:7023/api/blog");
            if(response.IsSuccessStatusCode)
            {
                var jsonStr = await response.Content.ReadAsStringAsync();

                //Json to C# Object => DeserializeObject
                //C# to Json => SerializeObject
                List<BlogDataModel> lst = JsonConvert.DeserializeObject<List<BlogDataModel>>(jsonStr);
                foreach (BlogDataModel item in lst)
                {
                    Console.WriteLine(item.BlogId);
                    Console.WriteLine(item.BlogTitle);
                    Console.WriteLine(item.BlogAuthor);
                    Console.WriteLine(item.BlogContent);
                    Console.WriteLine("--------------------------------------");
                }
            }
        }

        public async Task Edit(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"https://localhost:7023/api/blog/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonStr = await response.Content.ReadAsStringAsync();

                BlogResponseModel model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);
                if (model.IsSuccess)
                {
                    var item = model.Data;
                    Console.WriteLine(item.BlogId);
                    Console.WriteLine(item.BlogTitle);
                    Console.WriteLine(item.BlogAuthor);
                    Console.WriteLine(item.BlogContent);
                    Console.WriteLine("--------------------------------------");
                }
                else
                {
                    Console.WriteLine(model.Message);
                }
            }
            else
            {
                var jsonStr = await response.Content.ReadAsStringAsync();
                BlogResponseModel model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);
                Console.WriteLine(model.Message);
            }
        }

        public async Task Create(string title, string author, string content)
        
        {
            BlogDataModel blog = new BlogDataModel
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            string jsonStrBlog = JsonConvert.SerializeObject(blog);
            HttpContent httpContent = new StringContent(jsonStrBlog, Encoding.UTF8, Application.Json);

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync($"https://localhost:7023/api/blog", httpContent);
            if (response.IsSuccessStatusCode)
            {
                var jsonStr = await response.Content.ReadAsStringAsync();

                BlogResponseModel model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);
                Console.WriteLine(model.IsSuccess);
                Console.WriteLine(model.Message);

                //if (model.IsSuccess)
                //{
                //    var item = model.Data;
                //    Console.WriteLine(item.BlogId);
                //    Console.WriteLine(item.BlogTitle);
                //    Console.WriteLine(item.BlogAuthor);
                //    Console.WriteLine(item.BlogContent);
                //    Console.WriteLine("--------------------------------------");

                //}
                //else
                //{
                //    Console.WriteLine(model.Message);
                //}

            }
        }

        public async Task Update(int id,string title, string author, string content)

        {
            BlogDataModel blog = new BlogDataModel
            {
               // BlogId = id,
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            string jsonStrBlog = JsonConvert.SerializeObject(blog);
            HttpContent httpContent = new StringContent(jsonStrBlog, Encoding.UTF8, Application.Json);

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PutAsync($"https://localhost:7023/api/blog/{id}", httpContent);
            if (response.IsSuccessStatusCode)
            {
                var jsonStr = await response.Content.ReadAsStringAsync();

                BlogResponseModel model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);
                Console.WriteLine(model.IsSuccess);
                Console.WriteLine(model.Message);

            }
            
        }

        public async Task Patch(int id, string title, string author, string content)

        {
            BlogDataModel blog = new BlogDataModel();

            if (!string.IsNullOrWhiteSpace(title))
            {
                blog.BlogTitle = title;
            }
            if (!string.IsNullOrWhiteSpace(author))
            {
                blog.BlogAuthor = author;
            }
            if (!string.IsNullOrWhiteSpace(content))
            {
                blog.BlogContent = content;
            }
            
            string jsonStrBlog = JsonConvert.SerializeObject(blog);
            HttpContent httpContent = new StringContent(jsonStrBlog, Encoding.UTF8, Application.Json);

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PatchAsync($"https://localhost:7023/api/blog/{id}", httpContent);
            if (response.IsSuccessStatusCode)
            {
                var jsonStr = await response.Content.ReadAsStringAsync();

                BlogResponseModel model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);
                Console.WriteLine(model.IsSuccess);
                Console.WriteLine(model.Message);

            }

        }

        public async Task Delete(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.DeleteAsync($"https://localhost:7023/api/blog/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonStr = await response.Content.ReadAsStringAsync();

                BlogResponseModel model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);
                Console.WriteLine(model.IsSuccess);
                Console.WriteLine(model.Message);
            }
            else
            {
                var jsonStr = await response.Content.ReadAsStringAsync();
                BlogResponseModel model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);
                Console.WriteLine(model.Message);
            }
        }
    }
}
