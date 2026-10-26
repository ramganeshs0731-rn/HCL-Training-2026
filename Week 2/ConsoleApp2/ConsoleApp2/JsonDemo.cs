using System;
using System.Text.Json;

namespace ConsoleApp2;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Student(string name, int id)
    {
        Name = name;
        Id = id;
    }
}

    public  class JsonDemo
    {
        public void Meth()
        {
            Student S1 = new Student("John", 1);
            string s = JsonSerializer.Serialize(S1);
            Student s2 = JsonSerializer.Deserialize<Student>(s);
            Console.WriteLine(s2?.Name);
        }
    }



    //Deserialized




