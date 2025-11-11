namespace GameStore.Entities;

public class Game
{
    public int Id { set; get; }
    public  required string Name { set; get; }
    public int GenreId { set; get; }
    public Genre? Genre { set; get; }
    public decimal Price { set; get; }
    public DateOnly ReleaseDate { set; get; }
}