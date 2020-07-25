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
        private Player Player1 { get; set; }
        private Player Player2 { get; set; }
        private Player CurrentPlayer { get; set; }


        public GameManager()
        {
            //Make a board with 4 rings
            Board = new GameBoard(4);


            //Add Pieces in starting locations
            Player1 = new Player(1);
            Player2 = new Player(2);

            for (int i = 0; i < 6; i++)
            {
                if (i < 3)
                {
                    Player1.Pieces.Add(new Piece(1, new Coordinates(i, 0, 0)));
                }
                else
                {
                    Player2.Pieces.Add(new Piece(2, new Coordinates(i, 0, 0)));
                }
            }
            
            CurrentPlayer = Player1;
        }

        public int PlayGame()
        {
            while(GetVictor() == 0)
            {
                Console.WriteLine("Player " + CurrentPlayer + "'s Turn");
                
                PlayerTurn(CurrentPlayer.Pieces);

                if(CurrentPlayer.Team == 1)
                {
                    CurrentPlayer = Player2;
                }
                else
                {
                    CurrentPlayer = Player1;
                }
            }
            return GetVictor();
        }

        public int GetVictor()
        {
            if (Player1.Score >= 2)
            {
                return 1;
            }
            if (Player2.Score >= 2)
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
                int amountMined = 0;

                Console.WriteLine("Piece: " + location.ToString());
                Console.WriteLine("Move Forward, Left, or Right? (F,L,R) or Mine? (M)");

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
                    case ("M"):
                        if(space.asteroid != null)
                        {
                            amountMined = space.asteroid.Mine(1);
                            Console.WriteLine(amountMined + " mineral successfully mined!");
                        }
                        else
                        {
                            Console.WriteLine("No asteroid at this location");
                        }
                        
                        break;
                }

                Console.WriteLine("Piece now at " + piece.CurrentLocation.ToString() + "\n");

                piece.CollectGold(amountMined);

                Coordinates newLocation = piece.CurrentLocation;
                BoardSpace newSpace = Board.Spaces[newLocation.Section][newLocation.Row][newLocation.Position];
                if (newSpace.asteroid != null)
                {
                    Console.WriteLine("Asteroid Discovered!\n");
                }
                else if(newLocation.Row == 0)
                {
                    CurrentPlayer.Score += piece.DepositGold();
                }
            }
        }
    }
}
