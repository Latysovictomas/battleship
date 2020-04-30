using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Battleship
{
    class BattleshipBoard
    {


        public void ShowBoard(char[,] Board, bool show = false)
        {
            int Row;
            int Column;

            Console.WriteLine("  ¦ 0 1 2 3 4 5 6 7 8 9");
            Console.WriteLine("--+--------------------");
            for (Row = 0; Row <= 9; Row++)
            {
                Console.Write((Row).ToString() + " ¦ ");
                for (Column = 0; Column <= 9; Column++)
                {
                    if (show)
                    {
                        Console.Write(Board[Column, Row] + " ");
                    }
                    else
                    {
                        if (Board[Column, Row].Equals('S'))
                        {
                            Console.Write(" " + " ");
                        }
                        else
                        {
                            Console.Write(Board[Column, Row] + " ");
                        }

                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine("\n");
        }
    }

    class Player
    {
        char[,] Grid = new char[10, 10];
        public int hitCount = 0;
        public int missCount = 0;
        public bool isGivingUp = false;
        int x = 0;
        int y = 0;


        public int getHitCount()
        {
            return hitCount;
        }
        public int getMissCount()
        {
            return missCount;
        }
        public void Shoot()
        {

            bool wrong = true;
            string line;
            int value;
            while (wrong == true)
            {
                Console.WriteLine("Enter X");
                line = Console.ReadLine();

                if (line.Equals("EXIT"))
                {
                    isGivingUp = true;
                    break;
                }

                if (int.TryParse(line, out value))
                {
                    x = value;
                    wrong = false;
                }
                else
                {
                    Console.WriteLine("Not an integer!");
                    wrong = true;
                }
            }
            wrong = true;
            if (isGivingUp == false)
            {
                while (wrong == true)
                {

                    Console.WriteLine("Enter Y");
                    line = Console.ReadLine();

                    if (line.Equals("EXIT"))
                    {
                        isGivingUp = true;
                        break;
                    }

                    if (int.TryParse(line, out value))
                    {
                        y = value;
                        wrong = false;
                    }
                    else
                    {
                        Console.WriteLine("Not an integer!");
                        wrong = true;
                    }
                }

                if (isGivingUp == false)
                {
                    try
                    {
                        if (Grid[x, y].Equals('S'))
                        {
                            Grid[x, y] = 'H';
                            Console.Clear();
                            Console.WriteLine("Message: Hit!\r\n");
                            hitCount += 1;
                        }
                        else if (Grid[x, y].Equals('H'))
                        {
                            Console.Clear();
                            Console.WriteLine("Message: Miss!\r\n");
                            missCount += 1;
                        }
                        else
                        {

                            Grid[x, y] = 'M';
                            Console.Clear();
                            Console.WriteLine("Message: Miss!\r\n");
                            missCount += 1;
                        }
                    }
                    catch
                    {
                        Console.Clear();
                        Console.WriteLine("Error: Enter integer from 0 to 9.");
                    }
                }
            }

        }
        public char[,] GetGrid()
        {
            return Grid;
        }
        public void SetGrid(int q, int w)
        {
            Grid[q, w] = 'S';
        }

        private void PlaceShipRandomly(int shipLength)
        {
            bool notValidPlace = true;
            Random rnd = new Random();

            while (notValidPlace)
            {
                int xRand = rnd.Next(0, 10);
                int yRand = rnd.Next(0, 10);

                if (!Grid[xRand, yRand].Equals('S'))
                {


                    Dictionary<string, Boolean> directions = new Dictionary<string, bool>();
                    directions.Add("Right", true);
                    directions.Add("Top", true);
                    directions.Add("Left", true);
                    directions.Add("Bottom", true);




                    for (int i = 1; i < shipLength; i++)
                    {
                        // right
                        if (xRand + i > 9)
                        {
                            directions["Right"] = false;

                        }

                        else if (Grid[xRand + i, yRand].Equals('S'))
                        {

                            directions["Right"] = false;
                        }

                        // top
                        if (yRand - i < 0)
                        {
                            directions["Top"] = false;

                        }
                        else if (Grid[xRand, yRand - i].Equals('S'))
                        {

                            directions["Top"] = false;

                        }
                        // left
                        if (xRand - i < 0)
                        {
                            directions["Left"] = false;
                        }
                        else if (Grid[xRand - i, yRand].Equals('S'))
                        {

                            directions["Left"] = false;
                        }

                        // bottom
                        if (yRand + i > 9)
                        {
                            directions["Bottom"] = false;
                        }

                        else if ((Grid[xRand, yRand + i].Equals('S')))
                        {

                            directions["Bottom"] = false;
                        }

                    }


                    // place ships in random direction
                    Random rand = new Random();

                    bool placed = false;

                    while (placed == false)
                    {
                        int r = rand.Next(0, directions.Count);

                        if (directions.ElementAt(r).Value == true)
                        {

                            for (int i = 0; i < shipLength; i++)
                            {

                                if (directions.ElementAt(r).Key == "Right")
                                {

                                    SetGrid(xRand + i, yRand);

                                }

                                else if (directions.ElementAt(r).Key == "Top")
                                {

                                    SetGrid(xRand, yRand - i);

                                }

                                else if (directions.ElementAt(r).Key == "Left")
                                {

                                    SetGrid(xRand - i, yRand);

                                }

                                else
                                {

                                    SetGrid(xRand, yRand + i);

                                }

                            }

                            placed = true;
                            notValidPlace = false;
                        }

                        directions.Remove(directions.ElementAt(r).Key);

                        if (directions.Count == 0)
                        {

                            break;
                        }


                    }

                }

            }

        }
        public void InitializeShips()
        {

            // Carrier
            PlaceShipRandomly(5);
            // Battleship
            PlaceShipRandomly(4);
            // Cruiser
            PlaceShipRandomly(3);
            // Submarine
            PlaceShipRandomly(3);
            // Destroyer
            PlaceShipRandomly(2);


        }

    }

    class Program
    {

        static void Main(string[] args)
        {
            Dictionary<int, int> difficulty = new Dictionary<int, int>();
            difficulty.Add(1, 80); // 1. easy
            difficulty.Add(2, 60); // 2. medium
            difficulty.Add(3, 40); // 3. hard
            bool isPlaying = true;
            while (isPlaying)
            {
                Console.Clear();
                Console.WriteLine("Single player Battleship!\r\n\r\n");
                Console.WriteLine("Choose difficulty.");
                Console.WriteLine("1. Easy ");
                Console.WriteLine("2. Medium ");
                Console.WriteLine("3. Hard ");
                Console.WriteLine("Your choice: ");
                string input = Console.ReadLine();
                bool wrongInput = true;
                int choice = 0;
                while (wrongInput)
                {
                    if (int.TryParse(input, out int number))
                    {
                        if (number < 1 || number > 3)
                        {
                            Console.WriteLine("Wrong input, try again: ");
                            input = Console.ReadLine();
                            wrongInput = true;
                        }
                        else
                        {
                            choice = number;
                            wrongInput = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Wrong input, try again: ");
                        input = Console.ReadLine();
                        wrongInput = true;
                    }
                }


                Console.WriteLine();
                BattleshipBoard b = new BattleshipBoard();
                Player p = new Player();
                p.InitializeShips();
                Console.Clear();
                bool gameState = true;
                while (gameState)
                {
                    Console.WriteLine("INFO: " + "Type 'EXIT' to give up\r\n");
                    Console.WriteLine("You cannot miss: " + difficulty[choice] + " times\r\n");
                    Console.WriteLine("You missed: " + p.getMissCount() + " times\r\n");
                    b.ShowBoard(p.GetGrid(), false);
                    p.Shoot();

                    if (p.getMissCount() == difficulty[choice] || p.isGivingUp == true)
                    {
                        Console.Clear();
                        Console.WriteLine("You lose! See the ships below. \r\n");
                        b.ShowBoard(p.GetGrid(), true);
                        gameState = false;
                    }
                    else if (p.getHitCount() == 17)
                    {
                        Console.Clear();
                        Console.WriteLine("Congratulations! You Won!\r\n");
                        gameState = false;
                    }
                }


                Console.WriteLine("Play again? (y/n)");
                string answer = Console.ReadLine();
                bool notValid = true;
                while (notValid)
                {
                    if (answer.ToLower().Equals("n"))
                    {


                        isPlaying = false;
                        notValid = false;
                    }

                    else if (answer.ToLower().Equals("y"))
                    {

                        isPlaying = true;
                        notValid = false;
                    }
                    else
                    {
                        Console.WriteLine("Wrong input. Enter y or n: ");
                        answer = Console.ReadLine();
                        notValid = true;
                    }

                }
            }

        }


    }
}