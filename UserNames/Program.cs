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
        List<Person> people = new List<Person>();

        Console.Write("Enter first name: ");
        string firstName = Console.ReadLine();

        Console.Write("Enter last name: ");
        string lastName = Console.ReadLine();

        Person p = new Person
        {
            FirstName = firstName,
            LastName = lastName
        };

        people.Add(p);

        // SAVE TO JSON HERE 
        string json = JsonSerializer.Serialize(people, new JsonSerializerOptions
        {
            WriteIndented = true // make the JSON look nice
        });

        File.WriteAllText("people.json", json);

        Console.WriteLine("Saved to people.json");

        // SHOW STORED PEOPLE
        Console.WriteLine("Person stored :");
        foreach (Person person in people)
        {
            Console.WriteLine(person.FirstName + " " + person.LastName);
        }
    }
}
