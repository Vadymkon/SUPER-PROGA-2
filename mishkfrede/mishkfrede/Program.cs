using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace mishkfrede
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new TelegramBotClient("6227402570:AAH4u_9IAHzsgp3yoS1xJwKHZTwTpUxjhTM");
            client.StartReceiving(Update,Error);
            Console.WriteLine("Сейчас ты сервер\n");
            Console.ReadLine();
        }
        static List <string> citis; static bool gamestart, game;
        async static void HaveCities()
        {
            gamestart = true;
            string[] readText = await System.IO.File.ReadAllLinesAsync("citis.txt");
            citis = readText.Select(x => x.Replace("\"", "").Replace(":", "").Trim()).ToList();
        }
        async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            var message = update.Message;
            if (message.Text != null)
            {

                if (message.Text.ToLower().Contains("привет"))
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "о, ну шо ты?");
                    return;
                }

                 if (message.Text.ToLower().Contains("/end"))
                {
                    citis.Clear();
                    game = false;
                    await botClient.SendTextMessageAsync(message.Chat.Id, "оки");
                    return;
                }

                if (message.Text.ToLower().Contains("/play"))
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Окей давай поиграем!");
                    HaveCities();
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Кто начинает?");
                    return;
                }
                if (gamestart)
                    if (message.Text.ToLower()[0] == 'я' && message.Text.Length == 1)
                    {
                        await botClient.SendTextMessageAsync(message.Chat.Id, "начинай");
                        game = true;
                        return;
                    }
                    else if (message.Text.Length == 2) if (message.Text.ToLower().StartsWith( "ты") || message.Text.ToLower().StartsWith("ти"))
                        {
                        await botClient.SendTextMessageAsync(message.Chat.Id, "окей");
                        game = true;
                        Random rnd = new Random();
                        int taken = rnd.Next(0, citis.Count - 1);
                        await botClient.SendTextMessageAsync(message.Chat.Id, citis[taken]);
                        citis.Remove(citis[taken]);
                        return;
                    }
                
                if (game)
                {
                    if (message.Text.ToLower().Contains("нахуй"))
                    {
                        await botClient.SendTextMessageAsync(message.Chat.Id, "шо такое? не смогли смирится с поражением? ахаха");
                        game = false; gamestart = false;
                        return;
                    }
                    try
                    {
                        string city = message.Text.Trim();
                        city = city[0].ToString().ToUpper() + city.Substring(1);
                        if (citis.Contains(city))
                        {

                        Console.WriteLine(city + "\n");
                        Console.WriteLine(city[city.Length - 1]  + "\n");
                        string word = citis.First(x => x.StartsWith(city[city.Length - 1].ToString().ToUpper()));
                        await botClient.SendTextMessageAsync(message.Chat.Id, word);
                        citis.Remove(word);
                        return;
                        }
                        else
                        {
                            await botClient.SendTextMessageAsync(message.Chat.Id, "Нету такого города, попробуй ещё разок");
                            return;
                        }
                    }
                    catch
                    {
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Ты выиграл!");
                        game = false;
                        return;
                    }
                }
            }
        }

        private static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            throw new NotImplementedException();
        }


    }
}
