using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot
{
    class MyCommand : ICommand
    {
        DataTable GetDataFromQuery(string FIO)
        {
            //  преобразуем дату dd.MM.yyyy в формат SQL запроса dd/MM/yyyy
            //  и прибавим смещение в 2000 лет
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            //var startDate = new DateTime(2017, 10, 1);
            string startDateFormatted = startDate.AddYears(2000).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            //var endDate = new DateTime(2018, 2, 1);
            string endDateFormatted = endDate.AddYears(2000).AddDays(1).AddTicks(-1).ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

            string connectionString = @"Data Source=10.86.1.210; Initial Catalog=R32-ASZUP-WORK; User Id = USER_1C; Password = a4tPL0XX5F5NSgCxrH8g";
            string sqlExpression = @"SELECT
                                    T2._Description as ФИО,
                                    T1._Fld6831 as Сумма,
                                    CONVERT (varchar, dateadd(year, -2000, T3._Date_Time), 104) as 'Дата выплаты',
                                    case 
	                                    when cast ((sys.fn_varbintohexstr(T1._Fld6832RRef)) as nvarchar(18))
	                                    = cast('0x8CEA26A3252A789146CC4239F036668B' as nvarchar(18))
	                                    then 'Зарплата'   
	                                    when cast ((sys.fn_varbintohexstr(T1._Fld6832RRef)) as nvarchar(18))
	                                    = cast('0xAD17DFC84E55D4AA458F5D60B238960E' as nvarchar(18))
	                                    then 'Плановый Аванс' 
	                                    when cast ((sys.fn_varbintohexstr(T1._Fld6832RRef)) as nvarchar(18))
	                                    = cast('0xBDB467D57DEA73474E67E741B52036A5' as nvarchar(18))
	                                    then 'Аванс За Первую Половину Месяца'   
	                                    when cast ((sys.fn_varbintohexstr(T1._Fld6832RRef)) as nvarchar(18))
	                                    = cast('0x8D2AE2BF02AE74BC47D9AC999938D251' as nvarchar(18))
	                                    then 'По Больничным Листам' 
	                                    when cast ((sys.fn_varbintohexstr(T1._Fld6832RRef)) as nvarchar(18))
	                                    = cast('0xBF9830EFE72108CE49F5BBEF6121E875' as nvarchar(18))
	                                    then 'По Беременности И Родам'   
	                                    when cast ((sys.fn_varbintohexstr(T1._Fld6832RRef)) as nvarchar(18))
	                                    = cast('0x84B483ABC090B16641EAD47DEE91D6B8' as nvarchar(18))
	                                    then 'Отпускные' 
	                                    when cast ((sys.fn_varbintohexstr(T1._Fld6832RRef)) as nvarchar(18))
	                                    = cast('0xBEADA6CFCE52CFB34B744D3B7FA2727E' as nvarchar(18))
	                                    then 'Командировочные'   
	                                    when cast ((sys.fn_varbintohexstr(T1._Fld6832RRef)) as nvarchar(18))
	                                    = cast('0xA79E8B1BBBCBE361491EADBBE4709DD2' as nvarchar(18))
	                                    then 'Премии' 
	                                    when cast ((sys.fn_varbintohexstr(T1._Fld6832RRef)) as nvarchar(18))
	                                    = cast('0xA5654E3AB1D0AC5C4440A30793BE4B0A' as nvarchar(18))
	                                    then 'Расчет При Увольнении' 
	                                    when cast ((sys.fn_varbintohexstr(T1._Fld6832RRef)) as nvarchar(18))
	                                    = cast('0x81967099CEEDEADE4E8E6B8D78660C35' as nvarchar(18))
	                                    then 'Дивиденды'   
	                                    when cast ((sys.fn_varbintohexstr(T1._Fld6832RRef)) as nvarchar(18))
	                                    = cast('0x96BF9D06B5C5E28A48A363E1742A5132' as nvarchar(18))
	                                    then 'Прочие Разовые Начисления' 
	                                    when cast ((sys.fn_varbintohexstr(T1._Fld6832RRef)) as nvarchar(18))
	                                    = cast('0x899F0619595ADE6C40349471EBD4B6BF' as nvarchar(18))
	                                    then 'Пособия ФСС'   
	                                    when cast ((sys.fn_varbintohexstr(T1._Fld6832RRef)) as nvarchar(18))
	                                    = cast('0x9692F18FDF9720B041AEEDE397918A52' as nvarchar(18))
	                                    then 'ВозвратНДФЛ' 
	                                    when cast ((sys.fn_varbintohexstr(T1._Fld6832RRef)) as nvarchar(18))
	                                    = cast('0xA7DFC6F5B3CF820F4EBBFB4DB52E0DE8' as nvarchar(18))
	                                    then 'Задолженность' 
                                    end as 'Характер выплаты',  

                                    case 
	                                    when cast ((sys.fn_varbintohexstr(T1._Fld6833RRef)) as nvarchar(18))
	                                    = cast('0xB17249345E902311426EB61A0C1E1F85' as nvarchar(18))
	                                    then 'Через Кассу'   
	                                    when cast ((sys.fn_varbintohexstr(T1._Fld6833RRef)) as nvarchar(18))
	                                    = cast('0x8DFBBE1A60552B394E1560DBE3CA9417' as nvarchar(18))
	                                    then 'Через Банк' 
                                    end as 'Способ выплаты', 

                                    'Документ ''Зарплата к выплате'' № '+T3._Number+' от ' +
                                    CONVERT (varchar, dateadd(year, -2000, T3._Date_Time), 104) as Документ
                                    FROM dbo._AccumRg6827 T1
                                    LEFT OUTER JOIN dbo._Reference117 T2
                                    ON T1._Fld6828RRef = T2._IDRRef
                                    LEFT OUTER JOIN dbo._Document145 T3
                                    ON T1._Fld6834RRef = T3._IDRRef
                                    WHERE(T2._Description like '" + FIO + "%') AND((T1._Period >= '" + startDateFormatted +
                                    "') AND(T1._Period <= '" + endDateFormatted + "')) AND T1._RecordKind = 1";

            DataTable dTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                connection.Open();
                adapter.Fill(dTable);
                //dataGrid1.ItemsSource = dTable.DefaultView;
                return dTable;
            }
        }

        /// <summary>
        /// Имя команды
        /// </summary>
        public string Name { get; set; } = "/my";
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

            //DataTable dTableAll = GetDataFromQuery(message.Text.Substring(6)); //? message.From.LastName+" "+message.From.FirstName
            DataTable dTableAll = GetDataFromQuery(message.From.LastName.Split()[0] + " " + message.From.FirstName.Split()[0]);
            string rowTextAll = "";
            if (dTableAll.Rows.Count != 0)
            {
                //запишем содержимое dTableAll в строку rowTextAll
                for (int curRow = 0; curRow < dTableAll.Rows.Count; curRow++)
                {
                    for (int curCol = 0; curCol < dTableAll.Columns.Count; curCol++)
                    {
                        rowTextAll += dTableAll.Rows[curRow][curCol].ToString() + "\n";
                    }
                    rowTextAll += "\n";
                }
                // в ответ на команду (фамилию), введенную пользователем в Telegram, выводим сообщение
                await client.SendTextMessageAsync(chatId, rowTextAll);//, replyToMessageId: messageId);
                Bot.ConsoleWriteLog(message);
            }
            else
            {
                //await client.SendTextMessageAsync(chatId, "Сотруднику " + message.Text.Substring(6) + " не начислялась зарплата в текущем месяце!",
                //        replyToMessageId: messageId);
                await client.SendTextMessageAsync(chatId, "Сотруднику " + message.From.LastName + " " + message.From.FirstName + " не начислялась зарплата в текущем месяце!");//, replyToMessageId: messageId);
                Bot.ConsoleWriteLog(message);
            }
        }
        public async void OnError(Message message, TelegramBotClient client)
        {
            await client.SendTextMessageAsync(message.Chat.Id, @"Введите ""/my"" ");
            Bot.ConsoleWriteLog(message);
        }
    }
}
