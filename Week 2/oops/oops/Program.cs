
using Courses_Training.Models;
using TrainingManagementSystem.Models;

Course[] courses =
{
    new Course("CS101", "CSharp", 5000m, 30, 30),
    new Course("CS102", "Java", 6000m, 45, 25),
    new Course("CS103", "Python", 4500m, 20, 20),
    new Course("CS104", "Web Development", 7000m, 40, 35),
    new Course("CS105", "Database", 3500m, 15, 15)
};

foreach (Course course in courses)
{
    Console.WriteLine(
        $"{course.CourseCode} | {course.Title} | Fee: {course.Fee} | Available Seats: {course.AvailableSeats()}"
    );
}


Console.WriteLine("COURSE CATALOGUE");
Console.WriteLine("--------------------------------------------------------------");
Console.WriteLine($"{"Code",-10} {"Title",-22} {"Fee",-10} {"Seats",-10}");
Console.WriteLine("--------------------------------------------------------------");

foreach (Course course in courses)
{
    Console.WriteLine(
        $"{course.CourseCode,-10} {course.Title,-22} {course.Fee,-10} {course.AvailableSeats(),-10}"
    );
}
Console.Write("Enter course code: ");
string searchCode = Console.ReadLine() ?? "";

bool found = false;

foreach (Course course in courses)
{
    if (course.CourseCode.Equals(
        searchCode, StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine("Course found: " + course.Title);
        Console.WriteLine("Fee: " + course.Fee);
        found = true;
        break;
    }
}

if (!found)
{
    Console.WriteLine("Course not found.");
}
Console.Write("Enter course title: ");
string searchTitle = Console.ReadLine() ?? "";

found = false;

foreach (Course course in courses)
{
    if (course.Title.Contains(
        searchTitle, StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine("Course found: " + course.Title);
        Console.WriteLine("Course code: " + course.CourseCode);
        Console.WriteLine("Fee: " + course.Fee);
        found = true;
    }
}

if (!found)
{
    Console.WriteLine("Course not found.");
}

Console.WriteLine("1. Lowest fee first");
Console.WriteLine("2. Highest fee first");
Console.Write("Choose sorting order: ");

string choice = Console.ReadLine() ?? "";

if (choice == "1")
{
    Array.Sort(courses, (a, b) => a.Fee.CompareTo(b.Fee));
}
else if (choice == "2")
{
    Array.Sort(courses, (a, b) => b.Fee.CompareTo(a.Fee));
}
else
{
    Console.WriteLine("Invalid choice.");
}

foreach (Course course in courses)
{
    Console.WriteLine($"{course.Title} - {course.Fee}");
}





}
