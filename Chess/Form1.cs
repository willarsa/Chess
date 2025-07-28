using System;
using System.Drawing;
using System.Windows.Forms;

namespace Chess;

public partial class Form1 : Form
{
    private Button[,] boardSquares = new Button[8, 8];

    public Form1()
    {
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
                square.Tag = new Point(row, col); // optional for tracking position
                square.Click += Square_Click;
                boardSquares[row, col] = square;
                this.Controls.Add(square);
            }
        }

        this.ClientSize = new Size(8 * tileSize, 8 * tileSize);
    }

    private void Square_Click(object? sender, EventArgs e)
    {
        if (sender is Button clicked){
            Point? position = clicked.Tag as Point?;

            if (position.HasValue)
            {
                MessageBox.Show($"You clicked square at {position.Value.X}, {position.Value.Y}");
            }
            else
            {
                MessageBox.Show("Position data missing.");
            }
        }
    }
}
