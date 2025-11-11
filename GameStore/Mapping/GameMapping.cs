using GameStore.DTOs;
using GameStore.DTOs.GameDTOs;
using GameStore.Entities;

namespace GameStore.Mapping;

public static class GameMapping
{
    //create
    public static Game ToEntity(this CreateGameDto newGame)
    {
        return new Game(){
            Name = newGame.Name,
            GenreId = newGame.GenreId,
            Price = newGame.Price,
            ReleaseDate = newGame.ReleaseDate
            };
    }
    //get by id 
    public static GamesSummaryDto ToSummaryDto(this Game game)
    {
        return new(
            game.Id,
            game.Name,
            game.Genre!.Name,
            game.Price,
            game.ReleaseDate
            );
    }

    public static GamesDetailsDto ToDetailsDto(this Game game)
    {
        return new(
            game.Id,
            game.Name,
            game.GenreId,
            game.Price,
            game.ReleaseDate
            );
    }

    public static Game ToEntity(this UpdateGameDto game, int id)
    {
        return new Game()
        {
            Id = id,
            Name = game.Name,
            GenreId = game.GenreId,
            Price = game.Price,
            ReleaseDate = game.ReleaseDate
        };
    }
}