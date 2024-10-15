using System.Text.Json;

namespace MajorBot
{
    static class Program
    {
        static List<MajorQuery>? LoadQuery()
        {
            try
            {
                var contents = File.ReadAllText(@"data.txt");
                return JsonSerializer.Deserialize<List<MajorQuery>> (contents);
            }
            catch { }

            return null;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("----------------------- Major Bot Starting -----------------------");
            Console.WriteLine();

            var MajorQueries = LoadQuery();

            foreach (var Query in MajorQueries ?? [])
            {
                var BotThread = new Thread(() => MajorThread(Query)); BotThread.Start();
                Thread.Sleep(60000);
            }
        }

        public async static void MajorThread(MajorQuery Query)
        {
            while (true)
            {
                var RND = new Random();

                var Bot = new MajorBots(Query);
                if (!Bot.HasError)
                {
                    Log.Show("Major", Query.Name, $"login successfully.", ConsoleColor.Green);
                    var Sync = await Bot.MajprUserDetail();
                    if (Sync is not null)
                    {
                        Log.Show("Major", Query.Name, $"synced successfully. B<{Sync.Rating}>", ConsoleColor.Blue);
                        var taskList = await Bot.MajorGetTasks(true);
                        if (taskList is not null)
                        {
                            foreach (var task in taskList.Where(x => x.IsCompleted == false & (x.Id == 5 | x.Id == 16)))
                            {
                                var claimTask = await Bot.MajorDoneTask(task.Id);
                                if (claimTask is not null)
                                {
                                    if (claimTask.IsCompleted)
                                        Log.Show("Major", Query.Name, $"task '{task.Title}' completed", ConsoleColor.Green);
                                    else
                                        Log.Show("Major", Query.Name, $"task '{task.Title}' failed", ConsoleColor.Red);

                                    int eachtaskRND = RND.Next(7, 20);
                                    Thread.Sleep(eachtaskRND * 1000);
                                }
                            }
                        }

                        int Durev = await Bot.MajorDurov();
                        if (Durev == 2)
                            Log.Show("Major", Query.Name, $"puzzle durov completed", ConsoleColor.Green);
                        else if (Durev == 0)
                            Log.Show("Major", Query.Name, $"puzzle durov failed", ConsoleColor.Red);
                        Thread.Sleep(25000);

                        int holdCoin = await Bot.MajorHoldCoin();
                        if (holdCoin == 2)
                            Log.Show("Major", Query.Name, $"hold coin completed", ConsoleColor.Green);
                        else if (holdCoin == 0)
                            Log.Show("Major", Query.Name, $"hold coin failed", ConsoleColor.Red);
                        Thread.Sleep(25000);

                        int roulette = await Bot.MajorRoulette();
                        if (roulette == 2)
                            Log.Show("Major", Query.Name, $"roulette completed", ConsoleColor.Green);
                        else if (roulette == 0)
                            Log.Show("Major", Query.Name, $"roulette failed", ConsoleColor.Red);
                        Thread.Sleep(25000);

                        int swipeCoin = await Bot.MajorSwipeCoin(RND.Next(1500, 2500));
                        if (swipeCoin == 2)
                            Log.Show("Major", Query.Name, $"swipe coin completed", ConsoleColor.Green);
                        else if (swipeCoin == 0)
                            Log.Show("Major", Query.Name, $"swipe coin failed", ConsoleColor.Red);
                        Thread.Sleep(25000);
                    }
                    else
                        Log.Show("Major", Query.Name, $"synced failed", ConsoleColor.Red);

                    Sync = await Bot.MajprUserDetail();
                    if (Sync is not null)
                        Log.Show("Major", Query.Name, $"B<{Sync.Rating}>", ConsoleColor.Blue);
                }
                else
                    Log.Show("Major", Query.Name, $"{Bot.ErrorMessage}", ConsoleColor.Red);

                int syncRND = RND.Next(25000, 30000);
                Log.Show("Major", Query.Name, $"sync sleep '{Convert.ToInt32(syncRND / 3600d)}h {Convert.ToInt32(syncRND % 3600 / 60d)}m {syncRND % 60}s'", ConsoleColor.Yellow);
                Thread.Sleep(syncRND * 1000);
            }
        }
    }
}