using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

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