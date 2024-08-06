namespace WebApplication1.Dto
{
    public record class CreateGameDto(
        string Name,
        string Genre,
        decimal Price,
        DateOnly ReleaseDate

    );
}
