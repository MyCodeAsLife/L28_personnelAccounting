using System;
using System.Collections.Generic;
using System.Linq;
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

            string[,] persons = new string[0, 0];
            string[] jobTitles = new string[0];
            char delimiter = '-';
            char separator = ' ';
            int countWordsInFullName = 3;
            int lastNamePosition = 0;
            int displayAllPersons = -1;
            int numberOfPerson;
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
                        Console.Write("Введите полностью Фамилию Имя Отчество: ");
                        string[] addedPerson = Console.ReadLine().Split(separator);
                        Console.Write("Введите наименование должности: ");
                        string jobTitle = Console.ReadLine();
                        AddDossier(ref persons, ref jobTitles, addedPerson, countWordsInFullName, jobTitle);
                        continue;

                    case CommandDisplayAllDossier:
                        FormatOutput(persons, jobTitles, displayAllPersons, delimiter);
                        break;

                    case CommandDeleteDossier:
                        Console.Write("Введите номер досье, для его удаления: ");
                        numberOfPerson = Convert.ToInt32(Console.ReadLine()) - 1;
                        DeleteDossier(ref persons, ref jobTitles, numberOfPerson);
                        break;

                    case CommandSearchByLastName:
                        Console.Write("Введите фамилию для поиска досье: ");
                        string lastName = Console.ReadLine();
                        Console.Clear();
                        numberOfPerson = SearchByLastName(persons, lastName, lastNamePosition);

                        if (numberOfPerson > -1)
                            FormatOutput(persons, jobTitles, numberOfPerson, delimiter);
                        else
                            Console.WriteLine($"\nЧеловек с фамилией {lastName}, в базе данных не найден.");

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

        static void AddDossier(ref string[,] persons, ref string[] jobTitles, string[] addedPerson, int countWordsInFullName, string addedJobTitle)
        {
            string[,] tempFullName = new string[persons.GetLength(0) + 1, countWordsInFullName];
            string[] tempPosition = new string[jobTitles.Length + 1];

            for (int i = 0; i < persons.GetLength(0); i++)
                for (int j = 0; j < countWordsInFullName; j++)
                    tempFullName[i, j] = persons[i, j];

            for (int i = 0; i < jobTitles.Length; i++)
                tempPosition[i] = jobTitles[i];

            jobTitles = tempPosition;
            jobTitles[jobTitles.Length - 1] = addedJobTitle;
            persons = tempFullName;

            for (int i = 0; i < countWordsInFullName; i++)
                persons[persons.GetLength(0) - 1, i] = addedPerson[i];
        }

        static void FormatOutput(string[,] persons, string[] jobTitles, int numberOfPerson, char delimiter)
        {
            Console.Clear();

            if (numberOfPerson < 0)
            {
                for (int i = 0; i < persons.GetLength(0); i++)
                {
                    Console.Write($"{i + 1}{delimiter}");

                    for (int j = 0; j < persons.GetLength(1); j++)
                        Console.Write($"{persons[i, j]}{delimiter}");

                    Console.WriteLine($"{jobTitles[i]}.");
                }
            }
            else
            {
                Console.Write($"{numberOfPerson + 1}{delimiter}");

                for (int i = 0; i < persons.GetLength(1); i++)
                    Console.Write($"{persons[numberOfPerson, i]}{delimiter}");

                Console.WriteLine($"{jobTitles[numberOfPerson]}.\n");
            }
        }

        static int SearchByLastName(string[,] persons, string lastName, int lastNamePosition)
        {
            for (int i = 0; i < persons.GetLength(0); i++)
                if (persons[i, lastNamePosition].ToLower() == lastName.ToLower())
                    return i;

            return -1;
        }

        static void DeleteDossier(ref string[,] persons, ref string[] jobTitles, int numberOfPerson)
        {
            if (numberOfPerson < persons.GetLength(0) || numberOfPerson >= 0)
            {
                string[,] tempPersons = new string[persons.GetLength(0) - 1, persons.GetLength(1)];
                string[] tempJobTitles = new string[jobTitles.Length - 1];

                for (int i = 0; i < numberOfPerson; i++)
                    for (int j = 0; j < persons.GetLength(1); j++)
                        tempPersons[i, j] = persons[i, j];

                for (int i = numberOfPerson + 1; i < persons.GetLength(0); i++)
                    for (int j = 0; j < persons.GetLength(1); j++)
                        tempPersons[i, j] = persons[i, j];

                for (int i = 0; i < numberOfPerson; i++)
                    tempJobTitles[i] = jobTitles[i];

                for (int i = numberOfPerson + 1; i < jobTitles.Length; i++)
                    tempJobTitles[i] = jobTitles[i];

                persons = tempPersons;
                jobTitles = tempJobTitles;
            }
        }
    }
}
