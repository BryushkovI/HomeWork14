using System;
using System.Collections.Generic;
using Construct;
using MyMethods;

namespace MainApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Method method = new Method(); // Объявляем экземпляр методов
            Console.WriteLine("Какой файл импортировать? xml / json");
            string import = Console.ReadLine().ToLower(); // Выбирем, какой из файл импортировать
            List<DepartmentStruct> departments = new List<DepartmentStruct>();
            List<Worker> Workers = new List<Worker>();
            switch (import)
            {
                case "xml":
                    departments = method.DeserializeDepartmentXML(@"serializedWorker.xml"); // Импортируем список отделов
                    Workers = method.DeserializeWorkerXML(@"serializedWorker.xml"); // Импортируем список сотрудников
                    break;
                case "json":
                    departments = method.DeserializeDepartmentsJSON(@"serializedWorkers.json"); // Импортируем список отделов
                    Workers = method.DeserializeWorkerJSON(@"serializedWorkers.json"); // Импортируем список сотрудников
                    break;
            }
            string comand;
            do
            {
                comand = Console.ReadLine().ToLower(); // Принимаем команду из консоли
                switch (comand)
                {
                    case "новый департамент":
                        departments.Add(method.AddDepartment());
                        break;
                        break;
                    case "добавить":
                        //Workers = method.AddWorker(Workers, departments);
                        break;
                    case "выход":
                        Console.WriteLine("Программа завершена");
                        break;
                    case "сортировать":
                        string FirstComand;
                        string SecondComand;
                        string ThirdComand;
                        Console.WriteLine("Выберите параметры сортировки в порядке важности. Номер / Возраст / Зарплата / Проект");
                        FirstComand = Console.ReadLine().ToLower();
                        SecondComand = Console.ReadLine().ToLower();
                        ThirdComand = Console.ReadLine().ToLower();
                        Workers = method.SuperSorter(Workers, FirstComand,SecondComand,ThirdComand);
                        break;
                    case "удалить":
                        Console.WriteLine("Введите номер удаляемого сотрудника");
                        //Workers = method.DeliteWorker(Workers, int.Parse(Console.ReadLine()));
                        break;
                    case "изменить":
                        Console.WriteLine("Введите номер изменяемого сотрудника");
                        Workers = method.Edit(Workers, int.Parse(Console.ReadLine()));
                        break;
                    default:
                        Console.WriteLine("Неверная команда");
                        break;
                }
            }
            while (comand != "выход");

            method.SerializeXML(Workers, departments, @"serializedWorker.xml"); // Сериализуем список сотрудников и отделов в XML файл
            method.SerializeJSON(Workers, departments, @"serializedWorkers.json"); // Сериализуем список сотрудников и отделов в JSON файл

            //Organiztion organiztion = new Organiztion();
            Otdel Otdel = new Otdel();
            List<Worker> workers = new List<Worker>();
            Otdel.Workers.AddRange(workers);
            //organiztion.otdely.Add(Otdel);
            
        }
    }
}
