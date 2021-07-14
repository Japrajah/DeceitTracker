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



        public class Player
    {
        private string _Name;
        private string _id;
        private int _Blood;
        private int _oldBlood;
        private string _pRank;
        private int _games;
        private int _oldgames;
        new public int games
        {
            get
            {
                return _games;
            }
            set
            {
                _games = value;
            }
        }
        new public int oldgames
        {
            get
            {
                return _oldgames;
            }
            set
            {
                _oldgames = value;
            }
        }
        new public string Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        new public int oldBlood
        {
            get
            {
                return _oldBlood;
            }
            set
            {
                _oldBlood = value;
            }
        }
        new public string pRank
        {
            get
            {
                return _pRank;
            }
            set
            {
                _pRank = value;
            }
        }
        new public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }
        new public int Blood
        {
            get
            {
                return _Blood;
            }
            set
            {
                _Blood = value;
            }
        }
      
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
                HttpResponseMessage response = await client.GetAsync("https://live.deceit.gg/stats?userId=" + id);// https://live.deceit.gg/
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                //   File.Create(id+".json").Close();
                //    File.AppendAllText(id+"User.json", responseBody + Environment.NewLine);   
              
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
                //     lplayer = Players.list.ElementAt(Players.indx);
                //    Players.list.ElementAt(Players.indx);
                //     if (Players.indx != Players.list.Count - 1) { Players.indx++; }
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
                    //////////////////////////////////////////////////////////////// 
            //        var matches1 = Regex.Matches(localBody, @"\""rank\"":\d+"); //getRank TEMP IN FUTURE changed to get LEVEL BY ID
            //        var Rank = matches1.Cast<Match>()
            //        .Where((e) => e.Value.Split(':').Length == 2)
            //        .Select((e) => e.Value.Split(':')[1].Trim('\"'));
            //        string pRank = string.Join("\r\n", Rank);
            //Pl.pRank = pRank;
                    Players.list.Add(Pl);

                    int subIndex = responseBody.LastIndexOf("},");
                    if (subIndex == -1) {break;}
                    responseBody = responseBody.Substring(0, subIndex); 
                }




                //if (PlId != "")
                //{
                //    Pl.Blood = Convert.ToInt32(PlId);


                //}
                //else
                //{
                //    Pl.Blood = -1;
                //}
                //Players.list.Add(Pl);

            }
            catch
            {

            }

        }


    }// class end
}
