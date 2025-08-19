public class ChessPiece
{
    public string Type { get; set; }
    public bool isWhite { get; set; }
    public string Symbol => isWhite ? WhiteSymbol : BlackSymbol;
    public (int x, int y) Position { get; set;}

    private string WhiteSymbol => Type switch
    {
        "Pawn" => "♙",
        "Rook" => "♖",
        "Knight" => "♘",
        "Bishop" => "♗",
        "Queen" => "♕",
        "King" => "♔",
        _ => "?"
    };

    private string BlackSymbol => Type switch
    {
        "Pawn" => "♟",
        "Rook" => "♜",
        "Knight" => "♞",
        "Bishop" => "♝",
        "Queen" => "♛",
        "King" => "♚",
        _ => "?"
    };

    public ChessPiece(string type, bool IsWhite, int x, int y)
    {
        Type = type;
        isWhite = IsWhite;
        Position = (x, y);
    }
}