using Newtonsoft.Json;
using RestSharp;
using SDHDotNetCore.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SDHDotNetCore.ConsoleApp.RestClientExamples
{
    internal class RestClientExample
    {
        public async Task Run()
        {
            //await Read();
            await Edit(2);
            //await Create("test 2", "test 2", "test 3");
           // await Update(1, "sandar", "sandar", "test6");
            await Patch(2, "Sandar 1", null, null);
            await Patch(2, null, "Sandar 2", null);
            await Patch(2, null, null, "Sandar 3");
            await Edit(2);
            await Delete(2);
        }

        public async Task Read()
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest("https://localhost:7023/api/blog",Method.Get);
            RestResponse response = await client.ExecuteAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var jsonStr = response.Content;

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
            RestClient client = new RestClient();
            RestRequest request = new RestRequest($"https://localhost:7023/api/blog/{id}", Method.Get);
            RestResponse response = await client.ExecuteAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var jsonStr = response.Content;

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
                var jsonStr = response.Content;
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

            RestClient client = new RestClient();
            RestRequest request = new RestRequest("https://localhost:7023/api/blog", Method.Post);
            request.AddBody(blog);
            RestResponse response = await client.ExecuteAsync(request);

           if (response.IsSuccessStatusCode)
            {
                var jsonStr = response.Content;

                BlogResponseModel model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);
                Console.WriteLine(model.IsSuccess);
                Console.WriteLine(model.Message);
            }
        }

        public async Task Update(int id, string title, string author, string content)

        {
            BlogDataModel blog = new BlogDataModel
            {
                // BlogId = id,
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            RestClient client = new RestClient();
            RestRequest request = new RestRequest($"https://localhost:7023/api/blog/{id}", Method.Put);
            request.AddBody(blog);
            RestResponse response = await client.ExecuteAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var jsonStr = response.Content;

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

            RestClient client = new RestClient();
            RestRequest request = new RestRequest($"https://localhost:7023/api/blog/{id}", Method.Patch);
            request.AddBody(blog);
            RestResponse response = await client.ExecuteAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var jsonStr = response.Content;

                BlogResponseModel model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);
                Console.WriteLine(model.IsSuccess);
                Console.WriteLine(model.Message);

            }

        }

        public async Task Delete(int id)
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest($"https://localhost:7023/api/blog/{id}",Method.Delete);
            RestResponse response = await client.ExecuteAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var jsonStr = response.Content;

                BlogResponseModel model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);
                Console.WriteLine(model.IsSuccess);
                Console.WriteLine(model.Message);
            }
            else
            {
                var jsonStr = response.Content;
                BlogResponseModel model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);
                Console.WriteLine(model.Message);
            }
        }

    }
}
