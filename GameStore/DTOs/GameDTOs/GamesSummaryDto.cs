namespace GameStore.DTOs;

public record GamesSummaryDto(
    int Id,
    string Name , 
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
    );