namespace ConsoleApp1;

public class JokeRequest
{
    public required string Id  { get; set; }
    public required string Joke { get; set; }
    public int Status { get; set; }
}

public class JokeResponse
{
    public string? Joke { get; set; }
}
