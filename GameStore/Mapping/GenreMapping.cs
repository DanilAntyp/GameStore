using GameStore.DTOs.GenreDTOs;
using GameStore.Entities;

namespace GameStore.Mapping;

public static class GenreMapping
{
    public static Genre ToEntity(this CreateGenreDto newGenre)
    {
        return new Genre()
        {
            Name = newGenre.Name
        };


    }

    public static GenreSummaryDto ToSummaryDto(this Genre genre)
    {
        return new GenreSummaryDto(
            genre.Id,
            genre.Name 
            );
    }

    public static Genre ToEntity(this UpdateGenreDto genre, int id)
    {
        return new Genre()
        {
            Id = id,
            Name = genre.Name
        };
    }
}