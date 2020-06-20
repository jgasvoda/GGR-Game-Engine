using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGR_Game_Engine
{
    public class GameManager
    {
        private GameBoard Board { get; set; }
        private int CurrentPlayer { get; set; }

        private List<Piece> Player1Pieces { get; set; }
        private int Player1Score { get; set; }

        private List<Piece> Player2Pieces { get; set; }
        private int Player2Score { get; set; }

        public GameManager()
        {
            //Make a board with 4 rings
            Board = new GameBoard(4);


            //Add Pieces in starting locations
            Player1Pieces = new List<Piece>();
            Player2Pieces = new List<Piece>();

            for (int i = 0; i < 6; i++)
            {
                if (i < 3)
                {
                    Player1Pieces.Add(new Piece(1, new Coordinates(i, 0, 0)));
                }
                else
                {
                    Player2Pieces.Add(new Piece(2, new Coordinates(i, 0, 0)));
                }
            }

            CurrentPlayer = 1;
        }

        public int PlayGame()
        {
            while(GetVictor() == 0)
            {
                Console.WriteLine("Player " + CurrentPlayer + "'s Turn");
                if (CurrentPlayer == 1)
                {
                    PlayerTurn(Player1Pieces);
                }
                else
                {
                    PlayerTurn(Player2Pieces);
                }

                CurrentPlayer = CurrentPlayer == 1 ? 2 : 1;
            }
            return GetVictor();
        }

        public int GetVictor()
        {
            if (Player1Score >= 15)
            {
                return 1;
            }
            if (Player2Score >= 15)
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }

        public void PlayerTurn(List<Piece> playerPieces)
        {
            foreach(var piece in playerPieces)
            {
                Coordinates location = piece.CurrentLocation;
                BoardSpace space = Board.Spaces[location.Section][location.Row][location.Position];

                Console.WriteLine("Piece: " + location.ToString());
                Console.WriteLine("Move Forward, Left, or Right? (L,R,F)");

                var response = Console.ReadLine();
                switch(response)
                {
                    case ("F"):
                        piece.Move(space.AdjacentSpaces[0]);
                        break;
                    case ("L"):
                        piece.Move(space.AdjacentSpaces[1]);
                        break;
                    case ("R"):
                        piece.Move(space.AdjacentSpaces[2]);
                        break;
                }

                Console.WriteLine("Piece now at " + piece.CurrentLocation.ToString() + "\n");
            }
        }
    }
}
