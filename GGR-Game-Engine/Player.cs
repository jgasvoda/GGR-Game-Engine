using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGR_Game_Engine
{
    public class Player
    {
        public int Team { get; set; }
        public List<Piece> Pieces { get; set; }
        public int Score { get; set; }

        public Player(int team)
        {
            Team = team;
            Pieces = new List<Piece>();
        }
    }
}
