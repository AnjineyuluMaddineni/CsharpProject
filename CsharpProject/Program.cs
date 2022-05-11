using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace CsharpProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Student student1 = new Student()
            {
                Id = 1,
                Name = "Anjineyulu1",
            };
            Student student2 = new Student()
            {
                Id = 2,
                Name = "Anjineyulu2",
            };
            //step 01: Get students from Database

            Console.WriteLine("Getting Connection ...");
            var datasource = @"Sitecore10VM";//your server
            var database = "CsharpDB"; //your database name
            var username = "sa"; //username of server to connect
            var password = "123456"; //password

            //your connection string 
            string connString = @"Data Source=" + datasource + ";Initial Catalog="
                        + database + ";Persist Security Info=True;User ID=" + username + ";Password=" + password;

            //create instanace of database connection
            SqlConnection conn = new SqlConnection(connString);

            try
            {
                Console.WriteLine("Openning Connection ...");

                //open connection
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Student", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    //do work here
                    int id = Convert.ToInt32(reader["Id"]);
                    string name = reader["Name"].ToString();
                    Console.WriteLine(id+""+name+"");
                }
                reader.Close();

                Console.WriteLine("Connection successful!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            finally
            {
                conn.Close();
            }
            List<Student> students = new List<Student>();
            students.Add(student1);
            students.Add(student2);

            //Step 02: Store Students  in Json file
            Console.WriteLine("Step 1 completed");

            string strResultJson= JsonConvert.SerializeObject(students);
            File.WriteAllText(@"C:\ProjectMode\CsharpProject\CsharpProject\Files\student.json", strResultJson);
            Console.WriteLine("Step 2 serialize completed!");

            //Step 03: Deserialize json data to list of student 
            strResultJson = String.Empty;
            strResultJson = File.ReadAllText(@"C:\ProjectMode\CsharpProject\CsharpProject\Files\student.json");
            var collection = JsonConvert.DeserializeObject<List<Student>>(strResultJson);
            Console.WriteLine("Step 03 Deserialize completed");

            foreach (var item in collection)
            {
                Console.WriteLine(item.Id);
                Console.WriteLine(item.Name);
            }

            Console.ReadLine();
        }
    }
}
