using System;  
using System.Collections.Generic;  

namespace Chess
{
    public static class ChessMoves
    {
        public static Button[,] boardSquares = Form1.boardSquares;
        public static ChessPiece? selectedPiece = Form1.selectedPiece; 

        public static bool isInBounds(int x, int y)
        {
            if (y <= 7 && y >= 0 && x <= 7 && x >= 0)
            {
                return true;
            }
            return false;
        }

        public static bool sameSidePiece(int x, int y, bool isWhite)
        {
            if (boardSquares[y,x].Tag != null)
            {
                if (((ChessPiece?)boardSquares[y, x].Tag).isWhite == isWhite) {
                    return true;
                }
            }
            return false;
        } 

        public static Button? GetButtonFromPiece()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (boardSquares[i, j].Tag == selectedPiece)
                    {
                        return boardSquares[i, j];
                    }
                }
            }
            return null;
        }

        public static int GetButtonAxis(Button? button, string axis)
        { 
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (boardSquares[i, j] == button)
                    {
                        if (axis == "x")
                        {
                            return j;
                        }
                        else
                        {
                            return i;
                        }
                    }
                }
            }
            return -1;
        }

        public static void Line_Check(List<int> newXs, List<int> newYs, int x, int y, ChessPiece? piece)
        {
            if (piece != null)
            {
                bool shouldContinue = true;
                while (isInBounds(piece.Position.x + x, piece.Position.y + y))
                {
                    if (shouldContinue)
                    {
                        if (boardSquares[piece.Position.y + y, piece.Position.x + x].Tag == null)
                        {
                            newXs.Add(piece.Position.x + x);
                            newYs.Add(piece.Position.y + y);
                            if (x != 0) x += 1 * Math.Sign(x);
                            if (y != 0) y += 1 * Math.Sign(y);
                        }
                        else if (!sameSidePiece(piece.Position.x + x, piece.Position.y + y, ((ChessPiece?)boardSquares[piece.Position.y, piece.Position.x].Tag).isWhite))
                        {
                            newXs.Add(piece.Position.x + x);
                            newYs.Add(piece.Position.y + y);
                            shouldContinue = false;
                        }
                        else
                        {
                            shouldContinue = false;
                        }
                    }
                    else
                    {
                        break;
                    }
                }  
            }
            
        }

        public static void King_Moves(ChessPiece? piece)
        {
            if (piece != null)
            { 
                List<int> newXs = new List<int>();
                List<int> newYs = new List<int>();

                Knight_Check(newXs,newYs,piece.Position.x - 1,piece.Position.y,piece);
                Knight_Check(newXs,newYs,piece.Position.x - 1,piece.Position.y - 1,piece);
                Knight_Check(newXs,newYs,piece.Position.x,piece.Position.y - 1,piece);
                Knight_Check(newXs,newYs,piece.Position.x + 1,piece.Position.y + -1,piece);
                Knight_Check(newXs,newYs,piece.Position.x + 1,piece.Position.y,piece);
                Knight_Check(newXs,newYs,piece.Position.x + 1,piece.Position.y + 1,piece);
                Knight_Check(newXs,newYs,piece.Position.x,piece.Position.y + 1,piece);
                Knight_Check(newXs,newYs,piece.Position.x - 1,piece.Position.y + 1,piece);


                for (int i = 0; i < newXs.Count; i++)
                {
                    if (newXs[i] >= 0 && newXs[i] <= 7 && newYs[i] >= 0 && newYs[i] <= 7)
                    {
                        Button button = boardSquares[newYs[i], newXs[i]];
                        button.BackColor = Color.DarkRed;
                    }
                    else
                    {
                        Console.WriteLine("OUT OF BOUNDS");
                    }
                }
            }
            
        }

        public static void Queen_Moves(ChessPiece? piece)
        {
            List<int> newXs = new List<int>();
            List<int> newYs = new List<int>();

            Line_Check(newXs, newYs, 1, 0, piece);
            Line_Check(newXs, newYs, 0, 1, piece);
            Line_Check(newXs, newYs, -1, 0, piece);
            Line_Check(newXs, newYs, 0, -1, piece);
            Line_Check(newXs, newYs, 1, -1, piece);
            Line_Check(newXs, newYs, 1, 1, piece);
            Line_Check(newXs, newYs, -1, 1, piece);
            Line_Check(newXs, newYs, -1, -1, piece);

            for (int i = 0; i < newXs.Count; i++)
            {
                if (newXs[i] >= 0 && newXs[i] <= 7 && newYs[i] >= 0 && newYs[i] <= 7)
                {
                    Button button = boardSquares[newYs[i], newXs[i]];
                    button.BackColor = Color.DarkRed;
                }
                else
                {
                    Console.WriteLine("OUT OF BOUNDS");
                }
            }
        }

        public static void Rook_Moves(ChessPiece? piece)
        {
            List<int> newXs = new List<int>();
            List<int> newYs = new List<int>();

            Line_Check(newXs, newYs, 1, 0, piece);
            Line_Check(newXs, newYs, 0, 1, piece);
            Line_Check(newXs, newYs, -1, 0, piece);
            Line_Check(newXs, newYs, 0, -1, piece);

            for (int i = 0; i < newXs.Count; i++)
            {
                if (newXs[i] >= 0 && newXs[i] <= 7 && newYs[i] >= 0 && newYs[i] <= 7)
                {
                    Button button = boardSquares[newYs[i], newXs[i]];
                    button.BackColor = Color.DarkRed;
                }
                else
                {
                    Console.WriteLine("OUT OF BOUNDS");
                }
            }
        }

        public static void Bishop_Moves(ChessPiece? piece)
        {
            List<int> newXs = new List<int>();
            List<int> newYs = new List<int>();

            Line_Check(newXs, newYs, 1, -1, piece);
            Line_Check(newXs, newYs, 1, 1, piece);
            Line_Check(newXs, newYs, -1, 1, piece);
            Line_Check(newXs, newYs, -1, -1, piece);

            for (int i = 0; i < newXs.Count; i++)
            {
                if (newXs[i] >= 0 && newXs[i] <= 7 && newYs[i] >= 0 && newYs[i] <= 7)
                {
                    Button button = boardSquares[newYs[i], newXs[i]];
                    button.BackColor = Color.DarkRed;
                }
                else
                {
                    Console.WriteLine("OUT OF BOUNDS");
                }
            }
        }

        public static void Knight_Check(List<int> newXs, List<int> newYs, int x, int y, ChessPiece? piece)
        {
            if (piece != null)
            {
                if (isInBounds(x, y))
                {
                    if (!sameSidePiece(x, y, piece.isWhite))
                    {
                        newXs.Add(x);
                        newYs.Add(y);
                    }
                } 
            }
            
        }
        
        public static void Knight_Moves(ChessPiece? piece)
        {
            if (piece != null)
            {
                List<int> newXs = new List<int>();
                List<int> newYs = new List<int>();

                Knight_Check(newXs,newYs,piece.Position.x - 1,piece.Position.y - 2,piece);
                Knight_Check(newXs,newYs,piece.Position.x + 1,piece.Position.y - 2,piece);
                Knight_Check(newXs,newYs,piece.Position.x + 2,piece.Position.y - 1,piece);
                Knight_Check(newXs,newYs,piece.Position.x + 2,piece.Position.y + 1,piece);
                Knight_Check(newXs,newYs,piece.Position.x + 1,piece.Position.y + 2,piece);
                Knight_Check(newXs,newYs,piece.Position.x - 1,piece.Position.y + 2,piece);
                Knight_Check(newXs,newYs,piece.Position.x - 2,piece.Position.y - 1,piece);
                Knight_Check(newXs,newYs,piece.Position.x - 2,piece.Position.y + 1,piece);


                for (int i = 0; i < newXs.Count; i++)
                {
                    if (newXs[i] >= 0 && newXs[i] <= 7 && newYs[i] >= 0 && newYs[i] <= 7)
                    {
                        Button button = boardSquares[newYs[i], newXs[i]];
                        button.BackColor = Color.DarkRed;
                    }
                    else
                    {
                        Console.WriteLine("OUT OF BOUNDS");
                    }
                }
            }
            
        }

        public static void Pawn_Moves(ChessPiece? piece, int direction)
        {
            if (piece != null)
            {
                //add all possible moves to an array, then loop through and check if in bounds
                List<int> newXs = new List<int>();
                List<int> newYs = new List<int>();

                //General Move Forward Logic
                if (piece.Position.y + (1 * direction) <= 7 && piece.Position.y + (1 * direction) >= 0)
                {
                    if (boardSquares[piece.Position.y + (1 * direction), piece.Position.x].Tag == null)
                    {
                        if (!sameSidePiece(piece.Position.x, piece.Position.y + (1 * direction), piece.isWhite))
                        {
                            newXs.Add(piece.Position.x);
                            newYs.Add(piece.Position.y + (1 * direction));
                        }
                    }
                }

                //Move Two Square if at the beginning
                if ((piece.Position.y == 6 && piece.isWhite) || (piece.Position.y == 1 && !piece.isWhite))
                {
                    if (boardSquares[piece.Position.y + (2 * direction), piece.Position.x].Tag == null && boardSquares[piece.Position.y + (1 * direction), piece.Position.x].Tag == null)
                    {
                        if (!sameSidePiece(piece.Position.x, piece.Position.y + (2 * direction), piece.isWhite))
                        {
                            newXs.Add(piece.Position.x);
                            newYs.Add(piece.Position.y + (2 * direction));
                        }
                    }
                }
                //Target pieces to the top left and top right
                if (piece.Position.y + (1 * direction) <= 7 && piece.Position.y + (1 * direction) >= 0)
                {
                    if (piece.Position.x + 1 <= 7 && piece.Position.x + 1 >= 0)
                    {
                        if (boardSquares[piece.Position.y + (1 * direction), piece.Position.x + 1].Tag != null)
                        {
                            if (!sameSidePiece(piece.Position.x + 1, piece.Position.y + (1 * direction), piece.isWhite))
                            {
                                newXs.Add(piece.Position.x + 1);
                                newYs.Add(piece.Position.y + (1 * direction));
                            }
                        }
                    }

                    if (piece.Position.x - 1 <= 7 && piece.Position.x - 1 >= 0)
                    {
                        if (boardSquares[piece.Position.y + (1 * direction), piece.Position.x - 1].Tag != null)
                        {
                            if (!sameSidePiece(piece.Position.x - 1, piece.Position.y + (1 * direction), piece.isWhite))
                            {
                                newXs.Add(piece.Position.x - 1);
                                newYs.Add(piece.Position.y + (1 * direction));
                            }
                        }
                    }
                }

                for (int i = 0; i < newXs.Count; i++)
                {
                    if (newXs[i] >= 0 && newXs[i] <= 7 && newYs[i] >= 0 && newYs[i] <= 7)
                    {
                        Button button = boardSquares[newYs[i], newXs[i]];
                        button.BackColor = Color.DarkRed;
                    }
                    else
                    {
                        Console.WriteLine("OUT OF BOUNDS");
                    }
                }

            }

        }
        }
}