public class ChessPiece
{
    public string Type { get; set; }
    public bool IsWhite { get; set; }
    public string Symbol => IsWhite ? WhiteSymbol : BlackSymbol;

    private string WhiteSymbol => Type switch
    {
        "Pawn" => "P",
        "Rook" => "R",
        "Knight" => "K",
        "Bishop" => "B",
        "Queen" => "Q",
        "King" => "K",
        _ => "?"
    };

    private string BlackSymbol => Type switch
    {
        "Pawn" => "P",
        "Rook" => "R",
        "Knight" => "K",
        "Bishop" => "B",
        "Queen" => "Q",
        "King" => "K",
        _ => "?"
    };

    public ChessPiece(string type, bool isWhite)
    {
        Type = type;
        IsWhite = isWhite;
    }
}