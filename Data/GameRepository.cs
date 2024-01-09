using MongoDB.Driver;
using System.Threading.Tasks;

public class GameRepository
{
    private readonly IMongoCollection<Game> _games;

    public GameRepository(string connectionString)
    {
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("TicTacToeDb");
        _games = database.GetCollection<Game>("Games");
    }

    public async Task<Game> GetGameByIdAsync(string id)
    {
        return await _games.Find(game => game.Id == id).FirstOrDefaultAsync();
    }

    public async Task InsertGameAsync(Game game)
    {
        await _games.InsertOneAsync(game);
    }

}
