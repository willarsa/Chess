public class ChessPiece
{
    public string Type { get; set; }
    public bool IsWhite { get; set; }
    public string Symbol => IsWhite ? WhiteSymbol : BlackSymbol;
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

    public ChessPiece(string type, bool isWhite, int x, int y)
    {
        Type = type;
        IsWhite = isWhite;
        Position = (x, y);
    }
}