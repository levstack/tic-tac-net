using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class CreateGameRequest
{
    public string PlayerToken { get; set; }
}
public class MoveRequest
{
    public string GameToken { get; set; }
    public int MoveIndex { get; set; }
}
public class Game
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string Token { get; set; }
    public string Board { get; set; }
    public string NextMove { get; set; }
    public bool IsGameOver { get; set; }
}