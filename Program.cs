using System.Text.Json;

class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

class People
{
    static void Main()
    {
        // 1. Load existing people (if file exists)
        List<Person> people = LoadPeopleFromJson();

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("=== People Manager ===");
            Console.WriteLine("1) Add a person");
            Console.WriteLine("2) Show all people");
            Console.WriteLine("3) Exit");
            Console.Write("Choose an option (1-3): ");
            string? choice = Console.ReadLine();

            if (choice == "1")
            {
                AddPerson(people);
            }
            else if (choice == "2")
            {
                ShowPeople(people);
            }
            else if (choice == "3")
            {
                SavePeopleToJson(people);
                Console.WriteLine("Goodbye!");
                break;
            }
            else
            {
                Console.WriteLine("Invalid option, choose 1, 2, or 3.");
            }
        }
    }

    // Load people from JSON file if it exists
    private static List<Person> LoadPeopleFromJson()
    {
        if (!File.Exists("people.json"))
        {
            return new List<Person>();
        }

        string existingJson = File.ReadAllText("people.json");
        if (string.IsNullOrWhiteSpace(existingJson))
        {
            return new List<Person>();
        }

        List<Person>? loaded = JsonSerializer.Deserialize<List<Person>>(existingJson);
        return loaded ?? new List<Person>();
    }

    // Save people to JSON file
    private static void SavePeopleToJson(List<Person> people)
    {
        string json = JsonSerializer.Serialize(people, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        File.WriteAllText("people.json", json);
        Console.WriteLine("Saved to people.json");
    }

    // Add a single person
    private static void AddPerson(List<Person> people)
    {
        Console.Write("Enter first name: ");
        string? firstName = Console.ReadLine();

        Console.Write("Enter last name: ");
        string? lastName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
        {
            Console.WriteLine("First and last name cannot be empty.");
            return;
        }

        Person p = new Person
        {
            FirstName = firstName,
            LastName = lastName
        };

        // Add to list and save
        people.Add(p);
        SavePeopleToJson(people); // Save after adding
        Console.WriteLine("Person added.");
    }

    // Show all people
    private static void ShowPeople(List<Person> people)
    {
        if (people.Count == 0)
        {
            Console.WriteLine("No people stored yet.");
            return;
        }

        Console.WriteLine("People stored:");
        int index = 1;
        foreach (Person person in people)
        {
            Console.WriteLine($"{index}) {person.FirstName} {person.LastName}");
            index++;
        }
    }
}
