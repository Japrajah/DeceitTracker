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


        public static async Task Main(string[] args)
        {
            

            using var client = new HttpClient();

            await PIayerDB.Gethiscores(client, "elo");

            for (int elment = 1; elment < Players.list.Count; elment++)
            {
                Player plr = Players.list.ElementAt(elment);
                Console.WriteLine(plr.playerindex + " " + plr.Name);
            }


   
           string Inpt = Console.ReadLine();
            char[] separators = new char[] { ',' };
            string[] InptAr = Inpt.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            for (int elment = 0; elment < InptAr.Length; elment++)
            {
                StartTrack(client,InptAr[elment]);
                Thread.Sleep(1500);
            }

            Thread.Sleep(10000000);
            Console.ReadLine();


    }
        public static async Task StartTrack (HttpClient client, string place)
        {
            Player temper = Players.list.ElementAt(Convert.ToInt32(place));
            await PIayerDB.UpdatePlayerStat(client, temper);
            temper.oldgames = temper.games;
            temper.oldBlood = temper.Blood;
            Console.WriteLine(temper.Name + " Traked! ");
            while (true)
            {
                Thread.Sleep(10000);
                await PIayerDB.UpdatePlayerStat(client, temper); // UPDATE 
             
                if (temper.oldBlood != temper.Blood)
                {
                    Console.WriteLine(temper.Name + " Infected! ");
                    temper.oldBlood = temper.Blood;
                }
                if (temper.games != temper.oldgames)
                {
                    Console.WriteLine(temper.Name + " - Game Ends \n -------------------------------  ");
                    temper.oldgames = temper.games;
                }


            }



        }


    }


}