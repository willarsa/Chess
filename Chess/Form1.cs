using System;
using System.Drawing;
using System.Windows.Forms;

namespace Chess;

public partial class Form1 : Form
{
    private Button[,] boardSquares = new Button[8, 8];
    private ChessPiece[] whitePieces = new ChessPiece[16];
    private ChessPiece[] blackPieces = new ChessPiece[16];

    public Form1()
    {
        InitializeChessPieces();
        InitializeComponent();
        InitializeChessBoard();
    }

    private void InitializeChessBoard()
    {
        int tileSize = 60;
        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                Button square = new Button();
                square.Size = new Size(tileSize, tileSize);
                square.Location = new Point(col * tileSize, row * tileSize);
                square.BackColor = (row + col) % 2 == 0 ? Color.White : Color.Gray;
                square.Tag = GetPiece(col, row);
                square.Click += Square_Click;
                boardSquares[row, col] = square;
                this.Controls.Add(square);

                if (square.Tag is ChessPiece piece)
                {
                    square.Text = piece.Symbol;
                }

                square.Font = new Font("Arial", 18, FontStyle.Regular);
            }
        }

        this.ClientSize = new Size(8 * tileSize, 8 * tileSize);
    }

    private void InitializeChessPieces()
    {
        var whitePiecesPlaced = 0;
        var blackPiecesPlaced = 0;
        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                if (row == 1)
                {
                    blackPieces[blackPiecesPlaced] = new ChessPiece("Pawn", false, col, row);
                    blackPiecesPlaced++;
                }
                else if (row == 6)
                {
                    whitePieces[whitePiecesPlaced] = new ChessPiece("Pawn", true, col, row);
                    whitePiecesPlaced++;
                }
                else if (row == 0)
                {
                    if (col == 0 || col == 7)
                    {
                        blackPieces[blackPiecesPlaced] = new ChessPiece("Rook", false, col, row);
                    }
                    else if (col == 1 || col == 6)
                    {
                        blackPieces[blackPiecesPlaced] = new ChessPiece("Knight", false, col, row);
                    }
                    else if (col == 2 || col == 5)
                    {
                        blackPieces[blackPiecesPlaced] = new ChessPiece("Bishop", false, col, row);
                    }
                    else if (col == 4)
                    {
                        blackPieces[blackPiecesPlaced] = new ChessPiece("Queen", false, col, row);
                    }
                    else
                    {
                        blackPieces[blackPiecesPlaced] = new ChessPiece("King", false, col, row);
                    }
                    blackPiecesPlaced++;
                }
                else if (row == 7)
                {
                    if (col == 0 || col == 7)
                    {
                        whitePieces[whitePiecesPlaced] = new ChessPiece("Rook", true, col, row);
                    }
                    else if (col == 1 || col == 6)
                    {
                        whitePieces[whitePiecesPlaced] = new ChessPiece("Knight", true, col, row);
                    }
                    else if (col == 2 || col == 5)
                    {
                        whitePieces[whitePiecesPlaced] = new ChessPiece("Bishop", true, col, row);
                    }
                    else if (col == 4)
                    {
                        whitePieces[whitePiecesPlaced] = new ChessPiece("King", true, col, row);
                    }
                    else
                    {
                        whitePieces[whitePiecesPlaced] = new ChessPiece("Queen", true, col, row);
                    }
                    whitePiecesPlaced++;
                }
            }
        }

    }

    private ChessPiece? GetPiece(int col, int row)
    {
        for (int i = 0; i < 16; i++)
        {
            if (whitePieces[i] != null && whitePieces[i].Position == (col, row))
            {
                return whitePieces[i];
            }
        }
        for (int i = 0; i < 16; i++)
        {
            if (blackPieces[i] != null && blackPieces[i].Position == (col, row))
            {
                return blackPieces[i];
            }
        }
        return null;
    }

    private Button GetButton(int col, int row)
    {
        return boardSquares[col, row];
    }

    private void Square_Click(object? sender, EventArgs e)
    {
        Clear_Backs();
        if (sender is Button clicked)
        {
            ChessPiece? piece = clicked.Tag as ChessPiece;

            if (piece != null)
            {
                int direction = piece.IsWhite ? -1 : 1;
                string? type = piece.Type as string;
                (int x, int y)[] moves = new (int x, int y)[0];
                List<(int x, int y)> list = new List<(int x, int y)>(moves); //USE THIS TO STORE ALL POSSIBLE MOVES, THEN LOOP THROUGH TO DISPLAY THEM

                /* this goes down below in pawn field...
                FIND AND STORE ALL MOVES

                LOOP THROUGH AND CHECK BOUNDS ON EACH MOVE

                CHANGE COLOR FOR MOVE IF VALID
                */

                if (type == "Pawn")
                {
                    int newX = piece.Position.x;
                    int newY = (piece.Position.y == 6) ? piece.Position.y + (2 * direction) : piece.Position.y + (1 * direction);
                    if (newX >= 0 && newX <= 7 && newY >= 0 && newY <= 7)
                    {
                        Button button = GetButton(newY, newX);
                        button.BackColor = Color.DarkRed;
                    }
                }
            }
            else
            {
                MessageBox.Show("Position data missing.");
            }
        }
    }
    
    private void Clear_Backs()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                boardSquares[i,j].BackColor = (i + j) % 2 == 0 ? Color.White : Color.Gray;
            }
        }
    }
}
