using MySql.Data.MySqlClient;
using System;
using System.Data;
using Telegram.Bot;
using Telegram.Bot.Types;
using ZabbixApi;

namespace TelegramBot
{
    class ZabbixCommand : ICommand
    {
        /// <summary>
        /// Имя команды
        /// </summary>
        public string Name { get; set; } = "/zab";
        public int CountArgs { get; set; } = 0;
        /// <summary>
        /// Вызывает команду
        /// </summary>
        /// <param name="message">принимает сообщение</param>
        /// <param name="client">Ссылка на экземпляр бота</param>
        public async void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;

            string serverName = "10.86.1.252"; // Адрес сервера (для локальной базы пишите "localhost")
            string userName = "zabbix"; // Имя пользователя
            string dbName = "zabbix3"; //Имя базы данных
            string port = "3306"; // Порт для подключения
            string password = "Ua8ga3aiTahg0uu3"; // Пароль для подключения
            string connStr = "server=" + serverName + 
                ";user=" + userName +
                ";database=" + dbName +
                ";port=" + port +
                ";password=" + password + ";";

            string query = @"SELECT * FROM hosts WHERE host like ""24%"""; // Строка запроса

            DataTable dTable = new DataTable();
            using (MySqlConnection connection = new MySqlConnection(connStr))
            {
                MySqlCommand sqlCom = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(sqlCom);
                connection.Open();
                //sqlCom.ExecuteNonQuery();
                adapter.Fill(dTable);
            }
            string rowTextAll = "";
            if (dTable.Rows.Count != 0)
            {
                //запишем содержимое dTableAll в строку rowTextAll
                for (int curRow = 200; curRow < 201/*dTable.Rows.Count*/; curRow++)
                {
                    for (int curCol = 0; curCol < dTable.Columns.Count; curCol++)
                    {
                        rowTextAll += dTable.Rows[curRow][curCol].ToString() + "\n";
                    }
                    rowTextAll += "\n";
                }
                // в ответ на команду (фамилию), введенную пользователем в Telegram, выводим сообщение
                await client.SendTextMessageAsync(chatId, rowTextAll);
            }
            //Zabbix zabbix = new Zabbix("admin", "killers123", "https://10.86.1.252/zabbix/", true);

            //zabbix.login();
            //Response responseObj = zabbix.objectResponse("trigger.get", new
            //{
            //    output = new string[] { "hostname", "description", "lastchange", "priority", "value", "status", "triggerid" },
            //    min_severity = 3,
            //    expandData = true,
            //    expandDescription = true,
            //    expandExpression = true,
            //    selectHosts = "extend",
            //    selectGroups = "extend",
            //    monitored = true,
            //    sortfield = "hostname",
            //    skipDependent = true,
            //    filter = new { value = 1 }
            //});
            //zabbix.logout();

            //foreach (dynamic data in responseObj.result)
            //{
            //    Console.WriteLine(data.Hostname);
            //}
        }
        public async void OnError(Message message, TelegramBotClient client)
        {
            await client.SendTextMessageAsync(message.Chat.Id, @"Введите ""/zab"" ");
        }
    }
}

