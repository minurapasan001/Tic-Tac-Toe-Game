using System;
using System.Linq;
using System.Threading;

class Program
{
    static char[] board = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
    static char playerSymbol;
    static char computerSymbol;
    static bool playerTurn = true;

    static void Main()
    {
        Console.WriteLine("\n       * -- Welcome to Tic-Tac-Toe --* ");
        DisplayRules();
        ChoosePlayerSymbol();

        bool playAgain = true;
        int playerWins = 0;
        int computerWins = 0;
        int draws = 0;

        while (playAgain)
        {
            InitializeBoard();
            bool gameWon = false;

            while (!gameWon)
            {
                DrawBoard();
                if (playerTurn)
                {
                    PlayerMove();
                    gameWon = CheckForWin(playerSymbol);
                }
                else
                {
                    ComputerMove(computerSymbol);
                    gameWon = CheckForWin(computerSymbol);
                }

                if (!gameWon && board.All(c => c == 'X' || c == 'O'))
                {
                    draws++;
                    Console.WriteLine("It's a draw!");
                    break;
                }

                playerTurn = !playerTurn;
            }

            DrawBoard();
            if (gameWon)
            {
                if (playerTurn)
                {
                    playerWins++;
                    Console.WriteLine("Player wins!");
                }
                else
                {
                    computerWins++;
                    Console.WriteLine("Computer wins!");
                }
            }

            Console.WriteLine($"Player Wins: {playerWins}, Computer Wins: {computerWins}, Draws: {draws}");
            Console.Write("Play again? (Press 'r' to restart or any other key to quit): ");
            char restartChoice = char.ToLower(Console.ReadKey().KeyChar);
            if (restartChoice != 'r')
            {
                playAgain = false;
            }
        }
    }

    static void DisplayRules()
    {
        Console.WriteLine("Rules in thi game:");
        Console.WriteLine("\n1. Choose 'X' or 'O' as your symbol.");
        Console.WriteLine("2. You go first.");
        Console.WriteLine("3. Enter a number to place your symbol in the corresponding cell.");
        Console.WriteLine("4. You win if you get three in a row.");
        Console.WriteLine("5. The game ends in a draw if the board is full.");
        Console.WriteLine("6. Press 'r' to restart the game.");
        Console.WriteLine();
    }

    static void ChoosePlayerSymbol()
    {
        Console.Write("Choose your symbol ('X' or 'O'): ");
        char choice = char.ToUpper(Console.ReadKey().KeyChar);
        if (choice == 'X' || choice == 'O')
        {
            playerSymbol = choice;
            computerSymbol = (playerSymbol == 'X') ? 'O' : 'X';
        }
        else
        {
            Console.WriteLine("Invalid choice. Defaulting to 'X'.");
            playerSymbol = 'X';
            computerSymbol = 'O';
        }
        Console.WriteLine($"\nYou chose '{playerSymbol}'. Let's begin!\n");
    }

    static void InitializeBoard()
    {
        for (int i = 0; i < board.Length; i++)
        {
            board[i] = (i + 1).ToString()[0];
        }
    }

    static void DrawBoard()
    {
        Console.Clear();
        Console.WriteLine("  " + board[0] + " | " + board[1] + " | " + board[2]);
        Console.WriteLine(" ---+---+---");
        Console.WriteLine("  " + board[3] + " | " + board[4] + " | " + board[5]);
        Console.WriteLine(" ---+---+---");
        Console.WriteLine("  " + board[6] + " | " + board[7] + " | " + board[8]);
        Console.WriteLine();
    }

    static void PlayerMove()
    {
        Console.Write("Enter your move (1-9): ");
        if (int.TryParse(Console.ReadKey().KeyChar.ToString(), out int move) && move >= 1 && move <= 9 && board[move - 1] != 'X' && board[move - 1] != 'O')
        {
            board[move - 1] = playerSymbol;
        }
        else
        {
            Console.WriteLine("\nInvalid move. Try again.");
            PlayerMove();
        }
    }

    static void ComputerMove(char symbol)
    {
        Console.WriteLine("\nComputer is making a move...");
        Thread.Sleep(1000);

            for (int i = 0; i < board.Length; i++)
        {
            if (board[i] != 'X' && board[i] != 'O')
            {
                char originalValue = board[i];
                board[i] = symbol;

                if (CheckForWin(symbol))
                {
                    return;
                }

                board[i] = originalValue;
            }
        }

        char opponentSymbol = (symbol == 'X') ? 'O' : 'X';
        for (int i = 0; i < board.Length; i++)
        {
            if (board[i] != 'X' && board[i] != 'O')
            {
                char originalValue = board[i];
                board[i] = opponentSymbol;

                if (CheckForWin(opponentSymbol))
                {
                    board[i] = symbol;
                    return;
                }

                board[i] = originalValue;
            }
        }

        Random random = new Random();
        int randomMove;
        do
        {
            randomMove = random.Next(0, 9);
        } while (board[randomMove] == 'X' || board[randomMove] == 'O');

        board[randomMove] = symbol;
    }

    static bool CheckForWin(char symbol)
    {
        return (board[0] == symbol && board[1] == symbol && board[2] == symbol) ||  
               (board[3] == symbol && board[4] == symbol && board[5] == symbol) ||  
               (board[6] == symbol && board[7] == symbol && board[8] == symbol) ||  
               (board[0] == symbol && board[3] == symbol && board[6] == symbol) || 
               (board[1] == symbol && board[4] == symbol && board[7] == symbol) ||  
               (board[2] == symbol && board[5] == symbol && board[8] == symbol) ||  
               (board[0] == symbol && board[4] == symbol && board[8] == symbol) ||  
               (board[2] == symbol && board[4] == symbol && board[6] == symbol);


        for (int i = 0; i < board.Length; i++)
        {
            if (board[i] != 'X' && board[i] != 'O')
            {
                char originalValue = board[i];
                board[i] = symbol;

                if ((board[0] == symbol && board[1] == symbol && board[2] == symbol) ||  
                    (board[3] == symbol && board[4] == symbol && board[5] == symbol) ||  
                    (board[6] == symbol && board[7] == symbol && board[8] == symbol) ||  
                    (board[0] == symbol && board[3] == symbol && board[6] == symbol) ||  
                    (board[1] == symbol && board[4] == symbol && board[7] == symbol) ||  
                    (board[2] == symbol && board[5] == symbol && board[8] == symbol) ||  
                    (board[0] == symbol && board[4] == symbol && board[8] == symbol) ||  
                    (board[2] == symbol && board[4] == symbol && board[6] == symbol))   
                {
                    return true;
                }

                board[i] = originalValue;
            }
        }

        return false;
    }
}

