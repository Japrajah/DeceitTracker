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
            Console.WriteLine("Awalable: \nelo\nxp\nrep");
            string type;
            while (true)
            {

                type = Console.ReadLine();
                if (type == "elo" || type == "xp" || type == "rep")
                {
                    break;
                }


            }
            await PIayerDB.Gethiscores(client, type);
   

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

            Thread.Sleep(999999999);
            Console.ReadLine();
            Console.ReadLine();
          

        }
        public static async Task StartTrack (HttpClient client, string place)
        {
            Player temper = Players.list.ElementAt(Convert.ToInt32(place));
            await PIayerDB.UpdatePlayerStat(client, temper);
            temper.oldgames = temper.games;
            temper.gamesasinfold = temper.gamesasinf;
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
                    temper.oldgames = temper.games;
                    if (temper.gamesasinfold != temper.gamesasinf)
                    {
                        Console.WriteLine(temper.Name + " - Game Ends As Infected! \n ------------------------------- ");
                        temper.gamesasinfold = temper.gamesasinf;
                    }
                    else
                    {
                        Console.WriteLine(temper.Name + " - Game Ends \n -------------------------------  ");
                    }
                }


            }



        }


    }


}