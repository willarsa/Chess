using System;
using System.Drawing;
using System.Windows.Forms;

namespace Chess;

public partial class Form1 : Form
{
    private Button[,] boardSquares = new Button[8, 8];
    private ChessPiece[] whitePieces = new ChessPiece[16];
    private ChessPiece[] blackPieces = new ChessPiece[16];
    private ChessPiece? selectedPiece = null; 

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

                square.Font = new Font("Segoe UI Symbol", 18, FontStyle.Regular);
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

   private bool isInBounds(int x, int y)
    {
        if (y <= 7 && y >= 0 && x <= 7 && x >= 0)
        {
            return true;
        }
         return false;
    }

    private bool sameSidePiece(int x, int y, bool isWhite)
    {
        if (boardSquares[y,x].Tag != null)
        {
            if (((ChessPiece?)boardSquares[y, x].Tag).isWhite == isWhite) {
                return true;
            }
        }
        return false;
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

    private Button? GetButtonFromPiece()
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

    private int GetButtonAxis(Button? button, string axis)
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

    private void Line_Check(List<int> newXs, List<int> newYs, int x, int y, ChessPiece? piece)
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

    private void King_Moves(ChessPiece? piece)
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

    private void Queen_Moves(ChessPiece? piece)
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

    private void Rook_Moves(ChessPiece? piece)
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

    private void Bishop_Moves(ChessPiece? piece)
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

    private void Knight_Check(List<int> newXs, List<int> newYs, int x, int y, ChessPiece? piece)
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
    
    private void Knight_Moves(ChessPiece? piece)
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

    private void Pawn_Moves(ChessPiece? piece, int direction)
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

    private void Move_Piece(Button? selButton)
    {
        if (selButton != null && selectedPiece != null)
        {
            Button? oldButton = GetButtonFromPiece();
            if (oldButton != null)
            {
                oldButton.Tag = null;
                oldButton.Text = "";
                selButton.Tag = selectedPiece;
                selButton.Text = selectedPiece.Symbol;
                selectedPiece.Position = (GetButtonAxis(selButton, "x"), GetButtonAxis(selButton, "y"));
            }
        }
    }

    private void Square_Click(object? sender, EventArgs e)
    {
        if (sender is not Button clicked) return;

        ChessPiece? piece = clicked.Tag as ChessPiece;
        Button? selButton = clicked;

        if (selButton != null && selButton.BackColor == Color.DarkRed)
        {
            // Move the piece
            if (selectedPiece != null)
            {
                Move_Piece(selButton);
                Clear_Backs();
                return;
            }

        }

        Clear_Backs();

        if (piece == null)
        {
            MessageBox.Show("Position data missing.");
            return;
        }

        int direction = piece.isWhite ? -1 : 1;
        selectedPiece = piece;

        if (piece.Type == "Pawn")
        {
            Pawn_Moves(piece, direction);
        }
        else if (piece.Type == "Knight")
        {
            Knight_Moves(piece);
        }
        else if (piece.Type == "Bishop")
        {
            Bishop_Moves(piece);
        }
        else if (piece.Type == "Rook")
        {
            Rook_Moves(piece);
        }
        else if (piece.Type == "Queen")
        {
            Queen_Moves(piece);
        }
        else if (piece.Type == "King")
        {
            King_Moves(piece);
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
