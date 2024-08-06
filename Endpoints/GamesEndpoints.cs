using WebApplication1.Dto;

namespace WebApplication1.Endpoints
{
    public static class GamesEndpoints
    {

        const string GetGameEndpointName = "GetGame";


        private static readonly List<GameDto> games = [
            new (
                1,
                "MKII",
                "Fighting",
                19.99M,
                new DateOnly(1991, 2, 1)
                //ImageUri = "https://placehold.co/100"

            ),
            new (
                2,
                "Final Fantasy XIV",
                "Roleplaying",
                59.99M,
                new DateOnly(2010, 9, 30)
                // ImageUri = "https://placehold.co/100"

            ),

            new (
                3,
                "FIFA 2023",
                "Sports",
                69.99M,
                new DateOnly(2022, 9, 27)
                // ImageUri = "https://placehold.co/100"

            )
        ];

        public static WebApplication MapGamesEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/games").WithParameterValidation();

            group.MapGet("/", () => games);

            group.MapGet("/{id}", (int id) =>
            {
                GameDto? game = games.Find(game => game.Id == id);
                if (game is null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(game);
            }).WithName(GetGameEndpointName);

            group.MapPost("/", (CreateGameDto newGame) =>
            {
                GameDto game = new(
                    games.Count + 1,
                    newGame.Name,
                    newGame.Genre,
                    newGame.Price,
                    newGame.ReleaseDate);
                games.Add(game);

                return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
            });

            group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
            {
                var index = games.FindIndex(game => game.Id == id);

                if (index == -1)
                {
                    return Results.NotFound();
                }

                games[index] = new GameDto(
                    id,
                    updatedGame.Name,
                    updatedGame.Genre,
                    updatedGame.Price,
                    updatedGame.ReleaseDate
                    );


                return Results.NoContent();

            });

            group.MapDelete("/{id}", (int id) =>
            {
                /*Game? game = games.Find(game => game.Id == id);

                if (game is not null)
                {
                    games.Remove(game);
                }*/
                games.RemoveAll(game => game.Id == id);

                return Results.NoContent();

            });

            return app;
        }

    }
}
