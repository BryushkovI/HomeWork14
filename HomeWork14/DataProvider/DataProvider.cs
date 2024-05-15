using HomeWork15.Models;
using HomeWork15.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Media;
using OxyPlot;
using System.Data;
using static HomeWork15.DataProvider.IDataProvider;

namespace HomeWork15.DataProvider
{
    class DataProvider : IDataProvider
    {
        
        readonly SqlConnectionStringBuilder sqlConnectionStringBuilder = new()
        {
            DataSource = @"(localdb)\MSSQLLocalDB",
            InitialCatalog = "BankPrototype",
            IntegratedSecurity = true
        };
        /// <summary>
        /// Возвращает список клиентов выбранного отдела
        /// </summary>
        /// <param name="AccountType">Тип клиентов</param>
        /// <param name="Skip">Пропустить</param>
        /// <param name="Take">Получить</param>
        /// <returns></returns>
        public async Task<ObservableCollection<TitleClient>> GetTitleClientsAsync(int AccountType, int Skip = 0, int Take = int.MaxValue)
        {
            
            ObservableCollection<TitleClient> clients = new();

            try
            {
                using (SqlConnection connection = new(sqlConnectionStringBuilder.ConnectionString))
                {
                    await connection.OpenAsync();
                    string query = string.Format(@"SELECT id, Name FROM Clients WHERE AccountType = {0}
                                                                            ORDER BY AccountType
                                                                            OFFSET {1} ROWS
                                                                            FETCH NEXT {2} ROWS ONLY", AccountType, Skip, Take);
                    SqlCommand command = new(query, connection);
                    SqlDataReader dataReader = await command.ExecuteReaderAsync();
                    while (await dataReader.ReadAsync())
                    {
                        clients.Add(new(dataReader["Name"].ToString(), dataReader["id"].ToString()));
                    }
                }
            }
            catch (Exception ex)
            {

                
            }
            return clients;
        }
        /// <summary>
        /// Получение выбранного клиента
        /// </summary>
        /// <param name="AccountNumber">Номер УЗ клиента</param>
        /// <returns></returns>
        public async Task<Client> GetClientAsync(int AccountNumber)
        {
            try
            {
                Client client;
                SqlConnection connection = new(sqlConnectionStringBuilder.ConnectionString);

                await connection.OpenAsync();
                string query = string.Format(@"SELECT TOP 1 cl.id, AccountType, cl.Name, cl.BankAccount, cr.sum as Credit, cr.beginDate as DateCreditBegin,
                                                            cr.endDate as DateCreditEnd, dep.sum as Deposit, dep.beginDate as DateDepositBegin,
                                                            dep.endDate as DateDepositEnd, capitalization 
                                                       FROM Clients cl LEFT JOIN Credits cr ON cl.id = cr.clientId
                                                       LEFT JOIN Deposits dep ON cl.id = dep.clientId
                                                      WHERE cl.id = {0}", AccountNumber);
                SqlCommand command = new(query, connection);
                SqlDataReader dataReader = await command.ExecuteReaderAsync();
                while (await dataReader.ReadAsync())
                {

                    client = int.Parse(dataReader[nameof(Client.AccountType)].ToString()) switch
                    {
                        0 => new Regular(dataReader[nameof(Client.Name)].ToString(), AccountNumber, double.Parse(dataReader[nameof(Client.BankAccount)].ToString())),
                        1 => new VIP(dataReader[nameof(Client.Name)].ToString(), AccountNumber, double.Parse(dataReader[nameof(Client.BankAccount)].ToString())),
                        2 => new Entity(dataReader[nameof(Client.Name)].ToString(), AccountNumber, double.Parse(dataReader[nameof(Client.BankAccount)].ToString())),
                        _ => null,
                    };
                    try
                    {
                        if (double.TryParse(dataReader[nameof(Client.Credit)].ToString(), out double doubleResult))
                        {
                            client.Credit = doubleResult;
                            client.DateCreditBegin = DateTime.Parse(dataReader[nameof(Client.DateCreditBegin)].ToString());
                            client.DateCreditEnd = DateTime.Parse(dataReader[nameof(Client.DateCreditEnd)].ToString());
                        }
                        if (double.TryParse(dataReader[nameof(Client.Deposit)].ToString(), out doubleResult))
                        {
                            client.Deposit = doubleResult;
                            client.DateDepositBegin = DateTime.Parse(dataReader[nameof(Client.DateDepositBegin)].ToString());
                            client.DateDepositEnd = DateTime.Parse(dataReader[nameof(Client.DateDepositEnd)].ToString());
                            client.Capitalization = bool.Parse(dataReader[nameof(Client.Capitalization)].ToString());
                        }
                    }
                    catch (Exception)
                    {

                    }
                    return client;
                    
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        /// <summary>
        /// Обновление выбранного клиента
        /// </summary>
        /// <param name="client">Экземпляр клиента</param>
        /// <returns></returns>
        public async Task UpdateClientAsync(Client client)
        {
            try
            {
                using (SqlConnection connection = new(sqlConnectionStringBuilder.ConnectionString))
                {
                    await connection.OpenAsync();
                    string query = string.Format(@"UPDATE Clients
                                                  SET Name = N'{0}',
                                                      BankAccount = {1},
                                                      AccountType = {2}
                                                WHERE id = {3}
                                               UPDATE Credits
                                                   SET sum = {4}
                                                WHERE clientId = {3}
                                               UPDATE Deposits
                                                   SET sum = {5}, capitalization = {6}
                                                WHERE clientId = {3}", client.Name, client.BankAccount, client.AccountType,
                                                                     client.AccountNumber, client.Credit, client.Deposit,
                                                                     client.Capitalization ? 1 : 0);
                    SqlCommand command = new(query, connection);
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Создание записи клиента
        /// </summary>
        /// <param name="client">Клиент</param>
        /// <returns></returns>
        public async Task CreateClientAsync(Client client)
        {
            try
            {
                using (SqlConnection connection = new(sqlConnectionStringBuilder.ConnectionString))
                {
                    await connection.OpenAsync();
                    string query = string.Format(@"INSERT Clients 
                                               VALUES ({0}, N'{1}', {2})", client.AccountType, client.Name, client.BankAccount);
                    SqlCommand command = new(query, connection);
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// Удаление выбранного клиента
        /// </summary>
        /// <param name="client">Клиент</param>
        /// <returns></returns>
        public async Task DeleteClientAsync(Client client)
        {
            try
            {
                using(SqlConnection connection = new(sqlConnectionStringBuilder.ConnectionString))
                {
                    await connection.OpenAsync();
                    string query = string.Format(@"DELETE Credits
                                                    WHERE clientId = {0}
                                                   DELETE Deposits
                                                    WHERE clientId = {0}
                                                   DELETE Clients 
                                                    WHERE id = {0}", client.AccountNumber);
                    SqlCommand command = new(query, connection);
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                
            }
        }
        /// <summary>
        /// Открытие кредита\вклада клиенту
        /// </summary>
        /// <param name="client">Клиент</param>
        /// <param name="block">Кредит\Вклад</param>
        /// <returns></returns>
        public async Task AddFinToolForClientAsync(Client client, UpdateBlock block)
        {
            try
            {
                using (SqlConnection connection = new(sqlConnectionStringBuilder.ConnectionString))
                {
                    await connection.OpenAsync();
                    string query = string.Format(@"INSERT {0}
                                                   VALUES ({1}, {2},{3} '{4}', '{5}')",
                                                   block == UpdateBlock.Deposit ? "Deposits" : "Credits",
                                                   client.AccountNumber,
                                                   block == UpdateBlock.Deposit ? client.Deposit : client.Credit,
                                                   block == UpdateBlock.Deposit ? client.Capitalization ? 1 : 0 + "," : "",
                                                   block == UpdateBlock.Deposit ? client.DateDepositBegin.ToString("yyyy-MM-dd") : client.DateCreditBegin.ToString("yyyy-MM-dd"),
                                                   block == UpdateBlock.Deposit ? client.DateDepositEnd.ToString("yyyy-MM-dd") : client.DateCreditEnd.ToString("yyyy-MM-dd"));
                    SqlCommand command = new(query, connection);
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                
            }
        }
    }
    interface IDataProvider
    {
        enum UpdateBlock
        {
            Credit,
            Deposit
        }
        public Task<ObservableCollection<TitleClient>> GetTitleClientsAsync(int AccountType, int Skip, int Take);

        public Task<Client> GetClientAsync(int AccountNumber);

        public Task UpdateClientAsync(Client client);

        public Task CreateClientAsync(Client client);

        public Task DeleteClientAsync(Client client);

        public Task AddFinToolForClientAsync(Client client, UpdateBlock block);
    }
}