using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace DeceitConsoleCheat

{
    public class Players
         
    {
        internal static List<Player> list = new List<Player>();
        internal static int indx = 0;
    }


    class PIayerDB
    {
        private static Player lplayer;

        public static Player TryGetPlayerByName(string name)
        {
            
          
           for  (int cal = 0; cal < 9999999; cal++)
            {
                
                Player lplayer = Players.list.ElementAt(Players.indx);
                if (Players.indx != Players.list.Count - 1) 
                { 
                      if(lplayer.Name == name)
                    {


                        Console.WriteLine(name+" Id "+lplayer.Id);
                        return (lplayer);
                    }
                    Players.indx++; 
                }
                else
                {
                    Console.WriteLine("Cant Find Player!");
                    
                    break;
                }

            }

     

          
            return (lplayer);

        }




            public static async Task UpdatePlayerStat(HttpClient client, Player lplayer)
        {

            try
            {
            
               string id = lplayer.Id;
                HttpResponseMessage response = await client.GetAsync("https://deceit-live.baseline.gg/stats?userId=" + id);// https://live.deceit.gg/
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                if (responseBody.Length < 40)
                {

                    Console.WriteLine("limit exceeded");

                }


                    ////////////////////////////////////////////////////////////////
                    responseBody = await response.Content.ReadAsStringAsync();
                var matchesB = Regex.Matches(responseBody, @"\""s_blood_drank\"":\d+"); //getBlood
                var Bloodval = matchesB.Cast<Match>()
                .Where((e) => e.Value.Split(':').Length == 2)
                .Select((e) => e.Value.Split(':')[1].Trim('\"'));
                string blood = string.Join("\r\n", Bloodval);
                if (blood != "")
                {
                    lplayer.Blood = Convert.ToInt32(blood);
                }
                else
                {
                    Console.WriteLine(lplayer.Name +"- NDB!");
                     lplayer.Blood = -1;
                }
                
                var matchesa = Regex.Matches(responseBody, @"\""games_played\"":\d+"); //getGameCount
                var gamesval = matchesa.Cast<Match>()
                .Where((e) => e.Value.Split(':').Length == 2)
                .Select((e) => e.Value.Split(':')[1].Trim('\"'));
                string games = string.Join("\r\n", gamesval);
                if (games != "")
                {
                    lplayer.games = Convert.ToInt32(games);
                }
                else
                {
                    Console.WriteLine(lplayer.Name + "NFG");
                    lplayer.games = -1;
                }
                var matchesas = Regex.Matches(responseBody, @"\""games_as_infected\"":\d+"); //getGameCount,"games_as_infected":
                var gamesvals = matchesas.Cast<Match>()
                .Where((e) => e.Value.Split(':').Length == 2)
                .Select((e) => e.Value.Split(':')[1].Trim('\"'));
                string gamess = string.Join("\r\n", gamesvals);
                if (gamess != "")
                {
                    lplayer.gamesasinf = Convert.ToInt32(gamess);
                }
                else
                {
                    Console.WriteLine(lplayer.Name + "NFG");
                    lplayer.gamesasinf = -1;
                }
            }
            catch
            {

            }
            
        }// task end
        public static async Task Gethiscores(HttpClient client, string type)
        {

            try
            {
                HttpResponseMessage response = await client.GetAsync("https://live.deceit.gg/hiscores?type=" + type);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                //   File.Create(id+".json").Close();
                //    File.AppendAllText(id+"User.json", responseBody + Environment.NewLine);   
                responseBody = await response.Content.ReadAsStringAsync();

                while (true)
                {

                    Player Pl = new Player();
                    ///////////////////////////////////////////////////////////////
                   // responseBody.Split("\",\"userId\":");
                    string localBody = responseBody.Split("{\"username\":\"").Last(); //getName
                    int nameIndex = localBody.LastIndexOf("\",\"userId\":");
                    string playername;
                    playername = localBody.Substring(0, nameIndex);
                Pl.Name = playername;
                     localBody.Substring(0, nameIndex);
                    //////////////////////////////////////////////////////////////// "userId":5623207
                    playername = localBody.Substring(0, nameIndex);
                    var matches = Regex.Matches(localBody, @"\""userId\"":\d+"); 
                    var Pid = matches.Cast<Match>()
                    .Where((e) => e.Value.Split(':').Length == 2)
                    .Select((e) => e.Value.Split(':')[1].Trim('\"'));
                    string PlId = string.Join("\r\n", Pid);
                     Pl.Id = PlId;
                    Pl.playerindex = Players.indx;
                    //////////////////////////////////////////////////////////////// 
                    ///



                    //////

                    Players.list.Add(Pl);
                    Players.indx++;
                    Player.subIndex = responseBody.LastIndexOf("}]");
                    if (Player.subIndex == -1) {
                        Player.subIndex = responseBody.LastIndexOf("},");
                    }
                    if (Player.subIndex == -1) {break;}
                    responseBody = responseBody.Substring(0, Player.subIndex); 
                }
;

            }
            catch
            {

            }

        }


    }// class end
}
