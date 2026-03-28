using System;
using System.Collections.Generic;
using System.Linq;

namespace BibliotekaKonsolowa
{
    // Klasa reprezentująca książkę
    class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string Publisher { get; set; } // NOWE

        public override string ToString()
        {
            return $"[{Year}] \"{Title}\" - {Author}, {Publisher}";
        }
    }

    class Program
    {
        static List<Book> library = new List<Book>();
        //test
        static void Main(string[] args)
        {
            // Przykładowe dane na start
            library.Add(new Book { Title = "Wiedźmin", Author = "Andrzej Sapkowski", Year = 1993, Publisher = "SuperNOWA" });
            library.Add(new Book { Title = "Hobbit", Author = "J.R.R. Tolkien", Year = 1937, Publisher = "Allen & Unwin" });

            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("=== SYSTEM ZARZĄDZANIA BIBLIOTEKĄ ===");
                Console.WriteLine("1. Dodaj książkę");
                Console.WriteLine("2. Usuń książkę");
                Console.WriteLine("3. Wyświetl wszystkie książki");
                Console.WriteLine("4. Wyszukaj książkę");
                Console.WriteLine("5. Sortuj książki");
                Console.WriteLine("0. Wyjście");
                Console.Write("\nWybierz opcję: ");

                switch (Console.ReadLine())
                {
                    case "1": AddBook(); break;
                    case "2": RemoveBook(); break;
                    case "3": ShowBooks(library); break;
                    case "4": SearchBook(); break;
                    case "5": SortBooks(); break;
                    case "0": running = false; break;
                    default: Console.WriteLine("Nieprawidłowa opcja! Naciśnij dowolny klawisz..."); Console.ReadKey(); break;
                }
            }
        }

        static void AddBook()
        {
            Console.Write("Podaj tytuł: ");
            string title = Console.ReadLine();

            Console.Write("Podaj autora: ");
            string author = Console.ReadLine();

            Console.Write("Podaj wydawnictwo: "); // NOWE
            string publisher = Console.ReadLine();

            Console.Write("Podaj rok wydania: ");
            if (int.TryParse(Console.ReadLine(), out int year))
            {
                library.Add(new Book
                {
                    Title = title,
                    Author = author,
                    Year = year,
                    Publisher = publisher // NOWE
                });
                Console.WriteLine("Książka dodana pomyślnie!");
            }
            else
            {
                Console.WriteLine("Błędny rok!");
            }
            Console.ReadKey();
        }

        static void ShowBooks(List<Book> booksToShow)
        {
            Console.WriteLine("\n--- Lista Książek ---");
            if (booksToShow.Count == 0) Console.WriteLine("Baza jest pusta.");

            for (int i = 0; i < booksToShow.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {booksToShow[i]}");
            }
            Console.WriteLine("\nNaciśnij dowolny klawisz...");
            Console.ReadKey();
        }

        static void RemoveBook()
        {
            Console.WriteLine("Podaj numer książki do usunięcia:");
            for (int i = 0; i < library.Count; i++) Console.WriteLine($"{i + 1}. {library[i].Title}");

            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= library.Count)
            {
                library.RemoveAt(index - 1);
                Console.WriteLine("Usunięto.");
            }
            else Console.WriteLine("Błędny numer.");
            Console.ReadKey();
        }

        static void SearchBook()
        {
            Console.Write("Wpisz fragment tytułu lub autora: ");
            string phrase = Console.ReadLine().ToLower();
            var results = library.Where(b => b.Title.ToLower().Contains(phrase) || b.Author.ToLower().Contains(phrase)).ToList();

            Console.WriteLine("\nWyniki wyszukiwania:");
            ShowBooks(results);
        }

        static void SortBooks()
        {
            Console.WriteLine("Sortuj według: 1.Tytułu, 2.Autora, 3.Roku, 4.Wydawnictwa");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": library = library.OrderBy(b => b.Title).ToList(); break;
                case "2": library = library.OrderBy(b => b.Author).ToList(); break;
                case "3": library = library.OrderBy(b => b.Year).ToList(); break;
                case "4": library = library.OrderBy(b => b.Publisher).ToList(); break; // NOWE
                default:
                    Console.WriteLine("Nieprawidłowa opcja!");
                    break;
            }

            Console.WriteLine("Posortowano!");
            Console.ReadKey();
        }
    }
}