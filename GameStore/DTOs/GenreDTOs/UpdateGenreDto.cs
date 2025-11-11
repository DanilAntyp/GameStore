namespace GameStore.DTOs.GenreDTOs;
using System.ComponentModel.DataAnnotations;

public record UpdateGenreDto(
    [Required][StringLength(20)] string Name
    );