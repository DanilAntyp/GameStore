using GameStore.Data;
using GameStore.DTOs.GenreDTOs;
using GameStore.Entities;
using GameStore.Mapping;
using Microsoft.EntityFrameworkCore;


namespace GameStore.EndPoints;

public static class GenresEndPoints
{
    public static RouteGroupBuilder MapGenresEndPoints(this WebApplication app)
    {
        var group = app.MapGroup("/genres");

        
        //GET /genres/
        group.MapGet("/", async(GameStoreContext dbContext) =>
            await dbContext.Genres.Select(genre => genre.ToSummaryDto()).AsNoTracking().ToListAsync()
        );
        
        //GET /genres/{id}

        group.MapGet("/{id}" , async(int id, GameStoreContext dbContext) =>
        {
            Genre? genre = await dbContext.Genres.FindAsync(id);

            return genre is null ? Results.NotFound() : Results.Ok(genre.ToSummaryDto());
        }).WithName("GetGenreById");

        group.MapPost("/" , async(CreateGenreDto newGenre, GameStoreContext dbContext) =>
        {
            Genre genre = newGenre.ToEntity();
            dbContext.Genres.Add(genre);
            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute("GetGenreById", new{id = genre.Id} ,genre.ToSummaryDto());
        });
        
        //PUT  /genres/{id}

        group.MapPut("/{id}" , async (int id, UpdateGenreDto updateGenre, GameStoreContext dbContext) =>
        {
            var existing = await dbContext.Genres.FindAsync(id);

            if (existing is null)
            {
                return Results.NotFound();
            }
            dbContext.Entry(existing).CurrentValues.SetValues(updateGenre.ToEntity(id));
            
            await dbContext.SaveChangesAsync();
            
            return Results.NoContent();
        });
        
        //DELETE /genres/{id}
        group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            await dbContext.Genres.Where(genre=>genre.Id ==id).ExecuteDeleteAsync();
        });
        
        return group;
    }
}