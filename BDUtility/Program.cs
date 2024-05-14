using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Threading;

namespace BDUtility
{
    internal class Program
    {
        private readonly SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder()
        {
            DataSource = @"(localdb)\MSSQLLocalDB",
            InitialCatalog = "BankPrototype",
            IntegratedSecurity = true
        };

        private readonly SqlConnectionStringBuilder connectionStringDB = new SqlConnectionStringBuilder()
        {
            DataSource = @"(localdb)\MSSQLLocalDB",
            InitialCatalog = "master",
            IntegratedSecurity = true
        };
        static async Task Main(string[] args)
        {
            Console.WriteLine("Создание запущено.");
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;
            Task.Run(() =>
            {
                ConsoleSpiner consoleSpiner = new ConsoleSpiner();
                while(!cancellationToken.IsCancellationRequested)
                {
                    consoleSpiner.Turn();
                }
            },cancellationToken);
            Program program = new Program();

            try
            {
                await program.CreateDB();
                Console.WriteLine("База данных создана.");
                await program.CreateTables();
                Console.WriteLine("Таблица создана.");
                await program.FillClients();
                Console.WriteLine("Таблица Clients заполнена.");
                await program.FillCredits();
                Console.WriteLine("Таблица Credits заполнена.");
                await program.FillDeposits();
                Console.WriteLine("Таблица Deposits заполнена.");
                Console.WriteLine("Создание завершено.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            cancellationTokenSource.Cancel();
            Console.ReadKey();
        }
        /// <summary>
        /// Создание Таблиц
        /// </summary>
        /// <returns></returns>
        async Task CreateTables()
        {
            using (SqlConnection connection = new SqlConnection(connectionString.ConnectionString))
            {
                List<string> queries = new List<string>()
                {
                    @"CREATE TABLE [dbo].[Clients]
                                 ( 
                                    [id] INT PRIMARY KEY IDENTITY,
                                    [AccountType] INT NOT NULL CHECK( AccountType >= 0 AND AccountType <= 3),
                                    [Name] NVARCHAR(50) NOT NULL,
                                    [BankAccount] MONEY DEFAULT 0
                                 )",
                    @"CREATE TABLE [dbo].[Credits]
                                 (
                                    [id] INT PRIMARY KEY IDENTITY,
                                    [clientId] INT NOT NULL,
                                    [sum] MONEY DEFAULT 0,
                                    [beginDate] Date NOT NULL,
                                    [endDate] Date NOT NULL,
                                    FOREIGN KEY (clientId) REFERENCES Clients (id)
                                 )",
                    @"CREATE TABLE [dbo].[Deposits]
                                 (
                                    [id] INT PRIMARY KEY IDENTITY,
                                    [clientId] INT NOT NULL,
                                    [sum] MONEY DEFAULT 0,
                                    [capitalization] BIT NOT NULL,
                                    [beginDate] Date NOT NULL, 
                                    [endDate] Date NOT NULL,
                                    FOREIGN KEY (clientId) REFERENCES Clients (id)
                                 )"
                };
                
                connection.Open();
                foreach (string query in queries)
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = query;
                    await command.ExecuteNonQueryAsync();
                }
                
            }
        }
        /// <summary>
        /// Создание БД
        /// </summary>
        /// <returns></returns>
        async Task CreateDB()
        {
            using (SqlConnection connection = new SqlConnection(connectionStringDB.ConnectionString))
            {
                connection.Open();
                string query = @"CREATE DATABASE BankPrototype";
                SqlCommand command = new SqlCommand(query,connection);
                await command.ExecuteNonQueryAsync();
                
            }
        }

        async Task FillClients()
        {
            using(SqlConnection connection = new SqlConnection(connectionString.ConnectionString))
            {
                await connection.OpenAsync();
                string query = @"INSERT INTO Clients
                             VALUES
                             (2, N'ООО ""Ромашка""', 190000),
                             (2, N'ООО ""Лютик""', 160000),
                             (2, N'ООО ""Акация""', 170000),
                             (1, N'Еж Ежов', 25000),
                             (1, N'Лось Лосев', 20000),
                             (1, N'Бык Быков', 55000),
                             (1, N'Корова Быкова', 25000),
                             (1, N'Слон Слонов', 65000),
                             (1, N'Мамонт Мамонтов', 75000),
                             (1, N'Крокодил Крокодилов', 15000),
                             (1, N'Макака Макаова', 12000),
                             (1, N'Черепаха Черепахян', 43000),
                             (1, N'Чиж Чижов', 34000),
                             (1, N'Мангуст Мангустидзе', 65000),
                             (1, N'Рыба Рыбенко', 23000),
                             (1, N'Медуза Медузевич', 25000),
                             (1, N'Рак Раков', 123000),
                             (1, N'Свин Свинов', 25000),
                             (1, N'Попугай Попугаян', 54000),
                             (1, N'Зубр Зубриян', 25000),
                             (1, N'Осел Ослов', 25000),
                             (0, N'Коза Рогатая', 100012),
                             (0, N'Собака Собакова', 129000),
                             (0, N'Морж Моржов', 20200),
                             (0, N'Лиса Лисова', 1400)";
                SqlCommand command = new SqlCommand(query,connection);
                await command.ExecuteNonQueryAsync();
                
            }
            
        }

        async Task FillCredits()
        {
            using (SqlConnection connection = new SqlConnection(connectionString.ConnectionString))
            {
                connection.Open();
                string query = @"INSERT INTO Credits
                                 VALUES 
                                 (1, 30000, '2021-01-01', '2025-01-01'),
                                 (2, 5000, '2021-05-21', '2025-06-01'),
                                 (3, 10000, '2020-05-19', '2023-01-01'),
                                 (5, 1000, '2021-05-21', '2025-06-01'),
                                 (6, 3000, '2021-05-01', '2023-01-01'),
                                 (7, 3000, '2021-05-01', '2023-01-01'),
                                 (8, 3000, '2021-05-01', '2024-01-01'),
                                 (23, 31000, '2021-05-01', '2023-01-01'),
                                 (24, 1200, '2021-05-01', '2024-03-01')";
                SqlCommand command = new SqlCommand(query, connection);
                await command.ExecuteNonQueryAsync();
            }
        }

        async Task FillDeposits()
        {
            using( SqlConnection connection = new SqlConnection(connectionString.ConnectionString))
            {
                await connection.OpenAsync();
                string query = @"INSERT INTO Deposits
                                 VALUES 
                                 (1, 20000, 1, '2021-01-01', '2026-01-01'),
                                 (3, 5000, 0, '2021-05-01', '2026-01-01'),
                                 (4, 9000, 0, '2020-01-01', '2026-01-01'),
                                 (6, 12000, 1, '2021-01-01', '2026-01-01'),
                                 (7, 12000, 1, '2021-01-01', '2025-01-01'),
                                 (23, 100000, 1, '2024-04-27', '2025-05-21'),
                                 (24, 2000, 1, '2021-03-01', '2026-01-01'),
                                 (25, 20000, 0, '2021-01-01', '2026-01-01')";
                SqlCommand command = new SqlCommand(query, connection);
                await command.ExecuteNonQueryAsync();
            }
        }
    }
    /// <summary>
    /// СПИПЕРРРРРР
    /// </summary>
    public class ConsoleSpiner
    {
        int counter;
        public ConsoleSpiner()
        {
            counter = 0;
        }
        public void Turn()
        {
            counter++;
            switch (counter % 4)
            {
                case 0: Console.Write("/"); break;
                case 1: Console.Write("-"); break;
                case 2: Console.Write("\\"); break;
                case 3: Console.Write("|"); break;
            }
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            Thread.Sleep(200);
        }
    }
}