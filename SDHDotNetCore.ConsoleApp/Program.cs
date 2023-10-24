using HKSDotNetCore.ConsoleApp.DapperExamples;
using SDHDotNetCore.ConsoleApp.AdoDotNetExamples;
using SDHDotNetCore.ConsoleApp.EFCoreExamples;
using SDHDotNetCore.ConsoleApp.HttpClientExamples;
using SDHDotNetCore.ConsoleApp.RestClientExamples;
using System.Data;
using System.Data.SqlClient;

internal class Program
{
    private static async Task Main(string[] args)
    {
        #region Test
        //int myNum = 5;               // Integer (whole number)
        //double myDoubleNum = 5.99D;  // Floating point number
        //char myLetter = 'D';         // Character
        //bool myBool = true;          // Boolean
        //string myText = "Hello";     // String

        //Console.WriteLine("Hello, World!");

        //Console.WriteLine(myNum);
        //Console.WriteLine(myDoubleNum);
        //Console.WriteLine(myLetter);
        //Console.WriteLine(myBool);
        //Console.WriteLine(myText);
        //Console.WriteLine();

        ////Implicit Convertion (auto convert small size to large size)
        //int myInt = 9;
        //double myDouble = myInt;       // Automatic casting: int to double

        //Console.WriteLine(myInt);      
        //Console.WriteLine(myDouble);
        //Console.WriteLine();

        ////Explicit Conversion ( can't convert large size to small size. So,need to use manual casting)
        //double myDouble1 = 9.78;
        //int myInt1 = (int)myDouble;    // Manual casting: double to int

        //Console.WriteLine(myDouble1);   // Outputs 9.78
        //Console.WriteLine(myInt1);      // Outputs 9

        //int myInt2 = 10;
        //double myDouble2 = 5.25;
        //bool myBool2 = true;

        //Console.WriteLine(Convert.ToString(myInt2));    // convert int to string
        //Console.WriteLine(Convert.ToDouble(myInt2));    // convert int to double
        //Console.WriteLine(Convert.ToInt32(myDouble2));  // convert double to int
        //Console.WriteLine(Convert.ToString(myBool2));   // convert bool to string
        ////Console.WriteLine("Press any key to close this window . . .");

        //Console.WriteLine("Enter username:");
        //string userName = Console.ReadLine();
        //Console.WriteLine("Username is: " + userName);

        //Console.WriteLine("Enter your age:");
        //int age = Convert.ToInt32(Console.ReadLine());
        //Console.WriteLine("Your age is: " + age);

        //int sum1 = 100 + 50;        // 150 (100 + 50)
        //int sum2 = sum1 + 250;      // 400 (150 + 250)
        //int sum3 = sum2 + sum2;     // 800 (400 + 400)

        //int x = 5;
        //int y = 3;
        //Console.WriteLine(x > y);

        //string firstName = "John ";
        //string lastName = "Doe";
        //string name = string.Concat(firstName, lastName);
        //Console.WriteLine(name);

        //string firstName1 = "John";
        //string lastName1 = "Doe";
        //string name1 = $"My full name is: {firstName} {lastName}";
        //Console.WriteLine(name1);

        //string name2 = "John Doe";
        //int charPos = name.IndexOf("D");
        //string lastName2 = name.Substring(charPos);
        //Console.WriteLine(lastName2);

        //int time = 22;
        //if (time < 10)
        //{
        //    Console.WriteLine("Good morning.");
        //}
        //else if (time < 20)
        //{
        //    Console.WriteLine("Good day.");
        //}
        //else
        //{
        //    Console.WriteLine("Good evening.");
        //}

        //int day = 4;
        //switch (day)
        //{
        //    case 1:
        //        Console.WriteLine("Monday");
        //        break;
        //    case 2:
        //        Console.WriteLine("Tuesday");
        //        break;
        //    case 3:
        //        Console.WriteLine("Wednesday");
        //        break;
        //    case 4:
        //        Console.WriteLine("Thursday");
        //        break;
        //    case 5:
        //        Console.WriteLine("Friday");
        //        break;
        //    case 6:
        //        Console.WriteLine("Saturday");
        //        break;
        //    case 7:
        //        Console.WriteLine("Sunday");
        //        break;
        //}

        //int i = 0;
        //while (i < 5)
        //{
        //    Console.WriteLine(i);
        //    i++;
        //}

        //int j = 0;
        //do
        //{
        //    Console.WriteLine(j);
        //    j++;
        //}
        //while (j < 5);

        //for (int a = 1; a <= 2; ++a)
        //{
        //    Console.WriteLine("Outer: " + a); 

        //    // Inner loop
        //    for (int b = 1; b <= 3; b++)
        //    {
        //        Console.WriteLine(" Inner: " + b);  
        //    }
        //}

        //string[] cars = { "Volvo", "BMW", "Ford", "Mazda" };
        //foreach (string c in cars)
        //{
        //    Console.WriteLine(c);
        //}
        #endregion

        #region ADORead

        //SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        //{
        //    DataSource = "DESKTOP-DDE6MVJ\\TESTINGSDH",
        //    InitialCatalog = "HKSDotNetCore",
        //    UserID = "Sa",
        //    Password = "Sdh@1234"
        //};
        //SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
        //// open the connection
        //connection.Open();
        //Console.WriteLine("Connection opening...");
        //Console.WriteLine("Connection opened.");
        //Console.WriteLine();

        //string query = "select * from tbl_blog"; //Read data from table
        //SqlCommand command = new SqlCommand(query, connection); //Write query
        //SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command); //Create new query and add query
        //DataTable dt = new DataTable(); 
        //sqlDataAdapter.Fill(dt);

        //string queryforStudent = "select * from tbl_student"; //Read data from table
        //SqlCommand command1 = new SqlCommand(queryforStudent, connection); //Write query
        //SqlDataAdapter adapter = new SqlDataAdapter(command1); //Create new query and add query
        //DataTable dt_Student = new DataTable();
        //adapter.Fill(dt_Student);

        //connection.Close();
        //Console.WriteLine("Connection closed.");
        //Console.WriteLine();

        //foreach (DataRow dr in dt.Rows)
        //{
        //    Console.WriteLine(dr["BlogID"]);
        //    Console.WriteLine(dr["BlogTitle"]);
        //    Console.WriteLine(dr["BlogAuthor"]);
        //    Console.WriteLine(dr["BlogContent"]);
        //    Console.WriteLine("------------------------------");
        //}

        //Console.WriteLine("*****************************************************************");
        //Console.WriteLine();
        //Console.WriteLine("These are lists of Student....");
        //Console.WriteLine();

        //foreach (DataRow dr in dt_Student.Rows)
        //{
        //    Console.WriteLine(dr["StudentId"]);
        //    Console.WriteLine(dr["Name"]);
        //    Console.WriteLine(dr["Ph_no"]);
        //    Console.WriteLine(dr["Age"]);
        //    Console.WriteLine(dr["Subject"]);
        //    Console.WriteLine(dr["Address"]);
        //    Console.WriteLine("------------------------------");
        //}
        #endregion

        //AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
        //adoDotNetExample.Run();

        //DapperExample dapperExample = new DapperExample();
        //dapperExample.Run();

        //EFCoreExample eFCoreExample = new EFCoreExample();
        //eFCoreExample.Run();

       // Console.WriteLine("Waiting for API... when it is ready, please Enter.");
        //Console.ReadKey();

        //HttpClientExample httpClientExample = new HttpClientExample();
        //await httpClientExample.Run();

        RestClientExample restClientExample = new RestClientExample();
        await restClientExample.Run();

        Console.ReadKey();
    }
}