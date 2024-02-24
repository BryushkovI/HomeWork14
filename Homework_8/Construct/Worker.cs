using System;
using System.Collections.Generic;

namespace Construct
{
    public struct DepartmentStruct
    {
        /// <summary>
        /// Название департамента
        /// </summary>
        public string Nameing;
        /// <summary>
        /// Дата создания департамета
        /// </summary>
        public DateTime Create;
        /// <summary>
        /// Количесвто сотрудников в департаменте
        /// </summary>
        public uint Employees;

        public DepartmentStruct(string Nameing, DateTime Create, uint Employees)
        {
            this.Nameing = Nameing;
            this.Create = Create;
            this.Employees = Employees;
        }
    }
    public struct Worker
    {
        /// <summary>
        /// Номер сотудника
        /// </summary>
        public uint Number;
        /// <summary>
        /// Имя сотрудника
        /// </summary>
        public string Name;
        /// <summary>
        /// Фамилия сотрудника
        /// </summary>
        public string Surname;
        /// <summary>
        /// Возраст
        /// </summary>
        public uint Age;
        /// <summary>
        /// Департамент
        /// </summary>
        public string Department;
        /// <summary>
        /// Зарплата
        /// </summary>
        public uint Salery;
        /// <summary>
        /// Количество проектов
        /// </summary>
        public uint Projects;
        /// <summary>
        /// Руководитель
        /// </summary>
        public string Chief;
        /// <summary>
        /// Печать в консоль
        /// </summary>
        public void PrintConsole()
        {
            Console.WriteLine($"{Number,8} {Name,16} {Surname,24} {Age,8} {Department,24} {Salery,12} {Projects,8} {Chief,24}");
        }
        /// <summary>
        /// Структура сотрудника
        /// </summary>
        /// <param name="Number"></param>
        /// <param name="Name"></param>
        /// <param name="Surname"></param>
        /// <param name="Age"></param>
        /// <param name="Department"></param>
        /// <param name="Salery"></param>
        /// <param name="Projects"></param>
        public Worker(uint Number,string Name,string Surname, uint Age, string Department,uint Salery,uint Projects, string Chief)
        {
            this.Number = Number;
            this.Name = Name;
            this.Surname = Surname;
            this.Age = Age;
            this.Department = Department;
            this.Salery = Salery;
            this.Projects = Projects;
            this.Chief = Chief;
        }
    }
    public struct Otdel
    {
        public string Nameing;
        public DateTime Create;
        public string Chief;
        public List<Worker> Workers;
        public uint Employees;
        public Otdel(string Nameing, DateTime Create, string Chief, List<Worker> Workers, uint Employees)
        {
            this.Nameing = Nameing;
            this.Create = Create;
            this.Chief = Chief;
            this.Workers = Workers;
            this.Employees = Employees;
        }
    }
    public struct Organization
    {
        public List<Otdel> otdely;
        public Organization(List<Otdel> otdely)
        {
            this.otdely = otdely;
        }
    }
}
