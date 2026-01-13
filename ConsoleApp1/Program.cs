using System.Text.Json;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddHttpClient("DadJokes", client =>
            {
                client.BaseAddress = new Uri("https://icanhazdadjoke.com/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.UserAgent.ParseAdd("MinEnklaDemoApp");
            });

            var app = builder.Build();

            app.MapGet("/", async (IHttpClientFactory factory) =>
            {
                var client = factory.CreateClient("DadJokes");

                string json = await client.GetStringAsync("");

                // Deserialize JSON into JokeRequest
                var jokeRequest = JsonSerializer.Deserialize<JokeRequest>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (jokeRequest is null)
                    return Results.Problem("Failed to deserialize joke");

                // Map to JokeResponse
                var jokeResponse = new JokeResponse
                {
                    Joke = jokeRequest.Joke
                };

                return Results.Json(jokeResponse);
            });

            app.Run();
        }
    }
}