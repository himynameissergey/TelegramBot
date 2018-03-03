using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.IO;

namespace TelegramBot
{
    class PowerShellCommand : ICommand
    {
        /// <summary>
        /// Имя команды
        /// </summary>
        public string Name { get; set; } = "/ad";
        public int CountArgs { get; set; } = 1;
        /// <summary>
        /// Вызывает команду
        /// </summary>
        /// <param name="message">принимает сообщение</param>
        /// <param name="client">Ссылка на экземпляр бота</param>
        public async void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var userId = message.From.Id;
            var messageId = message.MessageId;
            //if (message.Text.Length > 8)
            //{

            string script = @"  #хочу найти что то похожее на это:
                                $want=""" + message.Text.Split()[1] + @"""
                                if ( $want.Length -lt 7 ) { echo ""Длина шаблона для поиска не может быть меньше 7 символов"" }
                                else {
                                    #ищем среди компьютеров
                                    $name = [string]::Concat($want, ""$"")
                                    $comps = Get-ADComputer -Filter { SamAccountName -like $name } -SearchBase ""OU=WKS,OU=R32,OU=FGUP,DC=main,DC=russianpost,DC=ru"" -Properties Description
                                    #ищем среди пользователей
                                    $users = Get-ADUser -Filter { SamAccountName -like $want -or Name -like $want -or CN -like $want -or DisplayName -like $want } -SearchBase  ""OU=Users,OU=R32,OU=FGUP,DC=main,DC=russianpost,DC=ru"" -Properties DisplayName,SamAccountName,Department,Title,Description,PasswordExpired,LastLogonDate,Enabled,LockedOut
                                    if ( $comps.Length -gt 1 ) {
                                        ForEach-Object -InputObject $comps -Process { ft -Property ObjectClass,SamAccountName -InputObject $_ -HideTableHeaders -AutoSize}
                                    } else {
                                        $comps | fl -Property ObjectClass,SamAccountName,Enabled,Description
                                    }
                                    if ( $users.Length -gt 1 ) {
                                        ForEach-Object -InputObject $users -Process { ft -Property  ObjectClass,DisplayName,SamAccountName -InputObject $_ -HideTableHeaders -AutoSize}
                                    } else {
                                        $users | fl -Property ObjectClass,DisplayName,SamAccountName,Department,Title,Description,PasswordExpired,LastLogonDate,Enabled,LockedOut
                                    }
                                }
                                ";

            string result = RunScript(script);
            //string result = "перезагрузка\n.\n.\n.\n";
            if (result != null)
            {
                if (result.Length > 3000)   //слишком длинная строка (слишком много вариантов)
                {
                    await client.SendTextMessageAsync(chatId, @"По строке """ + message.Text.Split()[1] + @""" найдено слишком много вариантов. Укажите более полное имя компьютера или пользователя");
                    Bot.ConsoleWriteLog(message);
                    return;
                }
                else if (result.Length < 10)    //что-то пошло не так
                {
                    await client.SendTextMessageAsync(chatId, "Компьютер или пользователь с именем " + message.Text.Split()[1] + " не найден в Active Directory");
                    Bot.ConsoleWriteLog(message);
                }
                else  //вот теперь все чотко
                {
                    await client.SendTextMessageAsync(userId, result);
                    Bot.ConsoleWriteLog(message);
                }
            }
            else
            {
                await client.SendTextMessageAsync(chatId, "В параметре " + message.Text.Split()[1] + " указаны некорректные символы");
                Bot.ConsoleWriteLog(message);
            }
        }
        public async void OnError(Message message, TelegramBotClient client)
        {
            await client.SendTextMessageAsync(message.Chat.Id, @"Введите ""/ad"" и через пробел часть имени компьютера или пользователя");
            Bot.ConsoleWriteLog(message);
        }
        static string RunScript(string scriptText)
        {
            try
            {
                Runspace runspace = RunspaceFactory.CreateRunspace(); // создание процесса
                runspace.Open(); // открытие процесса
                Pipeline pipeline = runspace.CreatePipeline(); // создание конвейера
                pipeline.Commands.AddScript(scriptText); //добавление сценария
                pipeline.Commands.Add("Out-String"); // эта команда форматирует вывод. Без нее возвращаются реальные объекты.
                Collection<PSObject> results = pipeline.Invoke(); // запуск сценария
                runspace.Close(); // закрыте процесса
                StringBuilder stringBuilder = new StringBuilder(); // конвертация результата в одну строку с использованием StringBuilder;
                foreach (PSObject obj in results)
                {
                    stringBuilder.AppendLine(obj.ToString());
                }
                return stringBuilder.ToString(); // возврат значения
            }
            catch (Exception exept)
            {
                Console.WriteLine(exept.Message);
                return null; // возврат значения
            }
        }
    }
}
