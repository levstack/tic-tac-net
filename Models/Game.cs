using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class CreateGameRequest
{
    [BsonProperty("playerToken")]
    public string PlayerToken { get; set; }
}

public class Game
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public BsonDocument Id { get; set; }

    [BsonProperty("token")]
    public string Token { get; set; }

    [BsonProperty("board")]
    public string Board { get; set; }

    [BsonProperty("nextMove")]
    public string NextMove { get; set; }

    [BsonProperty("isGameOver")]
    public bool IsGameOver { get; set; }
}
