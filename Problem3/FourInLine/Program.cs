using System;

namespace FourInLine
{
    public class FourInLineSample
    {
        // board size
        public static int ROWS = 6;
        public static int COLUMNS = 7;
        // how many discs to win
        public static int WIN = 4;
        // the discs
        public static char XPLAYER = 'X';
        public static char OPLAYER = 'O';
        public static char EMPTY = ' ';
        // the main menu
        public static char PLAY = '1';
        public static char PLAYCOMPUTER = '2';
        public static char QUIT = '0';

        // first index is row (horizontal), second is column (vertical)
        // [0,0] is the top left board cell
        public static char[][] board = new char[ROWS][]; //new char[ROWS][COLUMNS];

        public static char currentPlayer; // XPLAYER or OPLAYER
        public static Boolean gameover = false;
        public static Boolean computerplays = false;

        public static void initializeBoard(char[][] board)
        {
            for (int i = 0; i < ROWS; i++)
            {
                board[i] = new char[COLUMNS];
            }
            for (int i = 0; i < ROWS; i++)
                for (int j = 0; j < COLUMNS; j++)
                    board[i][j] = EMPTY;
        }

        // 'O' is player 1, 'X' is player 2
        public static int playerNum(char c)
        {
            if (c == XPLAYER) return 2; else return 1;
        }

        public static void printBoard(char[][] board)
        {
            Console.WriteLine("Printing board:");
            Console.WriteLine();
            for (int j = 0; j < ROWS; j++)
            {
                Console.Write("|");
                for (int k = 0; k < COLUMNS; k++)
                    Console.Write(board[j][k] + "|");
                Console.WriteLine();
            }
            for (int k = 0; k < 2 * COLUMNS + 1; k++)
                Console.Write("-");
            Console.WriteLine();
            Console.WriteLine();
        }

        public static Boolean isColumnFull(char[][] board, int colIndex)
        {
            for (int i = 0; i < ROWS; i++)
            {
                if (board[i][colIndex] == EMPTY)
                    return false;
            }
            return true;
        }

        // returns the ROW index of the first empty cell in the COLUMN rowIndex. -1 if all full
        public static int firstEmptyRow(char[][] board, int colIndex)
        {
            for (int i = ROWS - 1; i >= 0; i--)
            {
                if (board[i][colIndex] == EMPTY) return i;
            }
            return -1;
        }

        // is the disc at board[rowIndex][colIndex] winning?
        public static Boolean winningDisk(char[][] board, int rowIndex, int colIndex)
        {
            char c = board[rowIndex][colIndex];
            int count = 1;

            // horizontal right
            for (int i = colIndex + 1; i < COLUMNS; i++)
            {
                if (board[rowIndex][i] == c)
                    count++;
                else break;
            }
            if (count >= WIN) return true; // won horizontally
            // keep counting horizontal left
            for (int i = colIndex - 1; i >= 0; i--)
            {
                if (board[rowIndex][i] == c)
                    count++;
                else break;
            }
            if (count >= WIN) return true; // won horizontally

            count = 1;
            // vertical down
            for (int i = rowIndex + 1; i < ROWS; i++)
            {
                if (board[i][colIndex] == c)
                    count++;
                else break;
            }
            if (count >= WIN) return true; // won vertical
            // keep counting vertical up
            for (int i = rowIndex - 1; i >= 0; i--)
            {
                if (board[i][colIndex] == c)
                    count++;
                else
                    break;
            }
            if (count >= WIN) return true; // won vertical

            // first diagonal:  /
            count = 1;
            // up
            int kol = colIndex + 1;
            for (int i = rowIndex - 1; i >= 0; i--)
            {
                if (kol >= COLUMNS) break; // we reached the end of the board right side
                if (board[i][kol] == c)
                    count++;
                else
                    break;
                kol++;
            }
            if (count >= WIN) return true;
            // keep counting down
            kol = colIndex - 1;
            for (int i = rowIndex + 1; i < ROWS; i++)
            {
                if (kol < 0) break; // we reached the end of the board left side
                if (board[i][kol] == c)
                    count++;
                else
                    break;
                kol--;
            }
            if (count >= WIN) return true; // won diagonal "/"

            // second diagonal : \
            count = 1;
            // up
            kol = colIndex - 1;
            for (int i = rowIndex - 1; i >= 0; i--)
            {
                if (kol < 0) break; // we reached the end of the board left side
                if (board[i][kol] == c)
                    count++;
                else
                    break;
                kol--;
            }
            if (count >= WIN) return true; // won diagonal "\"
            // keep counting down
            kol = colIndex + 1;
            for (int i = rowIndex + 1; i < ROWS; i++)
            {
                if (kol >= COLUMNS) break; // we reached the end of the board right side
                if (board[i][kol] == c)
                    count++;
                else
                    break;
                kol++;
            }
            if (count >= WIN) return true; // won diagonal "\"

            return false;
        }

        public static void changePlayer()
        {
            if (currentPlayer == OPLAYER)
                currentPlayer = XPLAYER;
            else currentPlayer = OPLAYER;
        }

        public static void showWinner(char winner, Boolean isComp)
        {
            if (winner == EMPTY)
                Console.Write("Board is full! game has ended with a tie!");
            else
                if (isComp && playerNum(winner) == 2) Console.WriteLine("Game has ended! The computer won!");
                else Console.WriteLine("Game has ended! Player " + playerNum(winner) + " won!");
            Console.WriteLine();
        }

        public static void printMenu()
        {
            Console.WriteLine(QUIT + ". Exit");
            Console.WriteLine(PLAY + ". Play against a friend");
            Console.WriteLine(PLAYCOMPUTER + ". Play against the computer");
            Console.WriteLine("Please choose an option:");
        }

        public static Boolean boardIsFull(char[][] board)
        {
            // it's enough to check top row
            for (int i = 0; i < COLUMNS; i++)
                if (board[0][i] == EMPTY) return false;
            return true;
        }

        // returns a column number within 0...COLUMNS, -1 if board is full
        public static int computerChoice()
        {
            int emptyrow = 0;
            // first check if a move can win
            for (int i = 0; i < COLUMNS; i++)
            {
                if (!isColumnFull(board, i))
                {
                    emptyrow = firstEmptyRow(board, i);
                    board[emptyrow][i] = XPLAYER;
                    if (winningDisk(board, emptyrow, i))
                    {
                        board[emptyrow][i] = EMPTY; // reset
                        return i;
                    }
                    board[emptyrow][i] = EMPTY; // reset
                }
            }
            // otherwise then pick up any move that will prevent other player to win 
            // in case there is a win on next turn
            int counter = 0; // i count other player possible winnings
            int chosenrow = 0;
            for (int i = 0; i < COLUMNS; i++)
            {
                if (!isColumnFull(board, i))
                {
                    emptyrow = firstEmptyRow(board, i);
                    board[emptyrow][i] = OPLAYER; // assume the other player does this
                    if (winningDisk(board, emptyrow, i))
                    {
                        board[emptyrow][i] = EMPTY; // reset
                        counter++; // we found a winning disc
                        chosenrow = i; // remember the row
                    }
                    board[emptyrow][i] = EMPTY; // reset
                }
            }
            // we block the player if there is exactly one winning disc 
            if (counter == 1) return chosenrow;

            // else if other player wins no matter what, pick up first non full column
            for (int i = 0; i < COLUMNS; i++)
                if (!isColumnFull(board, i))
                {
                    return i;
                }
            return -1;

        }
        public static void Main(String[] args)
        {
            //Scanner terminalInput = new Scanner(System.in);

            int choice, row, col;
            Boolean badchoice = true;

            Console.WriteLine("Welcome to Four in a Line!");

            // each loop is a new game
            while (true)
            {
                do
                {
                    badchoice = false;
                    printMenu();
                    choice = Int32.Parse(Console.ReadLine()); // no exception handling...
                    badchoice = choice < 0 || choice > 2;
                    if (badchoice) Console.WriteLine("Input incorrect! Please try again.");
                } while (badchoice);

                // 0: quit the game
                if (choice == 0)
                {
                    Console.WriteLine("Bye bye!");
                    return;
                }
                // start the game
                initializeBoard(board);
                Console.WriteLine();

                printBoard(board); // empty board
                Console.WriteLine("Starting a game of 'Four in a Line'.");

                currentPlayer = OPLAYER;
                gameover = false;
                computerplays = false;
                if (choice == 2) computerplays = true;

                do
                {
                    // loop as long as the chosen column is full
                    // we request the player to enter a column which is not full
                    do
                    {
                        if (computerplays && currentPlayer == XPLAYER)
                        {
                            col = computerChoice();
                            Console.Write("Computer put a disk in column ");
                            Console.WriteLine(col + 1);
                            //System.out.println();
                        }
                        else
                        {
                            Console.Write("Player " + playerNum(currentPlayer) + ", choose a column: ");
                            col = Int32.Parse(Console.ReadLine()); // no exception handling...
                            col--; // the real index
                        }

                        row = -1;

                        // is this really a column number?
                        if (col < 0 || col >= COLUMNS)
                            Console.WriteLine("Illegal column number");
                        else
                            // find the row and check if winning
                            if (!isColumnFull(board, col))
                            {
                                row = firstEmptyRow(board, col);
                                Console.WriteLine();
                            }
                            else
                                // column is full, try again
                                Console.WriteLine("Column is full.");

                    } while (row == -1);
                    // now we have a valid (row,col) cell

                    board[row][col] = currentPlayer;

                    // in any case we print the board
                    printBoard(board);

                    if (winningDisk(board, row, col))
                    {
                        gameover = true;
                        showWinner(currentPlayer, computerplays);
                    }
                    else
                        if (boardIsFull(board))
                        {
                            gameover = true;
                            showWinner(EMPTY, computerplays); // tie
                        }
                    // switch to next player
                    changePlayer();

                } while (!gameover);
            }
        }
    }
}
