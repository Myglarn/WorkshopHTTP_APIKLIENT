namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
         

            var builder = WebApplication.CreateBuilder(args);

            // 1. Registrera en namngiven klient
            // Vi sätter bas-inställningarna här så vi slipper upprepa dem.
            builder.Services.AddHttpClient("GitHubClient", client =>
            {
                client.BaseAddress = new Uri("https://api.github.com/");
                // GitHub KRAV: En User-Agent måste finnas, annars får du 403 Forbidden.
                client.DefaultRequestHeaders.UserAgent.ParseAdd("MinEnklaDemoApp");
            });

            var app = builder.Build();

            // 2. Injicera IHttpClientFactory direkt i metoden
            app.MapGet("/", async (IHttpClientFactory factory) =>
            {
                // Skapa klienten baserat på namnet vi angav ovan
                var client = factory.CreateClient("GitHubClient");

                // Gör anropet (hämtar rå JSON-text eftersom vi inte har någon DTO)
                string json = await client.GetStringAsync("users/github");

                // omvandla json till ett objekt
                //var content = JsonSerializer.Deserialize<object>(json);

                return json;
            });

            app.Run();

        }
    }
}
