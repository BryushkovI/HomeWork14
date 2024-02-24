using System;
using System.Collections.Generic;
using Construct;
using MyMethods;

namespace NewMainApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Method method = new Method();
            Console.WriteLine("Какой файл импортировать? xml / json");
            string import = Console.ReadLine().ToLower(); // Выбирем, какой из файл импортировать
            Organization organization = new Organization();
            switch (import)
            {
                case "xml":
                    organization = method.DeserializeOrgXML(@"serializedorg.xml");
                    break;
                case "json":
                    organization = method.DezerializeOrgJSON(@"serializedOrg.json");
                    break;
            }
            string comand;
            do
            {
                comand = Console.ReadLine().ToLower();
                switch (comand)
                {
                    case "все":
                        method.Title();
                        method.Print(organization);
                        break;
                    case "новый сотрудник":
                        organization = method.AddWorker(organization);
                        break;
                    case "удалить сотрудника":
                        Console.WriteLine("Введите номер удаляемого сотрудника");
                        organization = method.DeliteWorker(organization, int.Parse(Console.ReadLine()));
                        break;
                    case "сортировать":
                        string FirstComand;
                        string SecondComand;
                        string ThirdComand;
                        Console.WriteLine("Выберите параметры сортировки в порядке важности. Номер / Возраст / Зарплата / Проект");
                        FirstComand = Console.ReadLine().ToLower();
                        SecondComand = Console.ReadLine().ToLower();
                        ThirdComand = Console.ReadLine().ToLower();
                        method.Title();
                        List<Worker> workers = method.SuperSorter(organization, FirstComand, SecondComand, ThirdComand);
                        foreach (var i in workers)
                        {
                            Console.WriteLine($"{i.Number,8} {i.Name,16} {i.Surname,24} {i.Age,8} {i.Department,24} {i.Salery,12} {i.Projects,8} {i.Chief,24}");
                        }
                        break;
                    case "изменить сотрудника":
                        Console.WriteLine("Введите номер изменяемого сотрудника");
                        uint NumberofWorker = uint.Parse(Console.ReadLine());
                        organization = method.Edit(organization, NumberofWorker);
                        break;
                    case "изменить отдел":
                        Console.WriteLine("Введите название изменяемого отдела");
                        string Nameing = Console.ReadLine();
                        organization = method.EditDep(organization, Nameing);
                        break;
                }
                
            }
            while (comand != "выход");

            method.SerializeOrgXML(organization, @"serializedorg.xml");
            method.SerialiseOrgJSON(organization, @"serializedOrg.json");
        }
    }
}
