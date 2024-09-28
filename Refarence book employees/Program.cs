using System;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using static System.Console;

namespace Refarence_book_employees
{
     class Program
    {

        static string filePath = "employees.txt";

        static void Main()
        {
            while (true)
            {
                WriteLine("Select action: Hello World 123");
                WriteLine("1 - Display data on screen");
                WriteLine("2 - Add new entry");
                WriteLine("0 - Exit");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ReadFromFile();
                        break;
                    case "2":
                        AddNewEmployee(); 
                        break;
                    case "0":
                        return;
                    default:
                        WriteLine("Wrong choice, try again.");
                        break;
                }
            }
        }
        


        #region Read File
        static void ReadFromFile()
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    string[] data = line.Split('#');
                    WriteLine($"ID: {data[0]}, Date added: {data[1]}, Full name: {data[2]}, Age: {data[3]}, Height: {data[4]}, Date of birth: {data[5]}, Plece of birth: {data[6]}");
                }
            }
            else
            {
                WriteLine("File not found");
            }
        }
        #endregion

        #region Add New Employee
        static void AddNewEmployee()
        {
            WriteLine("Enter full name: ");
            string fullName = ReadLine();

            WriteLine("Enter age: ");
            int age = int.Parse(ReadLine());

            WriteLine("Enter hight: ");
            int hight = int.Parse(ReadLine());

            WriteLine("Enter date of birth (dd.mm.yyyy): ");
            int birthDate = int.Parse(ReadLine());

            WriteLine("Enter place of birth: ");
            string birthPlace = ReadLine();

            DateTime now = DateTime.Now;

            string newRecord = $"{GetNextId()}#{now.ToString("dd.mm.yyyy hh.mm")}#{fullName}#{age}#{hight}#{birthDate}#{birthPlace}";
            
            //New entry at the end of the file
            using (StreamWriter sw = File.AppendText(filePath))
            {
                sw.WriteLine(newRecord);
            }

            WriteLine("Entry added.");

        }
        #endregion
        
        #region Get New ID
        static int GetNextId()
        {
            if (!File.Exists(filePath))
            {
                return 1;
            }

            string[] lines = File.ReadAllLines(filePath);
            if (lines.Length == 0)
            {
                return 1;
            }

            string lastLine = lines[lines.Length - 1];
            string[] data = lastLine.Split('#');
            int lastId = int.Parse(data[0]);

            return lastId + 1;
        }
        #endregion
     }
}
