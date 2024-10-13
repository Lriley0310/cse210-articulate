using System;
using System.Collections.Generic;
using System.IO;

namespace JournalApp
{
    // Class to represent a single journal entry
    public class JournalEntry
    {
        public string Date { get; set; }
        public string Prompt { get; set; }
        public string Response { get; set; }

        public JournalEntry(string prompt, string response)
        {
            Date = DateTime.Now.ToString("yyyy-MM-dd");
            Prompt = prompt;
            Response = response;
        }

        public override string ToString()
        {
            return $"Date: {Date}\nPrompt: {Prompt}\nResponse: {Response}\n";
        }
    }

    // Class to represent the entire journal and its operations
    public class Journal
    {
        private List<JournalEntry> entries = new List<JournalEntry>();
        private List<string> prompts = new List<string>
        {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?"
        };

        // Add a new journal entry
        public void AddEntry()
        {
            var random = new Random();
            int index = random.Next(prompts.Count);
            string prompt = prompts[index];

            Console.WriteLine(prompt);
            Console.Write("Your response: ");
            string response = Console.ReadLine();

            // Add the entry to the list
            entries.Add(new JournalEntry(prompt, response));
        }

        // Display all journal entries
        public void DisplayJournal()
        {
            if (entries.Count == 0)
            {
                Console.WriteLine("No journal entries found.");
            }
            else
            {
                foreach (JournalEntry entry in entries)
                {
                    Console.WriteLine(entry);
                }
            }
        }

        // Save the journal entries to a file
        public void SaveJournal(string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (var entry in entries)
                {
                    // Use | as a separator between data fields
                    writer.WriteLine($"{entry.Date}|{entry.Prompt}|{entry.Response}");
                }
            }
            Console.WriteLine("Journal saved to file.");
        }

        // Load journal entries from a file
        public void LoadJournal(string filename)
        {
            if (File.Exists(filename))
            {
                entries.Clear();
                string[] lines = File.ReadAllLines(filename);

                foreach (var line in lines)
                {
                    string[] parts = line.Split('|');
                    entries.Add(new JournalEntry(parts[1], parts[2]) { Date = parts[0] });
                }
                Console.WriteLine("Journal loaded from file.");
            }
            else
            {
                Console.WriteLine("File not found.");
            }
        }
    }

    // Main class to run the program
    class Program
    {
        static void Main(string[] args)
        {
            Journal journal = new Journal();
            string userChoice = "";

            while (userChoice != "5")
            {
                Console.WriteLine("1. Write a new entry");
                Console.WriteLine("2. Display the journal");
                Console.WriteLine("3. Save the journal to a file");
                Console.WriteLine("4. Load the journal from a file");
                Console.WriteLine("5. Exit");
                Console.Write("Select an option: ");
                userChoice = Console.ReadLine();

                switch (userChoice)
                {
                    case "1":
                        journal.AddEntry();
                        break;
                    case "2":
                        journal.DisplayJournal();
                        break;
                    case "3":
                        Console.Write("Enter the filename to save: ");
                        string saveFile = Console.ReadLine();
                        journal.SaveJournal(saveFile);
                        break;
                    case "4":
                        Console.Write("Enter the filename to load: ");
                        string loadFile = Console.ReadLine();
                        journal.LoadJournal(loadFile);
                        break;
                    case "5":
                        Console.WriteLine("Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
    }
}
