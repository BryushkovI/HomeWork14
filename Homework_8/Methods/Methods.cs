using System;
using Construct;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace MyMethods
{
    public class SortWorkersby : IComparer<Worker>
    {
        /// <summary>
        /// Сортирует по номеру
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public int Compare(Worker a, Worker b)
        {
            if (a.Number > b.Number) return 1;
            else if (a.Number < b.Number) return -1;
            else return 0;
        }
        /// <summary>
        /// Сортирует по возрасту
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public int ComparebyAge(Worker a, Worker b)
        {
            if (a.Age > b.Age) return 1;
            else if (a.Age < b.Age) return -1;
            else return 0;
        }
        /// <summary>
        /// Сортирует по зарплате
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public int ComparebySalary(Worker a, Worker b)
        {
            if (a.Salery > b.Salery) return 1;
            else if (a.Salery < b.Salery) return -1;
            else return 0;
        }
        /// <summary>
        /// Сортирует по количеству проектов
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public int ComparebyProjects(Worker a, Worker b)
        {
            if (a.Projects > b.Projects) return 1;
            else if (a.Projects < b.Projects) return -1;
            else return 0;
        }
    }
    public class Method
    {
        SortWorkersby sorter = new SortWorkersby();
        /// <summary>
        /// Метод печатает шапку
        /// </summary>
        public void Title()
        {
            Console.WriteLine("       №   Имя сотрудника       Фамилия сотрудника  Возраст                    Отдел     Зарплата  Проекты             Руководитель");
        }
        /// <summary>
        /// Печатеат коллекцию сотрудников
        /// </summary>
        /// <param name="ListOfWorkers">Коллекция сотрудников</param>
        public void Print(Organization organization)
        {
            foreach(var e in organization.otdely)
            {
                foreach(var i in e.Workers)
                {
                    Console.WriteLine($"{i.Number,8} {i.Name,16} {i.Surname,24} {i.Age,8} {i.Department,24} {i.Salery,12} {i.Projects,8} {i.Chief,24}");
                }
            }
        }
        /// <summary>
        /// Возвращает количество сотрудников в выбранном департаменте
        /// </summary>
        /// <param name="Workers">Коллекция сотрудников</param>
        /// <param name="Naming">Название департамента</param>
        /// <returns></returns>
        private uint Staff(Organization organization, string Naming)
        {
            uint staff = 0; 
            foreach(var e in organization.otdely)
            {
                if(e.Nameing == Naming)
                {
                    foreach(var i in e.Workers)
                    {
                        staff++;
                    }
                }
            }
            return staff;
        }
        /// <summary>
        /// Реализует редактирование сотрудника
        /// </summary>
        /// <param name="Workers">Коллекция сотрудников</param>
        /// <param name="NumberofWorker">Номер редактируемого сотрудника</param>
        /// <returns></returns>
        public Organization Edit(Organization organization, uint NumberofWorker)
        {
            bool check = false; //проверка наличия такого отдела
            Worker worker = new Worker(); //временный сотрудник
            foreach(var e in organization.otdely)
            {
                foreach (var i in e.Workers)
                {
                    if (i.Number == NumberofWorker)
                    {
                        Console.WriteLine("Введите поочередно Имя Фамилию Возраст Отдел Зарплату Количество проектов.");
                        worker.Name = Console.ReadLine();
                        worker.Surname = Console.ReadLine();
                        worker.Age = uint.Parse(Console.ReadLine());
                        worker.Department = Console.ReadLine();
                        worker.Salery = uint.Parse(Console.ReadLine());
                        worker.Projects = uint.Parse(Console.ReadLine());
                        e.Workers.Remove(i);
                        break;
                    }//записываем измененения во временного сотрудника
                }
            }
            foreach (var e in organization.otdely)
            {
                if (e.Nameing == worker.Department)
                {
                    check = true;
                    worker.Chief = e.Chief;
                    e.Workers.Add(worker);
                }
            } //добавляем в существующий отдел
            if (check == false)
            {
                worker.Chief = worker.Surname;
                Console.WriteLine("Введенного отдела не существует. Он будет создан автоматически.");
                Otdel otdel = new Otdel()
                {
                    Nameing = worker.Department,
                    Create = DateTime.Today,
                    Employees = 1,
                    Chief = worker.Surname
                };
                List<Worker> workers = new List<Worker>
                {
                    worker
                };
                otdel.Workers = workers;
                organization.otdely.Add(otdel);
            } // или создаем новый и добавляем
            return organization;
        }
        /// <summary>
        /// Метод возвращает поле по которому необходимо сделать сортировку
        /// </summary>
        /// <param name="obj">Объект</param>
        /// <param name="name">Имя поля</param>
        /// <returns></returns>
        private object GetPropertyValue(object obj, string name)
        {
            switch (name) // принимает искомое поле и проверяет на соответсвие
            {
                case "номер":
                    obj = obj.GetType().GetField("Number").GetValue(obj);
                    return obj;
                case "возраст":
                    obj = obj.GetType().GetField("Age").GetValue(obj);
                    return obj;
                case "зарплата":
                    obj = obj.GetType().GetField("Salery").GetValue(obj);
                    return obj;
                case "проект":
                    obj = obj.GetType().GetField("Projects").GetValue(obj);
                    return obj;
                case "отдел":
                    obj = obj.GetType().GetField("Department").GetValue(obj);
                    return obj;
                default:
                    return obj;
            }
        }
        /// <summary>
        /// Реализует изменение департамета
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="Nameing"></param>
        /// <returns></returns>
        public Organization EditDep(Organization organization, string Nameing)
        {
            Otdel otdel = new Otdel(); // создаем новы отдел
            foreach(var e in organization.otdely)
            {
                if (e.Nameing == Nameing)
                {
                    Console.WriteLine("Введите Название и Фамилию руководителя"); // если отдел совпадает, то вводим новое название и руководителя
                    otdel.Nameing = Console.ReadLine();
                    otdel.Create = e.Create;
                    otdel.Chief = Console.ReadLine();
                    otdel.Employees = uint.Parse(e.Workers.Count().ToString()); // остальные данные неизменны
                    List<Worker> Workers = new List<Worker>(); // список сотрудников
                    foreach (var i in e.Workers)
                    {
                        Worker newWorker = new Worker()
                        {
                            Age = i.Age,
                            Name = i.Name,
                            Surname = i.Surname,
                            Department = otdel.Nameing,
                            Salery = i.Salery,
                            Chief = otdel.Chief,
                            Number = i.Number,
                            Projects = i.Projects
                        };
                        Workers.Add(newWorker);// каждого сотрудника помещаем во временный список
                    }
                    otdel.Workers = Workers; // добавляем их в измененный отдел
                    organization.otdely.Remove(e); // удаляем старый
                    break;
                }
            }
            organization.otdely.Add(otdel); //вместо него добвалем новый
            return organization;
        }
        /// <summary>
        /// Сортирует по 3 параметрам
        /// </summary>
        /// <param name="Workers"></param>
        /// <param name="FirstComand"></param>
        /// <param name="SecondComand"></param>
        /// <param name="ThirdComand"></param>
        /// <returns></returns>
        public List<Worker> SuperSorter(Organization organization, string FirstComand,string SecondComand, string ThirdComand)
        {
            List<Worker> workers = new List<Worker>();
            foreach(var e in organization.otdely)
            {
                foreach(var i in e.Workers)
                {
                    workers.Add(i);
                }
            }
            return workers = workers.OrderBy(x => GetPropertyValue(x, FirstComand))
                                    .ThenBy(x => GetPropertyValue(x, SecondComand))
                                    .ThenBy(x => GetPropertyValue(x, ThirdComand))
                                    .ToList();
        }
        /// <summary>
        /// Определяет свободный номер для сотрудника
        /// </summary>
        /// <param name="organization">организация</param>
        /// <returns></returns>
        private int NumberOfWorker(Organization organization)
        {
            int EmptyNumber = 1;

            List<Worker> Workers = SuperSorter(organization, "номер", null, null); // сортируем по номеру
            for (int i = EmptyNumber; i < Workers.Count; i++)
            {
                if (EmptyNumber != Workers[i].Number) // проверяем есть ли такой номер уже
                {
                    break; // если нет, возвращаем его
                }
                else
                {
                    EmptyNumber++;
                }
            }
            return EmptyNumber;
        }
        /// <summary>
        /// Добавляет нового сотрудника в нужный отдел
        /// </summary>
        /// <param name="organization">Организация</param>
        /// <returns></returns>
        public Organization AddWorker(Organization organization)
        {
            Console.WriteLine("Введите поочередно Имя Фамилию Возраст Отдел Зарплату Количество проектов.");
            bool check = false;
            Worker newWorker = new Worker()
            {
                Number = uint.Parse(NumberOfWorker(organization).ToString()),
                Name = Console.ReadLine(),
                Surname = Console.ReadLine(),
                Age = uint.Parse(Console.ReadLine()),
                Department = Console.ReadLine(),
                Salery = uint.Parse(Console.ReadLine()),
                Projects = uint.Parse(Console.ReadLine())
            };
            foreach(var e in organization.otdely)
            {
                if (e.Nameing == newWorker.Department)
                {
                    check = true;
                    newWorker.Chief = e.Chief;
                    e.Workers.Add(newWorker);
                }
            }
            if (check == false)
            {
                newWorker.Chief = newWorker.Surname;
                Console.WriteLine("Введенного отдела не существует. Он будет создан автоматически.");
                Otdel otdel = new Otdel()
                {
                    Nameing = newWorker.Department,
                    Create = DateTime.Today,
                    Employees = 1,
                    Chief = newWorker.Surname
                };
                List<Worker> workers = new List<Worker>
                {
                    newWorker
                };
                otdel.Workers = workers;
                organization.otdely.Add(otdel);
            }
            return organization;
        }
        /// <summary>
        /// Удаляет сотрудника по выбранному номеру
        /// </summary>
        /// <param name="Workers">Коллекция сотрудников</param>
        /// <param name="NumberOfWorker">Номер удаляемого сотрудника</param>
        /// <returns></returns>
        public Organization DeliteWorker(Organization organization, int NumberOfWorker)
        {
            foreach(var e in organization.otdely)
            {
                foreach(var i in e.Workers)
                {
                    if (i.Number == NumberOfWorker)
                    {
                        e.Workers.Remove(i);
                        break;
                    }
                }
            } // пробегаем по всем сотрудникам и удаляем указанного
            return organization;
        }
        /// <summary>
        /// Десериализует XML файл
        /// </summary>
        /// <param name="Path">Ссылка на XML</param>
        /// <returns></returns>
        public Organization DeserializeOrgXML(string Path)
        {
            Organization organiztion = new Organization(); // создаем организацию
            List<Otdel> Otdel = new List<Otdel>(); // создаем список отделов
            string XML = File.ReadAllText(Path); //читаем файл
            var otd = XDocument.Parse(XML).Descendants("Отдел").ToList(); 
            var work = XDocument.Parse(XML).Descendants("Отдел").Elements("Сотрудник").ToList();
            foreach (var e in otd)
            {
                List<Worker> Workers = new List<Worker>();
                Otdel otdel = new Otdel();
                otdel.Nameing = e.Attribute("Название").Value;
                otdel.Create = DateTime.Parse(e.Attribute("Дата_создания").Value);
                otdel.Chief = e.Attribute("Руководитель").Value;
                foreach (var i in work)
                {
                    Worker worker = new Worker();
                    worker.Number = uint.Parse(i.Attribute("Номер_сотрудника").Value);
                    worker.Name = i.Attribute("Имя").Value;
                    worker.Surname = i.Attribute("Фамилия").Value;
                    worker.Age = uint.Parse(i.Attribute("Возраст").Value);
                    worker.Department = i.Attribute("Департамент").Value;
                    worker.Salery = uint.Parse(i.Attribute("Зарплата").Value);
                    worker.Projects = uint.Parse(i.Attribute("Проекты").Value);
                    worker.Chief = i.Attribute("Руководитель").Value;
                    if (worker.Department == otdel.Nameing)
                    {
                        Workers.Add(worker);
                    }
                } //создаем каждого встречающегося сотрудника и добавляем в отдел
                otdel.Workers = Workers;
                otdel.Employees = uint.Parse(e.Attribute("Количество_сотрудников").Value);
                Otdel.Add(otdel);
            } // создаем встречающийся отдел
            organiztion.otdely = Otdel; //добавляем в организацию список отделов
            return organiztion;
        }
        /// <summary>
        /// Сериализует организацию в XML
        /// </summary>
        /// <param name="organiztion">Огранизация</param>
        /// <param name="Path">Ссылка на файл</param>
        public void SerializeOrgXML(Organization organization, string Path)
        {
            XElement org = new XElement("Огранизация");

            foreach(var e in organization.otdely)
            {
                XElement Dep = new XElement("Отдел");
                XAttribute Nameing = new XAttribute("Название", e.Nameing);
                XAttribute Create = new XAttribute("Дата_создания", e.Create);
                XAttribute Employees = new XAttribute("Количество_сотрудников", Staff(organization,e.Nameing));
                XAttribute DepChief = new XAttribute("Руководитель", e.Chief);
                Dep.Add(Nameing, Create, Employees, DepChief);
                foreach(var i in e.Workers)
                {
                    XElement Worker = new XElement("Сотрудник");
                    XAttribute Number = new XAttribute("Номер_сотрудника", i.Number);
                    XAttribute Name = new XAttribute("Имя", i.Name);
                    XAttribute Surname = new XAttribute("Фамилия", i.Surname);
                    XAttribute Age = new XAttribute("Возраст", i.Age);
                    XAttribute Department = new XAttribute("Департамент", i.Department);
                    XAttribute Salery = new XAttribute("Зарплата", i.Salery);
                    XAttribute Projects = new XAttribute("Проекты", i.Projects);
                    XAttribute Chief = new XAttribute("Руководитель", i.Chief);
                    if(Department.Value == Nameing.Value)
                    {
                        Worker.Add(Number, Name, Surname, Age, Department, Salery, Projects, Chief);
                        Dep.Add(Worker);
                    }
                }
                org.Add(Dep);
            }
            org.Save(Path);
        }
        /// <summary>
        /// Десериализует организацию из JSON
        /// </summary>
        /// <param name="Path">Ссылка на файл</param>
        /// <returns></returns>
        public Organization DezerializeOrgJSON(string Path)
        {
            Organization organization = new Organization();// создаем организацию
            List<Otdel> Otdels = new List<Otdel>();// создаем список отделов
            string JSON = File.ReadAllText(Path);//читаем файл
            var departments = JObject.Parse(JSON)["Отделы"].ToArray();
            foreach(var e in departments)
            {
                List<Worker> Workers = new List<Worker>();
                Otdel otdel = new Otdel
                {
                    Nameing = e["Nameing"].ToString(),
                    Create = DateTime.Parse(e["Create"].ToString()),
                    Employees = uint.Parse(e["Employees"].ToString()),
                    Chief = e["Chief"].ToString()
                };
                var work = e["Сотрудники"].ToArray();
                foreach(var i in work) //создаем каждого встречающегося сотрудника и добавляем в отдел
                {
                    Worker worker = new Worker()
                    {
                        Number = uint.Parse(i["Number"].ToString()),
                        Name = i["Name"].ToString(),
                        Surname = i["Surname"].ToString(),
                        Age = uint.Parse(i["Age"].ToString()),
                        Department = i["Department"].ToString(),
                        Salery = uint.Parse(i["Salery"].ToString()),
                        Projects = uint.Parse(i["Projects"].ToString()),
                        Chief = i["Chief"].ToString()
                    };
                    Workers.Add(worker);
                }
                otdel.Workers = Workers;
                Otdels.Add(otdel);
            }// создаем встречающийся отдел
            organization.otdely = Otdels; //добавляем в организацию список отделов// 
            return organization;
        }
        /// <summary>
        /// Сериализует организацию в JSON
        /// </summary>
        /// <param name="organization">Организация</param>
        /// <param name="Path">Ссылка на файл</param>
        public void SerialiseOrgJSON(Organization organization, string Path)
        {
            JObject Org = new JObject(); 
            JArray Deps = new JArray();
            foreach(var e in organization.otdely)
            {
                JObject dep = new JObject()
                {
                    ["Nameing"] = e.Nameing,
                    ["Create"] = e.Create,
                    ["Employees"] = Staff(organization,e.Nameing),
                    ["Chief"] = e.Chief
                };
                JArray Works = new JArray();
                foreach(var i in e.Workers)
                {
                    JObject worker = new JObject()
                    {
                        ["Number"] = i.Number,
                        ["Name"] = i.Name,
                        ["Surname"] = i.Surname,
                        ["Age"] = i.Age,
                        ["Department"] = i.Department,
                        ["Salery"] = i.Salery,
                        ["Projects"] = i.Projects,
                        ["Chief"]= i.Chief
                    };
                    Works.Add(worker);
                }
                dep["Сотрудники"] = Works;
                Deps.Add(dep);
            }
            Org["Отделы"] = Deps;
            string JSON = Org.ToString();
            File.WriteAllText(Path, JSON);
        }
    }
}
