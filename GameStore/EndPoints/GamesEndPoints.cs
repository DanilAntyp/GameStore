using GameStore.Data;
using GameStore.DTOs;
using GameStore.Entities;
using GameStore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.EndPoints;

public static class GamesEndPoints
{
    public static RouteGroupBuilder MapGamesEndPoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");
        
    //  GET /games/
        group.MapGet("/", (GameStoreContext dbContext) =>
            dbContext.Games.Include(game => game.Genre).Select(game => game.ToSummaryDto()).AsNoTracking()
        );
        
        //GET /games/{id}
        group.MapGet("{id}" , (int id, GameStoreContext dbContext) =>
        {
            Game? game  = dbContext.Games.Find(id);

            return game is null ? Results.NotFound() : Results.Ok(game.ToDetailsDto());
        }).WithName("GetGameById");
        
        //POST /games/
        group.MapPost("/", (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            Game game = newGame.ToEntity();
            dbContext.Games.Add(game);
            dbContext.SaveChanges();
            
            return Results.CreatedAtRoute("GetGameById" , new {id = game.Id} , game.ToDetailsDto());

        });
        
        //PUT  /games/{id}

        group.MapPut("/{id}" , (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) =>
        {
            var existing = dbContext.Games.Find(id);
            if (existing is null)
            {
                return Results.NotFound();
            }
            
            dbContext.Entry(existing).CurrentValues.SetValues(updatedGame.ToEntity(id));
            
            dbContext.SaveChanges();
            
            return Results.NoContent();
        });
        
        //DELETE /games/{id}

        group.MapDelete("/{id}" , (int id, GameStoreContext dbContext) =>
        {
            dbContext.Games.Where(game => game.Id == id).ExecuteDelete();
            
            return Results.NoContent();
        });
        return group;
    }
}