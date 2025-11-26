using System.Text.Json;

class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string AGE { get; set; }
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
            Console.WriteLine("3) Update people");
            Console.WriteLine("4) Remove people");
            Console.WriteLine("5) search person");
            Console.WriteLine("6) Exit");
            Console.Write("Choose an option (1-6): ");
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
                UpdatePerson(people);
            }
            else if (choice == "4")
            {
                DeletePerson(people);
            }
            else if (choice == "5")
            {
                SearchPerson(people);
            }
            else if (choice == "6")
            {
                SavePeopleToJson(people);
                Console.WriteLine("Goodbye!");
                break;
            }
            else
            {
                Console.WriteLine("Invalid option, choose 1, 2, 3, 4 or 5.");
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

        Console.Write("Enter Age: ");
        string? age = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(age))
        {
            Console.WriteLine("First and last name , age cannot be empty.");
            return;
        }

        Person p = new Person
        {
            FirstName = firstName,
            LastName = lastName,
            AGE = age
        };

        // Add to list and save
        people.Add(p);
        SavePeopleToJson(people); // Save after adding
        Console.WriteLine("Person added.");
    }

    // Update a person's details
    private static void UpdatePerson(List<Person> people)
    {
        if (people.Count == 0)
        {
            Console.WriteLine("No people to update.");
            return;
        }

        ShowPeople(people);
        Console.Write("Enter the number of the person to update: ");
        string? input = Console.ReadLine();
        if (!int.TryParse(input, out int index) || index < 1 || index > people.Count)
        {
            Console.WriteLine("Invalid selection.");
            return;
        }

        Person personToUpdate = people[index - 1];

        Console.Write($"Enter new first name (current: {personToUpdate.FirstName}): ");
        string? newFirstName = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newFirstName))
        {
            personToUpdate.FirstName = newFirstName;
        }

        Console.Write($"Enter new last name (current: {personToUpdate.LastName}): ");
        string? newLastName = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newLastName))
        {
            personToUpdate.LastName = newLastName;
        }

        Console.Write($"Enter new age (current: {personToUpdate.AGE}): ");
        string? newAge = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newAge))
        {
            personToUpdate.AGE = newAge;
        }

        SavePeopleToJson(people); // Save after updating
        Console.WriteLine("Person updated.");
    }

    // Delete a person
    private static void DeletePerson(List<Person> people)
    {
        if (people.Count == 0)
        {
            Console.WriteLine("No people to delete.");
            return;
        }

        ShowPeople(people);
        Console.Write("Enter the number of the person to delete: ");
        string? input = Console.ReadLine();
        if (!int.TryParse(input, out int index) || index < 1 || index > people.Count)
        {
            Console.WriteLine("Invalid selection.");
            return;
        }

        people.RemoveAt(index - 1);
        SavePeopleToJson(people); // Save after deleting
        Console.WriteLine("Person removed.");
    }

    // Search for a person
    private static void SearchPerson(List<Person> people)
    {
        Console.WriteLine("Search by name: ");
        string? searchTerm = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            Console.WriteLine(" Search term cannot be empty.");
            return;
        }
        var results = people.Where(p => p.FirstName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) || p.LastName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
        if (results.Count == 0)
        {
            Console.WriteLine("No matching people found.");
            return;
        }
        Console.WriteLine("Search results:");
        foreach (var person in results)
        {
            Console.WriteLine($"{person.FirstName} {person.LastName} Age: {person.AGE}");
        }
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
