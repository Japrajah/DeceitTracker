using System;
using System.Collections.Generic;
using System.Text;

namespace DeceitConsoleCheat
{
    public class Player
    {
        internal static int subIndex = 0;
        private string _Name;
        private string _id;
        private int _Blood;
        private int _oldBlood;
        private string _pRank;
        private int _games;
        private int _oldgames;
        private int _playerindex;
        private int _gamesasinf;
        private int _gamesasinfold;
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
        new public int gamesasinf
        {
            get
            {
                return _gamesasinf;
            }
            set
            {
                _gamesasinf = value;
            }
        }
        new public int gamesasinfold
        {
            get
            {
                return _gamesasinfold;
            }
            set
            {
                _gamesasinfold = value;
            }
        }
        new public int playerindex
        {
            get
            {
                return _playerindex;
            }
            set
            {
                _playerindex = value;
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
}
