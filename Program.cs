using System;
using System.Collections.Generic;
using System.IO;
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
        // 1. Create the list
        List<Person> people = new List<Person>();

        // 2. Load existing people from JSON if the file exists
        if (File.Exists("people.json"))
        {
            string existingJson = File.ReadAllText("people.json");
            if (!string.IsNullOrWhiteSpace(existingJson))
            {
                List<Person>? loaded = JsonSerializer.Deserialize<List<Person>>(existingJson);
                if (loaded != null)
                {
                    people = loaded;
                }
            }
        }

        // 3. Let the user add multiple people
        while (true)
        {
            Console.Write("Do you want to add a person? (y/n): ");
            string? choice = Console.ReadLine();

            if (string.Equals(choice, "n", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

            if (!string.Equals(choice, "y", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Please enter 'y' or 'n'.");
                continue;
            }

            Console.Write("Enter first name: ");
            string? firstName = Console.ReadLine();

            Console.Write("Enter last name: ");
            string? lastName = Console.ReadLine();

            Person p = new Person
            {
                FirstName = firstName ?? string.Empty,
                LastName = lastName ?? string.Empty
            };

            people.Add(p);
            Console.WriteLine("Person added.\n");
        }

        // 4. Save all people back to JSON
        string json = JsonSerializer.Serialize(people, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        File.WriteAllText("people.json", json);
        Console.WriteLine("Saved to people.json\n");

        // 5. Show all stored people
        Console.WriteLine("People stored in JSON:");
        foreach (Person person in people)
        {
            Console.WriteLine(person.FirstName + " " + person.LastName);
        }
    }
}