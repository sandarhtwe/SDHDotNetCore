using HWDotNetCore.ConsoleApp.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HWDotNetCore.ConsoleApp.RestClientExamples
{
    internal class RestClientExample
    {
        public async Task Run()
        {
            //await Read();
            await Edit(5);
           // await Create("test 2", "test 2",23,"test 3", "test 4");
            //await Update(1, "sandar", "sandar",26,"test 3", "test6");
            await Patch(5, "Sandar 1", null, 24,null,null);
            await Edit(5);
            //await Edit(1);
            //await Delete(15);
        }

        public async Task Read()
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest("https://localhost:7185/api/Student", Method.Get);
            RestResponse response = await client.ExecuteAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var jsonStr = response.Content;

                //Json to C# Object => DeserializeObject
                //C# to Json => SerializeObject
                List<StudentDataModel> lst = JsonConvert.DeserializeObject<List<StudentDataModel>>(jsonStr);
               // List<StudentDataModel> lst = JsonConvert.DeserializeObject<List<StudentDataModel>>(jsonStr);
                foreach (StudentDataModel item in lst)
                {
                    Console.WriteLine(item.StudentId);
                    Console.WriteLine(item.Student_Name);
                    Console.WriteLine(item.Student_PhNo);
                    Console.WriteLine(item.Age);
                    Console.WriteLine(item.Subject);
                    Console.WriteLine(item.Address);
                    Console.WriteLine("--------------------------------------");
                }
            }

        }

        public async Task Edit(int id)
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest($"https://localhost:7185/api/Student/{id}", Method.Get);
            RestResponse response = await client.ExecuteAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var jsonStr = response.Content;

                StudentResponseModel model = JsonConvert.DeserializeObject<StudentResponseModel>(jsonStr);
                if (model.IsSuccess)
                {
                    var item = model.Data;
                    Console.WriteLine(item.StudentId);
                    Console.WriteLine(item.Student_Name);
                    Console.WriteLine(item.Student_PhNo);
                    Console.WriteLine(item.Age);
                    Console.WriteLine(item.Subject);
                    Console.WriteLine(item.Address);
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
                StudentResponseModel model = JsonConvert.DeserializeObject<StudentResponseModel>(jsonStr);
                Console.WriteLine(model.Message);
            }

        }

        public async Task Create(string name, string ph_no,int age,string subject, string address)

        {
            StudentDataModel student = new StudentDataModel
            {
                Student_Name = name,
                Student_PhNo = ph_no,
                Age = age,
                Subject = subject,
                Address = address
            };

            RestClient client = new RestClient();
            RestRequest request = new RestRequest("https://localhost:7185/api/Student", Method.Post);
            request.AddBody(student);
            RestResponse response = await client.ExecuteAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var jsonStr = response.Content;

                StudentResponseModel model = JsonConvert.DeserializeObject<StudentResponseModel>(jsonStr);
                Console.WriteLine(model.IsSuccess);
                Console.WriteLine(model.Message);
            }
        }

        public async Task Update(int id,string name, string ph_no, int age, string subject, string address)

        {
            StudentDataModel student = new StudentDataModel
            {
                Student_Name = name,
                Student_PhNo = ph_no,
                Age = age,
                Subject = subject,
                Address = address
            };
            RestClient client = new RestClient();
            RestRequest request = new RestRequest($"https://localhost:7185/api/Student/{id}", Method.Put);
            request.AddBody(student);
            RestResponse response = await client.ExecuteAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var jsonStr = response.Content;

                StudentResponseModel model = JsonConvert.DeserializeObject<StudentResponseModel>(jsonStr);
                Console.WriteLine(model.IsSuccess);
                Console.WriteLine(model.Message);

            }

        }

        public async Task Patch(int id, string name, string ph_no, int age, string subject, string address)

        {
            StudentDataModel student = new StudentDataModel();

            if (!string.IsNullOrWhiteSpace(name))
            {
                student.Student_Name = name;
            }
            if (!string.IsNullOrWhiteSpace(ph_no))
            {
                student.Student_PhNo = ph_no;
            }
            if (age != 0)
            {
                student.Age = age;
            }
            if (!string.IsNullOrWhiteSpace(subject))
            {
                student.Subject = subject;
            }
            if (!string.IsNullOrWhiteSpace(address))
            {
                student.Address = address;
            }
            RestClient client = new RestClient();
            RestRequest request = new RestRequest($"https://localhost:7185/api/Student/{id}", Method.Patch);
            request.AddBody(student);
            RestResponse response = await client.ExecuteAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var jsonStr = response.Content;

                StudentResponseModel model = JsonConvert.DeserializeObject<StudentResponseModel>(jsonStr);
                Console.WriteLine(model.IsSuccess);
                Console.WriteLine(model.Message);

            }

        }

        public async Task Delete(int id)
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest($"https://localhost:7185/api/Student/{id}", Method.Delete);
            RestResponse response = await client.ExecuteAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var jsonStr = response.Content;

                StudentResponseModel model = JsonConvert.DeserializeObject<StudentResponseModel>(jsonStr);
                Console.WriteLine(model.IsSuccess);
                Console.WriteLine(model.Message);
            }
            else
            {
                var jsonStr = response.Content;
                StudentResponseModel model = JsonConvert.DeserializeObject<StudentResponseModel>(jsonStr);
                Console.WriteLine(model.Message);
            }
        }


    }
}
