using System.Text.Json.Serialization;

namespace Application.Models;


public class LiraRateBM
{
    [JsonPropertyName("buy")]
    public List<List<Int64>> Buy { get; set; }

    [JsonPropertyName("sell")]
    public List<List<Int64>> Sell { get; set; }
}