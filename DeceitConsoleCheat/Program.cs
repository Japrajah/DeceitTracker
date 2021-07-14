using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Linq;

using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;


namespace DeceitConsoleCheat
{


    class Program
    {
        public class TempUser
        {

            internal static int PlayerID;
            internal static string Name;
            internal static int Blood;
            internal static int newBlood;

        }

        public class User1
        {

            internal static int PlayerID;
            internal static string Name;
            internal static int Blood;
            internal static int newBlood;

        }
       
        public System.Diagnostics.ProcessModuleCollection Modules { get; }
        public static async Task Main(string[] args)
        {
            

            using var client = new HttpClient();
       //     Console.WriteLine("Write id");
   //        User1.PlayerID = Convert.ToInt32(Console.ReadLine());
            await PIayerDB.Gethiscores(client, "elo");
            Player temper = PIayerDB.TryGetPlayerByName("morgenshtern");
            await PIayerDB.GetPlayerStat(client, temper);

            User1.Blood = temper.Blood;
            while (true)
            {
                Thread.Sleep(5000);
               // await PIayerDB.GetPlayerStatByID(client, User1.PlayerID.ToString());
                        User1.newBlood = TempUser.Blood;
                if (User1.newBlood != User1.Blood) { 
                        Console.WriteLine(User1.Name + "\n   Current_blood: " + User1.newBlood + "\n   old_Blood: " + User1.Blood);
                    }
               

            }
        }






        public static async Task GetPlayerStatByID(HttpClient client, string id)
        {

            try
            {  
                HttpResponseMessage response = await client.GetAsync("https://deceit-live.baseline.gg/stats?userId=" + id);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                //   File.Create(id+".json").Close();
                //    File.AppendAllText(id+"User.json", responseBody + Environment.NewLine);   
                        TempUser.PlayerID = Convert.ToInt32(id);
                ///////////////////////////////////////////////////////////////
                responseBody = await response.Content.ReadAsStringAsync();  
                responseBody = responseBody.Split("\"name\":\"").Last(); //getName
                int name = responseBody.LastIndexOf("\",\"elo\":");
                        string playername;
                        playername = responseBody.Substring(0, name);
                        //  Console.WriteLine("Nickname - " + playername);
                        TempUser.Name = playername;
                ////////////////////////////////////////////////////////////////
                responseBody = await response.Content.ReadAsStringAsync();
                var matchesB = Regex.Matches(responseBody, @"\""s_blood_drank\"":\d+"); //getBlood
                        var steamidsB = matchesB.Cast<Match>()
                        .Where((e) => e.Value.Split(':').Length == 2)
                        .Select((e) => e.Value.Split(':')[1].Trim('\"'));
                        string blood = string.Join("\r\n", steamidsB);
                if (blood != "")
                {
                    TempUser.Blood = Convert.ToInt32(blood);


                }
                else
                {
                    TempUser.Blood = -1;
                }


            }
            catch
            {

            }

        }
       
       
     

        
    }
}
