
using TrainingManagementSystem.Models;

namespace TrainingManagementSystem.Models
{
    public class Learner
    {
        public string LearnerId { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }

        public Learner(string learnerId, string name,
                       string contactNumber, string email)
        {
            LearnerId = learnerId;
            Name = name;
            ContactNumber = contactNumber;
            Email = email;
        }
    }
}

using System.Linq;
using TrainingManagementSystem.Models;
Dictionary<string, Learner> learners =new Dictionary<string, Learner>();

while (true)
{
    Console.WriteLine("\n--- LEARNER MANAGEMENT ---");
    Console.WriteLine("1. Register Learner");
    Console.WriteLine("2. Display All Learners");
    Console.WriteLine("3. Search Learner by ID");
    Console.WriteLine("4. Remove Learner");
    Console.WriteLine("5. Exit");
    Console.Write("Enter choice: ");

    string choice = Console.ReadLine() ?? "";

    switch (choice)
    {
        case "1":
            Console.Write("Enter learner ID: ");
            string id = Console.ReadLine() ?? "";

            Console.Write("Enter learner name: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("Enter contact number: ");
            string contact = Console.ReadLine() ?? "";

            Console.Write("Enter email: ");
            string email = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(id) ||
                string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("ID and name cannot be empty.");
            }
            else if (learners.ContainsKey(id))
            {
                Console.WriteLine("Learner ID already exists.");
            }
            else if (contact.Length != 10 ||
                     !contact.All(char.IsDigit))
            {
                Console.WriteLine(
                    "Contact number must contain exactly 10 digits.");
            }
            else if (!email.Contains('@') ||
                     !email.Contains('.'))
            {
                Console.WriteLine("Enter a valid email address.");
            }
            else
            {
                Learner learner = new Learner(
                    id, name, contact, email);

                learners.Add(id, learner);

                Console.WriteLine("Learner registered successfully.");
            }
            break;

        case "2":
            if (learners.Count == 0)
            {
                Console.WriteLine("No learners registered.");
            }
            else
            {
                foreach (Learner learner in learners.Values)
                {
                    Console.WriteLine(
                        $"{learner.LearnerId} | " +
                        $"{learner.Name} | " +
                        $"{learner.ContactNumber} | " +
                        $"{learner.Email}");
                }
            }
            break;

        case "3":
            Console.Write("Enter learner ID to search: ");
            string searchId = Console.ReadLine() ?? "";

            if (learners.TryGetValue(searchId, out Learner? foundLearner))
            {
                Console.WriteLine("Learner found!");
                Console.WriteLine("ID: " + foundLearner.LearnerId);
                Console.WriteLine("Name: " + foundLearner.Name);
                Console.WriteLine("Contact: " + foundLearner.ContactNumber);
                Console.WriteLine("Email: " + foundLearner.Email);
            }
            else
            {
                Console.WriteLine("Learner not found.");
            }
            break;

        case "4":
            Console.Write("Enter learner ID to remove: ");
            string removeId = Console.ReadLine() ?? "";

            if (learners.Remove(removeId))
            {
                Console.WriteLine("Learner removed successfully.");
            }
            else
            {
                Console.WriteLine("Learner ID not found.");
            }
            break;

        case "5":
            Console.WriteLine("Exiting learner management.");
            return;

        default:
            Console.WriteLine("Invalid choice. Try again.");
            break;
    }
}
