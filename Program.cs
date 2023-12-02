using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace L28_personnelAccounting
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int CommandAddDossier = 1;
            const int CommandDisplayAllDossier = 2;
            const int CommandDeleteDossier = 3;
            const int CommandSearchByLastName = 4;
            const int CommandExit = 5;

            string[] persons = new string[0];
            string[] jobTitles = new string[0];
            char delimiter = '-';
            int displayAllPersons = -1;
            int numberMenu;
            bool isOpen = true;


            while (isOpen)
            {
                Console.Clear();
                Console.WriteLine("Реестр компании \"Рога и копыта\"");
                Console.WriteLine($"{CommandAddDossier}) Добавить досье.\n{CommandDisplayAllDossier}) Вывести все досье.\n{CommandDeleteDossier}) Удалить досье." +
                                  $"\n{CommandSearchByLastName}) Поиск по фамилии.\n{CommandExit}) Выход.\n");
                Console.Write("Выбирите номер меню: ");
                numberMenu = Convert.ToInt32(Console.ReadLine());
                Console.Clear();

                switch (numberMenu)
                {
                    case CommandAddDossier:
                        AddDossier(ref persons, ref jobTitles);
                        continue;

                    case CommandDisplayAllDossier:
                        FormatOutput(persons, jobTitles, displayAllPersons, delimiter);
                        break;

                    case CommandDeleteDossier:
                        DeleteDossier(ref persons, ref jobTitles);
                        break;

                    case CommandSearchByLastName:
                        SearchByLastName(persons, jobTitles, delimiter);
                        break;

                    case CommandExit:
                        isOpen = false;
                        continue;

                    default:
                        Console.WriteLine("Введена неизвестная команда.");
                        break;
                }

                Console.WriteLine("\nДля возврата в меню, нажмите любую клавишу.");
                Console.ReadKey();
            }
        }

        private static void SearchByLastName(string[] persons, string[] jobTitles, char delimiter)
        {
            Console.Write("Введите фамилию для поиска досье: ");
            string lastName = Console.ReadLine().ToLower();
            Console.Clear();
            int indexOfPerson = SearchByWord(persons, lastName);

            if (indexOfPerson > -1)
                FormatOutput(persons, jobTitles, indexOfPerson, delimiter);
            else
                Console.WriteLine($"\nЧеловек с фамилией {lastName}, в базе данных не найден.");
        }

        private static void DeleteDossier(ref string[] persons, ref string[] jobTitles)
        {
            Console.Write("Введите номер досье, для его удаления: ");
            int indexOfPerson = Convert.ToInt32(Console.ReadLine()) - 1;

            if (indexOfPerson < persons.Length && indexOfPerson > -1)
            {
                persons = DeleteElement(persons, indexOfPerson);
                jobTitles = DeleteElement(jobTitles, indexOfPerson);
            }
            else
            {
                Console.WriteLine($"Досье под номером {indexOfPerson}, нет в базе данных.");
            }
        }

        private static void AddDossier(ref string[] persons, ref string[] jobTitles)
        {
            Console.Write("Введите полностью Фамилию Имя Отчество: ");
            string addedPerson = Console.ReadLine();
            Console.Write("Введите наименование должности: ");
            string jobTitle = Console.ReadLine();
            persons = ExpandArray(persons, addedPerson);
            jobTitles = ExpandArray(jobTitles, jobTitle);
        }

        static string[] ExpandArray(string[] array, string element)
        {
            string[] tempArray = new string[array.Length + 1];

            for (int i = 0; i < array.Length; i++)
                tempArray[i] = array[i];

            tempArray[array.Length] = element;
            return tempArray;
        }

        static void FormatOutput(string[] persons, string[] jobTitles, int indexOfPerson, char delimiter)
        {
            Console.Clear();
            string[] tempLines;

            if (indexOfPerson < 0)
            {
                for (int i = 0; i < persons.Length; i++)
                {
                    tempLines = persons[i].Split();
                    Console.Write(i + 1);

                    foreach (string line in tempLines)
                        Console.Write(delimiter + line);

                    Console.WriteLine(delimiter + jobTitles[i]);
                }
            }
            else
            {
                Console.Write(indexOfPerson + 1);
                tempLines = persons[indexOfPerson].Split();

                foreach (string line in tempLines)
                    Console.Write(delimiter + line);

                Console.WriteLine(delimiter + jobTitles[indexOfPerson]);
            }
        }

        static int SearchByWord(string[] array, string word)
        {
            string[] tempLines;

            for (int i = 0; i < array.Length; i++)
            {
                tempLines = array[i].ToLower().Split();

                for (int j = 0; j < tempLines.Length; j++)
                    if (tempLines[j] == word)
                        return i;
            }

            return -1;
        }

        static string[] DeleteElement(string[] array, int index)
        {
            string[] tempArray = new string[array.Length - 1];

            for (int i = 0; i < index; i++)
                tempArray[i] = array[i];

            for (int i = index + 1; i < array.Length; i++)
                tempArray[i - 1] = array[i];

            return tempArray;
        }
    }
}
