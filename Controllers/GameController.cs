using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class GamesController : ControllerBase
{
    private readonly GameRepository _gameRepository;

    public GamesController(GameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateGame([FromBody] CreateGameRequest createGameRequest)
    {
        try
        {
            // Validate request
            if (createGameRequest == null || string.IsNullOrWhiteSpace(createGameRequest.PlayerToken))
            {
                return BadRequest("Invalid request");
            }

            // Check if a game with the same token already exists
            var existingGame = await _gameRepository.GetGameByTokenAsync(createGameRequest.PlayerToken);
            if (existingGame != null)
            {
                return BadRequest("Game with the same token already exists");
            }

            // Create a new game
            var newGame = new Game
            {
                Token = createGameRequest.PlayerToken,
                Board = ".........", // Initial empty board (9 dots)
                NextMove = "X",      // Assume the player (X) goes first
                IsGameOver = false
            };

            // Process bot's move if it goes first
            if (newGame.NextMove == "O")
            {
                
                newGame.Board = MakeBotMove(newGame.Board);
                newGame.NextMove = "X";
            }

            // Add the game to the MongoDB
            await _gameRepository.InsertGameAsync(newGame);

            // Return the game details
            return Ok(new { Token = newGame.Token, Board = newGame.Board, NextMove = newGame.NextMove });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGame(string id)
    {
        try
        {
            // Implement get game logic based on the provided game ID
            var game = await _gameRepository.GetGameByIdAsync(id);

            if (game == null)
            {
                return NotFound("Game not found");
            }

            return Ok(new { Token = game.Token, Board = game.Board, NextMove = game.NextMove, IsGameOver = game.IsGameOver });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost("move")]
    public async Task<IActionResult> MakeMove([FromBody] MoveRequest moveRequest)
    {
        try
        {
            // Implement move logic based on the provided move request
            

            //return a placeholder response
            return Ok(new { Message = "Move successfully processed" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // Placeholder method for bot move logic
    private string MakeBotMove(string currentBoard)
    {
        //random move for the bot. Not even legal. Just random
        var random = new Random();
        var emptyIndices = new List<int>();
        for (var i = 0; i < currentBoard.Length; i++)
        {
            if (currentBoard[i] == '.')
            {
                emptyIndices.Add(i);
            }
        }

        if (emptyIndices.Count == 0)
        {
            return currentBoard; // No more empty spaces
        }

        var randomIndex = random.Next(0, emptyIndices.Count);
        var botMoveIndex = emptyIndices[randomIndex];
        var botMove = currentBoard.ToCharArray();
        botMove[botMoveIndex] = 'O';
        return new string(botMove);
    }
}
